Ext.define('Workflow.view.common.worklist.components.UserGridPanel', {
    extend: 'Ext.grid.Panel',
    xtype: 'common.worklist.components.user-grid',
    frame: false,
    initComponent: function () {
        var me = this;

        me.buildColumns();

        me.callParent();
    },

    buildColumns: function () {
        var me = this;
        me.columns = [
            { xtype: 'rownumberer' },
            { text: "Id", flex: 1, sortable: false, dataIndex: 'EmpNo' },
            { text: "Name", flex: 1, sortable: false, dataIndex: 'DisplayName' },
            { text: "Position", flex: 1, sortable: false, dataIndex: 'Position' },
            { text: "Department", flex: 1, sortable: false, dataIndex: 'TeamName' },
            { text: "Login", flex: 1, sortable: false, dataIndex: 'LoginName' }
        ];
    },
    dockedItems: [{
        dock: 'top',
        xtype: 'toolbar',
        items: [
            {
                xtype: 'button',
                text: 'Add',
                iconCls: 'fa fa-plus-circle',
                reference: 'btnUserAdd',
                handler: 'onAddUserClickHandler'
            },
            {
                xtype: 'button',
                text: 'Remove',
                iconCls: 'fa fa-times',
                reference: 'btnUserRemove',
                bind: {
                    disabled: '{!destinationGridPanel.selection}'
                },
                handler: 'onRemoveUserClickHandler'
            }
        ]
    }]

});