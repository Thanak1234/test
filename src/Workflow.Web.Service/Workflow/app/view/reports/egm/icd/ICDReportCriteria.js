Ext.define("Workflow.view.reports.icd.ICDReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-icdCriteria',
    title: 'EGMIR - Criteria',
    viewModel: {
        type: "report-icd"
    },
    buildFields: function (fields) {
        var me = this;

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'MCID',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [{                           
                emptyText: 'MCID',
                xtype: 'numberfield',
                minValue: 1,
                allowExponential: false,
                enableKeyEvents: true,

                // Remove spinner buttons, and arrow key and mouse wheel listeners
                hideTrigger: true,
                keyNavEnabled: false,
                mouseWheelEnabled: false,

                scope: me,
                bind: {
                    value: '{criteria.mcid}'
                }
            }]
        });

        return fields;
    }
});