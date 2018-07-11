using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Workflow.DataObject;
using Workflow.Domain.Entities;
using Workflow.Service;
using Workflow.Web.Models;

namespace Workflow.Web.Service.Controllers
{  
    public class ProfileController : ApiController
    {
        private string _callbackQueryParameter;

        [HttpGet] 
        public SerializeJsonHeader GetEmployeeProfile([FromUri]String empNo)
        {
            var callback = GetCallbackName();

            var empProfile = new List<EmployeeDto> { new EmployeeService().GetEmployeeProfile(empNo) };
            var SerializeJsonHeader = new SerializeJsonHeader {
                        callback = callback,
                        data = empProfile
                 };
            string json = JsonConvert.SerializeObject(SerializeJsonHeader, Formatting.Indented);

            SerializeJsonHeader jsonObject = JsonConvert.DeserializeObject<SerializeJsonHeader>(json);

            return jsonObject;
        }
         

        [HttpPut] 
        public void UpdateEmployeeProfile([FromBody]EmployeeViewModel val)
        {
            Mapper.CreateMap<EmployeeViewModel, Employee>();
            var empDto = Mapper.Map<Employee>(val); 
            new EmployeeService().UpdateEmployeeProfile(empDto);
        }


        [HttpGet] 
        public string GetProfile()
        {
            return "";
        }  

        public string CallbackQueryParameter
        {
            get { return _callbackQueryParameter ?? "callback"; }
            set { _callbackQueryParameter = value; }
        }
        private string GetCallbackName()
        {
            if (HttpContext.Current.Request.HttpMethod != "GET")
                return null;
            return HttpContext.Current.Request.QueryString[CallbackQueryParameter];
        }

    }

    public class SerializeJsonHeader
    {
        public List<EmployeeDto> data { get; set; } 
        public string callback { get; set; } 
        public string success { get; set; } 

    }
}
