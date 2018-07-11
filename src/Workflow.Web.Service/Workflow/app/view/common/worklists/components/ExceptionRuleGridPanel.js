Ext.define('Workflow.view.worklist.components.ExceptionRuleGridPanel', {
    extend: 'Ext.grid.Panel',
    xtype: 'common.worklist.components.exceptionrule-grid',
    requires: [
        'Ext.ux.form.SearchField'
    ],

    title: 'Exceptions',
    frame: true,

    initComponent: function () {
        var me = this;
        
        me.buildColumns();
        me.buildDockItems();

        me.callParent();
    },

    buildColumns: function () {
        var me = this;

        me.columns = [
            { xtype: 'rownumberer' },
            { text: "Rule Name", flex: 1, sortable: false, dataIndex: 'Name' },
            { text: "Process", flex: 1, sortable: false, dataIndex: 'Process' },
            { text: "Activity", flex: 1, sortable: false, dataIndex: 'ActDisplayName' },
            {
                text: "Users", flex: 1, sortable: false, dataIndex: 'Destinations',
                renderer: function (value, metaData, record, row, col, store, gridView) {
                    return me.getUsersRender(value);
                }
            }
        ];
    },

    buildDockItems: function () {
        var me = this;
        me.dockedItems = [{
            dock: 'top',
            xtype: 'toolbar',
            items: [                
                {
                    xtype: 'button',
                    text: 'Add',
                    iconCls: 'fa fa-plus-circle',
                    reference: 'exceptionAddBtn',
                    handler: 'onExceptionRuleAddClickHandler'
                },
                {
                    xtype: 'button',
                    text: 'Edit',
                    iconCls: 'fa fa-pencil-square-o',
                    handler: 'onExceptionRuleEditClickHandler',
                    bind: {
                        disabled: '{!exceptionRuleGridPanel.selection}'
                    }
                },
                {
                    xtype: 'button',
                    text: 'Remove',
                    iconCls: 'fa fa-times',
                    handler: 'onExceptionRuleRemoveClickHandler',
                    bind: {
                        disabled: '{!exceptionRuleGridPanel.selection}'
                    }
                }
            ]
        }];

    },
    getUsersRender: function (records) {
        var result = '';
        Ext.each(records, function (value) {
            result += value.DisplayName + ', ';
        });

        var index = result.lastIndexOf(',');
        if (index > 0) {
            result = result.substr(0, index - 1);
        }
        return result;
    }

});