Ext.define("Workflow.view.reports.mcn.MCNReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-mcnCriteria',
    title: 'EGMMR - Criteria',
    viewModel: {
        type: "report-mcn"
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
            },
            {
                xtype: 'combo',
                emptyText: 'TYPE',
                fieldLabel: 'Type',
                editable: false,
                columnWidth: 0.3,
                displayField: 'type',
                
                scope : me,
                bind: {
                    store: '{types}',
                    value: '{criteria.type}',
                    readOnly: '{readOnly}'
                }
            }
                

            ]
        });

        return fields;
    }
});