Ext.define("Workflow.view.events.common.FormBase", {
    extend: "Workflow.view.AbstractRequestForm",
    header: {
        hidden: true
    },
    infoTitle: 'Request Info',
    buildItems: function () {
        var me = this;

        var container = {
            xtype: 'panel',
            reference: 'container',
            
            align: 'center',
            width: '100%',
            layout: {
                type: 'vbox',
                pack: 'start',
                align: 'stretch'
            },
            defaults: {
                margin: 5,
                border: true,
                collapsible: true,
                xtype: 'form'
            },
            items: []
        };

        if (me.buildFormHeader) {
            var header = me.buildFormHeader();
            container.items.push(header);
        }

        var infoItems = {
            title: me.infoTitle,
            reference: 'reporterInfo',
            layout: 'column',
            defaults: {
                xtype: 'textfield',
                columnWidth: 0.5,
                padding: 10,
                labelWidth: 125,
                defaultType: 'textfield'
            },
            iconCls: 'fa fa-user',
            items: [
                {
                    fieldLabel: 'Employee',
                    xtype: 'employeePickup',
                    reference: 'reporter',
                    employeeEditable: true,
                    allowBlank: false,
                    changeOnly: true,
                    triggers: {
                        add: {
                            weight: 3,
                            cls: Ext.baseCSSPrefix + 'form-add-trigger',
                            scope: 'controller',
                            reference: 'addNewBt',
                            handler: 'showAddWindow',
                            hidden: true
                        },
                        edit: {
                            weight: 3,
                            cls: Ext.baseCSSPrefix + 'form-edit-trigger',
                            scope: 'controller',
                            reference: 'editBt',
                            handler: 'showEditWindow',
                            hidden: true
                        }
                    },
                    bind: {
                        selection: '{employeeInfo}',
                        value: '{formRequestData.employeeId}',
                        readOnly: '{readOnly}'
                    }
                },
                {
                    fieldLabel: 'Employee Number',
                    readOnly: true,
                    bind: {
                        value: '{employeeInfo.employeeNo}'
                    }
                },
                {
                    fieldLabel: 'Position',
                    readOnly: true,
                    bind: {
                        value: '{employeeInfo.position}'
                    }
                }
            ]
        };

        if (me.buildInfoItems) {
            var items = me.buildInfoItems();
            if (items.length > 0) {
                infoItems.items = infoItems.items.concat(items);
            }
        }

        container.items.push(infoItems);

        if (me.buildBodyItems) {
            var items = me.buildBodyItems();
            if (items.length > 0) {
                container.items = container.items.concat(items);
            }
        }

        return container;
        
    },
    buildButtons: function () {
        var me = this;

        var activity = me.getViewModel().get('activity');

        var actions = [];

        if ('Form View'.toLowerCase() === activity.toLowerCase()) {
            actions = [
             {
                 xtype: 'button',
                 text: 'Export PDF',
                 listeners: {
                     click: 'exportPDFHandler'
                 }
             },
             , '->',
             {
                 xtype: 'button',
                 text: 'Close',
                 listeners: {
                     click: 'closeWindow'
                 }
             }];
        } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
            actions = [{
                xtype: 'button',
                text: 'Export PDF',
                listeners: {
                    click: 'exportPDFHandler'
                }
            }, '->', {
                xtype: 'button',
                text: 'Actions',
                iconCls: 'toolbar-overflow-list',
                menu: [{
                    text: 'Edit',
                    listeners: {
                        click: 'formActions'
                    }
                }]
            }];
        }

        return actions;
    },
    getWorkflowFormConfig: function () {
        var me = this;
        var activity = me.getViewModel().get('activity');
        console.log(activity);
        if (!activity) {
            throw new Error("No activity found for workflow form config building");
        }

        if (activity.toUpperCase() === 'Submission'.toUpperCase()) {
            return me.getSubmissionConfig();
        } else if ('Form View'.toLowerCase() === activity.toLowerCase()) {
            return this.getViewConfig();
        }
        else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
            return this.getEditConfig();
        }
    },
    getSubmissionConfig: function () {
        return {
            requestorFormBlock: {
                visible: false,
                readOnly: false
            },
            activityHistoryForm: {
                visible: false
            },
            formUploadBlock: {
                visible: false,
                readOnly: false
            },
            containerBlock: {
                readOnly: false
            },
            commentBlock: {
                requiredActions: [],
                visible: false
            },
            openIn: 'TAB',
            afterActonState: 'RESET'
        };
    },
    getViewConfig: function () {
        return {
            requestorFormBlock: {
                visible: false,
                readOnly: false
            },
            activityHistoryForm: {
                visible: false
            },
            containerBlock: {
                readOnly: true
            },
            formUploadBlock: {
                visible: false,
                readOnly: false
            },
            commentBlock: {
                requiredActions: [],
                visible: false
            },
            openIn: 'TAB',
            afterActonState: 'RESET'
        };
    },
    getEditConfig: function () {
        return {
            requestorFormBlock: {
                visible: false,
                readOnly: false
            },
            activityHistoryForm: {
                visible: false
            },
            containerBlock: {
                readOnly: false
            },
            formUploadBlock: {
                visible: false,
                readOnly: false
            },
            commentBlock: {
                requiredActions: [],
                visible: false
            },
            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    }
});
