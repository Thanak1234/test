Ext.define("Workflow.view.osha.Employee", {
    extend: "Ext.grid.Panel",
    xtype: 'osha-employee',
    controller: "osha-employee",
    viewModel: {
        type: "osha-employee"
    },
    border: true,
    collapsible: true,
    bind: {
        selection: '{selectedItem}',
        store: '{userStore}'
    },
    initComponent: function () {
        var me = this;
        me.buildColumns();
        me.buildButtons();
        me.callParent(arguments);
    },
    buildColumns: function () {
        this.columns = [
            {
                text: 'EMP NO',
                width: 100,
                sortable: false,
                menuDisabled: true,
                dataIndex: 'empNo'
            }, {
                text: 'EMP NAME',
                width: 150,
                sortable: false,
                menuDisabled: true,
                dataIndex: 'empName'

            }, {
                text: 'DEPT NAME',
                flex: 1,
                sortable: false,
                menuDisabled: true,
                dataIndex: 'deptName'
            }, {
                text: 'POSITION',
                width: 150,
                sortable: false,
                menuDisabled: true,
                dataIndex: 'position'
            }, {
                text: 'EMAIL',
                width: 100,
                sortable: false,
                menuDisabled: true,
                dataIndex: 'email'
            }, {
                text: 'REPORT TO',
                width: 80,
                sortable: false,
                hidden: true,
                menuDisabled: true,
                dataIndex: 'manager'
            }, {
                text: 'PHONE',
                width: 100,
                sortable: false,
                menuDisabled: true,
                dataIndex: 'phone'
            }, {

                menuDisabled: true,
                sortable: false,
                width: 50,
                xtype: 'actioncolumn',
                align: 'center',
                bind: {
                    hidden: '{!(config.edit)}'
                },
                items: [{
                    iconCls: 'fa fa-trash-o',
                    tooltip: 'Remove',
                    width: 150,
                    handler: 'removeRecord'
                }]
            }
        ];
    },
    buildButtons: function () {
        this.tbar = ['->', {
            xtype: 'button',
            text: 'Add',
            iconCls: 'fa fa-plus-circle',
            handler: 'addUserAction',
            bind: {
                disabled: '{!config.add}'
            }

        }, {
            xtype: 'button',
            text: 'Edit',
            bind: {
                disabled: '{!config.edit}'
            },
            iconCls: 'fa fa-plus-circlefa fa-pencil-square-o',
            handler: 'editUserAction'
        }, {
            xtype: 'button',
            text: 'View',
            bind: {
                disabled: '{!config.view}'
            },
            iconCls: 'fa fa-eye',
            handler: 'viewUserAction'
        }];
    }
});
