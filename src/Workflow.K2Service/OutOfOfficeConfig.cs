using SourceCode.Workflow.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Workflow.DataContract.K2;
using Action = SourceCode.Workflow.Client.Action;

namespace Workflow.K2Service
{
    public class OutOfOfficeConfig
    {
        #region Data Memebers

        private Connection _connection = null;

        #endregion

        #region Contructors

        public OutOfOfficeConfig(Connection connection)
        {
            if (connection != null) {
                // connection.User.FQN :: current user
                _connection = connection;
            }
            
        }

        #endregion
        /// <summary>
        /// Load worklist shared from K2 HostServer
        /// </summary>
        /// <returns>The worklist shares</returns>
        public WorklistShares LoadOOF()
        {
            return _connection.GetCurrentSharingSettings(ShareType.OOF);
        }


        /// <summary>
        /// Create or set the Out Of Office of current user
        /// </summary>
        /// <param name="wrapper">The Out Of Office criteria</param>
        /// <returns>Success(true) or Fail(False)</returns>
        public bool SetOutOfOffice(OOFWrapper wrapper)
        {

            if (wrapper.WorkType == null)
            {
                _connection.SetUserStatus(Convert.ToBoolean(wrapper.Status) ? UserStatuses.Available : UserStatuses.OOF);
                return false;
            }

            
            bool isNew = false;
            WorklistShares worklistShares = new WorklistShares();
            worklistShares = _connection.GetCurrentSharingSettings(ShareType.OOF);
            WorklistShare worklistShare = null;
            if (worklistShares.Count > 0)
            {
                worklistShare = worklistShares[0];
                worklistShare.ShareType = ShareType.OOF;
            }
            else
            {
                isNew = true;
            }

            if (worklistShare == null)
            {
                worklistShare = new WorklistShare();
                worklistShare.ShareType = ShareType.OOF;
                isNew = true;
            }


                worklistShare.StartDate = wrapper.StartDate;
            worklistShare.EndDate = wrapper.EndDate;

            WorkTypes workTypes = worklistShare.WorkTypes;
            WorkType workType = new WorkType();

            if (workTypes.Count > 0)
            {
                workType = workTypes[0];
            }
            else
            {
                workType = new WorkType();
                workTypes.Add(workType);
            }
            workType.Name = Guid.NewGuid().ToString();
            Destinations destinations = new Destinations();

            foreach (DestinationDto dest in wrapper.WorkType.Destinations)
            {
                var destination = new Destination(dest.LoginName.FQNWithK2Label(), DestinationType.User);
                destinations.Add(destination);
            }

            workType.Destinations = destinations;
            workType.WorkTypeExceptions = GetWorkTypeExceptions(wrapper.WorkType.WorkTypeExceptions);
            worklistShare.WorkTypes = workTypes;

            if (isNew)
            {
                _connection.ShareWorkList(worklistShare);
            }
            _connection.UpdateWorkType(worklistShare.WorkTypes[0]);
            _connection.SetUserStatus(Convert.ToBoolean(wrapper.Status) ? UserStatuses.Available : UserStatuses.OOF);
            

            return true;
        }


        /// <summary>
        /// Get The Out Of Office work type exception
        /// </summary>
        /// <param name="exceptionDtos">The Work type exceptions</param>
        /// <returns>The work type exception</returns>
        public WorkTypeExceptions GetWorkTypeExceptions(IList<WorkTypeExceptionDto> exceptionDtos)
        {

            WorkTypeExceptions worktypeExceptions = new WorkTypeExceptions();

            foreach (WorkTypeExceptionDto exceptionDto in exceptionDtos)
            {
                var exception = new WorkTypeException();
                Destinations destinations = new Destinations();
                foreach (DestinationDto dest in exceptionDto.Destinations)
                {
                    var destination = new Destination(dest.LoginName.FQNWithK2Label(), DestinationType.User);
                    destinations.Add(destination);
                }
                exception.Name = exceptionDto.Name;
                exception.Destinations = destinations;
                exception.WorklistCriteria = this.configureWorklistCriteria(exceptionDto.ProcessPath, exceptionDto.Activity);
                worktypeExceptions.Add(exception);
            }

            return worktypeExceptions;
        }


