/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.AV
{
    public class AvbItem
    {
        public int Id { get; set; }

        public int ItemTypeId { get; set; }

        public virtual AvbItemType ItemType { get; set; }

        public string ItemName { get; set; }

        public string Description { get; set; }

    }
}
