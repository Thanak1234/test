/**
*@author : Yim Samaune
*/
using System;
using System.Collections.Generic;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.JRAM;
using Workflow.Domain.Entities.Forms;

namespace Workflow.Business
{
    public class JRAMRequestFormBC : 
        AbstractRequestFormBC<JRAMRequestWorkflowInstance, IDataProcessing>, 
        IJRAMRequestFormBC
    {
        private IRamClearRepository _ramClearRepository = null;

        public JRAMRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _ramClearRepository = new RamClearRepository(dbFactory);
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
            return PROCESSCODE.JRAM;
        }
        
        #endregion

        #region Load/Save Form

        protected override void LoadFormData() {
            var ramClear = _ramClearRepository.GetByRequestHeader(RequestHeader.Id);
            if (ramClear != null) {
                WorkflowInstance.RamClear = ramClear;
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
                if (WorkflowInstance.RamClear != null)
                {
                    var RamClear = WorkflowInstance.RamClear;
                    bool isUpdate = false;

                    if (RequestHeader.Id > 0)
                    {
                        var _risk = _ramClearRepository.GetByRequestHeader(RequestHeader.Id);
                        if (_risk != null)
                        {
                            _risk.RequestHeaderId = RamClear.RequestHeaderId;
                            _risk.Props = RamClear.Props;
                            _risk.Gmid = RamClear.Gmid;
                            _risk.Game = RamClear.Game;
                            _risk.Rtp = RamClear.Rtp;
                            _risk.ClearDate = RamClear.ClearDate;
                            _risk.Instances = RamClear.Instances;
                            _risk.CheckList = RamClear.CheckList;
                            _risk.Descr = RamClear.Descr;

                            _ramClearRepository.Update(_risk);
                            isUpdate = true;
                        }
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.RamClear.RequestHeaderId = RequestHeader.Id;
                        _ramClearRepository.Add(RamClear);
                    }
                }
                else
                {
                    throw new Exception("Risk Assessment Form has no request instance");
                }
            }
        }
        
        #endregion
    }
}
