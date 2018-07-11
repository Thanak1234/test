using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketViewDto
    {
        public int id { get; set; }

        public int empId { get; set; }
        public string empName { get; set; }
        public string empNo { get; set; }
        public string position { get; set; }
        public string subDept { get; set; }
        public string groupName { get; set; }
        public string division { get; set; }
        public string phone { get; set; }
        public string ext { get; set; }
        public string email { get; set; }
        public string hod { get; set; }

        public string ticketNo { get; set; }
        public string subject { get; set; }
        public string description { get; set; }

        public int? typeId { get; set; }
        public string type { get; set; }

        public int? statusId { get; set; }
        public string status { get; set; }
        public int stateId { get; set; }

        public int? sourceId { get; set; }
        public string source { get; set; }

        public int? impactId { get; set; }
        public string impact { get; set; }

        public int? urgencyId { get; set; }
        public string urgency { get; set; }

        public int? priorityId { get; set; }
        public string priority { get; set; }

        public int? siteId { get; set; }
        public string site { get; set; }

        public int? teamId { get; set; }
        public string team { get; set; }

        public int? assigneeId { get; set; }
        public string assignee{ get; set; }
        public string assigneeNo { get; set; }

        public int? categoryId { get; set; }
        public string category { get; set; }

        public int? subCateId { get; set; }
        public string subCate { get; set; }

        public int? itemId { get; set; }
        public string item { get; set; }

        public int version { get; set; }

        public bool hasAttachment { get; set; }

        public decimal? estimatedHours { get; set; }
        public decimal? actualMinutes { get; set; }

        public int assignedEmpId { get; set; }

        public DateTime? dueDate { get; set; }

        public DateTime? firstResponseDate { get; set; }

        public DateTime? createdDate { get; set; }
        public DateTime? finishedDate { get; set; }

        public List<FileUploadInfo> fileUpload { get; set; }
        public List<SimpleActivityViewDto> activities { get; set; }
        public List<ActionDto> actions { get; set; }
        public List<object> activityTypes { get; set; }

        public int? slaId { get; set; }
        public string sla { get; set; }

        public string refType { get; set; }
        public int? reference { get; set; }

        public object emailItem { get; set; }
    }
}
