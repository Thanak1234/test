/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.iteirq.QuotationView", {
    extend: "Workflow.view.GridComponent",
    xtype: 'iteirq-quotation-view',
    modelName: 'quotation',
    collectionName: 'quotations',
    title: 'Quotation',
    header: false,
    actionListeners: {
        beforeAdd: function (grid, datamodel) {
            
        },
        add: function (grid, store, record) {
            var newRecord = store.createModel(record);
            store.add(newRecord);
        }
    },
    afterSaveChange: function (grid) {
        // TODO: SOMTHING
    },
    buildGridComponent: function (component) {
        var me = this;
       
        return [{
            header: 'Company Name',
            flex: 1,
            sortable: true,
            dataIndex: 'companyName'
        }, {
            xtype: 'datecolumn',
            header: 'Date Issued',
            width: 150,
            format: 'm/d/Y',
            dataIndex: 'dateIssued'
        }, {
            xtype: 'numbercolumn',
            header: 'Validity (days)',
            width: 150,
            sortable: true,
            dataIndex: 'validity'
        }, {
            xtype: 'numbercolumn',
            header: 'Price',
            format: '$0,000.00',
            width: 200,
            dataIndex: 'price'
        }];
    },
    buildWindowComponent: function (component) {
        component.width = 520;
        component.height = 410;
        component.labelWidth = 160;

        return [{
            xtype: 'textfield',
            fieldLabel: 'Quotation',
            allowBlank: false,
            bind: {
                value: '{quotation.companyName}'
            }
        }, {
            xtype: 'datefield',
            fieldLabel: 'Date Issued',
            allowBlank: false,
            bind: {
                value: '{quotation.dateIssued}'
            },
            listeners: {
                change: function (datepick) {
                    //console.log('datepick', datepick);
                },
                select: function (datepick, record) {
                    //console.log(datepick, record);
                }
            }
        }, {
            xtype: 'numberfield',
            fieldLabel: 'Validity (days)',
            allowBlank: false,
            minValue: 0.001,
            bind: { value: '{quotation.validity}' }
        }, {
            xtype: 'currencyfield',
            fieldLabel: 'Price ($USD)',
            allowBlank: false,
            minValue: 0.00001,
            bind: { value: '{quotation.price}' }
        }];
    }
});