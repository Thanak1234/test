Ext.define("Workflow.view.reports.vr.VRReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-vrcriteria',
    title: 'Complimentary Vouchers & Discount Vouchers - Criteria',
    viewModel: {
        type: "report-vr"
    },
    buildFields: function (fields) {
        var me = this;

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Information',
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
                    margin: '5 0 5 0',
                    defaults: {
                        flex: 1,
                        hideLabel: true,
                        width: 250,
                        margin: '0 5 0 0',
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
                            xtype: 'combo',
                            fieldLabel: 'Voucher Type',
                            emptyText: 'VOUCHER TYPE',
                            margin: '0 5 0 0',
                            editable: false,
                            store: ['Complimentary', 'Discount'],
                            bind: {
                                value: '{criteria.VoucherType}'
                            }
                        },
                        {
                            xtype: 'numberfield',
                            emptyText: 'QUANTITY REQUESTED',
                            allowExponential: false,
                            allowDecimals: false,
                            decimalPrecision: null,
                            margin: '0 5 0 0',
                            fieldLabel: 'Quantity Requested',
                            bind: {
                                value: '{criteria.QtyRequest}'
                            }
                        },
                        {
                            xtype: 'textfield',
                            fieldLabel: 'Voucher Number',
                            emptyText: 'VOUCHER NUMBER',
                            margin: '0 5 0 0',
                            bind: {
                                value: '{criteria.VoucherNo}'
                            }
                        }
                    ]
                },

                {
                    xtype: 'fieldcontainer',
                    layout: 'hbox',
                    margin: 0,
                    defaults: {
                        flex: 1,
                        hideLabel: true,
                        width: 200,
                        margin: '0 5 0 0'
                    },
                    items: [
                        {
                            xtype: 'checkbox',
                            boxLabel: 'Is this a Reprint?',
                            bind: {
                                value: '{criteria.IsReprint}'
                            }
                        },
                        {
                            xtype: 'checkbox',
                            hideLabel: false,
                            boxLabel: 'Hotel Voucher',
                            bind: {
                                value: '{criteria.IsHotelVoucher}'
                            }
                        },
                        {
                            xtype: 'checkbox',
                            hideLabel: false,
                            boxLabel: 'Gaming Voucher',
                            bind: {
                                value: '{criteria.IsGamingVoucher}'
                            }
                        }                        
                    ]
                },
                {
                    xtype: 'fieldcontainer',
                    layout: 'hbox',
                    margin: 0,
                    defaults: {
                        flex: 1,
                        hideLabel: true,
                        width: 760,
                        margin: '0 5 0 0',
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
                            xtype: 'textfield',
                            margin: '0 5 0 0',
                            emptyText: 'HEADER ON VOUCHER',
                            bind: {
                                value: '{criteria.VoucherHeader}'
                            }
                        }
                    ]
                }
            ]
        });

        return fields;
    }
});