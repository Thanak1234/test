/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.hgvr.VoucherHotelFinanceView", {
    extend: "Workflow.view.GridComponent",
    xtype: 'hgvr-voucher-finance',
    modelName: 'voucherHotelFinance',
    collectionName: 'voucherHotelFinances',
    title: 'For Finance Use Only',
    //header: false,
    actionListeners: {
        add: function (grid, store, record) {
            var newRecord = store.createModel(record);
            store.add(newRecord);
        }
    },
    buildGridComponent: function (component) {
        var me = this;
       
        return [{
            header: 'Voucher No',
            width: 200,
            dataIndex: 'issuedNo'
        }, {
            header: 'Entitles Bearer To',
            width: 180,
            sortable: true,
            dataIndex: 'entitiesBearerTo'
        }, {
            header: 'Department Charged',
            flex: 1,
            dataIndex: 'inChargedDept'
        }];
    },
    buildWindowComponent: function (component) {
        var me = this;
        component.width = 520;
        component.height = 410;
        component.labelWidth = 160;

        
        return [{
            xtype: 'textfield',
            fieldLabel: 'Voucher No',
            allowBlank: false,
            bind: {
                value: '{voucherHotelFinance.issuedNo}'
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Entitles Bearer To',
            allowBlank: false,
            bind: {
                value: '{voucherHotelFinance.entitiesBearerTo}'
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Department Charged',
            allowBlank: false,
            bind: { value: '{voucherHotelFinance.inChargedDept}' }
        }];
    }
});