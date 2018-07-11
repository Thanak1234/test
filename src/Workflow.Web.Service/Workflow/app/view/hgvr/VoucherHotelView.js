/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.hgvr.VoucherHotelView", {
    extend: "Workflow.view.FormComponent",
    xtype: 'hgvr-voucher',
    modelName: 'voucherHotel',
    loadData: function (data, viewSetting) {
        var me = this, 
            viewmodel = me.getViewModel(),
            reference = me.getReferences();
        
        if(data){
            viewmodel.set('voucherHotel', data.voucherHotel);
        }
    },
    buildComponent: function () {
        var me = this;

        return [{
            xtype: 'textfield',
            fieldLabel: 'Presented to',
			maxWidth: 420,
            allowBlank: false,
            bind: {
                value: '{voucherHotel.presentedTo}'
            },
            margin: '5 0 10 0'
        }, {
            xtype: 'textfield',
            fieldLabel: 'Department to charge to',
			maxWidth: 420,
            allowBlank: false,
            bind: {
                value: '{voucherHotel.inChargedDept}'
            },
            margin: '5 0 10 0'
        }, {
            xtype: 'textarea',
            fieldLabel: 'Justification',
			
            allowBlank: false,
            bind: {
                value: '{voucherHotel.justification}'
            },
            margin: '5 0 10 0'
        }];
    }
});