/**
*@author : Phanny
*/
using Workflow.Business.Ticketing;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject.Ticket;
using Workflow.Service.Interfaces.ticketing;

namespace Workflow.Service.Ticketing
{
    public abstract class AbstractTicketIntegrated<T>
    {

        protected readonly IDataProcessingProvider dataProProvider;
        protected readonly IEmployeeRepository empRepo;
        protected readonly ITicketService ticketService;

        public AbstractTicketIntegrated()
        {
            var dbSource = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            ticketService = new TicketService();
            dataProProvider = new TicketDataProcessingProvider(dbSource);
            empRepo = new EmployeeRepository(dbSource);
        }

        protected virtual void createTicket(T item)
        {
            ticketService.takeAction(new TicketDataParser(ticketDtoTransformation(item)));
        }

        protected abstract TicketDto ticketDtoTransformation(T item);
    }
}