        /// <summary>
        /// Edit or update the Out of Office forward users
        /// </summary>
        /// <param name="workType">The work type</param>
        /// <param name="oofSetting_workType">The Out of Office setting work type</param>
        /// <returns>The Forward users</returns>
        public Destinations editOOFUsers(WorkType workType, Dictionary<string, object> oofSetting_workType)
        {
            Destinations destinations = new Destinations();
            ArrayList arrayList = (ArrayList)oofSetting_workType["destinations"];
            foreach (Destination destination in workType.Destinations)
            {
                foreach (Dictionary<string, object> dictionary in arrayList)
                {
                    DestinationType destinationType = (DestinationType)Enum.Parse(typeof(DestinationType), dictionary["destinationType"].ToString());
                    if (dictionary["id"] != null)
                    {
                        string b = dictionary["id"].ToString();
                        if (destination.ID.ToString() == b)
                        {
                            destination.DestinationType = destinationType;
                            destination.Name = dictionary["name"].ToString();
                            destinations.Add(destination);
                            break;
                        }
                    }
                    else
                    {
                        destinations.Add(new Destination(dictionary["name"].ToString(), destinationType));
                    }
                }
            }
            return destinations;
        }

        /// <summary>
        /// Add or save new exception user for the Out of Office status
        /// </summary>
        /// <param name="oofException">The Out of Office exception users</param>
        /// <returns>The Exception users</returns>
        public Destinations addNewExceptionUser(Dictionary<string, object> oofException)
        {
            Destinations destinations = new Destinations();
            ArrayList arrayList = (ArrayList)oofException["users"];
            for (int i = 0; i < arrayList.Count; i++)
            {
                Dictionary<string, object> dictionary = (Dictionary<string, object>)arrayList[i];
                string value = (string)dictionary["destinationType"];
                DestinationType type = (DestinationType)Enum.Parse(typeof(DestinationType), value);
                string name = (string)dictionary["name"];
                destinations.Add(new Destination(name, type));
            }
            return destinations;
        }


        /// <summary>
        /// Configure worklist criteria for specific process name and activity name
        /// </summary>
        /// <param name="fullProcessName">The full path of workflow</param>
        /// <param name="activityName">The activity name of workflow</param>
        /// <returns>The worklist criteria</returns>
        public WorklistCriteria configureWorklistCriteria(string fullProcessName, string activityName)
        {
            WorklistCriteria worklistCriteria = new WorklistCriteria();
            WCField field = (WCField)Enum.Parse(typeof(WCField), "ProcessFullName");
            WCCompare compare = (WCCompare)Enum.Parse(typeof(WCCompare), "Equal");
            worklistCriteria.AddFilterField(field, compare, fullProcessName);
            WCField field2 = (WCField)Enum.Parse(typeof(WCField), "ActivityName");
            WCCompare compare2 = (WCCompare)Enum.Parse(typeof(WCCompare), "Equal");
            WCLogical logical = (WCLogical)Enum.Parse(typeof(WCLogical), "And");
            worklistCriteria.AddFilterField(logical, field2, compare2, activityName);
            return worklistCriteria;
        }


        /// <summary>
        /// Get In Of Office or Out Of Office of current user
        /// </summary>
        /// <returns>The Out Of Office(true) or In Of Office(false)</returns>
        public bool GetOOFStatus()
        {
            bool status = false;

            switch (_connection.GetUserStatus())
            {
                case UserStatuses.None:
                case UserStatuses.Available:
                    status = true;
                    break;
                case UserStatuses.OOF:
                    status = false;
                    break;
            }

            return status;
        }


