using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket
{
    public interface ITicketTeamRepository : IRepository<TicketTeam>
    {
        IEnumerable<TicketTeamDto> getTeams(string query = null);
        IEnumerable<TicketAgentTeamsDto> getAgentTeams(int agentId = 0);
        IEnumerable<TicketTeamAgentsDto> getTeamAgents(int teamId = 0);
        Boolean isTeamExisted(TicketTeamDto instance);
        IEnumerable<TicketTeamDto> getTeams(TicketSettingCriteria criteria);
        List<int> GetTeamsByEmployeeId(int empId);
    }
}
