/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataAcess.Infrastructure
{
    public interface IUnitOfWork
    {
        void commit();
        DbContextTransaction begin();

        void commit(DbContextTransaction tran);
        void rollBack(DbContextTransaction tran);
    }
     
}
