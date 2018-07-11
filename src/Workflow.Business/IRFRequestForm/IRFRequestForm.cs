using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Repositories.ITApp;
using Workflow.Domain.Entities.Core.ITApp;
using Workflow.DataAcess.Repositories.VAF;
using System.Linq;
using System.Collections.Generic;
using Workflow.Domain.Entities.VAF;
using Workflow.DataAcess.Repositories.IRF;
using Workflow.Domain.Entities.IRF;
using Workflow.ReportingService.Report;
using Workflow.MSExchange.Core;
using Workflow.DataAcess.Repositories;
using Workflow.MSExchange;
using System;

namespace Workflow.Business.IRFRequestForm
{

    public class IRFRequestForm : AbstractRequestFormBC<IRFWorkflowInstance, IDataProcessing>, IIRFRequestForm
    {

        private Dictionary<string, object> _dataField = new Dictionary<string, object>();
        private IIRFRequestItemRepository itemRepository;
        private IIRFVendorRepository vendorRepository;

        public IRFRequestForm(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            itemRepository = new IRFRequestItemRepository(dbFactory);
            vendorRepository = new IRFVendorRepository(dbFactory);
        }

        protected override void InitActivityConfiguration()
        {
            AddActivities(new ActivityEngine());
            ActivityList.Each(p => {
                if (p.Name.IsCaseInsensitiveEqual("repair"))
                {
                    AddActivities(new ActivityEngine(p, () => {
                        return GetEmailData("ITIRF_REPAIR_GROUP");
                    }));
                }
                else
                {
                    AddActivities(new ActivityEngine(p));
                }
            });
        }

        public IEmailData GetEmailData(string codesOf)
        {
            var email = new DefaultEmailData();
            RequestHeader.Status = "Updated";

            var body = @"<font style='font-family:Arial;font-size:11pt;font-weight:Normal;font-style:Normal;font-stretch:Normal;color:#000000;text-align:Left;text-decoration:None;line-height:1'>
                          Dear All,<br/><br/>
                          <p>@@FORM_NAME process has been @@ACTION by: @@DECISION_BY.</p>
                          <p>Items has been return back.</p>
                          <p>Please refer to attachment file.</p>
                          <span>Ref: @@FORM_NO</span><br/>
                          <span>Comment: @@COMMENT</span><br/>
                        </font >
                        @@SIGNATURE";

            string signature = "";
            try
            {
                var contents = new Repository().ExecDynamicSqlQuery(@"SELECT CONTENT [content] FROM [SYSTEM].[SETTINGS] WHERE MODULE = 'EMAIL' AND [KEY] = 'EMAIL_SIGNATURE'");
                signature = contents[0].content;
            }
            catch
            {
                signature = string.Empty;
            }

            var originator = requestHeaderRepository.GetRequestorEmail(RequestHeader.SubmittedBy);

            email.Subject = string.Format(
                "Notification (Ref:{0}) ({1} by: {2})",
                RequestHeader.Title,
                RequestHeader.Status,
                WorkflowInstance.fullName
            );

            email.Body = body
                    .Replace("@@ORIGINATOR", originator.DISPLAY_NAME)
                    .Replace("@@FORM_NAME", REQ_APP.ProcessName)
                    .Replace("@@ACTION", RequestHeader.Status.ToLower())
                    .Replace("@@DECISION_BY", WorkflowInstance.fullName)
                    .Replace("@@FORM_NO", RequestHeader.Title)
                    .Replace("@@COMMENT", WorkflowInstance.Comment)
                    .Replace("@@ORIGINATOR", "ITC Stone Sans Std Medium")
                    .Replace("@@SIGNATURE", signature);

            string[] codes = codesOf.Split(',');

            IEnumerable<string> ccParticipantList = new List<string>();
            foreach (var code in codes)
            {
                if (!string.IsNullOrEmpty(code) && code != ",")
                {
                    ccParticipantList = ccParticipantList.Concat(requestHeaderRepository.GetEmailNotification(-1, REQ_APP.RequestCode, code, false));
                }
            }
            email.Recipients = ccParticipantList.Distinct().ToList();

            var genericForm = new GenericFormRpt();
            byte[] buffer = genericForm.Export(new GenericFormParam { RequestHeaderId = RequestHeader.Id }, REQ_APP.ReportPath, ExportType.Pdf);
            var FileName = string.Concat(RequestHeader.Title, "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf");
            var fileAttachments = new EmailFileAttachment(FileName, buffer);

            email.AttachmentFiles.Add(fileAttachments);
            return email;
        }

