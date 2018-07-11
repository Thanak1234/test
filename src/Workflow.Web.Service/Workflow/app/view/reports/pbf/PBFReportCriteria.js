Ext.define("Workflow.view.reports.pbf.PBFReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-pbfCriteria',
    title: 'PBF - Criteria',
    viewModel: {
        type: "report-pbf"
    },
    buildFields: function (fields) {
        var me = this;

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Item Name',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [{
                xtype: 'lookupfield',
                allowBlank: true,
                emptyText: 'ITEM NAME',
                namespace: '[EVENT].[SPECIFICATION].[NAME]',
                isChild: true,
                margin: '0 5 0 0',
                bind: {
                    value: '{criteria.ItemId}'
                },
                diabledClear: false
            }, {
                xtype: 'textfield',
                fieldLabel: 'Project',
                emptyText: 'PROJECT NAME/REF <ALL>',
                enableKeyEvents: true,
                scope: me,
                bind: {
                    value: '{criteria.ProjectName}'
                }
            }]
        }, {
            xtype: 'fieldcontainer',
            fieldLabel: 'Dateline',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [{
                xtype: 'datefield',
                name: 'startDate',
                fieldLabel: 'Start',
                emptyText: 'START DATE',
                margin: '0 5 0 0',
                bind: {
                    value: '{criteria.DatelineStarted}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'datefield',
                name: 'endDate',
                fieldLabel: 'End',
                emptyText: 'END DATE',
                bind: {
                    value: '{criteria.DatelineEnded}',
                    readOnly: '{!editable}'
                }
            }]
        });

        return fields;
    }
});