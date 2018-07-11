Ext.define("Workflow.view.reports.jram.JRAMReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-jramcriteria',
    title: 'Ram Clear Form - Criteria',
    viewModel: {
        type: "report-jram"
    },
    buildFields: function (fields) {
        var me = this;
        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Property',
            layout: 'vbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                margin: '0 5 0 0'
            },
            items: [
                {
                    xtype: 'fieldcontainer',
                    layout: 'hbox',
                    margin: '5 0 5 0',
                    defaults: {
                        flex: 1,
                        hideLabel: true,
                        width: 250,
                        margin: '0 5 0 0',
                        xtype: 'textfield',
                        triggers: {
                            clear: {
                                type: 'clear',
                                hideWhenEmpty: true,
                                clearOnEscape: true,
                                weight: -1
                            }
                        }
                    },
                    items: [
                        {
                            xtype: 'combobox',
                            margin: '0 5 0 0',
                            emptyText: 'PROPERTY <ALL>',
                            store: ['Naga 1', 'Naga 2'],
                            bind: {
                                value: '{criteria.property}'
                            }
                        },
                        {
                            emptyText: 'GMID <ALL>',
                            margin: '0 5 0 0',
                            bind: {
                                value: '{criteria.gmid}'
                            }
                        }
                    ]
                }
            ]
        });
        return fields;
    }
});