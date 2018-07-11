/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.AVRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject.AV;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class AvbRequestFormService : AbstractRequestFormService<IAvbRequestFormBC, AvbRequestWorkflowInstance>, IAvbRequestFormService
    {
        
        public AvbRequestFormService()
        {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
           BC  = new AvbRequestFormBC(workflow, docWorkflow);
        }

        IEnumerable<AvbItemDto> IAvbRequestFormService.getItem(int itemTypeId)
        {
            IAvbRequestItemRepository avbRequestItemRepository = new AvbRequestItemRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            string sql = "  SELECT ID id, "+
                         "   ITEM_NAME itemName,  "+
                         "   DESCRIPTION description  "+
                         " FROM [EVENT].ITEM  " +
                         " WHERE ITEM_TYPE_ID =@itemTypeId ";

            return avbRequestItemRepository.SqlQuery<AvbItemDto>(sql, new object[] { new SqlParameter("@itemTypeId", itemTypeId) });

        }

        public IEnumerable<AvbItemTypeDto> getItemType()
        {
            IAvbRequestItemRepository avbRequestItemRepository = new AvbRequestItemRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            string sql = "SELECT ID id, " +
                             "ITEM_TYPE_NAME itemTypeName,  " +
                             "[DESCRIPTION] description  " +
                        "FROM [EVENT].ITEM_TYPE ";

            return avbRequestItemRepository.SqlQuery<AvbItemTypeDto>(sql);
        }

        public IEnumerable<AvbItemDto> getItem(string itemTypeName)
        {
            IAvbRequestItemRepository avbRequestItemRepository = new AvbRequestItemRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            string sql = @"SELECT 
	                    I.ID id, 
	                    I.ITEM_NAME itemName,  
	                    I.[DESCRIPTION] description  
                    FROM [EVENT].ITEM I INNER JOIN [EVENT].[ITEM_TYPE] T ON T.ID = I.ITEM_TYPE_ID
                    WHERE T.ITEM_TYPE_NAME = @itemTypeName";

            return avbRequestItemRepository.SqlQuery<AvbItemDto>(sql, new object[] { new SqlParameter("@itemTypeName", itemTypeName) });
        }
    }
}
