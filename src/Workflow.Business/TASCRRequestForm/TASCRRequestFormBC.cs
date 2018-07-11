/**
*@author : Yim Samaune
*/
using System;
using System.Collections.Generic;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Forms;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;

namespace Workflow.Business
{
    public class TASCRRequestFormBC : 
        AbstractRequestFormBC<TASCRRequestWorkflowInstance, IDataProcessing>, 
        ITASRequestFormBC
    {
        private ICourseRegistrationRepository _CourseRegistrationRepository = null;
        private ICourseEmployeeRepository _CourseEmployeeRepository = null;
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public TASCRRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _CourseRegistrationRepository = new CourseRegistrationRepository(dbFactory);
            _CourseEmployeeRepository = new CourseEmployeeRepository(dbFactory);
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
            return PROCESSCODE.TASCR;
        }

        protected override Dictionary<string, object> GetDataField()
        {
            return _dataField;
        } 
        #endregion

        #region Load/Save Form

        protected override void LoadFormData() {
            var CourseRegistration = _CourseRegistrationRepository.GetByRequestHeader(RequestHeader.Id);
            if (CourseRegistration != null) {
                WorkflowInstance.CourseRegistration = CourseRegistration;
                WorkflowInstance.CourseEmployees = _CourseEmployeeRepository.GetByRequestHeaderId(RequestHeader.Id);
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
                if (WorkflowInstance.CourseRegistration != null)
                {
                    var CourseRegistration = WorkflowInstance.CourseRegistration;
                    bool isUpdate = false;

                    if (RequestHeader.Id > 0)
                    {
                        var currentCourseRegistration = _CourseRegistrationRepository.GetByRequestHeader(RequestHeader.Id);
                        if (currentCourseRegistration != null)
                        {
                            currentCourseRegistration.RequestHeaderId = CourseRegistration.RequestHeaderId;
                            currentCourseRegistration.CourseId = CourseRegistration.CourseId;
                            currentCourseRegistration.OriginalCourseId = CourseRegistration.OriginalCourseId;
                            currentCourseRegistration.OriginalCourseDate = CourseRegistration.OriginalCourseDate;
                            currentCourseRegistration.CourseDate = CourseRegistration.CourseDate;
                            currentCourseRegistration.TrainerName = CourseRegistration.TrainerName;
                            currentCourseRegistration.Venue = CourseRegistration.Venue;
							currentCourseRegistration.ReminderOn = CourseRegistration.ReminderOn;
                            currentCourseRegistration.Duration = CourseRegistration.Duration;
                            
                            _CourseRegistrationRepository.Update(currentCourseRegistration);
                            isUpdate = true;
                        }
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.CourseRegistration.RequestHeaderId = RequestHeader.Id;
                        _CourseRegistrationRepository.Add(CourseRegistration);
                    }
                    
                    // Process transaction data for request items
                    ProcessCourseEmployeeData(WorkflowInstance.AddCourseEmployees, DataOP.AddNew);
                    ProcessCourseEmployeeData(WorkflowInstance.EditCourseEmployees, DataOP.EDIT);
                    ProcessCourseEmployeeData(WorkflowInstance.DelCourseEmployees, DataOP.DEL);
                    
                }
                else
                {
                    throw new Exception("Fixed Course Registration form has no request instance");
                }
            }
        }
        
        private void ProcessCourseEmployeeData(IEnumerable<CourseEmployee> CourseEmployees, DataOP op) {
            if (CourseEmployees == null) return;

            foreach (var CourseEmployee in CourseEmployees)
            {
                CourseEmployee.RequestHeaderId = RequestHeader.Id;
                if (DataOP.AddNew == op)
                {
                    _CourseEmployeeRepository.Add(CourseEmployee);
                }
                else if (DataOP.EDIT == op)
                {
                    _CourseEmployeeRepository.Update(CourseEmployee);
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _CourseEmployeeRepository.GetById(CourseEmployee.Id);
                    if (removeRecord != null) {
                        _CourseEmployeeRepository.Delete(removeRecord);
                    }
                }
            }
        } 
        #endregion
    }
}
