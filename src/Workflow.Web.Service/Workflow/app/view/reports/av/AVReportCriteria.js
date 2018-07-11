Ext.define("Workflow.view.reports.av.AVReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-avCriteria',
    title: 'AV Job Brief - Criteria',
    viewModel: {
        type: "report-av"
    },
    buildFields: function (fields) {
        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Item Type Name',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [{
                xtype: 'combo',
                fieldLabel: 'Workflow',
                emptyText: 'ITEM TYPE NAME <ALL>',
                editable: false,
                displayField: 'displayName',
                valueField: 'value',
                margin: '0 5 0 0',
                value: null,
                store: Ext.create('Ext.data.Store', {
                    fields: ['value', 'displayName'],
                    data: [
                        { "value": null, "displayName": "--- ALL ---" },
                        { "value": "Sound", "displayName": "Sound" },
                        { "value": "Visual", "displayName": "Visual" },
                        { "value": "Lights", "displayName": "Lights" }
                    ]
                }),
                bind: {
                    value: '{criteria.ItemType}'
                }
            }]
        });
        return fields;
    }
});