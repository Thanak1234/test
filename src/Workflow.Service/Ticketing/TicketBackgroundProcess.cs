/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Workflow.Service.Ticketing
{
    public class TicketBackgroundProcess
    {

        private static BackgroundWorker worker = new BackgroundWorker();
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static List<ITicketIntegrated> ticketIntegrateds = new List<ITicketIntegrated>();

        public static void startBackground()
        {

            //Register ticket integrated

            ticketIntegrateds.Add(new MailEntegrated());
            ticketIntegrateds.Add(new EFormIntegrated());

            // Code that runs on application startup
            worker.DoWork += new DoWorkEventHandler(DoWork);
            worker.WorkerReportsProgress = false;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkerCompleted);

            //Add this BackgroundWorker object instance to the cache (custom cache implementation)
            //so it can be cleared when the Application_End event fires.


            // Calling the DoWork Method Asynchronously
            worker.RunWorkerAsync(); //we can also pass parameters to the async method....
        }

        public static void endBackground()
        {
            worker.CancelAsync();

        }

        private static void DoWork(object sender, DoWorkEventArgs e)
        {

            try {

                ticketIntegrateds.Each(t => t.process());

            }catch(Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex);
            }
        }

        private static void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            logger.Info("Finish");

            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker != null)
            {
                // sleep for 20 secs and again call DoWork to get FxRates..we can increase the time to sleep and make it configurable to the needs
                System.Threading.Thread.Sleep(20000);
                worker.RunWorkerAsync();
            }
        }
    }
}
