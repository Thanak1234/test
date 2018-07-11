/**
*@author : Yim Samaune
*/
using System;
using System.Collections.Generic;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.GMU;
using Workflow.Domain.Entities.Forms;

namespace Workflow.Business
{
    public class GMURequestFormBC : 
        AbstractRequestFormBC<GMURequestWorkflowInstance, IDataProcessing>, 
        IGMURequestFormBC
    {
        private IGmuRamClearRepository _gmuRamClearRepository = null;
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public GMURequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _gmuRamClearRepository = new GmuRamClearRepository(dbFactory);
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
            return PROCESSCODE.GMU;
        }

        protected override Dictionary<string, object> GetDataField()
        {
            return _dataField;
        }

        #endregion

        #region Load/Save Form

        protected override void LoadFormData() {
            var gmuRamClear = _gmuRamClearRepository.GetByRequestHeader(RequestHeader.Id);
            if (gmuRamClear != null) {
                WorkflowInstance.GmuRamClear = gmuRamClear;
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
                if (WorkflowInstance.GmuRamClear != null)
                {
                    var GmuRamClear = WorkflowInstance.GmuRamClear;
                    bool isUpdate = false;

                    if (RequestHeader.Id > 0)
                    {
                        var _risk = _gmuRamClearRepository.GetByRequestHeader(RequestHeader.Id);
                        if (_risk != null)
                        {
                            _risk.RequestHeaderId = GmuRamClear.RequestHeaderId;
                            _risk.Props = GmuRamClear.Props;
                            _risk.Gmid = GmuRamClear.Gmid;
                            _risk.Ip = GmuRamClear.Ip;
                            _risk.Gmus = GmuRamClear.Gmus;
                            _risk.CheckList = GmuRamClear.CheckList;
                            _risk.Descr = GmuRamClear.Descr;
                            _risk.Remark = GmuRamClear.Remark;
                            _risk.MacAddress = GmuRamClear.MacAddress;

                            
                            _gmuRamClearRepository.Update(_risk);
                            isUpdate = true;
                        }
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.GmuRamClear.RequestHeaderId = RequestHeader.Id;
                        _gmuRamClearRepository.Add(GmuRamClear);
                    }
                    
                    _dataField.Add("isGmuChange", (GmuRamClear.Gmus.IndexOf('2') > -1));
                }
            }
        }
        
        #endregion
    }
}