        ///// <summary>
        ///// Get User Out of Office status from K2 HostServer
        ///// </summary>
        ///// <param name="originalDestinationUser">The Originator Destination User</param>
        ///// <returns>The Worklist user status</returns>
        //public bool GetOOFStatus(string originalDestinationUser)
        //{
        //    bool status = false;
        //    bool worklistUserStatus = true;

        //    switch (string.IsNullOrEmpty(originalDestinationUser) ? _connection.GetUserStatus() : _connection.GetUserStatus(originalDestinationUser))
        //    {
        //        case UserStatuses.None:
        //        case UserStatuses.Available:
        //            status = true;
        //            break;
        //        case UserStatuses.OOF:
        //            status = false;
        //            break;
        //    }
        //    worklistUserStatus = status;
        //    if (!worklistUserStatus)
        //    {
        //        WorklistShares worklistShares = new WorklistShares();
        //        worklistShares = _connection.GetCurrentSharingSettings(ShareType.OOF);
        //        if (worklistShares.Count > 0)
        //        {
        //            WorklistShare worklistShare = worklistShares[0];
        //            worklistShare.ShareType = ShareType.OOF;
        //            List<OOFUser> list = new List<OOFUser>();
        //            foreach (Destination destination in worklistShare.WorkTypes[0].Destinations)
        //            {
        //                list.Add(new OOFUser
        //                {
        //                    UserName = destination.Name,
        //                    Type = destination.DestinationType.ToString()
        //                });
        //            }
        //            worklistUserStatus.users = list;
        //        }
        //    }
        //    return worklistUserStatus;
        //}
        
        public List<DestinationDto> GetDestinationDto(Destinations destinations)
        {
            var destinationDto = new List<DestinationDto>();
            foreach (Destination dest in destinations)
            {
                var destUser = dest.Name.FQNWithoutK2Label();
                var dto = new DestinationDto()
                {
                    DisplayName = destUser,
                    EmpNo = destUser,
                    LoginName = destUser,
                    Position = destUser,
                    TeamName = destUser
                };

                destinationDto.Add(dto);
            }

            return destinationDto;
        }

        public List<FilterDto> GetFiltersDto(WCFilter[] filters)
        {
            var dtos = new List<FilterDto>();

            foreach (WCFilter wc in filters)
            {
                var dto = new FilterDto()
                {
                    Compare = Convert.ToInt16(wc.Compare),
                    Field = Convert.ToInt16(wc.Field),
                    Logical = Convert.ToInt16(wc.Logical),
                    SubField = wc.SubField,
                    Value = wc.Value
                };
                dtos.Add(dto);
            }

            return dtos;
        }

        public WorklistCriteriaDto GetWorklistCriteriaDto(WorklistCriteria worklistCriteria)
        {
            var dto = new WorklistCriteriaDto()
            {
                Filters = GetFiltersDto(worklistCriteria.Filters),
                Count = worklistCriteria.Count,
                ManagedUser = worklistCriteria.ManagedUser,
                NoData = worklistCriteria.NoData,
                Platform = worklistCriteria.Platform,
                StartIndex = worklistCriteria.StartIndex
            };

            return dto;
        }

        public WorkTypeDto GetWorktypeDto(object work)
        {
            if (work is WorkType)
            {
                var worktype = (WorkType)work;
                var dto = new WorkTypeDto();
                dto.Destinations = GetDestinationDto(worktype.Destinations);
                dto.WorklistCriteria = GetWorklistCriteriaDto(worktype.WorklistCriteria);
                dto.Name = worktype.Name;

                return dto;
            }
            else
            {
                var worktype = (WorkTypeException)work;
                var dto = new WorkTypeDto();
                dto.Destinations = GetDestinationDto(worktype.Destinations);
                dto.WorklistCriteria = GetWorklistCriteriaDto(worktype.WorklistCriteria);
                dto.Name = worktype.Name;

                return dto;
            }
        }
        
    }
}
