/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.Business.Interfaces;
using Workflow.Business.ITRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.IT;
using Workflow.Domain.Entities.BatchData;


namespace Workflow.Business
{
    public class ItRequestFormBC : AbstractRequestFormBC<ItRequestWorkflowInstance,IItFormDataProcessing>, IItRequestFormBC
    {
        
        public const string IT_APPROVAL = "IT Approval";
        public const string IT_HOD_APPROVAL = "IT HoD Approval";
        public const string IT_TECHNECIAN = "IT Technician"; // Old activity of workflow
        public const string IT_IMPLEMENTATION = "IT Implementation"; // New activity of workflow
        public const string REWORKED = "Requestor Rework";
        public const string EDIT = "Modification";

        private IRequestUserRepository _requestUserRepository = null;
        private IRequestItemRepository _requestItemRepository = null;
        private IItRequestFilesRepository _requestFilesRepository = null;
        private IAttachementRepository<ItRequestFiles> _attachementRepository = null;
        

        public ItRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _requestUserRepository = new RequestUserRepository(dbFactory);
            _requestItemRepository = new RequestItemRepository(dbFactory);
            _requestFilesRepository = new ItRequestFilesRepository(dbDocFactory);
            _attachementRepository = new AttachementRepository<ItRequestFiles>(dbDocFactory);

            AddActivities(new ItRequstSubmissionActivity());
            AddActivities(new ItRequstDraftActivity());
            AddActivities(new DeptHoDApprovalActivity());
            AddActivities(new ItDeptApprovalActivity());
            AddActivities(new ItHoDApprovalActivity());
            AddActivities(new ItTechnicianActivity());
            AddActivities(new ItImplementationActivity());
            AddActivities(new ItRequestFormReworkedActivity());
            AddActivities(new ITEditFormAcitvity(() => {
                return CreateEmailData("MODIFICATION");
            }));
        }

        protected override void TakeFormAction()
        {
            if( CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData )
            {
                ProcessRequestUserData(WorkflowInstance.AddRequestUsers, DataOP.AddNew);
                ProcessRequestUserData(WorkflowInstance.DelRequestUsers, DataOP.DEL);
                ProcessRequestUserData(WorkflowInstance.EditRequestUsers, DataOP.EDIT);

                ProcessRequestItemData(WorkflowInstance.AddRequestItems, DataOP.AddNew);
                ProcessRequestItemData(WorkflowInstance.DelRequestItems, DataOP.DEL);
                ProcessRequestItemData(WorkflowInstance.EditRequestItems, DataOP.EDIT);
            }

        }

        private void ProcessRequestUserData(IEnumerable<RequestUser> requestUsers, DataOP op)
        {

            if (requestUsers == null) return;

            foreach(RequestUser requestUser in requestUsers)

            {
                requestUser.RequestHeader = RequestHeader;
                if (DataOP.AddNew == op)
                {
                   
                    _requestUserRepository.Add(requestUser);
                }

                if (DataOP.DEL == op)
                {
                    var user = _requestUserRepository.GetById(requestUser.Id);
                    _requestUserRepository.Delete(user);
                }

                if (DataOP.EDIT == op)
                {
                    var user = _requestUserRepository.GetById(requestUser.Id);
                    user.EmpNo = requestUser.EmpNo;
                    user.EmpName = requestUser.EmpName;
                    user.EmpId = requestUser.EmpId;
                    user.Email = requestUser.Email;
                    user.HiredDate = requestUser.HiredDate;
                    user.Manager = requestUser.Manager;
                    user.Position = requestUser.Position;
                    user.TeamId = requestUser.TeamId;
                    user.Team = requestUser.Team;
                    user.Phone = requestUser.Phone;
                    _requestUserRepository.Update(user);
                }
            }

        }

