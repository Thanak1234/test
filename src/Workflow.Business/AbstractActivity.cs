﻿


using Workflow.MSExchange;
/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business
{
    public abstract class AbstractActivity<T>
        where T: IFormDataProcessing
    { 
        protected const string FORM_SUBMISSION_ACTIVITY = "Submission";
        protected const string FORM_DRAFT_ACTIVITY = "Draft";

        #region Properties

        protected ICollection<IAction<T>> actions = null;
       
        private string _activityName;
        private string _currActionName;
        private IEmailData _email;


        public virtual IEmailData Email { get; set; }

        private IAction<T> _currentAction;

        public string ActivityName {
            get
            {
                return _activityName;
            }
        }


        public void SetCurrActionName(string actionName)
        {
            _currActionName = actionName;
        }

        public IAction<T> CurrAction {
            get
            {
                return _currentAction ?? (_currentAction = GetCurrActionByName(_currActionName)) ;
            }

            set {
                if (checkExistingAction(value)){
                    _currentAction = value;
                }
                else
                {
                    throw new Exception(String.Format("Action: {0} could not be found in Activity: {1}", value.ActionName, ActivityName));
                }
               
            }
        }
        #endregion

        #region Constructor

        public AbstractActivity(string activityName, string curAction)
        {
            this._currActionName = curAction;
            _activityName = activityName;
        }

        public AbstractActivity(string activityName)
        {
            _activityName = activityName;
        }


        #endregion

        #region Methods


        public bool Matched(string activityName)
        {
            return ActivityName.Equals(activityName);
        }

        
        protected void AddAction(IAction<T> action)
        {
            if (actions == null)
            {
                actions = new List<IAction<T>>();
            }
            else
            {
                var actionRemove = actions.Where(p => p.ActionName == action.ActionName);
                if (actionRemove != null && actionRemove.Count() > 0) {
                    actions.Remove(actionRemove.Single());
                }
            }
            
            actions.Add(action);
        }

        protected bool checkExistingAction(IAction<T> action)
        {
            foreach(IAction<T> ac in actions)
            {
                if (ac.Match(action))
                {
                    return true;
                }
            }

            return false;
        }

        private IAction<T> GetCurrActionByName(string actionName)
        {
            IAction<T> currentAction = null;
            string suggestAction = string.Empty;

            foreach (IAction<T> action in actions)
            {
                suggestAction += action.ActionName + ",";
                if (action.ActionName == actionName)
                {
                    currentAction = action;
                }
            }
            if (currentAction == null)
            {
                throw new Exception(string.Format("Action: {0} could not be found in Activity: {1}, please try action [{2}]", actionName, ActivityName, suggestAction));
            }
            return currentAction;
           
        }
        
    }
    #endregion
}
