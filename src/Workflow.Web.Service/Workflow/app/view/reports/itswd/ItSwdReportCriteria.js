Ext.define("Workflow.view.reports.itswd.ItSwdReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-itswdcriteria',
    title: 'IT Software Development - Criteria',
    viewModel: {
        type: "report-itswd"
    },
    buildFields: function (fields) {
        var me = this;

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Project Init',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250,
                triggers: {
                    clear: {
                        type: 'clear',
                        weight: -1
                    }
                }
            },
            items: [
                {
                    xtype: 'combo',
                    fieldLabel: 'Application',
                    emptyText: 'APPLICATION',
                    margin: '0 5 0 0',
                    editable: false,
                    store: ['Casino Applications', 'Hotel Applications', 'Back Office Applications'],
                    bind: {
                        value: '{criteria.Application}'
                    }
                },
                {
                    xtype: 'textfield',
                    fieldLabel: 'Purpose Change',
                    emptyText: 'PURPOSED CHANGE',
                    margin: '0 5 0 0',
                    bind: {
                        value: '{criteria.PurposedChange}'
                    }
                }
            ]
        });

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Target delivery',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250,
                triggers: {
                    clear: {
                        type: 'clear',
                        weight: -1
                    }
                }
            },
            items: [
                {
                    xtype: 'textfield',
                    emptyText: 'TARGET DELIVERY DATE',
                    bind: {
                        value: '{criteria.TD}'
                    },
                    margin: '0 5 0 0',
                    triggers: {
                        clear: {
                            type: 'clear',
                            weight: -1
                        }
                    }
                }
            ]
        });

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Go Live Date',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250,
                triggers: {
                    clear: {
                        type: 'clear',
                        weight: -1
                    }
                }
            },
            items: [
                {
                    xtype: 'datefield',
                    emptyText: 'START DATE',
                    bind: {
                        value: '{criteria.GLStartDate}'
                    },
                    margin: '0 5 0 0',
                    triggers: {
                        clear: {
                            type: 'clear',
                            weight: -1
                        }
                    }
                },
                {
                    xtype: 'datefield',
                    emptyText: 'END DATE',
                    bind: {
                        value: '{criteria.GLEndDate}'
                    },
                    margin: '0 5 0 0',
                    triggers: {
                        clear: {
                            type: 'clear',
                            weight: -1
                        }
                    }
                }
            ]
        });

        return fields;
    }
});