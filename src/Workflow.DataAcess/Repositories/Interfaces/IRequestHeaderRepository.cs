/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Scheduler;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Dto;

namespace Workflow.DataAcess.Repositories.Interfaces
{
    public interface IRequestHeaderRepository : IRepository<RequestHeader>
    {
        string GetRequestNo(string prefix, string requestCode);
        bool Editable(int requestHeaderId, string requestCode, string currentUser);
        RequestHeaderRepository.WorkflowStatus GetWorkflowStatus(int requestHeaderId);
        RequestHeaderRepository.Originator GetRequestorEmail(string loginName);
        List<string> GetEmailsByRole(string[] roleNames);
        List<string> GetMTFEmailNotification(int requestHeaderId);
        List<string> GetEmailNotification(int requestHeaderId, string requestCode, string activityCode, bool returnAsString);
        List<EmailAccount> GetEmailModification(int requestHeaderId);
        IList<FormLongPending> GetFormLongPending(int maxDays = 0, string requestCode = "");
    }
}
