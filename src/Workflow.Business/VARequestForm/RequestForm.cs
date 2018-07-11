using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Repositories.ITApp;
using Workflow.Domain.Entities.Core.ITApp;
using Workflow.DataAcess.Repositories.VAF;
using System.Linq;
using System.Collections.Generic;
using Workflow.Domain.Entities.VAF;

namespace Workflow.Business.VARequestForm {

    public class RequestForm : AbstractRequestFormBC<VAFWorkflowInstance, IDataProcessing>, IRequestForm {

        private IInformationRepository informationRepo;
        private IOutlineRepository outlineRepo;
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public RequestForm(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory) {
            informationRepo = new InformationRepository(dbFactory);
            outlineRepo = new OutlineRepository(dbFactory);
        }

        protected override void InitActivityConfiguration() {
            AddActivities(new ActivityEngine());
            ActivityList.Each(p => { AddActivities(new ActivityEngine(p)); });
            AddActivities(new ActivityEngine(
                () => {
                    return CreateEmailData("MODIFICATION");
                },
                new FormDataProcessing() {
                    IsAddNewRequestHeader = false,
                    IsEditPriority = false,
                    IsEditRequestor = false,
                    IsSaveActivityHistory = true,
                    IsUpdateLastActivity = true,
                    IsSaveRequestData = true,
                    IsSaveAttachments = true,
                    TriggerWorkflow = false
                })
            );
        }

        protected override string GetRequestCode() {
            return PROCESSCODE.IAVAF;
        }

        #region Load/Save Form

        protected override void LoadFormData() {
            WorkflowInstance.Information = informationRepo.Get(p => p.RequestHeaderId == RequestHeader.Id);
            WorkflowInstance.AllOutlines = outlineRepo.GetMany(p => p.RequestHeaderId == RequestHeader.Id).ToList();
        }

        protected override void TakeFormAction() {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData) {
                var entity = informationRepo.Get(p => p.RequestHeaderId == RequestHeader.Id);
                if(entity == null && WorkflowInstance.Information != null) {
                    var information = new Information();
                    information.RequestHeaderId = RequestHeader.Id;
                    information.AdjType = WorkflowInstance.Information.AdjType;
                    information.Remark = WorkflowInstance.Information.Remark;
                    informationRepo.Add(information);
                    _dataField.Add("AdjustType", WorkflowInstance.Information.AdjType);
                } else {
                    entity.RequestHeaderId = RequestHeader.Id;
                    entity.AdjType = WorkflowInstance.Information.AdjType;
                    entity.Remark = WorkflowInstance.Information.Remark;
                    informationRepo.Update(entity);
                    _dataField.Add("AdjustType", WorkflowInstance.Information.AdjType);
                }
                ProcessData(WorkflowInstance.NewOutlines, DataOP.AddNew);
                ProcessData(WorkflowInstance.ModifiedOutlines, DataOP.EDIT);
                ProcessData(WorkflowInstance.DeletedOutlines, DataOP.DEL);
            }
        }

        private void ProcessData(IEnumerable<Outline> outlines, DataOP op) {
            if (outlines == null || outlines.Count() == 0) return;
            outlines.Each(o => {
                o.RequestHeaderId = RequestHeader.Id;
                if(op == DataOP.AddNew) {
                    outlineRepo.Add(o);
                } else if(op == DataOP.EDIT) {
                    outlineRepo.Update(o);
                } else if(op == DataOP.DEL) {
                    var entity = outlineRepo.GetById(o.Id);
                    if (entity != null)
                        outlineRepo.Delete(entity);
                }
            });
        }

        protected override Dictionary<string, object> GetDataField() {
            return _dataField;
        }

        #endregion
    }
}
