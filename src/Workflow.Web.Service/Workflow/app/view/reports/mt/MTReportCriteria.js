Ext.define("Workflow.view.reports.mt.MTReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-mtCriteria',
    //title: 'Medical Treatment - Criteria',
    viewModel: {
        type: "report-mt"
    },
    buildFields: function (fields) {
        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Work Shift',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [{
                xtype: 'combo',
                fieldLabel: 'Work Shift',
                editable: false,
                emptyText: 'WORK SHIFT <ALL>',
                displayField: 'displayName',
                valueField: 'value',
                margin: '0 5 0 0',
                store: Ext.create('Ext.data.Store', {
                    fields: ['value', 'displayName'],
                    data: [
                        { "value": null, "displayName": "--- All ---" },
                        { "value": 'SPLIT', "displayName": "SPLIT" },
                        { "value": 'MORNING', "displayName": "MORNING" },
                        { "value": 'AFTERNOON', "displayName": "AFTERNOON" },
                        { "value": 'OFFICE_HOURS', "displayName": "OFFICE HOURS" },
                        { "value": 'NIGHT', "displayName": "NIGHT" },
                        { "value": "PH", "displayName": "PH" },
                        { "value": "DAY_OFF", "displayName": "Day Off" }
                    ]
                }),
                bind: {
                    value: '{criteria.Shift}'
                }
            }, {
                xtype: 'combo',
                fieldLabel: 'Work Shift',
                editable: false,
                emptyText: 'STATUS CHECK <ALL>',
                displayField: 'displayName',
                valueField: 'value',
                store: Ext.create('Ext.data.Store', {
                    fields: ['value', 'displayName'],
                    data: [
                        { "value": 0, "displayName": "--- All ---" },
                        { "value": 1, "displayName": "Fit To Work" },
                        { "value": 2, "displayName": "Unfit To Work" }
                    ]
                }),
                bind: {
                    value: '{criteria.FitToWork}'
                }
            }]
        });
        return fields;
    }
});