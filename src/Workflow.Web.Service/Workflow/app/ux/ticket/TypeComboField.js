Ext.define('Workflow.ux.ticket.TicketTypeComboField', {
    extend: 'Ext.form.field.ComboBox',
    alias: ['widget.tickettypecombofield'],
    config: {

    },
    fieldLabel: 'Type',
    labelWidth: 100,
    labelAlign: 'right',
    emptyText: 'Select Here...',
    forceSelection: true,
    queryMode: 'Local',
    gridStore: null,
    value: null,
    initComponent: function () {
        var me = this;
        me.store = Ext.create('Ext.data.Store', {
            model: 'Workflow.model.ticket.TicketType',
            autoLoad: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/ticket_type',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        });
        me.listeners= {
            change: function (self, newValue, oldValue, eOpts){
                if(me.gridStore){
                    me.gridStore.getProxy().extraParams = Ext.Object.merge(me.gridStore.getProxy().extraParams, { ticketTypeId: newValue });
                    me.gridStore.load();
                }
            }
        };
        me.callParent();

    },
    queryMode: 'local',
    displayField: 'typeName',
    valueField: 'id',
    listConfig: {
        minWidth: 200,
        //resizable: true,
        //loadingText: 'Searching...',
        //itemSelector: '.search-item',
        itemTpl: [            
            '<div><img src="{icon}"/><span style="margin-left: 10px!important;">{typeName}</span></div>'           
        ]
    }
});
