/**
*@author : Phanny
*/
using Workflow.MSExchange;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workflow.Business.Ticketing;
using Workflow.DataObject;
using Workflow.MSExchange.Core;
using Workflow.Service.FileUploading;
using Workflow.Business.Ticketing.Dto;
using Workflow.Domain.Entities.Ticket;
using Workflow.Domain.Entities.Email;

namespace Workflow.Service.Ticketing
{
    public class TicketNotifyHander : ITicketNotifyHandler
    {
        private IEmailService emailService = null;
        private IFileUploadService uploadService = new FileUploadService();
        private IDataProcessingProvider dataProcessingProvider;
        private const string K2SERVICE = "k2service@nagaworld.com";

        public TicketNotifyHander(IDataProcessingProvider dataProcessingProvider)
        {
            this.dataProcessingProvider = dataProcessingProvider;
        }

    
        public void notify(AbstractNotifyData notifyData)
        {


            if (notifyData == null)
            {
                return;
            }

            /**
            Get sender
            */
            MailList senderAcc = null;
            if(notifyData.ticketDept != null && !string.IsNullOrWhiteSpace(notifyData.ticketDept.AutomationEmail))
            {
                senderAcc = dataProcessingProvider.getMailConfig(notifyData.ticketDept.AutomationEmail);
                emailService = new EmailService(senderAcc.EmailAddress, senderAcc.EmailPassword);
            }
            else
            {
                emailService = new EmailService();
            }



            if (notifyData.IsMail) {
                var tos = notifyData.DestUsers.FindAll(t => t.DestType == DestUser.DEST_TYPE.TO);


                List<string> emailTos = new List<string>();
                foreach (var to in tos)
                {
                    if ( senderAcc==null ||  !senderAcc.EmailAddress.Equals(to.User.Email) )
                    {
                        if (!K2SERVICE.Equals(to.User.Email))
                        {
                            emailTos.Add(to.User.Email);
                        }
                        
                    }
                    
                }

                notifyData.NotifyMessage.To = emailTos;
                var ccs = notifyData.DestUsers.FindAll(t => t.DestType == DestUser.DEST_TYPE.CC);

                List<string> emailCcs = new List<string>();
                foreach (var to in ccs)
                {
                    if (senderAcc == null ||  !senderAcc.EmailAddress.Equals(to.User.Email))
                    {
                        if (!K2SERVICE.Equals(to.User.Email))
                        {
                            emailCcs.Add(to.User.Email);
                        }
                    }
                        
                }

                notifyData.NotifyMessage.Cc = emailCcs;
                send(notifyData.NotifyMessage);
            }

            //Save notification
            if (notifyData.IsUI)
            {
                saveNotification(notifyData);
            }
            
        }


        private void saveNotification(AbstractNotifyData notifyData)
        {

            var notificationType = "";
            if (notifyData.IsMail)
            {
                notificationType = "EMAIL";
            }

            if (notifyData.IsSMS)
            {
                notificationType += " SMS";
            }

            var dest = notifyData.DestUsers.FindAll(t => t.DestType == DestUser.DEST_TYPE.TO);

            dest.ForEach(t =>
            {
                var notification = new TicketNotification()
                {
                   NotificationType = notificationType,
                   ActivityId = notifyData.Activity.Id,
                   Status = "UNREAD",
                   EmpId = t.User.Id
                };

                dataProcessingProvider.saveNotificatin(notification);
            });

        }

        private void  send(NotifyMessage nogifyMessage)
        {
            Task.Factory.StartNew(() =>
            {
                return emailService.SendEmail(getEmailData(nogifyMessage));
            });
            
        }

        private IEmailData getEmailData(NotifyMessage notifyMessage)
        {
            IEmailData data = new DefaultEmailData()
            {
                Recipients =notifyMessage.To,
                Ccs= notifyMessage.Cc,
                Bccs= notifyMessage.Bcc,
                Subject = notifyMessage.Subject,
                Body = notifyMessage.Body,
                AttachmentFiles = getAttachments(notifyMessage.attachedFiles)

            };

            return data;
        }

        private List<EmailFileAttachment> getAttachments(List<FileUploadInfo> files)
        {

            if (files == null)
            {
                return null;
            }
            var list = new List<EmailFileAttachment>();
            files.ForEach(t => {
                list.Add(new EmailFileAttachment(t.fileName,uploadService.getUploadFile(t.serial).DataContent));
            });

            return list;
        }

        
    }
}
