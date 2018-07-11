Ext.define("Workflow.view.reports.osha.OSHAReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-oshacriteria',
    title: 'OSHA Accident/Incident - Criteria',
    viewModel: {
        type: "report-osha"
    },
    buildFields: function (fields) {
        var me = this;

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Accident/Incident',
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
            items: [{
                xtype: 'textfield',
                emptyText: 'NATURE/TYPE OF THE ACCIDENT',
                margin: '0 5 0 0',
                enableKeyEvents: true,
                scope: me,
                bind: {
                    value: '{criteria.NTA}'
                }
            }, {
                xtype: 'textfield',
                emptyText: 'LOCATION OF ACCIDENT/INCIDENT',
                margin: '0 5 0 0',
                enableKeyEvents: true,
                scope: me,
                bind: {
                    value: '{criteria.LAI}'
                }
            }]
        });

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Victim & Witness',
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
            items: [{
                xtype: 'textfield',
                emptyText: 'VICTIM NAME',
                margin: '0 5 0 0',
                enableKeyEvents: true,
                scope: me,
                bind: {
                    value: '{criteria.Victim}'
                }
            }, {
                xtype: 'textfield',
                emptyText: 'WITNESS NAME',
                margin: '0 5 0 0',
                enableKeyEvents: true,
                scope: me,
                bind: {
                    value: '{criteria.Witness}'
                }
            }]
        });

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Date of Accident/Incident',
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
                        value: '{criteria.DTAStartDate}'
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
                        value: '{criteria.DTAEndDate}'
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