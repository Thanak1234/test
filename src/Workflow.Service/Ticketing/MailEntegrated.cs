/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.Business.Ticketing.Impl;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Attachment;
using Workflow.DataAcess.Repositories.ticket;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Email;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.Service.Ticketing
{
    public class MailEntegrated : AbstractTicketIntegrated<EmailItem>, ITicketIntegrated
    {
        private readonly ISimpleRepository<EmailItem> emailRepo;
        private readonly ISimpleRepository<Domain.Entities.Email.FileAttachement> emailAttchedRepo;
        
        private readonly ITicketDepartmentRepository deptRepo;
        private  List<TicketDepartment> depts;
        private readonly IUploadFileRepository fileUploadRepo;
        
        //If no submitter is specified, k2 service will be used
        //private const string SERVICE_EMAIL = "k2service@nagaworld.com";

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MailEntegrated():base()
        {
            var dbSource = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            emailRepo = new SimpleRepository<EmailItem>(dbSource);
            deptRepo = new TicketDepartmentRepository(dbSource);
            fileUploadRepo = new UploadFileRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc));
            emailAttchedRepo = new SimpleRepository<Domain.Entities.Email.FileAttachement>(dbSource);
        }

        public void process()
        {
            //Alway read depts, no chache 
            depts = deptRepo.GetMany(t => t.Status == "ACTIVE").ToList();
            createTicketFromEmail();
        }

        private void createTicketFromEmail()
        {
            var emailItems = getNewItems();
            emailItems.ForEach(t => {
                takeAction(t);
            });
        }


        private void takeAction(EmailItem item)
        {
            try
            {
                //Automation email end point must be exist in receipient list.
                var validReceipiant = depts.FindAll(t => !string.IsNullOrWhiteSpace(item.Receipient) && item.Receipient.IndexOf(t.AutomationEmail.Trim()) >= 0);

                if (validReceipiant.Count() == 0)
                {
                    return;
                }

                var log = string.Format("Create ticket for Id : {0}, Subject: {1}", item.UniqueIdentifier, item.Subject);
                logger.Info(log);
                createTicket(item);

                item.Status = "FECTHED";
                item.Description = log;
            }
            catch(Exception e)
            {
                logger.Info(e);
                item.Status = "FAILED";
                item.Description = e.Message;  
            }
            emailRepo.Update(item);
        }

        private List<EmailItem> getNewItems()
        {
            var items = emailRepo.GetMany(t => t.Status == "NEW").ToList();
            logger.Info(string.Format("Email fetched : {0}", items.Count()));
            return items;
        }

        
        private List<FileUploadInfo> getFileInfo(List<Domain.Entities.Email.FileAttachement> items)
        {
            List<FileUploadInfo> itemInfos = new List<FileUploadInfo>();
            items.ForEach(t=> {
                itemInfos.Add(new FileUploadInfo() {
                    serial = t.Serial,
                    fileName =t.FileName,
                    ext = t.Ext,
                    Identifier ="ticket"                   
                });
            });
            return itemInfos;
        }



        private TicketDepartment getDept(EmailItem item)
        {
            TicketDepartment dept = null;
            depts.Where(t =>  !string.IsNullOrWhiteSpace(item.Receipient) && item.Receipient.IndexOf(t.AutomationEmail.Trim())>0).ToList().ForEach(t =>
            {
                dept = t;
            });


            if(dept == null)
            {
                depts.Where(t => t.IsDefault).ToList().ForEach(t => dept = t);

                if(dept == null)
                {
                    throw new Exception("Cannot find default department setup in the system. Please check department configuration");
                }
            }

            return dept;
        }


        private Employee getEmployee(EmailItem item)
        {
            if (item.Originator.IsEmpty())
            {
                return null;
            }
            try
            {
                var emp = empRepo.Get(t => t.Email != null && item.Originator.Trim() == t.Email.Trim());

                if(emp == null )
                {
                    return null;
                }

                //if (emp.Active == null || !(bool)emp.Active)
                //{
                //    var smg = string.Format(" Employee is not active, {0} / {1} / {2}", emp.Id, emp.EmpNo, emp.Email);

                //    throw new Exception(smg);
                //}
                return emp;
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                throw e;
            }
            
        }

        protected override TicketDto ticketDtoTransformation(EmailItem item)
        {
           List<string> msgs = new List<string>();
            TicketDto ticket = null;
            try
            {
                var dept = getDept(item);
               
                var user = getEmployee(item);
               
                EmployeeDto empDto = null;
                int requestorId = -1;
                if (user != null)
                {
                    requestorId = user.Id;
                }
                else
                {
                   user = empRepo.Get(t => dept.AutomationEmail == t.Email);

                    if (user == null)
                    {
                        throw new Exception(string.Format("Cannot find {0} in employee list", dept.AutomationEmail));
                    }

                }

                empDto = new EmployeeDto()
                {
                    id = user.Id,
                    fullName = user.DisplayName,
                    email = user.Email
                };


                var ticketNoneReqEmp = new TicketNoneReqEmpDto()
                {
                    EmailItemId = item.Id,
                    Originator = item.Originator,
                    Receipient = item.Receipient,
                    Cc = item.Cc
                };

                ticket = new TicketDto()
                {
                    ActivityCode = TicketActivityHandler.ACTIVITY_CODE,
                    Subject = item.Subject,
                    Description = item.Body,
                    SourceId = 1,
                    RequestorId = requestorId,
                    CurrUser = empDto,
                    DeptOwnerId = dept!=null? dept.Id : 0,
                    TicketNoneReqEmp = ticketNoneReqEmp,
                    IsAutomation = true,
                    Reference = item.Id,
                    RefType = "EMAIL"
                };


                //Add attachment
                var attachs = emailAttchedRepo.GetMany(t => t.MailItemId == item.Id).ToList();
                if (!attachs.IsNullOrEmpty())
                {
                    ticket.UserAttachFiles = getFileInfo(attachs);

                    attachs.ForEach(t => {
                        fileUploadRepo.Add(new Domain.Entities.Attachment.UploadFile()
                        {
                            Serial = t.Serial,
                            DataContent = t.DataContent,
                            Status = "TMP"
                        });
                    });

                }
            }
            catch(Exception e)
            {
                logger.Error(e);
                throw new Exception(string.Concat(msgs));
            }
            return ticket;
        }
    }

}
