Ext.define('Workflow.view.icd.employeeList.requestEmployee', {
    extend: 'Ext.grid.Panel',
    xtype: 'icd-employeelist-requestemployee',

    requires: [
        'Workflow.view.icd.employeeList.requestEmployeeController',
        'Workflow.view.icd.employeeList.requestEmployeeModel'
    ],

    controller: 'icd-employeelist-requestemployee',
    viewModel: {
        type: "icd-employeelist-requestemployee"
    },
    iconCls: 'fa fa-users',
    title: 'Staff List',
    stateful: true,
    collapsible: true,
    headerBorders: false,

    viewConfig: {
        enableTextSelection: true
    },
    listeners: {
        rowdblclick: 'viewUserAction'
    },

    bind: {
        selection: '{selectedItem}',
        store: '{userStore}'
    },

    initComponent: function () {
        var me = this;
        me.tbar = ['->', {
            xtype: 'button',
            text: 'Add',
            iconCls: 'fa fa-plus-circle',
            handler: 'addUserAction',
            bind: {
                disabled: '{!canAddRemove}'
            }

        }, {
            xtype: 'button',
            text: 'Edit',
            bind: {
                disabled: '{!editable}'
            },
            iconCls: 'fa fa-plus-circlefa fa-pencil-square-o',
            handler: 'editUserAction'
        }, {
            xtype: 'button',
            text: 'View',
            bind: {
                disabled: '{!canView}'
            },
            iconCls: 'fa fa-eye',
            handler: 'viewUserAction'
        }];
        me.columns = [
            {
                text: 'EMP NO',
                width: 100,
                sortable: true,
                dataIndex: 'empNo'
            }, {
                text: 'EMP NAME',
                width: 150,
                sortable: true,
                dataIndex: 'empName'

            }, {
                text: 'DEPT NAME',
                width: 150,
                sortable: true,
                dataIndex: 'teamName'
            }, {
                text: 'POSITION',
                width: 150,
                sortable: true,
                dataIndex: 'position'
            }, {
                text: 'EMAIL',
                width: 100,
                sortable: true,
                dataIndex: 'email'
            }, {
                text: 'HIRED DATE',
                width: 100,
                sortable: true,
                hidden: true,
                dataIndex: 'hiredDate',
                renderer: function (v) {
                    return Ext.util.Format.date(v, 'Y/m/d');
                }
            }, {
                text: 'REPORT TO',
                width: 100,
                sortable: true,
                hidden: true,
                dataIndex: 'manager'
            }, {
                text: 'PHONE',
                width: 100,
                sortable: true,
                dataIndex: 'phone'
            }, {

                menuDisabled: true,
                sortable: false,
                width: 50,
                xtype: 'actioncolumn',
                align: 'center',
                bind: {
                    hidden: '{!canAddRemove}'
                },
                items: [{
                    iconCls: 'fa fa-trash-o',
                    tooltip: 'Remove', width: 150,
                    handler: 'removeRecord'
                }]
            }
        ];

        me.callParent(arguments);

    }



});