Ext.define("Workflow.view.reports.att.ATTReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-attCriteria',
    title: 'EGMATT - Criteria',
    viewModel: {
        type: "report-att"
    },
    buildFields: function (fields) {
        var me = this;

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Particular Detail',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [
            {
                xtype: 'combo',
                emptyText: 'PARTICULAR DETAIL',
                fieldLabel: 'Type',
                editable: false,
                columnWidth: 0.3,
                displayField: 'type',
                allowBlank: false,
                bind: {
                    store: '{types}',
                    value: '{criteria.detailtype}',
                    readOnly: '{readOnly}'
                }
            }


            ]
        });

        return fields;
    }
});