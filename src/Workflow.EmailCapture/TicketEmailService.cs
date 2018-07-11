using log4net;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Timers;

namespace Workflow.EmailCapture {

    using DataAcess;
    using MSExchange.Core;

    public partial class TicketEmailService : ServiceBase {
        
        #region Properties and Contructor
        protected ILog logger = LogManager.GetLogger(typeof(TicketEmailService));
        protected List<EmailReader> _EmailReaderList = null;
        protected Timer timer = null;

        public TicketEmailService() {
            InitializeComponent();
            InitEmailService();
        }

        #endregion

        #region Window service events
        /// <summary>
        /// Window service start event
        /// </summary>
        /// <param name="args">The window service args</param>
        protected override void OnStart(string[] args) {
            logger.Info("OnStart");
            PullEmailStartup();
            StartEmailSchedule();            
            //StartEmailNotification();            
        }


        /// <summary>
        /// Window service stop event
        /// </summary>
        protected override void OnStop() {            
            StopEmailSchedule();
            logger.Info("OnStop");
        }

        /// <summary>
        /// Manual command in window service event
        /// </summary>
        /// <param name="command">The number of command</param>
        protected override void OnCustomCommand(int command) {
            logger.InfoFormat("OnCustomCommand: {0}", command);
            switch (command) {
                case 128:
                    PullEmail();
                    logger.Info("pull new email by mandule execute done.");
                    break;
                default:

                    break;
            }
        }
        #endregion

        #region Helper methods
        private void InitEmailService() {
            _EmailReaderList = new List<EmailReader>();
            BuildEmailReader();
        }        

        private void StartEmailNotification() {
            if(_EmailReaderList != null && _EmailReaderList.Count > 0) {
                _EmailReaderList.ForEach((x) => x.StartNotification());
            }
        }

        private void StopEmailNotification() {
            if (_EmailReaderList != null && _EmailReaderList.Count > 0) {
                _EmailReaderList.ForEach((x) => x.StopNotification());
            }
        }

        private void StartEmailSchedule() {
            if (_EmailReaderList != null && _EmailReaderList.Count > 0) {
                _EmailReaderList.ForEach((x) => x.StartSchedule());
            }
        }

        private void StopEmailSchedule() {
            if (_EmailReaderList != null && _EmailReaderList.Count > 0) {
                _EmailReaderList.ForEach((x) => x.StopSchedule());
            }
        }

        private void PullEmailStartup() {
            timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 10000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            logger.Info("start pull email");
            timer.Stop();            
            PullEmail();
            timer.Dispose();
            timer = null;
        }

        private void PullEmail() {
            if (_EmailReaderList != null && _EmailReaderList.Count > 0) {
                _EmailReaderList.ForEach((x) => x.PullEmail());
            }
        }


        /// <summary>
        /// Build email service object from email list (EMAIL.EMAL_LIST table) in database
        /// </summary>
        private void BuildEmailReader() {
            try {
                using (var context = new WorkflowContext()) {
                    var emails = context.MailLists.ToList();
                    emails.ForEach((p) => {
                        var userInfo = UserInfo.CreateUserData(p.EmailAddress, p.EmailPassword, ExchangeVersion.Exchange2013);
                        var emailReader = new EmailReader(userInfo);
                        _EmailReaderList.Add(emailReader);
                    });
                }
            } catch(Exception e) {
                logger.Error(e.Message, e);
            }            
        }

        #endregion
    }
}
