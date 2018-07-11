Ext.define('Workflow.view.bcj.BcjRequestFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.bcj-bcjrequestform',
    actionUrl: Workflow.global.Config.baseUrl + 'api/bcjrequest',
    renderSubForm: function (data, acl) {
        var me = this,
            view = me.getView(),
            refs = view.getReferences(),
            projectDetailView = refs.projectDetailView,
            purchaseOrderItemView = refs.purchaseOrderItemView,
            requestItemView = projectDetailView.getReferences().requestItemView,
            analysisItemView = projectDetailView.getReferences().analysisItemView,
            viewSetting = view.getWorkflowFormConfig(acl);

        projectDetailView.fireEvent('loadData', { projectDetail: data && data.projectDetail ? data.projectDetail : null, viewSetting: viewSetting });
        requestItemView.fireEvent('loadData', { data: data && data.requestItems ? data.requestItems : null, viewSetting: viewSetting });
        analysisItemView.fireEvent('loadData', { data: data && data.analysisItems ? data.analysisItems : null, viewSetting: viewSetting });
        purchaseOrderItemView.loadData(
				data?data.purchaseOrderItems:null, 
				data?data.projectDetail:null, viewSetting);
    },
    validateForm: function (data) {
        var me = this,
            view = me.getView(),
            refs = view.getReferences(),
            projectDetailView = refs.projectDetailView,
            purchaseOrderItemView = refs.purchaseOrderItemView,
            projectDetailModel = projectDetailView.getViewModel(),
            requestItemView = projectDetailView.getReferences().requestItemView;

        var completion = projectDetailModel.get('projectDetail.completion'),
            commencement = projectDetailModel.get('projectDetail.commencement');

        if (completion < commencement) {
            return "Completion date must be after Commencement date.";
        }

        if (!projectDetailView.form.isValid()) {
            return "Some field(s) of BCJ is required. Please input the required field(s) before you click the Submit button.";
        }

        if (requestItemView.getStore().getCount() == 0) {
            return "There is no request user item in this request. Please add at least one item before you click the Submit button.";
        }

        if (data && (data.activity == 'Purchasing' || data.activity == 'Modification') && purchaseOrderItemView.getStore().getCount() == 0) {
            return "There is no P.O item, please add at least one item before you click the Submit button.";
        }
    },
    clearData: function () {
        var me = this,
            view = me.getView(),
            refs = view.getReferences(),
            projectDetailView = refs.projectDetailView,
            purchaseOrderItemView = refs.purchaseOrderItemView,
            requestItemView = projectDetailView.getReferences().requestItemView,
            analysisItemView = projectDetailView.getReferences().analysisItemView;

        var requestItemViewModel = requestItemView.getViewModel(),
            analysisItemViewModel = analysisItemView.getViewModel();

        projectDetailView.getForm().reset();
        requestItemView.fireEvent('onDataClear');
        analysisItemView.fireEvent('onDataClear');
        purchaseOrderItemView.onDataClear();

        requestItemViewModel.set('totalAmount', Ext.util.Format.number(0, '$0,000.00'));
        analysisItemViewModel.set('totalAmount', Ext.util.Format.number(0, '$0,000.00'));
    },
    getRequestItem : function(){
        var me = this,
            view = me.getView(),
            refs = view.getReferences(),
            projectDetailView = refs.projectDetailView,
            purchaseOrderItemView = refs.purchaseOrderItemView,
            requestItemView = projectDetailView.getReferences().requestItemView,
            analysisItemView = projectDetailView.getReferences().analysisItemView,
            projectDetailModel = projectDetailView.getViewModel(),
            purchaseOrderItemStore = purchaseOrderItemView.getStore();
            requestItemStore = requestItemView.getStore(),
            analysisItemStore = analysisItemView.getStore();

        var data = {
            // Project Detail
            projectDetail: projectDetailModel.getData().projectDetail,
            // Request Item
            addRequestItems: me.getOriginDataFromCollection(requestItemStore.getNewRecords()),
            editRequestItems: me.getOriginDataFromCollection(requestItemStore.getUpdatedRecords()),
            delRequestItems: me.getOriginDataFromCollection(requestItemStore.getRemovedRecords()),
            // Analysis Item
            addAnalysisItems: me.getOriginDataFromCollection(analysisItemStore.getNewRecords()),
            editAnalysisItems: me.getOriginDataFromCollection(analysisItemStore.getUpdatedRecords()),
            delAnalysisItems: me.getOriginDataFromCollection(analysisItemStore.getRemovedRecords()),
            // Purchase Order Item
            addPurchaseOrderItems: me.getOriginDataFromCollection(purchaseOrderItemStore.getNewRecords()),
            editPurchaseOrderItems: me.getOriginDataFromCollection(purchaseOrderItemStore.getUpdatedRecords()),
            delPurchaseOrderItems: me.getOriginDataFromCollection(purchaseOrderItemStore.getRemovedRecords())
        }

        return data;
    }
    
});
