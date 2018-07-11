Ext.define("Workflow.view.reports.admcpp.AdmCppReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-admcppcriteria',
    title: 'Car Park Permit - Criteria',
    viewModel: {
        type: "report-admcpp"
    },
    buildFields: function (fields) {
        var me = this;

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Verhicle',
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
                    xtype: 'textfield',
                    fieldLabel: 'Model',
                    emptyText: 'MODEL',
                    margin: '0 5 0 0',
                    bind: {
                        value: '{criteria.model}',
                        readOnly: '{!editable}'
                    }
                },
                {
                    xtype: 'textfield',
                    emptyText: 'PLAT NO',
                    margin: '0 5 0 0',
                    bind: {
                        value: '{criteria.platNo}',
                        readOnly: '{!editable}'
                    }
                }]
        });

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Admin Issue',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [
                {
                    xtype: 'textfield',
                    emptyText: 'CAR PARK S/N',
                    bind: {
                        value: '{criteria.cpsn}'
                    },
                    margin: '0 5 0 0',
                    triggers: {
                        clear: {
                            type: 'clear',
                            weight: -1
                        }
                    }
                },
                {
                    xtype: 'datefield',
                    emptyText: 'ISSUE DATE',
                    bind: {
                        value: '{criteria.issueDate}'
                    },
                    margin: '0 5 0 0',
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