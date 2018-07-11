/**
*@author : Phanny
*/
using System;
using Workflow.Business.Ticketing.Dto;

namespace Workflow.Business.Ticketing.Impl
{
    public class TicketActivityFactoryImpl : ITicketActivityFactory
    {
       

        private static ITicketActivityFactory ticketActivityFacotry;
        public static ITicketActivityFactory getInstance()
        {

            if(ticketActivityFacotry == null)
            {
                ticketActivityFacotry = new TicketActivityFactoryImpl();
            }
            return ticketActivityFacotry;
        }

        public ITicketActivityHandler getTicketActivityHandler(IDataProcessingProvider dataProcessingProvider, AbstractTicketParam ticketParams)
        {

            ITicketActivityHandler ticketActivityInstance = getInstance(dataProcessingProvider, ticketParams);
            if (ticketActivityInstance != null)
            {
                return ticketActivityInstance;
            }
            
            throw new Exception(String.Format(" {0} cannot be found to instance. Please make sure that it have been registered", ticketParams.ActivityCode));
        }

        private ITicketActivityHandler getInstance(IDataProcessingProvider dataProcessingProvider, AbstractTicketParam ticketParams)
        {
            if (TicketActivityHandler.ACTIVITY_CODE.Equals(ticketParams.ActivityCode))
            {
                return new TicketActivityHandler(dataProcessingProvider, (TicketParams)ticketParams);
            }
            else if (PostReplyActivityHandler.ACTIVITY_CODE.Equals(ticketParams.ActivityCode))
            {
                return new PostReplyActivityHandler(dataProcessingProvider, (PostReplyTicketParams)ticketParams);
            }
            else if (AssignTicketActivityHandler.ACTIVITY_CODE.Equals(ticketParams.ActivityCode))
            {
                return new AssignTicketActivityHandler(dataProcessingProvider, (AssignedTicketParams)ticketParams);
            }
            else if (PostInternalNoteActivityHandler.ACTIVITY_CODE.Equals(ticketParams.ActivityCode))
            {
                return new PostInternalNoteActivityHandler(dataProcessingProvider, (SimpleActParams)ticketParams);
            }
            else if (PostPublicNoteActivityHandler.ACTIVITY_CODE.Equals(ticketParams.ActivityCode))
            {
                return new PostPublicNoteActivityHandler(dataProcessingProvider, (SimpleActParams)ticketParams);
            }
            else if (ChangeStatusActivityHandler.ACTIVITY_CODE.Equals(ticketParams.ActivityCode))
            {
                return new ChangeStatusActivityHandler(dataProcessingProvider, (ChangeStatusActParams)ticketParams);

            }
            else if (RemoveTicketActivityHandler.ACTIVITY_CODE.Equals(ticketParams.ActivityCode))
            {
                return new RemoveTicketActivityHandler(dataProcessingProvider, (SimpleActParams)ticketParams);
            }
            else if (TicketMergedActivityHandler.ACTIVITY_CODE.Equals(ticketParams.ActivityCode))
            {
                return new TicketMergedActivityHandler(dataProcessingProvider, (MergedTicketActParam)ticketParams);
            }else if (SubTicketActivityHandler.ACTIVITY_CODE.Equals(ticketParams.ActivityCode))
            {
                return new SubTicketActivityHandler(dataProcessingProvider, (TicketParams)ticketParams);
            }else if (K2IntegratedActivityHander.ACTIVITY_CODE.Equals(ticketParams.ActivityCode))
            {
                return new K2IntegratedActivityHander(dataProcessingProvider, (SimpleActParams)ticketParams);
            }

            return null;
        }
    }
}
