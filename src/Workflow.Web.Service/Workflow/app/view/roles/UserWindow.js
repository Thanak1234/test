Ext.define("Workflow.view.roles.UserWindow", {
    extend: "Ext.window.Window",
    xtype: 'roles-userwindow',
    controller: "roles-userwindow",
    viewModel: {
        type: "roles-userwindow"
    },
    title: 'User',
    formReadonly: false,
    frame: true,
    width: 600,
    minHeight: 200,
    layout: {
        type: 'vbox',
        pack: 'start',
        align: 'stretch'
    },
    autoWidth: true,
    bodyPadding: 10,
    resizable: false,
    modal: true,
    defaults: {
        layout: 'form',
        xtype: 'form',
        defaultType: 'textfield',
        flex: 1
    },
    initComponent: function () {
        var me = this;

        me.buildItems();
        me.buildButtons();

        me.callParent(arguments);
    },
    buildItems: function() {
        var me = this;

        var employeePickup = null;

        if (me.action == 'add') {
            employeePickup = {
                fieldLabel: 'Employee',
                xtype: 'employeePickup',
                reference: 'employeePickup',
                integrated: true,
                excludeOwner: false,
                allowBlank: false,
                bind: {
                    selection: '{employeeInfo}',
                    readOnly: '{readOnly}'
                }
            };
        } else {
            employeePickup = {
                fieldLabel: 'Employee', bind: '{employeeInfo.fullName}', readOnly: true
            };
        }


        me.items = [
            {
                reference: 'windowForm',
                items: [
                    employeePickup,
                    { fieldLabel: 'Employee No', bind: '{employeeInfo.employeeNo}', readOnly: true },
                    { fieldLabel: 'Position', bind: '{employeeInfo.position}', readOnly: true },
                    { fieldLabel: 'Sub Department', bind: '{employeeInfo.subDept}', readOnly: true },
                    { fieldLabel: 'Group', bind: '{employeeInfo.groupName}', readOnly: true },
                    { fieldLabel: 'Devision', bind: '{employeeInfo.devision}', readOnly: true },
                    { fieldLabel: 'Active Directory', bind: '{employeeInfo.loginName}', readOnly: true, allowBlank: false, fieldStyle: 'text-transform:uppercase' },
                    {
                        xtype: 'checkbox',
                        fieldLabel: 'Include',                        
                        allowBlank: false,
                        bind: '{include}'
                    }
                ]
            }];
    },
    buildButtons: function () {
        var me = this;

        me.buttons = [
            {
                text: me.buttonText,
                handler: 'onSaveClick'
            },
            {
                text: 'Cancel',
                handler: 'onCloseClick'
            }                        
        ];
    }
});
