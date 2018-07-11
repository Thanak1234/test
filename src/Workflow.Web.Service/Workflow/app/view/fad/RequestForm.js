Ext.define("Workflow.view.fad.RequestForm", {
    extend: "Workflow.view.ApplicationComponent",
    xtype: 'fad-request-form',
    title: 'Fixed Asset Disposal Form',
    formType: 'FAD_REQ',
	header: false,
    actionUrl: Workflow.global.Config.baseUrl + 'api/fadrequest',
    bindPayloadData: function (reference) {
        var me = this,
            assetDisposal = reference.assetDisposal,
            assetDisposalDetail = reference.assetDisposalDetail,
            assetControlDetail = reference.assetControlDetail;

        var assetDisposalData = assetDisposal.getViewModel().getData().assetDisposal,
            assetDisposalDetailstore = assetDisposalDetail.getStore(),
            assetControlDetailStore = assetControlDetail.getStore();

        return {
            assetDisposal: assetDisposalData, //Ext.Object.merge(obj1, obj2),
            // asset disposal details
            addAssetDisposalDetails: me.getOriginDataFromCollection(assetDisposalDetailstore.getNewRecords()),
            editAssetDisposalDetails: me.getOriginDataFromCollection(assetDisposalDetailstore.getUpdatedRecords()),
            delAssetDisposalDetails: me.getOriginDataFromCollection(assetDisposalDetailstore.getRemovedRecords()),
            // asset control details
            addAssetControlDetails: me.getOriginDataFromCollection(assetControlDetailStore.getNewRecords()),
            editAssetControlDetails: me.getOriginDataFromCollection(assetControlDetailStore.getUpdatedRecords()),
            delAssetControlDetails: me.getOriginDataFromCollection(assetControlDetailStore.getRemovedRecords())
        };
    },
    loadDataToView: function (reference, data, acl) {
        var me = this,
            viewmodel = me.getViewModel(),
            viewSetting = me.currentActivityProperty;
        
        this.clearData(reference);
        this.fireEventLoad(reference.assetDisposal, data);
        this.fireEventLoad(reference.assetDisposalDetail, data);
        this.fireEventLoad(reference.assetControlDetail, data);
    },
    clearData: function (reference) {
        reference.assetDisposal.getViewModel().set('assetDisposal', {});
        reference.assetDisposal.reset();
        reference.assetDisposalDetail.fireEvent('onDataClear');
        reference.assetControlDetail.fireEvent('onDataClear');
        
    },
    validateForm: function(reference, data){
        var me = this,
            assetDisposal = reference.assetDisposal,
            assetDisposalDetail = reference.assetDisposalDetail,
            assetControlDetail = reference.assetControlDetail;

        if (data) {
            if (!(assetDisposal.isValid())) {
                return "Some fields of form request are required. Please input the required field(s) before you click the take action.";
            }
            if (!assetDisposalDetail.isHidden() && !assetDisposalDetail.getStore().getAt(0)) {
                return "Please add item to Asset Disposal Detail list before you take action.";
            }

            var store = assetControlDetail.getStore();
            var valid = true;
            store.each(function (record) {
                var netBookValue = record.get('netBookValue');
                if (!netBookValue || netBookValue == 0) {
                    valid = false;
                }
            })

            if (!valid && !assetControlDetail.isHidden()) {
                return "Please update item to Asset Control Detail list before you take action.";
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
            title: 'Originator',
            bodyPadding: 0,
            margin: 5,
            border: true,
            items: [{
                margin: 5,
                xtype: 'fad-disposal-view',
                reference: 'assetDisposal',
                mainView: me,
                bind: {
                    hidden: '{assetDisposalProperty.hidden}'
                }
            }, {
                margin: 5,
                border: true,
                xtype: 'fad-disposal-detail-view',
                reference: 'assetDisposalDetail',
                minHeight: 120,
                mainView: me,
                bind: {
                    hidden: '{assetDisposalProperty.assetDisposalDetail.hidden}'
                }
            }]
        }, {
            margin: 5,
            border: true,
            title: 'Asset Control',
            xtype: 'fad-control-detail-view',
            reference: 'assetControlDetail',
            minHeight: 120,
            mainView: me,
            bind: {
                hidden: '{assetDisposalProperty.assetControlView.hidden}'
            }
        }];
    }
});
