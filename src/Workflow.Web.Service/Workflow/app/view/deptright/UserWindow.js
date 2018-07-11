Ext.define("Workflow.view.deptright.UserWindow", {
    extend: "Ext.window.Window",
    xtype: 'rights-deptuserwindow',
    controller: "rights-deptuserwindow",
    viewModel: {
        type: "rights-deptuserwindow"
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
                fieldLabel: 'Employee Name', bind: '{employeeInfo.displayname}', readOnly: true
            };
        }

        me.items = [
            {
                reference: 'windowForm',
                items: [
                    employeePickup,
                    //{ fieldLabel: 'Employee No', bind: '{employeeInfo.empno}', readOnly: true },                    
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
