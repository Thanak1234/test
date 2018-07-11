/**
*@author : Yim Samaune
*/
using System;
using System.Collections.Generic;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.RMD;
using Workflow.Domain.Entities.RMD;

namespace Workflow.Business
{
    public class RMDRequestFormBC : 
        AbstractRequestFormBC<RMDRequestWorkflowInstance, IDataProcessing>, 
        IRMDRequestFormBC
    {
        private IRiskAssessmentRepository _riskAssessmentRepository = null;
        private IWorksheet1Repository _worksheet1Repository = null;
        private IWorksheet2Repository _worksheet2Repository = null;
        private IWorksheet3Repository _worksheet3Repository = null;

        public RMDRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _riskAssessmentRepository = new RiskAssessmentRepository(dbFactory);
            _worksheet1Repository = new Worksheet1Repository(dbFactory);
            _worksheet2Repository = new Worksheet2Repository(dbFactory);
            _worksheet3Repository = new Worksheet3Repository(dbFactory);
        }

        #region Override Method
        protected override void InitActivityConfiguration()
        {
            AddActivities(new ActivityEngine());
            AddActivities(new ActivityEngine(REQUESTOR_REWORKED));
            ActivityList.Each(p => { AddActivities(new ActivityEngine(p)); });
            AddActivities(new ActivityEngine(() =>
            {
                return CreateEmailData("MODIFICATION");
            },
            new FormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = true,
                IsUpdateLastActivity = true,
                IsSaveRequestData = true,
                IsSaveAttachments = true,
                TriggerWorkflow = false
            }));
        }

        protected override string GetRequestCode()
        {
            return PROCESSCODE.RMD;
        }
        
        #endregion

        #region Load/Save Form

        protected override void LoadFormData() {
            var riskAssessment = _riskAssessmentRepository.GetByRequestHeader(RequestHeader.Id);
            if (riskAssessment != null) {
                WorkflowInstance.RiskAssessment = riskAssessment;
                WorkflowInstance.Worksheet1s = _worksheet1Repository.GetByRequestHeaderId(RequestHeader.Id);
                WorkflowInstance.Worksheet2s = _worksheet2Repository.GetByRequestHeaderId(RequestHeader.Id);
                //WorkflowInstance.Worksheet3s = _worksheet3Repository.GetByRequestHeaderId(RequestHeader.Id);
            }
        }

        protected override bool IsAuthorizeRemoveAttachment(string activityCode)
        {
            return (CurrentActivityCode == activityCode);
        }

        protected override void TakeFormAction() {
            var currentActvity = CurrentActivity();
            if (currentActvity.CurrAction.FormDataProcessing.IsSaveRequestData)
            {
                if (WorkflowInstance.RiskAssessment != null)
                {
                    var RiskAssessment = WorkflowInstance.RiskAssessment;
                    bool isUpdate = false;

                    if (RequestHeader.Id > 0)
                    {
                        var _risk = _riskAssessmentRepository.GetByRequestHeader(RequestHeader.Id);
                        if (_risk != null)
                        {
                            _risk.RequestHeaderId = RiskAssessment.RequestHeaderId;
                            _risk.BusinessUnit = RiskAssessment.BusinessUnit;
                            _risk.Objective = RiskAssessment.Objective;

                            _riskAssessmentRepository.Update(_risk);
                            isUpdate = true;
                        }
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.RiskAssessment.RequestHeaderId = RequestHeader.Id;
                        _riskAssessmentRepository.Add(RiskAssessment);
                    }

                    // Process transaction data for Worksheet1
                    ProcessWorksheet1Data(WorkflowInstance.AddWorksheet1s, DataOP.AddNew);
                    ProcessWorksheet1Data(WorkflowInstance.EditWorksheet1s, DataOP.EDIT);
                    ProcessWorksheet1Data(WorkflowInstance.DelWorksheet1s, DataOP.DEL);

                    // Process transaction data for Worksheet2
                    ProcessWorksheet2Data(WorkflowInstance.AddWorksheet2s, DataOP.AddNew);
                    ProcessWorksheet2Data(WorkflowInstance.EditWorksheet2s, DataOP.EDIT);
                    ProcessWorksheet2Data(WorkflowInstance.DelWorksheet2s, DataOP.DEL);

                    // Process transaction data for Worksheet3
                    //ProcessWorksheet3Data(WorkflowInstance.AddWorksheet3s, DataOP.AddNew);
                    //ProcessWorksheet3Data(WorkflowInstance.EditWorksheet3s, DataOP.EDIT);
                    //ProcessWorksheet3Data(WorkflowInstance.DelWorksheet3s, DataOP.DEL);
                }
                else
                {
                    throw new Exception("Risk Assessment Form has no request instance");
                }
            }
        }
        
        private void ProcessWorksheet1Data(IEnumerable<Worksheet1> worksheet1s, DataOP op)
        {
            if (worksheet1s == null) return;

            foreach (var worksheet1 in worksheet1s)
            {
                worksheet1.RequestHeaderId = RequestHeader.Id;
                if (DataOP.AddNew == op)
                {
                    _worksheet1Repository.Add(worksheet1);
                }
                else if (DataOP.EDIT == op)
                {
                    _worksheet1Repository.Update(worksheet1);
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _worksheet1Repository.GetById(worksheet1.Id);
                    if (removeRecord != null)
                    {
                        _worksheet1Repository.Delete(removeRecord);
                    }
                }
            }
        }

        private void ProcessWorksheet2Data(IEnumerable<Worksheet2> worksheet2s, DataOP op)
        {
            if (worksheet2s == null) return;

            foreach (var worksheet2 in worksheet2s)
            {
                worksheet2.RequestHeaderId = RequestHeader.Id;
                if (DataOP.AddNew == op)
                {
                    _worksheet2Repository.Add(worksheet2);
                }
                else if (DataOP.EDIT == op)
                {
                    _worksheet2Repository.Update(worksheet2);
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _worksheet2Repository.GetById(worksheet2.Id);
                    if (removeRecord != null)
                    {
                        _worksheet2Repository.Delete(removeRecord);
                    }
                }
            }
        }

        private void ProcessWorksheet3Data(IEnumerable<Worksheet3> worksheet3s, DataOP op)
        {
            if (worksheet3s == null) return;

            foreach (var worksheet3 in worksheet3s)
            {
                worksheet3.RequestHeaderId = RequestHeader.Id;
                if (DataOP.AddNew == op)
                {
                    _worksheet3Repository.Add(worksheet3);
                }
                else if (DataOP.EDIT == op)
                {
                    _worksheet3Repository.Update(worksheet3);
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _worksheet3Repository.GetById(worksheet3.Id);
                    if (removeRecord != null)
                    {
                        _worksheet3Repository.Delete(removeRecord);
                    }
                }
            }
        }

        #endregion
    }
}
