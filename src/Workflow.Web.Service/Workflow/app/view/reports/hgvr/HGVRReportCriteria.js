Ext.define("Workflow.view.reports.hgvr.HGVRReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-hgvrcriteria',
    title: 'Hotel Gift Voucher Request - Criteria',
    viewModel: {
        type: "report-hgvr"
    },
    buildFields: function (fields) {
        var me = this;
        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'VOUCHER',
            layout: 'vbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                margin: '0 5 0 0'
            },
            items: [
                {
                    xtype: 'fieldcontainer',
                    layout: 'hbox',
                    margin: '0 0 5 0',
                    defaults: {
                        flex: 1,
                        hideLabel: true,
                        width: 250,
                        margin: '0 5 0 0',
                        xtype: 'textfield',
                        triggers: {
                            clear: {
                                type: 'clear',
                                hideWhenEmpty: true,
                                clearOnEscape: true,
                                weight: -1
                            }
                        }
                    },
                    items: [
                        {
                            xtype: 'numberfield',
                            emptyText: 'In Charge Department <ALL>',
                            minValue: 0.001,
                            margin: '0 5 0 0',
                            bind: {
                                value: '{criteria.inChargedDept}'
                            }
                        },
                        {
                            emptyText: 'Quantity Request <ALL>',
                            margin: '0 5 0 0',
                            bind: {
                                value: '{criteria.quantityRequest}'
                            }
                        }
                    ]
                }
            ]
        });
        return fields;
    }
});