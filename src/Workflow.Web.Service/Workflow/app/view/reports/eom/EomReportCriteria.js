Ext.define("Workflow.view.reports.avir.EomReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-eomcriteria',
    title: 'Eomployee Of The Month - Criteria',
    viewModel: {
        type: "report-eom"
    },
    buildFields: function (fields) {

        var me = this;
        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Month',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250,
                editable: false,
                triggers: {
                    clear: {
                        type: 'clear',
                        weight: -1
                    }
                }
            },
            items: [
                {
                    xtype: 'monthfield',
                    fieldLabel: 'Month',
                    emptyText: 'START MONTH',
                    margin: '0 5 0 0',
                    format: 'F, Y',
                    bind: {
                        value: '{criteria.StartMonth}',
                        readOnly: '{!editable}'
                    }
                },
                {
                    xtype: 'monthfield',
                    emptyText: 'END MONTH',
                    format: 'F, Y',
                    margin: '0 5 0 0',
                    bind: {
                        value: '{criteria.EndMonth}',
                        readOnly: '{!editable}'
                    }
                }]
        });

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Activity',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [
                {
                    xtype: 'combo',
                    hidden: false,
                    emptyText: 'ACTIVITY <ALL>',
                    editable: false,
                    store: ['HOD Approval', 'T&D Review', 'T&D Approval', 'Payroll', 'Creative'],
                    bind: {
                        value: '{criteria.Activity}'
                    },
                    triggers: {
                        clear: {
                            type: 'clear',
                            weight: -1
                        }
                    }
                }]
        });

        return fields;
    }
});