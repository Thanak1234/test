Ext.define("Workflow.view.roles.EditApplication", {
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",
    xtype: 'roles-edit-application',
    requires: [
        'Ext.view.MultiSelector'
    ],
    applicationId: 0,
    viewModel: {
        data: {
            submitBtText: 'Save',
            workflow: {
                processName: null,
                processPath: null,
                requestCode: null,
                processCode: null,
                xtype: null,
                formNumber: null,
                active: false,
                users: null
            }
        }
    },
    controller: {
        type: 'common-windowdialog',
        getFormItem: function () {
            var view = this.getView(),
                viewModel = view.getViewModel();
            
            console.log('viewModel', view.getReferences().workflowUsers.getStore());
            return null;
        }
    },
    title: 'Workflow Setting',
    initComponent: function () {
        var me = this,
            viewModel = me.getViewModel();

        me.items = [{
            xtype: 'form',
            frame: false,
            bodyPadding: '10 10 10 10',
            reference: 'form',
            method: 'POST',
            defaultListenerScope: true,
            defaults: me.formsFieldSet,
            items: [{
                xtype: 'container',
                layout: 'hbox',
                defaults: me.defaultsFieldSet,
                items: [{
                    xtype: 'container',
                    defaults: me.formsFieldSet,
                    items: [{
                        xtype: 'lookupfield',
                        fieldLabel: 'Process Name',
                        namespace: '[EVENT].[SPECIFICATION].[NAME]',
                        isChild: true,
                        listeners: {
                            select: function (combo) {
                                //   me.getViewModel().set('name', combo.getRawValue());
                            }
                        },
                        bind: {
                            value: '{workflow.processPath}'
                        }
                    }, {
                        xtype: 'textfield',
                        fieldLabel: 'Application Name',
                        //allowBlank: false,
                        bind: {
                            value: '{workflow.processName}'
                        }
                    }, {
                        xtype: 'textfield',
                        fieldLabel: 'Request Code',
                        //allowBlank: false,
                        bind: {
                            value: '{workflow.requestCode}'
                        }
                    }, {
                        xtype: 'textfield',
                        fieldLabel: 'Process Code',
                        //allowBlank: false,
                        bind: {
                            value: '{workflow.processCode}'
                        }
                    }, {
                        xtype: 'textfield',
                        fieldLabel: 'Form XType',
                        //allowBlank: false,
                        bind: {
                            value: '{workflow.xtype}'
                        }
                    }, {
                        xtype: 'numberfield',
                        fieldLabel: 'Form Number',
                        //allowBlank: false,
                        allowDecimals: false,
                        bind: {
                            value: '{workflow.formNumber}'
                        }
                    }, {
                        xtype: 'checkboxfield',
                        fieldLabel: 'Active',
                        checked: true,
                        bind: {
                            value: '{workflow.active}'
                        }
                    }]
                }, {
                    xtype: 'multiselector',
                    title: 'Users',
                    fieldName: 'value',
                    margin: '15 0 5 0',
                    border: 1,
                    reference: 'workflowUsers',
                    viewConfig: {
                        deferEmptyText: false,
                        height: 210,
                        emptyText: 'No employees selected'
                    },
                    search: {
                        field: 'value',
                        width: 180,
                        store: {
                            type: 'form-lookup',
                            proxy: {
                                type: 'rest',
                                url: Workflow.global.Config.baseUrl + 'api/forms/lookups',
                                autoLoad: true,
                                reader: {
                                    type: 'json'
                                },
                                extraParams: {
                                    name: '[EVENT].[SPECIFICATION].[NAME]',
                                    parentId: -1
                                }
                            }
                        }
                    },
                    bind: {
                        store: '{workflow.users}'
                    }
                }]
            }]
        }];

        me.callParent(arguments);
    },
    defaultsFieldSet: function () {
        return {
            flex: 1,
            anchor: '100%',
            defaultType: 'textfield',
            labelAlign: 'right',
            labelWidth: 150,
            layout: 'form',
            margin: '5 0 5 0'
        };
    }
});