        protected override Dictionary<string, object> GetDataField()
        {
            return _dataField;
        }

        protected override string GetRequestCode()
        {
            return PROCESSCODE.ITIRF;
        }

        #region Load/Save Form

        protected override void LoadFormData()
        {
            WorkflowInstance.ItemRecords = itemRepository.GetMany(x => x.RequestHeaderId == RequestHeader.Id);
            WorkflowInstance.VendorRecords = vendorRepository.GetMany(x => x.RequestHeaderId == RequestHeader.Id);
        }

        protected override void TakeFormAction()
        {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData)
            {
                ProcessItemRecords(WorkflowInstance.ItemNewRecords, DataOP.AddNew);
                ProcessItemRecords(WorkflowInstance.ItemUpdatedRecords, DataOP.EDIT);
                ProcessItemRecords(WorkflowInstance.ItemRemovedRecords, DataOP.DEL);

                ProcessVendorRecords(WorkflowInstance.VendorNewRecords, DataOP.AddNew);
                ProcessVendorRecords(WorkflowInstance.VendorUpdatedRecords, DataOP.EDIT);
                ProcessVendorRecords(WorkflowInstance.VendorRemovedRecords, DataOP.DEL);
            }
        }

        private void ProcessItemRecords(IEnumerable<IRFRequestItem> itemRecords, DataOP op)
        {
            if (itemRecords == null || itemRecords.Count() == 0) return;
            itemRecords.Each(record => {
                record.RequestHeaderId = RequestHeader.Id;
                if (op == DataOP.AddNew)
                {
                    itemRepository.Add(record);
                }
                else if (op == DataOP.EDIT)
                {
                    var oEntity = itemRepository.GetById(record.Id);
                    oEntity.RequestHeaderId = record.RequestHeaderId;
                    oEntity.ItemName = record.ItemName;
                    oEntity.ItemModel = record.ItemModel;
                    oEntity.SerialNo = record.SerialNo;
                    oEntity.PartNo = record.PartNo;
                    oEntity.Qty = record.Qty;
                    oEntity.SendDate = record.SendDate;
                    oEntity.ReturnDate = record.ReturnDate;
                    oEntity.Remark = record.Remark;
                    itemRepository.Update(oEntity);
                }
                else if (op == DataOP.DEL)
                {
                    var entity = itemRepository.GetById(record.Id);
                    if (entity != null)
                        itemRepository.Delete(entity);
                }
            });
        }

        private void ProcessVendorRecords(IEnumerable<IRFVendor> vendorRecords, DataOP op)
        {
            if (vendorRecords == null || vendorRecords.Count() == 0) return;
            vendorRecords.Each(record => {
                record.RequestHeaderId = RequestHeader.Id;
                if (op == DataOP.AddNew)
                {
                    vendorRepository.Add(record);
                }
                else if (op == DataOP.EDIT)
                {
                    var oEntity = vendorRepository.GetById(record.Id);
                    oEntity.RequestHeaderId = record.RequestHeaderId;
                    oEntity.Vendor = record.Vendor;
                    oEntity.ContactNo = record.ContactNo;
                    oEntity.Email = record.Email;
                    oEntity.Address = record.Address;
                    vendorRepository.Update(oEntity);
                }
                else if (op == DataOP.DEL)
                {
                    var entity = vendorRepository.GetById(record.Id);
                    if (entity != null)
                        vendorRepository.Delete(entity);
                }
            });
        }
        #endregion
    }
}