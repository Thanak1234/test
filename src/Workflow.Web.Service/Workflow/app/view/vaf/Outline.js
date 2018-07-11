Ext.define("Workflow.view.vaf.Outline", {
    extend: "Ext.grid.Panel",
    xtype: 'vaoutline',
    title: 'Outline Of Variance',
    controller: 'vaoutline',
    viewModel: {
        type: 'vaoutline'
    },
    border: true,
    collapsible: true,
    scrollable: true,
    iconCls: 'fa fa-cogs',
    bind: {
        selection: '{selectedRow}'
    },
    initComponent: function () {
        var me = this;        
        me.buildItems();
        me.callParent(arguments);
    },
    buildItems: function () {
        var me = this;
        me.tbar = ['->', {
            xtype: 'button',
            text: 'Add',
            iconCls: 'fa fa-plus-circle',
            handler: 'addOutline',
            bind: {
                disabled: '{!crud}'
            }
        }, {
            xtype: 'button',
            text: 'Edit',
            iconCls: 'fa fa-plus-circlefa fa-pencil-square-o',
            handler: 'editOutline',
            bind: {
                disabled: '{!(selectedRow && crud)}'
            }
        }, {
            xtype: 'button',
            text: 'View',
            iconCls: 'fa fa-eye',
            handler: 'viewOutline',
            bind: {
                disabled: '{!selectedRow}'
            }
        }];

        me.dockedItems = [{
            xtype: 'toolbar',
            dock: 'bottom',
            items: [
                '->', {
                    xtype: 'label',
                    margin: '0 60 0 0',
                    bind: {
                        text: 'TOTAL = {totalAmount}'
                    }
                }
            ]
        }];

        me.columns = [
            {
                text: 'Gaming Date',
                sortable: false,
                flex: 1,
                menuDisabled: true,
                dataIndex: 'GamingDate',
                renderer: Ext.util.Format.dateRenderer('d/m/Y')
            },
            {
                text: 'Area',
                sortable: false,
                flex: 1,
                menuDisabled: true,
                dataIndex: 'Area'
            },
            {
                text: 'MCID/Locn',
                sortable: false,
                flex: 1,
                menuDisabled: true,
                dataIndex: 'McidLocn'
            }, {
                text: 'Variance Type',
                sortable: false,
                menuDisabled: true,
                flex: 1,
                dataIndex: 'VarianceType',
                align: 'right'
            },
            {
                xtype: 'numbercolumn',
                text: 'Amount(USD)',
                sortable: false,
                flex: 1,
                menuDisabled: true,
                dataIndex: 'Amount',
                format: '$0,000.00',
                align: 'right'
            },
            {
                text: 'Subject',
                sortable: false,
                flex: 1,
                menuDisabled: true,
                dataIndex: 'Subject'
            },
            {
                text: 'Report Comparison',
                sortable: false,
                menuDisabled: true,
                flex: 1,
                dataIndex: 'RptComparison'
            },
            {
                text: 'Incident Report Ref',
                sortable: false,
                menuDisabled: true,
                flex: 1,
                dataIndex: 'IncidentRptRef',
                renderer: function (value, metaData, record) {
                    return value ? Ext.String.format('<a href="#icd-request-form/SN={0}_99999">{1}</a>', record.get('ProcessInstanceId'), value) : null;
                }
            },
            {
                text: 'Comment',
                sortable: false,
                menuDisabled: true,
                flex: 1,
                dataIndex: 'Comment'
            },
            {

                menuDisabled: true,
                sortable: false,
                width: 50,
                xtype: 'actioncolumn',
                align: 'center',
                bind: {
                    hidden: '{!crud}'
                },
                items: [{
                    iconCls: 'fa fa-trash-o',
                    tooltip: 'Remove',
                    width: 150,
                    handler: 'removeRecord'
                }]
            }
        ];
    },
    refreshData: function () {
        var totalAmount = 0;
        var viewmodel = this.getViewModel();
        var store = this.getStore();
        store.each(function (record) {
            var amount = record.get('Amount');
            totalAmount += amount;
        });
        viewmodel.set('totalAmount', Ext.util.Format.number(totalAmount, '$0,000.00'));
    }
});
