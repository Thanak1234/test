/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.Domain.Entities;

namespace Workflow.Service.Interfaces
{
    public interface IRequestHeaderService
    {
        RequestHeader GetRequestHeader(int id);
        void SaveRequestHeader(RequestHeader requestHeader);
        object GetIncidentRequestHeaders(QueryParameter queryParam);
        object GetFormsByREQ(QueryParameter queryParam, string requestCode);
    }
}
