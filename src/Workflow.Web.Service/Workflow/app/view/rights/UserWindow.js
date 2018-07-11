Ext.define("Workflow.view.rights.UserWindow", {
    extend: "Ext.window.Window",
    xtype: 'rights-userwindow',
    controller: "rights-userwindow",
    viewModel: {
        type: "rights-userwindow"
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
    buildItems: function () {
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
                    { fieldLabel: 'Department', bind: '{employeeInfo.subDept}', readOnly: true },
                    { fieldLabel: 'Email', bind: '{employeeInfo.email}', readOnly: true },
                    { fieldLabel: 'Ext', bind: '{employeeInfo.ext}', readOnly: true },
                    { fieldLabel: 'Description', bind: '{desc}', readOnly: false },
                    {
                        xtype: 'checkbox',
                        reference: 'chkActive',
                        fieldLabel: 'Active',
                        allowBlank: false,
                        bind: '{active}'
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
