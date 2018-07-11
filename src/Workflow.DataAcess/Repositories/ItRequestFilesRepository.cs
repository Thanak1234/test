/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Repositories
{
    public class ItRequestFilesRepository : RepositoryBase<ItRequestFiles>, IItRequestFilesRepository
    {
        public ItRequestFilesRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
