Ext.define("Workflow.view.reports.itirf.ITIRFReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-itirfCriteria',
    title: 'IT Item Repair - Criteria',
    viewModel: {
        type: "report-itirf"
    },
    buildFields: function (fields) {
        var me = this;
        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Vendor',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [{
                xtype: 'textfield',
                emptyText: 'NAME <ALL>',
                maxLength: 100,
                bind: {
                    value: '{criteria.VendorName}'
                }
            }, {
                xtype: 'checkbox',
                margin: '0 10',
                boxLabel: 'Returned (Yes/No)',
                inputValue: true,
                checked: true,
                listeners: {
                    change: function(cb, val){
                        console.log(me.getViewModel(), val);
                        me.getViewModel().set('isReturn', val) ;
                    }
                },
                bind: {
                    value: '{criteria.IsReturn}'
                }
            }]
        }, {
            xtype: 'fieldcontainer',
            fieldLabel: 'Send Date',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [{
                xtype: 'datefield',
                emptyText: 'FROM <ALL>',
                bind: {
                    value: '{criteria.SendDateFrom}'
                }
            }, {
                xtype: 'datefield',
                margin: '0 5',
                emptyText: 'TO <ALL>',
                bind: {
                    value: '{criteria.SendDateTo}'
                }
            }]
        }, {
            xtype: 'fieldcontainer',
            fieldLabel: 'Return Date',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            bind: {
                disabled: '{!isReturn}'
            },
            items: [{
                xtype: 'datefield',
                emptyText: 'FROM <ALL>',
                bind: {
                    value: '{criteria.ReturnDateFrom}'
                }
            }, {
                xtype: 'datefield',
                margin: '0 5',
                emptyText: 'TO <ALL>',
                bind: {
                    value: '{criteria.ReturnDateTo}'
                }
            }]
        });
        return fields;
    }
});