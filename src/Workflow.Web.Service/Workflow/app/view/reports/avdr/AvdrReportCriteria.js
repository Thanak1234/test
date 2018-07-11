Ext.define("Workflow.view.reports.avdr.AvdrReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-avdrCriteria',
    title: 'AV Equipment Damage - Criteria',
    viewModel: {
        type: "report-avdr"
    },
    buildFields: function (fields) {

        var me = this;

        me.setFieldHidden(fields);

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Incident Date',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [
                {
                    xtype: 'datefield',
                    name: 'incidentDate',
                    fieldLabel: 'Incident Date',
                    emptyText: 'START DATE',
                    margin: '0 5 0 0',
                    bind: {
                        value: '{criteria.incidentStartDate}',
                        readOnly: '{!editable}'
                    }
                },
                {
                    xtype: 'datefield',
                    name: 'incidentDate',
                    fieldLabel: 'Incident Date',
                    emptyText: 'END DATE',
                    margin: '0 5 0 0',
                    bind: {
                        value: '{criteria.incidentEndDate}',
                        readOnly: '{!editable}'
                    }
                }]
        });

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Status',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [
                {
                    xtype: 'combo',
                    name: 'sdl',
                    fieldLabel: 'Status',
                    editable: false,
                    emptyText: 'STATUS <ALL>',
                    displayField: 'displayName',
                    valueField: 'value',
                    store: Ext.create('Ext.data.Store', {
                        fields: ['value', 'displayName'],
                        data: [
                            { "value": null, "displayName": "--- All ---" },
                            { "value": 'Damage', "displayName": "Damage" },
                            { "value": 'Lost', "displayName": "Lost" }
                        ]
                    }),
                    bind: {
                        value: '{criteria.sdl}'
                    }
                }]
        });

        return fields;
    },

    setFieldHidden: function (fields) {
        var field = fields[0];
        field.items[1].hidden = true;
        field = fields[1];
        field.items[0].hidden = true;
        field.maxWidth = 610;
    }
});