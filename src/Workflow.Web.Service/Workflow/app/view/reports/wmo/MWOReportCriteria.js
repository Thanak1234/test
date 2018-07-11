Ext.define("Workflow.view.reports.mwo.MWOReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-mwoCriteria',
    title: 'Maintenance Work Order - Criteria',
    viewModel: {
        type: "report-mwo"
    },
    buildFields: function (fields) {
        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Location Type',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [{
                xtype: 'combo',
                fieldLabel: 'Location',
                editable: false,
                emptyText: 'NAME <ALL>',
                multiSelect: true,
                maxLength: 9000,
                store: ['CASINO', 'HOTEL', 'KTV', 'NAGA CITY WALK', 'PUBLIC AREA', 'SPA'],
                bind: {
                    value: '{criteria.Location}'
                }
            }]
        });
        return fields;
    }
});