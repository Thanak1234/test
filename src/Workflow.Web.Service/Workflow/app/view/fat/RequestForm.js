Ext.define("Workflow.view.fat.RequestForm", {
    extend: "Workflow.view.ApplicationComponent",
    xtype: 'fat-request-form',
    title: 'Fixed Asset Disposal Form',
    formType: 'FAT_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/fatrequest',
    bindPayloadData: function (reference) {
        var me = this,
            assetTransfer = reference.assetTransfer,
            assetTransferBranch = reference.assetTransferBranch,
            assetTransferDetail = reference.assetTransferDetail;

        var assetTransferData = assetTransfer.getViewModel().getData().assetTransfer,
            store = assetTransferDetail.getStore();

        //var assetTransferBranchData = assetTransferBranch.getViewModel().getData().assetTransfer;
        //if (assetTransferBranchData) {
        //    console.log('companyBranch', assetTransferBranchData.companyBranch);
        //    assetTransfer.companyBranch = assetTransferBranchData.companyBranch;
        //}
        
        var data = {
            assetTransfer: assetTransferData,
            addAssetTransferDetails: me.getOriginDataFromCollection(store.getNewRecords()),
            editAssetTransferDetails: me.getOriginDataFromCollection(store.getUpdatedRecords()),
            delAssetTransferDetails: me.getOriginDataFromCollection(store.getRemovedRecords())
        }

        return data;
    },
    loadDataToView: function (reference, data, acl) {
        var me = this,
            viewmodel = me.getViewModel();

        var viewSetting = me.currentActivityProperty;

        this.fireEventLoad(reference.assetTransfer, data);
        this.fireEventLoad(reference.assetTransferDetail, data);
        this.fireEventLoad(reference.assetTransferBranch, data);
        
    },
    clearData: function (reference) {
        reference.assetTransfer.getForm().reset();
        reference.assetTransferBranch.getForm().reset();
        reference.assetTransferDetail.fireEvent('onDataClear');
    },
    validateForm: function(reference, data){
        var me = this,
            assetTransfer = reference.assetTransfer,
            assetTransferDetail = reference.assetTransferDetail;

        var treatmentData = assetTransfer.getViewModel().getData().assetTransfer,
            assetTransferDetailStore = assetTransferDetail.getStore();
        if (data) {
            if (!assetTransfer.isValid()) {
                return "Some fields of form request are required. Please input the required field(s) before you click the take action.";
            }
            if (!assetTransferDetail.isHidden() && !assetTransferDetailStore.getAt(0)) {
                return "Please add item to Asset Transfer Detail list before you take action.";
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
            title: 'Asset Details',
            bodyPadding: 0,
            margin: 5,
            border: true,
            items: [ {
                margin: '5 5 0 5',
                xtype: 'fat-transfer-view',
                reference: 'assetTransferBranch',
                viewSection: 'BRANCH',// BRANCH/DEPARTMENT
                mainView: me,
                bind: {
                    hidden: '{assetTransferProperty.hidden}'
                }
            }, {
                margin: 5,
                minHeight: 200,
                border: true,
                xtype: 'fat-transfer-detail-view',
                reference: 'assetTransferDetail',
                mainView: me,
                bind: {
                    hidden: '{assetTransferDetailProperty.hidden}'
                }
            }]
        }, {
            margin: 5,
            title: 'Transfer To',
            xtype: 'fat-transfer-view',
            reference: 'assetTransfer',
            viewSection: 'DEPARTMENT',// BRANCH/DEPARTMENT
            mainView: me,
            bind: {
                hidden: '{assetTransferProperty.hidden}'
            },
            border: true
        }];
    }
});
