Ext.define('Workflow.view.ticket.setting.item.ItemPanel',{
    extend: 'Ext.grid.Panel',
    xtype: 'ticket-setting-item-panel',
    requires: [
        'Workflow.view.ticket.setting.item.ItemPanelController',
        'Workflow.view.ticket.setting.item.ItemPanelModel'
    ],
    controller: 'ticket-setting-item-item',
    viewModel: {
        type: 'ticket-setting-item-item'
    },
    scrollable: 'y',
    bind : {
        store: '{ticketSettingItemStore}'
    },
    border: true,
    region: 'center',
    hideHeaders: false,
    title: 'Setting Items',
    height: 100,    
    buildDockItems: function(){
      var me = this;
      var viewmodel = me.getViewModel();
      var store = viewmodel.getStore('ticketSettingItemStore');
      return [{
          xtype: 'toolbar',
          dock: 'top',
          items: [
              {
                  xtype: 'textfield',
                  width: 300,
                  bind: '{query}',
                  emptyText: 'Search Item, Sub Category, or Team...',
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
                  handler: 'onItemAddHander',
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
                text: 'Item Name', dataIndex: 'itemDisplayName', flex: 2,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	}

            },
            { text: 'Team', dataIndex: 'teamName', flex: 1,
                renderer : function(value, metadata, record) {
                   return me.showToolTip(value, metadata);
			 	}
            },
            { text: 'Team Description', dataIndex: 'teamDescription', hidden: true,
                renderer : function(value, metadata, record) {
                    return me.showToolTip(value, metadata);
			 	}
            },
            { text: 'SLA Id', dataIndex: 'slaId', flex: 1, hidden: true },
            { text: 'Description', dataIndex: 'description',
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
                text: 'Modified Date',
                dataIndex: 'modifiedDate',                
                renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                bind: {
                    width: '{columnDateWidth}'
                }
            },
            { text: 'Sub Category', dataIndex: 'subCateName', flex: 1 ,hidden: true,
                renderer : function(value, metadata, record) {
                   return me.showToolTip(value, metadata);
			 	}
            },
            { text: 'Sub Category Description', dataIndex: 'subCateDescription', flex: 2 ,hidden: true,
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
            {
                xtype: 'actioncolumn',
                //cls: 'tasks-icon-column-header tasks-edit-column-header',
                iconCls: 'fa fa-pencil-square-o',
                width: 24,
                //iconCls: 'grid-edit-record',
                tooltip: 'Edit',
                sortable: false,
                handler: 'onItemEditHandler'
            }
        ];

        me.callParent(arguments);
    },
    listeners: {
        itemdblclick: 'onItemDblClickHandler'
    },
    showToolTip: function(value, metadata){
        value ? metadata.tdAttr = 'data-qtip="' + value + '"' : '';
        return value;
    }
});
