/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.hgvr.VoucherHotelDetailView", {
    extend: "Workflow.view.GridComponent",
    xtype: 'hgvr-voucher-detail', 
    modelName: 'voucherHotelDetail',
    collectionName: 'voucherHotelDetails',
    actionListeners: {
        add: function (grid, store, record) {
            var newRecord = store.createModel(record);
            store.add(newRecord);
        }
    },
    buildGridComponent: function (component) {
        var me = this;
        
        return [{
            header: 'Quantity Requested',
            width: 180,
            sortable: true,
            dataIndex: 'quantityRequest'
        }, {
            header: 'Entitles Bearer To',
            width: 180,
            sortable: true,
            dataIndex: 'entitiesBearerTo'
        }, {
            xtype: 'datecolumn',
            header: 'Valid From',
            width: 120,
            format: 'm/d/Y',
            dataIndex: 'validDateFrom'
        }, {
            xtype: 'datecolumn',
            header: 'Valid To',
            width: 120,
            format: 'm/d/Y',
            dataIndex: 'validDateTo'
        }, {
            xtype: 'numbercolumn',
            header: 'Total Cash Collected (if paid voucher)',
            format: '$0,000.00',
            width: 250,
            dataIndex: 'totalCashCollected'
        }, {
            xtype: 'datecolumn',
            header: 'Date Required (5 working days needed)',
            width: 250,
            format: 'm/d/Y',
            dataIndex: 'dateRequired'
        }];
    },
    buildWindowComponent: function (component) {
        component.width = 520;
        component.height = 410;
        component.labelWidth = 130;
        component.title = 'Voucher List';

        return [{
            xtype: 'numberfield',
            fieldLabel: 'Quntity Requested',
            allowBlank: false,
            minValue: 1,
            maxValue: 50,
            maxText: 'Please go to Complimentary/Discount Voucher Form (quantity > 50).',
            bind: {
                value: '{voucherHotelDetail.quantityRequest}'
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Entitles Bearer To',
            allowBlank: false,
            bind: {
                value: '{voucherHotelDetail.entitiesBearerTo}'
            }
        }, {
            xtype: 'datefield',
            fieldLabel: 'Valid From Date',
            format: 'm/d/Y',
            name: 'startdt',
            itemId: 'startdt',
            vtype: 'daterange',
            endDateField: 'enddt', // id of the end date field
            allowBlank: false,
            bind: { 
                value: '{voucherHotelDetail.validDateFrom}'
            },
            listeners: {
                change: function(field, value){
                    var vm = component.getViewModel();
                    console.log('vm', vm.getData());
                    if(value){
                        vm.set('voucherHotelDetail.maxDateRequired', Ext.Date.subtract(value, Ext.Date.DAY, 1));
                    }
                }
            }
        }, {
            xtype: 'datefield',
            fieldLabel: 'Valid To Date',
            format: 'm/d/Y',
            name: 'enddt',
            itemId: 'enddt',
            vtype: 'daterange',
            startDateField: 'startdt', // id of the start date field
            allowBlank: false,
            bind: { 
                value: '{voucherHotelDetail.validDateTo}'
            }
        }, {
            xtype: 'currencyfield',
            fieldLabel: 'Total Cash Collected',
            format: '$0,000.00',
            allowBlank: false,
            minValue: 0,
            bind: { 
                value: '{voucherHotelDetail.totalCashCollected}',
                emptyText: '(if paid voucher)'
            }
        }, {
            xtype: 'datefield',
            fieldLabel: 'Date Required',
            format: 'm/d/Y',
            allowBlank: false,
            bind: { 
                value: '{voucherHotelDetail.dateRequired}',
                emptyText: '(5 working days needed)',
                maxValue: '{voucherHotelDetail.maxDateRequired}'
            }
        }];
    }
});