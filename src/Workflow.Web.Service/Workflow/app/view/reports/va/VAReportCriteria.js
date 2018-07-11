Ext.define("Workflow.view.reports.va.VAReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-vacriteria',
    title: 'Gaming IA-Variance Approval Form - Criteria',
    viewModel: {
        type: "report-va"
    },
    buildFields: function (fields) {
        var me = this;

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Information',
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
                    xtype: 'combo',
                    fieldLabel: 'Information',
                    emptyText: 'TYPE OF ADJUSTMENT',
                    margin: '0 5 0 0',
                    editable: false,
                    store: ['Slot Operation', 'TR-Soft Count', 'TR-Table Drop'],
                    bind: {
                        value: '{criteria.AdjType}'
                    }
                }
            ]
        });

        return fields;
    }
});