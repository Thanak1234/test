Ext.define('Workflow.view.approvalmgr.EmployeeEdit', {
    extend: 'Ext.form.Panel',
    xtype: 'employee-edit',
    title: 'EMPLOYEE',
    frame: false,
    controller: true,
    viewModel: {
        data: {
            employee: {}
        }
    },
    bodyPadding: '5 5 0',
    fieldDefaults: {
        labelAlign: 'top',
        msgTarget: 'side'
    },
    defaults: {
        border: false,
        xtype: 'panel',
        flex: 1,
        layout: 'anchor'
    },

    layout: 'hbox',
    bind: {
        title: 'EMPLOYEE - {employee.labelEmployee}'
    },
    items: [{
        items: [{
            xtype: 'textfield',
            fieldLabel: 'Emp. No',
            anchor: '-5',
            bind: {
                value: '{employee.empNo}'
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'First Name',
            anchor: '-5',
            bind: {
                value: '{employee.firstName}'
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Position',
            anchor: '-5',
            bind: {
                value: '{employee.position}'
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Account',
            anchor: '-5',
            bind: {
                value: '{employee.loginName}'
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Ext. Phone',
            anchor: '-5',
            bind: {
                value: '{employee.extNum}'
            }
        }, {
            xtype: 'datefield',
            fieldLabel: 'Join Date',
            anchor: '-5',
            bind: {
                value: '{employee.joinDate}'
            }
        }]
    }, {
        items: [{
            xtype: 'textfield',
            fieldLabel: 'Emp. Name',
            anchor: '100%',
            bind: {
                value: '{employee.displayName}'
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Email',
            anchor: '100%',
            bind: {
                value: '{employee.lastName}'
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Dept.',
            anchor: '100%',
            bind: {
                value: '{employee.deptName}'
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Email',
            anchor: '100%',
            bind: {
                value: '{employee.email}'
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Phone',
            anchor: '100%',
            bind: {
                value: '{employee.phoneNum}'
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Status',
            anchor: '100%',
            bind: {
                value: '{employee.empType}'
            }
        }]
    }]

})