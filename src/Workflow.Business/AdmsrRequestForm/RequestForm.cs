using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;
using System.Linq;
using System.Collections.Generic;
using Workflow.DataAcess.Repositories.Admsr;
using Workflow.Domain.Entities.Admsr;

namespace Workflow.Business.AdmsrRequestForm
{
    public class RequestForm : AbstractRequestFormBC<AdmsrRequestWorkflowInstance, IDataProcessing>, IRequestForm {

        private IAdmsrInformationRepository informationRepo;
        private IAdmsrCompanyRepository companyRepo;
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public RequestForm(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory) {
            informationRepo = new AdmsrInformationRepository(dbFactory);
            companyRepo = new AdmsrCompanyRepository(dbFactory);
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
            return PROCESSCODE.ADMSR;
        }

        #region Load/Save Form

        protected override void LoadFormData() {
            WorkflowInstance.Information = informationRepo.Get(p => p.RequestHeaderId == RequestHeader.Id);
            WorkflowInstance.Companies = companyRepo.GetMany(p => p.RequestHeaderId == RequestHeader.Id).ToList();
        }

        protected override void TakeFormAction() {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData) {
                var entity = informationRepo.Get(p => p.RequestHeaderId == RequestHeader.Id);
                if (entity == null && WorkflowInstance.Information != null)
                {
                    var information = new AdmsrInformation();
                    information.RequestHeaderId = RequestHeader.Id;
                    information.Salod = WorkflowInstance.Information.Salod;
                    information.Slod = WorkflowInstance.Information.Slod ?? false;
                    information.Adc = WorkflowInstance.Information.Adc;
                    information.Dr = WorkflowInstance.Information.Dr;
                    information.Dsrj = WorkflowInstance.Information.Dsrj;
                    information.ECC = WorkflowInstance.Information.ECC ?? false;
                    information.Efinance = WorkflowInstance.Information.Efinance ?? false;
                    information.Epurchasing = WorkflowInstance.Information.Epurchasing ?? false;
                    informationRepo.Add(information);
                }
                else
                {
                    entity.RequestHeaderId = RequestHeader.Id;
                    entity.Salod = WorkflowInstance.Information.Salod;
                    entity.Slod = WorkflowInstance.Information.Slod ?? false;
                    entity.Adc = WorkflowInstance.Information.Adc;
                    entity.Dr = WorkflowInstance.Information.Dr;
                    entity.Dsrj = WorkflowInstance.Information.Dsrj;
                    entity.ECC = WorkflowInstance.Information.ECC ?? false;
                    entity.Efinance = WorkflowInstance.Information.Efinance ?? false;
                    entity.Epurchasing = WorkflowInstance.Information.Epurchasing ?? false;
                    informationRepo.Update(entity);
                }

                _dataField.Add("lod", WorkflowInstance.Information.Slod ?? false);
                _dataField.Add("finance", WorkflowInstance.Information.Efinance ?? false);
                _dataField.Add("costcontrol", WorkflowInstance.Information.ECC ?? false);
                _dataField.Add("purchasing", WorkflowInstance.Information.Epurchasing ?? false);

                if (CurrentActivity().ActivityName == "Admin HOD Approval")
                {
                    _dataField.Add("salod", WorkflowInstance.Information.Salod ?? false);
                }

                ProcessData(WorkflowInstance.NewCompanies, DataOP.AddNew);
                ProcessData(WorkflowInstance.ModifiedCompanies, DataOP.EDIT);
                ProcessData(WorkflowInstance.DeletedCompanies, DataOP.DEL);
            }
        }

        private void ProcessData(IEnumerable<AdmsrCompany> companies, DataOP op) {
            if (companies == null || companies.Count() == 0) return;
            companies.Each(o => {
                o.RequestHeaderId = RequestHeader.Id;
                if (op == DataOP.AddNew)
                {
                    companyRepo.Add(o);
                }
                else if (op == DataOP.EDIT)
                {
                    companyRepo.Update(o);
                }
                else if (op == DataOP.DEL)
                {
                    var entity = companyRepo.GetById(o.Id);
                    if (entity != null)
                        companyRepo.Delete(entity);
                }
            });
        }

        protected override Dictionary<string, object> GetDataField() {
            return _dataField;
        }

        #endregion
    }
}
