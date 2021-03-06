Ext.define('Workflow.view.ticket.setting.department.DepartmentPanel',{
    extend: 'Ext.grid.Panel',
    xtype: 'ticket-setting-department-panel',
    requires: [
        'Workflow.view.ticket.setting.department.DepartmentPanelController',
        'Workflow.view.ticket.setting.department.DepartmentPanelModel'
    ],
    controller: 'ticket-setting-department-department',
    viewModel: {
        type: 'ticket-setting-department-department'
    },
    scrollable: 'y',
    bind : {
        store: '{ticketSettingDepartmentStore}'
    },
    border: true,
    region: 'center',
    hideHeaders: false,
    title: 'Setting Department',
    height : 100,
    buildDockItems: function(){
      var me = this;
      var viewmodel = me.getViewModel();
      var store = viewmodel.getStore('ticketSettingDepartmentStore');
      return [{
          xtype: 'toolbar',
          dock: 'top',
          items: [
              {
                  xtype: 'textfield',
                  width: 300,
                  bind: '{query}',
                  emptyText: 'Search here...',
                  triggers: {
                      clear: {
                          cls: 'x-form-clear-trigger',
                          handler: 'onNavFilterClearTriggerClick',
                          hidden: true,
                          scope: 'controller'
                      },
                      search: {
                          cls: 'x-form-search-trigger',
                          weight: 1,
                          handler: 'onFilterHandler',
                          scope: 'controller'
                      }
                  },

                  listeners: {
                      specialkey: 'onEnterFilterHandler',
                      change: 'onNavFilterChanged'
                  }
              },{
                      xtype: 'statuscombofield',
                      gridStore: store,
                      bind: '{status}'
                  },
              '->',
              {
                  text: 'Add',
                  xtype: 'button',
                  handler: 'onAddHandler',
                  iconCls: 'fa fa-plus-circle'
              }]
      }];
    },
    initComponent: function () {
        var me = this;
        me.dockedItems = me.buildDockItems();
        me.columns = [
            {xtype: 'rownumberer'},
            {
                text: 'Department', dataIndex: 'deptName', flex: 1,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	}
            },
            {
                text: 'Automation Email', dataIndex: 'automationEmail', flex: 1,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	}
            },{
                text: 'Status',                
                dataIndex: 'status',
                bind: {
                    width: '{columnStatusWidth}'
                }
            },
            { text: 'Description', dataIndex: 'description', flex: 1,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	}
            },
            {
                text: 'Created Date', dataIndex: 'createdDate',
                renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                bind: {
                    width: '{columnDateWidth}'
                }

            },
            {
                text: 'Modified Date', dataIndex: 'modifiedDate',
                renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                bind: {
                    width: '{columnDateWidth}'
                }
            },
            {
                xtype: 'actioncolumn',
                //cls: 'tasks-icon-column-header tasks-edit-column-header',
                width: 24,
                //iconCls: 'grid-edit-record',
                iconCls: 'fa fa-pencil-square-o',
                tooltip: 'Edit',
                sortable: false,
                handler: 'onEditHandler'
            }
        ];

        this.callParent(arguments);
    },
    listeners: {
        itemdblclick: 'onDblClickHandler'
    },
    showToolTip: function(value, metadata){
        value ? metadata.tdAttr = 'data-qtip="' + value + '"' : '';
        return value;
    }
});
