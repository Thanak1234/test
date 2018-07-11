Ext.define('Workflow.view.vr.VRModel', {
    extend: 'Workflow.view.common.GenericModel',
    alias: 'viewmodel.vr',
    data: {
        Information: {
            VoucherType: null,
            QtyRequest: 0,
            DateRequired: null,
            VoucherNo: null,
            AvailableStock: 0,
            MonthlyUtilsation: 0,
            IsReprint: 0,
            HeaderOnVoucher: null,
            Detail: null,
            Justification: null,
            ValidFrom: null,
            ValidTo: null,
            Validity: null,
            Discount: 0,
            TC1ChangesByRequestor: null,
            TC1ChangesByFinance: null,
            TC2ChangesByRequestor: null,
            TC2ChangesByFinance: null,
            TC3ChangesByRequestor: null,
            TC3ChangesByFinance: null,
            TC4ChangesByRequestor: null,
            TC4ChangesByFinance: null,
            TC5ChangesByRequestor: null,
            TC5ChangesByFinance: null,
            TC6ChangesByRequestor: null,
            TC6ChangesByFinance: null,
            TC7ChangesByRequestor: null,
            TC7ChangesByFinance: null,
            TC8ChangesByRequestor: null,
            TC8ChangesByFinance: null,
            DoneByCreative: true,
            DoneByOutsideVendor: false
        },
        JobDoneBy: {
            DoneByCreative: true
        },
        viewSetting: null
    },
    stores: {
        TCStore: {
            fields: [
               'TC',
               'Finance',
               'Requestor'
            ],
            data: [
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
                }
            ]
        }
    },
    formulas: {
        readOnly: function (get) {
            if (get('viewSetting') && get('viewSetting').container.readOnly) {
                return true;
            } else {
                return false;
            }
        },
        hideCreative: function (get) {
            if (get('viewSetting') && get('viewSetting').container.hideCreative) {
                return true;
            } else {
                return false;
            }
        },
        readOnlyCreative: function (get) {
            if (get('viewSetting') && get('viewSetting').container.readOnlyCreative) {
                return true;
            } else {
                return false;
            }
        }
    }
});
