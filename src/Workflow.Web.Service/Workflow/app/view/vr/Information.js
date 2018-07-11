Ext.define("Workflow.view.vr.Information", {
    extend: "Ext.form.Panel",
    xtype: 'vr-information',
    border: true,
    layout: 'column',
    margin: '0 0 10 0',
    defaults: {
        xtype: 'textfield',
        width: '100%',
        margin: '5 10',
        columnWidth: .5,
        labelWidth: 200
    },
    width: '100%',
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'Request Information',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'combo',
                editable: false,
                forceSelect: true,
                allowBlank: false,
                fieldLabel: 'Voucher Type',
                query: 'local',
                store: ['Complimentary', 'Discount'],
                bind: {
                    readOnly: '{readOnly}',
                    value: '{Information.VoucherType}'
                }
            },            
            {
                xtype: 'currencyfield',
                currencySign: null,
                allowExponential: false,
                allowDecimals: false,
                decimalPrecision: null,
                fieldLabel: 'Quantity Requested',
                allowBlank: false,
                minValue: 1,
				minText: 'Please go to Hotel Gift Voucher Request Form (quantity <= 50 ).',
                bind: {
                    readOnly: '{readOnly || (Information.VoucherNo != null)}',
                    value: '{Information.QtyRequest}',
                    minValue: '{Information.QtyRequestMinValue}'
                }
            },
            {
                xtype: 'radiogroup',
                cls: 'x-check-group-alt',
                name: 'isHotel',
                columnWidth: 1,
                allowBlank: false,
                padding: '0 200',
                bind: {
                    value: '{hotelVoucher}',
                    readOnly: '{readOnly}'
                },
                listeners: {
                    change: function(ck, value){
						var vm = me.parent.getViewModel();
						if(value.isHotel){
							vm.set('Information.QtyRequestMinValue', 51);
						}else{
							vm.set('Information.QtyRequestMinValue', 1);
						}
                    }
                },
                //
                items: [
                    { boxLabel: 'Hotel Voucher', inputValue: true },
                    { boxLabel: 'Gaming Voucher', inputValue: false }
                ]
            },
            {
                xtype: 'datefield',
                altFormats: 'c',
                format: 'd/m/Y',
                fieldLabel: 'Date Required',
                bind: {
                    readOnly: '{readOnly}',
                    value: '{Information.DateRequired}'
                }
            },
            {
                fieldLabel: 'Voucher Number',
                readOnly: true,
                bind: {
                    value: '{Information.VoucherNo}'
                }
            },
            {
                xtype: 'checkbox',
                fieldLabel: 'Is this a Reprint?',
                columnWidth: 1,
                listeners: {
                    change: function (el, v, o) {
                        var viewmodel = me.parent.getViewModel();
                        if (v == false) {
                            viewmodel.set('editable', false);
                            viewmodel.set('Information.AvailableStock', 0);
                            viewmodel.set('Information.MonthlyUtilsation', 0);
                        } else {
                            viewmodel.set('editable', true);
                        }
                    }
                },
                bind: {
                    readOnly: '{readOnly}',
                    value: '{Information.IsReprint}'
                }
            },
            {
                xtype: 'currencyfield',
                currencySign: null,
                allowExponential: false,
                allowDecimals: false,
                decimalPrecision: null,
                fieldLabel: 'Available Stock',
                allowBlank: false,
                minValue: 0,
                disabled: true,
                bind: {
                    disabled: '{!editable}',
                    readOnly: '{readOnly}',
                    value: '{Information.AvailableStock}'
                }
            },             
            {
                xtype: 'currencyfield',
                currencySign: null,
                allowExponential: false,
                allowDecimals: false,
                decimalPrecision: null,
                fieldLabel: 'Monthly Utilisation',
                allowBlank: false,
                minValue: 0,
                disabled: true,
                bind: {
                    readOnly: '{readOnly}',
                    disabled: '{!editable}',
                    value: '{Information.MonthlyUtilsation}'
                }
            },           
            {
                fieldLabel: 'Header on Voucher',
                allowBlank: false,
                columnWidth: 1,
                bind: {
                    readOnly: '{readOnly}',
                    value: '{Information.HeaderOnVoucher}'
                }
            },
            {
                fieldLabel: 'Detailed Description',
                allowBlank: false,
                columnWidth: 1,
                bind: {
                    readOnly: '{readOnly}',
                    value: '{Information.Detail}'
                }
            },
            {
                fieldLabel: 'Justification For Request',
                allowBlank: false,
                columnWidth: 1,
                bind: {
                    readOnly: '{readOnly}',
                    value: '{Information.Justification}'
                }
            },
            {
                xtype: 'datefield',
                fieldLabel: 'Valid From',
                altFormats: 'c',
                format: 'd/m/Y',
                allowBlank: false,
                bind: {
                    readOnly: '{readOnly}',
                    value: '{Information.ValidFrom}'
                }
            },
            {
                xtype: 'datefield',
                fieldLabel: 'Valid To',
                altFormats: 'c',
                format: 'd/m/Y',
                allowBlank: false,
                bind: {
                    readOnly: '{readOnly}',
                    value: '{Information.ValidTo}'
                }
            },
            {
                fieldLabel: 'Validity Description',
                allowBlank: false,
                columnWidth: 1,
                bind: {
                    readOnly: '{readOnly}',
                    value: '{Information.Validity}'
                }
            },
            {
                xtype: 'percentfield',
                fieldLabel: 'If Discount Voucher % of Discount',
                columnWidth: 1,
                minValue: 0,
                maxValue: 99,
                allowExponential: false,
                allowDecimals: false,
                allowBlank: false,
                bind: {
                    readOnly: '{readOnly}',
                    value: '{Information.Discount}'
                }
            },
            {
                xtype: 'grid',
                reference: 'tcGridPanel',
                columnWidth: 1,
                columnLines: true,
                multiColumnSort: false,
                border: true,
                scrollable: false,
                bind: {
                    store: '{TCStore}'
                },
                selModel: {
                    type: 'cellmodel'
                },
                plugins: {
                    ptype: 'cellediting',
                    clicksToEdit: 1,
                    listeners: {
                        beforeedit: function (editor, context, eOpts) {
                            var field = context.field;
                            var activity = context.grid.activity;
                            if (
                                ((activity == 'Submission' || activity == 'Requestor Rework') && field == 'Requestor')
                                || (activity == 'Finance Review' && field == 'Finance')) {
                                context.cancel = false;
                            } else {
                                context.cancel = true;
                            }
                        }
                    }
                },
                columns: [
                    {
                        width: 30,
                        align: 'center',
                        hidden: false,
                        xtype: 'rownumberer',
                        sortable: false,
                        menuDisabled: true,
                        renderer: function (value, metaData, record, rowIdx, colIdx, store) {
                            if (value) {
                                metaData.tdAttr = 'data-qtip="' + value + '"';
                            }
                            return value;
                        }
                    },
                    {
                        text: 'Terms & Conditions on Voucher',
                        dataIndex: 'TC',
                        cellWrap: true,
                        width: 400,
                        sortable: false,
                        menuDisabled: true,
                        renderer: function (value, metaData, record, rowIdx, colIdx, store) {
                            if (value) {
                                metaData.tdAttr = 'data-qtip="' + value + '"';
                            }
                            return value;
                        }
                    },
                    {
                        text: 'Changes by Requestor',
                        name: 'requestor',
                        dataIndex: 'Requestor',
                        width: 265,
                        sortable: false,
                        menuDisabled: true,
                        editor: {
                            xtype: 'textarea',
                            allowBlank: true
                        },
                        renderer: function (value, metaData, record, rowIdx, colIdx, store) {
                            if (value) {
                                metaData.tdAttr = 'data-qtip="' + value + '"';
                            }
                            return value;
                        }
                    },
                    {
                        text: 'Changes by Finance',
                        name: 'finance',
                        dataIndex: 'Finance',
                        sortable: false,
                        menuDisabled: true,
                        width: 265,
                        editor: {
                            allowBlank: true,
                            xtype: 'textarea'
                        },
                        renderer: function (value, metaData, record, rowIdx, colIdx, store) {
                            if (value) {
                                metaData.tdAttr = 'data-qtip="' + value + '"';
                            }
                            return value;
                        }
                    }
                ]
            }
        ];

        me.callParent(arguments);
    }
});
