Ext.define("Workflow.view.vr.VRForm", {
    extend: "Workflow.view.common.GenericForm",
    title: 'Complimentary Vouchers & Discount Vouchers',
    viewModel: {
        type: 'vr'
    },
    formType: 'VR_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/vrrequest',
    buildComponents: function () {
        var me = this;
        return [
            {
                xtype: 'vr-information',
                reference: 'information',
                parent: me,
                margin: 5
            },
            {
                xtype: 'vr-creative',
                reference: 'creative',
                margin: 5,
                bind: {
                    hidden: '{hideCreative}'
                }
            }            
        ];
    },
    excludeProps: ['tcGridPanel'],
    loadData: function (viewmodel) {
        var me = this;
        var refs = this.getReferences();
        var store = viewmodel.get('TCStore');
        if (store) {
            for (var i = 0; i < 8; i++) {
                me.setValToStore(store, viewmodel, i);
            }
        };
        var donebycreative = viewmodel.get('Information.DoneByCreative');        
        var isDone = donebycreative ? true : donebycreative;
        viewmodel.set('JobDoneBy.DoneByCreative', isDone);
        viewmodel.set('JobDoneBy.DoneByOutsideVendor', !isDone);

        var isHotel = viewmodel.get('Information.IsHotelVoucher');
        if (isHotel != null) {
            viewmodel.set('hotelVoucher', { isHotel: isHotel });
        }
        setTimeout(function () {
            var grid = refs.tcGridPanel;
            grid.activity = viewmodel._data.activity;
        }, 1000);        
    },
    transformData: function (viewmodel) {
        var me = this;
        var refs = this.getReferences();
        var store = refs.tcGridPanel.getStore();
        if (store) {
            for (var i = 0; i < 8; i++) {
                me.setValToModel(store, viewmodel, i);
            }
        }
        var hotelVoucher = viewmodel.get('hotelVoucher');
        if (hotelVoucher) {
            var isHotel = hotelVoucher.isHotel;
            viewmodel.set('Information.IsHotelVoucher', isHotel)
            viewmodel.set('Information.IsGamingVoucher', !isHotel);
        }
        var donebycreative = viewmodel.get('JobDoneBy.DoneByCreative');
        if (donebycreative != null) {
            viewmodel.set('Information.DoneByCreative', donebycreative)
            viewmodel.set('Information.DoneByOutsideVendor', !donebycreative);
        }
    },
    validate: function (data) {
        var actName = data.activity;
        var refs = this.getReferences();
        var information = refs.information;
        var info = data.dataItem.Information;

        if (!information.form.isValid()) {
            return 'Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.';
        }
        if (info.ValidTo < info.ValidFrom) {
            return 'The ValidTo must be greater than ValidFrom.';
        }
    },
    buildConfigs: function (curAct, lastAct, config) {
        var container = config.container;
        if (curAct == "Form View" && (lastAct == 'HOD Approval' || lastAct == 'Finance Approval' || lastAct == 'Modification' || lastAct == 'Creative Review')) {
            container.hideCreative = false;
        }
        return config;
    },
    clearData: function () {
        var refs = this.getReferences();
        var viewmodel = this.getViewModel();
        refs.container.reset();
        var store = refs.tcGridPanel.getStore();
        store.setData(this.getTermCondition());
    },
    setValToModel: function (store, viewmodel, index) {
        viewmodel.set(Ext.String.format('Information.TC{0}ChangesByRequestor', index + 1), store.getAt(index).data.Requestor);
        viewmodel.set(Ext.String.format('Information.TC{0}ChangesByFinance', index + 1), store.getAt(index).data.Finance);
    },
    setValToStore: function (store, viewmodel, index) {
        store.getAt(index).data.Requestor = viewmodel.get(Ext.String.format('Information.TC{0}ChangesByRequestor', index + 1));
        store.getAt(index).data.Finance = viewmodel.get(Ext.String.format('Information.TC{0}ChangesByFinance', index + 1));
    },
    getTermCondition: function () {
        var data = [
            {
            TC: 'Vouchers are non-transferable, refundable in cash or replaceable if lost, destroyed, stolen or expired.  Vouchers are void if altered, photocopied or reproduced.<br /><br/><br />',
            Finance: null,
            Requestor: null
            },
            {
                TC: 'Vouchers can only be used for a single transaction; any remaining amount is not exchangeable for cash or another voucher and will be automatically for feited.<br /><br/><br />',
                Finance: null,
                Requestor: null
            },
            {
                TC: 'Vouchers cannot be used in conjunction with any special promotions or discounts or during block out dates.<br /><br/><br />',
                Finance: null,
                Requestor: null
            },
            {
                TC: 'Reservations are subject to availability, at our discretion.<br/><br/><br /><br/><br />',
                Finance: null,
                Requestor: null
            },
            {
                TC: 'The voucher expiry date cannot be extended.<br/><br/><br /><br/><br />',
                Finance: null,
                Requestor: null
            },
            {
                TC: 'The company reserves the right to amend these terms and conditions without notice.<br/><br/><br /><br/>',
                Finance: null,
                Requestor: null
            },
            {
                TC: 'Other terms and conditions apply.<br/><br/><br /><br/><br />',
                Finance: null,
                Requestor: null
            },
            {
                TC: 'Additional Terms & Conditions Requested.<br/><br/><br /><br/><br/>',
                Finance: null,
                Requestor: null
            }];
        return data;
    }
});