        private void ProcessRequestItemData(IEnumerable<RequestItem> requestItems, DataOP op)
        {
            if(requestItems == null) return;

            foreach(RequestItem requestItem in requestItems)
            {

                requestItem.RequestHeader = RequestHeader;
                if (DataOP.AddNew == op)
                {
                    _requestItemRepository.Add(requestItem);
                }

                if (DataOP.DEL == op)
                {
                    var item = _requestItemRepository.GetById(requestItem.Id);
                    _requestItemRepository.Delete(item);
                }

                if (DataOP.EDIT == op)
                {
                    var item = _requestItemRepository.GetById(requestItem.Id);
                    item.ItemId = requestItem.ItemId;
                    item.Comment = requestItem.Comment;
                    item.ItemTypeId = requestItem.ItemTypeId;
                    item.ItemRoleId = requestItem.ItemRoleId;
                    _requestItemRepository.Update(item);
                }
            }
        }

        protected override void SaveForm()
        {
            throw new NotImplementedException();
        }

        protected override void LoadFormData()
        {
            //Load request user session
            WorkflowInstance.RequestUsers = _requestUserRepository.GetRequestUserByRequestHeaderId(RequestHeader.Id);
            WorkflowInstance.RequestItems = _requestItemRepository.GetMany(p => p.RequestHeaderId == RequestHeader.Id);
        }

        protected override void CreateWorkflowInstance()
        {
            if (WorkflowInstance== null)
            {
                WorkflowInstance =  new ItRequestWorkflowInstance();
            }
        }

        protected override IEnumerable<FileAttachement> GetUploadFiles()
        {
            //return _requestFilesRepository.GetMany(p => p.RequestHeaderId == RequestHeader.Id);

            var uploadFiles = _requestFilesRepository.GetMany(p => p.RequestHeaderId == RequestHeader.Id);

            uploadFiles.Each(p => {
                p.Status = (p.Status == null ? "REQUESTOR" : p.Status);
                p.ReadOnlyRecord =  p.Status != getActivityforFileUpload();
            });
            return uploadFiles;
        }

        protected override string getActivityforFileUpload()
        {
            if (FORM_SUBMISSION.Equals(CurrentActivity().ActivityName) || REWORKED.Equals(CurrentActivity().ActivityName))
            {
                return "REQUESTOR";
            }
            else
            {
                return CurrentActivity().ActivityName.ToUpper();
            }
        }

        protected override string GetRequestCode()
        {
            return "IT_REQ";
        }

        protected override string GetRequestCodePrefix()
        {
            return "ITRQ";
        }

        protected override string getFullProccessName()
        {
            return _processFolderName +  "IT Request WF";
        }

        protected override void SaveAttachmentFiles() {
            AddAttachements();
            //UpdateAttachements();
            DelAttachements();
        }

        private void AddAttachements() {

            IList<string> serials = new List<string>();

            foreach (FileAttachement fileAttachement in WorkflowInstance.AddUploadFiles) {
                serials.Add(fileAttachement.Serial);
            }

            var uploadFiles = fileAttachementRepository.GetFileAttachementsBySerials(serials);

            if (uploadFiles == null || uploadFiles.Count() == 0) {
                return;
            }

            ///Add database

            foreach (FileTemp uploadFile in uploadFiles) {
                var entity = new ItRequestFiles() {
                    RequestHeaderId = RequestHeader.Id,
                    Name = uploadFile.Name,
                    Comment = uploadFile.Comment,
                    FileContent = uploadFile.FileContent,
                    Status = getActivityforFileUpload()
                };
                _requestFilesRepository.Add(entity);
                fileAttachementRepository.Delete(uploadFile);
            }
        }

        private void DelAttachements() {
            foreach (var attachment in WorkflowInstance.DelUploadFiles)
            {
                var uploadFile = _requestFilesRepository.GetById(attachment.Id);
                if (uploadFile != null) {
                    _requestFilesRepository.Delete(uploadFile);
                }
            }
        }

        

        //private void UpdateAttachements() {
        //    foreach (var uploadFile in WorkflowInstance.EditUploadFiles) {
        //        var uploadFile = _requestFilesRepository.GetById(uploadFile.Id);
        //        _requestFilesRepository.Update(uploadFile);
        //    }
        //}
    }
}
