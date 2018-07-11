using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Workflow.DataAcess.Repositories;
using Workflow.Domain.Entities.Core;

namespace Workflow.App.Service.SignalR
{
    public class ApplicationHub //: Hub
    {
        //private static Timer _timer = null;

        //public ApplicationHub()
        //{
        //    if (_timer == null) {
        //        _timer = new Timer();
        //        RunTask();
        //    }
        //}

        //public Timer RunTask()
        //{
            
        //    _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        //    _timer.Interval = 5000;
        //    _timer.Enabled = true;
        //    _timer.Start();
        //    return _timer;
        //}

        //private void OnTimedEvent(object source, ElapsedEventArgs e)
        //{
        //    var repository = new Repository<Profiler>();
        //    var profiler = repository.ExecDynamicSqlQuery("SELECT * FROM [BPMDATA].[PROFILER] WHERE DATEADD(MINUTE, 10, LastLoginDateUtc) > GETUTCDATE()");
        //    Clients.All.CheckTokenSession(profiler);
        //}

    }
}