using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Configuration
{
    public class MenuConfiguration : AbstractModelConfiguration<Menu> {
        public MenuConfiguration() {
            
            this.ToTable("SP_MENU", "ADMIN");
            this.Property(t => t.ParentId).HasColumnName("PARENT_ID");
            this.Property(t => t.RightId).HasColumnName("RIGHT_ID");
            this.Property(t => t.MenuName).HasColumnName("MENU_NAME");
            this.Property(t => t.MenuDesc).HasColumnName("MENU_DESC");
            this.Property(t => t.Workflow).HasColumnName("WORK_FLOW");
            this.Property(t => t.IsWorkflow).HasColumnName("IS_WORK_FLOW");
            this.Property(t => t.MenuUrl).HasColumnName("MENU_URL");
            this.Property(t => t.MenuIcon).HasColumnName("MENU_ICON");
            this.Property(t => t.MenuClass).HasColumnName("MENU_CLASS");
            this.Property(t => t.Sequence).HasColumnName("SEQUENCE");
            this.Property(t => t.NoChild).HasColumnName("NO_CHILD");
            this.Property(t => t.RouteId).HasColumnName("ROUTE_ID");
            this.Property(t => t.ViewClass).HasColumnName("VIEW_CLASS");
            this.Property(t => t.ClosableTab).HasColumnName("CLOSABLE_TAB");
            this.Property(t => t.Active).HasColumnName("ACTIVE");
            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");
            this.Property(t => t.IconCls).HasColumnName("ICON_CLS");

        }
    }
}
