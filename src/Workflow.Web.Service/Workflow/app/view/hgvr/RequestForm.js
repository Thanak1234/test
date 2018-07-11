Ext.define("Workflow.view.hgvr.RequestForm", {
    extend: "Workflow.view.ApplicationComponent",
    xtype: 'hgvr-request-form',
    title: 'Hotel Gift Voucher Request Form',
    formType: 'HGVR_REQ',
	header: false,
    actionUrl: Workflow.global.Config.baseUrl + 'api/hgvrrequest',
    bindPayloadData: function (reference) {
        var me = this,
            voucherHotel = reference.voucherHotel,
            voucherHotelDetail = reference.voucherHotelDetail,
            voucherHotelFinance = reference.voucherHotelFinance;

        var voucherHotelData = voucherHotel.getViewModel().getData().voucherHotel,
            voucherHotelDetailstore = voucherHotelDetail.getStore(),
            voucherHotelFinanceStore = voucherHotelFinance.getStore();

        return {
            voucherHotel: voucherHotelData, //Ext.Object.merge(obj1, obj2),
            // voucher details
            addVoucherHotelDetails: me.getOriginDataFromCollection(voucherHotelDetailstore.getNewRecords()),
            editVoucherHotelDetails: me.getOriginDataFromCollection(voucherHotelDetailstore.getUpdatedRecords()),
            delVoucherHotelDetails: me.getOriginDataFromCollection(voucherHotelDetailstore.getRemovedRecords()),
            // vourcher finance
            addVoucherHotelFinances: me.getOriginDataFromCollection(voucherHotelFinanceStore.getNewRecords()),
            editVoucherHotelFinances: me.getOriginDataFromCollection(voucherHotelFinanceStore.getUpdatedRecords()),
            delVoucherHotelFinances: me.getOriginDataFromCollection(voucherHotelFinanceStore.getRemovedRecords())
        };
    },
    loadDataToView: function (reference, data, acl) {
        var me = this,
            viewmodel = me.getViewModel(),
            viewSetting = me.currentActivityProperty;
        
        this.clearData(reference);
        this.fireEventLoad(reference.voucherHotel, data);
        this.fireEventLoad(reference.voucherHotelDetail, data);
        this.fireEventLoad(reference.voucherHotelFinance, data);
    },
    clearData: function (reference) {
        reference.voucherHotel.getViewModel().set('voucherHotel', {});
        reference.voucherHotel.reset();
        reference.voucherHotelDetail.fireEvent('onDataClear');
        reference.voucherHotelFinance.fireEvent('onDataClear');
        
    },
    validateForm: function(reference, data){
        var me = this,
            voucherHotel = reference.voucherHotel,
            voucherHotelDetail = reference.voucherHotelDetail,
            voucherHotelFinance = reference.voucherHotelFinance;

        if (data) {
            if (!(voucherHotel.isValid())) {
                return "Some fields of form request are required. Please input the required field(s) before you click the take action.";
            }
            if (!voucherHotelDetail.isHidden() && !voucherHotelDetail.getStore().getAt(0)) {
                return "Please add item to voucher list before you take action.";
            }
            if (!voucherHotelFinance.isHidden() && !voucherHotelFinance.getStore().getAt(0)) {
                return "Please add item to finance list before you take action.";
            }
        }
    },
    buildComponent: function () {
        var me = this;

        return [{
            xtype: 'panel',
            layout: {
                type: 'vbox',
                pack: 'start',
                align: 'stretch'
            },
            title: 'Voucher Request List',
            bodyPadding: 0,
            margin: 5,
            border: true,
            items: [{
                margin: 5,
                xtype: 'hgvr-voucher',
                reference: 'voucherHotel',
                mainView: me,
                bind: {
                    hidden: '{voucherHotelProperty.hidden}'
                }
            }, {
                margin: 5,
                border: true,
                xtype: 'hgvr-voucher-detail',
                reference: 'voucherHotelDetail',
                minHeight: 120,
                mainView: me,
                bind: {
                    hidden: '{voucherHotelDetailProperty.hidden}'
                }
            }]
        }, {
            margin: 5,
            border: true,
            title: 'For Finance Use Only',
            xtype: 'hgvr-voucher-finance',
            reference: 'voucherHotelFinance',
            minHeight: 120,
            mainView: me,
            bind: {
                hidden: '{voucherHotelFinanceProperty.hidden}'
            }
        }];
    }
});
