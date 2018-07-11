Ext.define('Workflow.view.ticket.setting.category.CategoryPanel',{
    extend: 'Ext.grid.Panel',
    xtype: 'ticket-setting-category-panel',
    requires: [
        'Workflow.view.ticket.setting.category.CategoryPanelController',
        'Workflow.view.ticket.setting.category.CategoryPanelModel'
    ],
    controller: 'ticket-setting-category-category',
    viewModel: {
        type: 'ticket-setting-category-category'
    },
    scrollable: 'y',
    bind : {
        store: '{ticketSettingCategoryStore}'
    },
    border: true,
    region: 'center',
    hideHeaders: false,
    title: 'Setting category',
    height : 100,
    buildDockItems: function(){
      var me = this;
      var viewmodel = me.getViewModel();
      var store = viewmodel.getStore('ticketSettingCategoryStore');
      return [{
          xtype: 'toolbar',
          dock: 'top',
          items: [
              {
                  xtype: 'textfield',
                  width: 300,
                  bind: '{query}',
                  emptyText: 'Search Category, Department...',
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
                  handler: 'onAddHander',
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
                text: 'Category', dataIndex: 'cateName', flex: 1,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	}

            },
            { text: 'Description', dataIndex: 'description', flex: 1,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	}
            },
            { text: 'Department', dataIndex: 'deptName', flex: 1 },
            { text: 'Department Description', dataIndex: 'deptDescription', flex: 2 ,
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
            },{
                text: 'Status',                
                dataIndex: 'status',
                bind: {
                    width: '{columnStatusWidth}'
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

        me.callParent(arguments);
    },
    listeners: {
        itemdblclick: 'onDblClickHandler'
    },
    showToolTip: function(value, metadata){
        value ? metadata.tdAttr = 'data-qtip="' + value + '"' : '';
        return value;
    }
});
