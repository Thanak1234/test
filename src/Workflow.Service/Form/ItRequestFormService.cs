/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business;
using Workflow.Business.Interfaces;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject.IT;
using Workflow.Domain.Entities.IT;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class ItRequestFormService : AbstractRequestFormService<IItRequestFormBC, ItRequestWorkflowInstance>, IItRequestFormService
    {
        
        public ItRequestFormService()
        {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new ItRequestFormBC(workflow, docWorkflow);

        }

        public IEnumerable<ItemRoleDto> GetItemRoles(int itemId, int itemTypeId)
        {
            IRequestItemRepository requestItemRepository = new RequestItemRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));

            var sql=" SELECT IR.ID id, "+
                    "    IR.ROLE_NAME roleName,  "+
                    "    IR.DESCRIPTION description,  "+
                    "     IR.ADMIN isAdmin  "+
                    "FROM IT.ITEM_ITEM_TYPE IIT  " +
                    "    INNER JOIN IT.ITEM_ROLE_MAPPING TM ON IIT.ID = TM.ITEM_ITEM_TYPE_ID  " +
                    "    INNER JOIN IT.ITEM_ROLE IR ON IR.ID = TM.ITEM_ROLE_ID  " +
                    "WHERE IIT.ITEM_ID = @itemId AND IIT.ITEM_TYPE_ID = @itemTypeId ORDER BY IR.SEQUENCE, IR.ROLE_NAME ";

            IEnumerable<ItemRoleDto> itemRoles = requestItemRepository.SqlQuery<ItemRoleDto>(sql, new object[] { new SqlParameter("@itemId", itemId), new SqlParameter("@itemTypeId", itemTypeId) });
            return itemRoles;
        }

        public IEnumerable<ItemDto> GetItems(int sessionId, bool deprecated = false)
        {
            
            var sql = "SELECT ID id, "+
                "    DEPT_SESSION_ID sessionId, "+
                "    REQUEST_ITEM_NAME itemName, " +
                "    DESCRIPTION description, " +
                "    HOD_REQUIRED hodRequired " +
                "FROM IT.ITEM " +
                "WHERE DEPT_SESSION_ID = @sessionId " +
                (!deprecated ? " AND DEPRECATED = 0 " : string.Empty) + 
                "ORDER BY SEQUENCE, REQUEST_ITEM_NAME ";

            IRequestItemRepository requestItemRepository = new RequestItemRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));

            IEnumerable<ItemDto> items = requestItemRepository.SqlQuery<ItemDto>(sql, new object[] {new SqlParameter("@sessionId", sessionId)});

            return items;
        }

        public IEnumerable<ItemTypeDto> GetItemTypes(int itemId)
        {
            var sql = "SELECT IT.ID, "+
                    "    IT.ITEM_TYPE_NAME itemTypeName,  "+
                    "    IT.DESCRIPTION description " +
                    "FROM IT.ITEM_ITEM_TYPE IIT " +
                    "    INNER JOIN IT.ITEM_TYPE IT ON IIT.ITEM_TYPE_ID = IT.ID " +
                    "WHERE IIT.ITEM_ID = @itemId ORDER BY IT.SEQUENCE, IT.ITEM_TYPE_NAME ";
            IRequestItemRepository requestItemRepository = new RequestItemRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));

            IEnumerable<ItemTypeDto> itemTypes = requestItemRepository.SqlQuery<ItemTypeDto>(sql, new object[] { new SqlParameter("@itemId", itemId) });
            return itemTypes;
        }

    }
}
