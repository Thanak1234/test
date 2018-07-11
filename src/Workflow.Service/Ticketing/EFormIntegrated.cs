/**
*@author : Phanny
*/
using System;
using System.Linq;
using Workflow.Business.Ticketing;
using Workflow.Business.Ticketing.Dto;
using Workflow.Business.Ticketing.Impl;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.Service.Ticketing
{
    public class EFormIntegrated : AbstractTicketIntegrated<RequestHeader>, ITicketIntegrated
    {

        private readonly IRequestHeaderRepository requestHeaderRepo;
        private readonly ISimpleRepository<TicketFormIntegrated> formIntegratedRepo;

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EFormIntegrated():base()
        {
            var dbSource = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            requestHeaderRepo = new RequestHeaderRepository(dbSource);
            formIntegratedRepo = new SimpleRepository<TicketFormIntegrated>(dbSource);
        }

        public void process()
        {
            var forms = formIntegratedRepo.GetMany(t => t.Status == "NEW").ToList();

            forms.ForEach(t => {
                newTicket(t);
            });
        }

        private void newTicket(TicketFormIntegrated formIntegrated)
        {
            var form = requestHeaderRepo.GetById(formIntegrated.RequestHaderId);
            try
            {
                IActivityMessageHandler integratedMsgHandler = new IntegratedActMsgHandler(formIntegrated, formIntegratedRepo);
                ticketService.takeAction(new TicketDataParser(ticketDtoTransformation(form)), integratedMsgHandler);
            }
            catch (Exception e)
            {
                formIntegrated.Status = "FAILED";
                formIntegratedRepo.Update(formIntegrated);

                var errorMsg= string.Format("Forrm {0} cannot be created. Reason: {1}", form.Title, e.Message);
                logger.Fatal(errorMsg);
            }
        }

        protected override TicketDto ticketDtoTransformation(RequestHeader item)
        {
            var sumittor = empRepo.Get(t => item.SubmittedBy.IndexOf(t.LoginName) > 0);

            var empDto = new EmployeeDto()
            {
                id = sumittor.Id,
                fullName = sumittor.DisplayName,
                email = sumittor.Email
            };
            
            var ticket = new TicketDto() {
                ActivityCode = TicketActivityHandler.ACTIVITY_CODE,
                Subject = "K2 Form No: " + item.Title,
                Description = "Ticket posted by K2 form integration. For more detail, please go to the form",
                SourceId = 5,
                RequestorId = item.RequestorId,
                CurrUser = empDto,
                DeptOwnerId = 0, //TODO: get from DB
                IsFormIntegrated = true
            };
            if (item.RequestCode == "GMU_REQ") {
                ticket.PriorityId = 2;
            }

            return ticket;
        }
    }

    class IntegratedActMsgHandler : IActivityMessageHandler
    {
        private TicketFormIntegrated formIntegrated;
        private ISimpleRepository<TicketFormIntegrated> formIntegratedRepo;

        public IntegratedActMsgHandler(TicketFormIntegrated fm, ISimpleRepository<TicketFormIntegrated> formIntegratedRepo)
        {
            this.formIntegrated = fm;
            this.formIntegratedRepo = formIntegratedRepo;
        }

        public void onActivityCreation(TicketActivity activity, AbstractTicketParam tkParam)
        {
            return;
        }

        public void onTicketCreation(Ticket ticket)
        {
            formIntegrated.TicketId = ticket.Id;
            formIntegrated.Status = "FECTHED";
            formIntegratedRepo.Update(formIntegrated);
        }
    }
}
