Ext.define('Workflow.view.hr.att.RequestFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.att-requestform',
    actionUrl: Workflow.global.Config.baseUrl + 'api/attrequest',

    init: function() {
        var me = this;
        var view = me.getView();
        var vm = me.getViewModel();
        var activity = vm.get('activity');
        if ('Submission'.toLowerCase() === activity.toLowerCase()
            || 'Requestor Rework'.toLowerCase() === activity.toLowerCase()) {
            me.getViewModel().bind('{requestorId}', me.onRequestorChanged, me);
        }
    },
    onRequestorChanged: function () {
        var me = this;
        var view = me.getView();
        var vm = me.getViewModel();
        var formView = view.getReferences().formView;
        var requestorId = vm.get('requestorId');
        formView.getViewModel().set('requestorId', requestorId);
        formView.fireEvent('updateTokenInYear', { requestorId: requestorId });

    },
    renderSubForm: function (data) {
        
        var me = this,
            view = me.getView(),
            formView = view.getReferences().formView,
            nagaTravelView = view.getReferences().nagaTravelView,
            destinationView = formView.getReferences().destinationView,
            flightDetailView = nagaTravelView.getReferences().flightDetailView,
            viewSetting = view.getWorkflowFormConfig();        
        
        formView.fireEvent('loadData', { travelDetail: data && data.travelDetail? data.travelDetail : null, viewSetting: viewSetting });

        destinationView.fireEvent('loadData', { data: data && data.destinations ? data.destinations  : null, viewSetting: viewSetting });
        flightDetailView.fireEvent('loadData', { data: data && data.flightDetails ? data.flightDetails : null, viewSetting: viewSetting });

        //set cost ticket
        nagaTravelView.getViewModel().set('costTicket', (data && data.travelDetail != null ? data.travelDetail.costTicket : 0));
        nagaTravelView.getViewModel().set('extraCharge', (data && data.travelDetail != null ? data.travelDetail.extraCharge : 0));
        nagaTravelView.getViewModel().set('remark', (data && data.travelDetail != null ? data.travelDetail.remark : null));
        
    },
    validateForm: function () {
        var me = this,
           view = me.getView(),
           formView = view.getReferences().formView,
           model = formView.getViewModel(),
            destinationList = formView.getReferences().destinationView.getStore(),
               activity = view.getViewModel().get('activity'),

            nagaTravelView = view.getReferences().nagaTravelView,
            flightDetailView = nagaTravelView.getReferences().flightDetailView,
            flightDetailStore = flightDetailView.getStore();        

        if ('Submission'.toLowerCase() === activity.toLowerCase()
            || 'Requestor Rework'.toLowerCase() === activity.toLowerCase()) {

            var formViewVM = formView.getViewModel();
            var purpose = formView.getReferences().purposeTravel.getValue().purposeTravel;
            var requestorId = me.getViewModel().get('requestorId');
            var token = formView.getTokenInThisYear(requestorId, purpose);
            var currentToken = formViewVM.get('travelDetail.noRequestTaken');
            if (token != currentToken) {
                formViewVM.set('travelDetail.noRequestTaken', token);
                return "Travel Taken was update on Server. Please click submit button again.";                
            }
        }


        if (destinationList.getCount() == 0) {
            return "There is no Flight Detail item in this request. Please add at least one item before you click the button submit.";
        }else if (model.get('travelDetail.estimatedCostTicket')<=0) {
            return 'Estimated Cost of Ticket (US$) is required...';
        }else if (
                    (
                    'NAGA Travel'.toLowerCase() === activity.toLowerCase()
                    || 'Modification'.toLowerCase() === activity.toLowerCase()
                    ) &&

                    model.get('travelDetail.costTicket') <= 0

            ) {
            return 'Cost of Ticket (US$) is required...';
        } else if (
                    (
                    'NAGA Travel'.toLowerCase() === activity.toLowerCase()
                    || 'Modification'.toLowerCase() === activity.toLowerCase()
                    ) &&
                    flightDetailStore.getCount() == 0
            ) {
            return "There is no Flight Detail item in this request. Please add at least one item before you click the button submit.";
        }
        
    },
    getRequestItem : function(){
        var me = this,
            view = me.getView(),
            formView = view.getReferences().formView,            
            formModel = formView.getViewModel(),

            nagaTravelView = view.getReferences().nagaTravelView,
            nagaTravelModel = nagaTravelView.getViewModel(),

            ref = me.getReferences();
        
        destinationView = formView.getReferences().destinationView,
        destinationStore = destinationView.getStore(),

        flightDetailView = nagaTravelView.getReferences().flightDetailView,
        flightDetailStore = flightDetailView.getStore();

        var travelDetail = formModel.getData().travelDetail;

        travelDetail.classTravelEntitlement = ref.formView.getReferences().classTravelEntitlement.getValue().classTravelEntitlement;
        travelDetail.classTravelRequest = ref.formView.getReferences().classTravelRequest.getValue().classTravelRequest;
        travelDetail.purposeTravel = ref.formView.getReferences().purposeTravel.getValue().purposeTravel;

        travelDetail.costTicket = ref.nagaTravelView.getReferences().costTicket.getValue();
        travelDetail.extraCharge = ref.nagaTravelView.getReferences().extraCharge.getValue();
        travelDetail.remark = ref.nagaTravelView.getReferences().remark.getValue();

        
        var data = {
            travelDetail: travelDetail,

            delRequestDestinations: me.getOriginDataFromCollection(destinationStore.getRemovedRecords()),
            editRequestDestinations: me.getOriginDataFromCollection(destinationStore.getUpdatedRecords()),
            addRequestDestinations: me.getOriginDataFromCollection(destinationStore.getNewRecords()),

            delRequestFlightDetails: me.getOriginDataFromCollection(flightDetailStore.getRemovedRecords()),
            editRequestFlightDetails: me.getOriginDataFromCollection(flightDetailStore.getUpdatedRecords()),
            addRequestFlightDetails: me.getOriginDataFromCollection(flightDetailStore.getNewRecords())
        };
        return data;
    },
    clearData: function () {
        var me = this,
            view = me.getView(),
            formView = view.getReferences().formView,
                destinationView = formView.getReferences().destinationView,
            nagaTravelView = view.getReferences().nagaTravelView,
                flightDetailView = nagaTravelView.getReferences().flightDetailView
            ;

        formView.getForm().reset();
        nagaTravelView.getForm().reset();
            destinationView.getStore().removeAll(); 
            flightDetailView.getStore().removeAll();
        
    },
    isTextFieldBlank: function (text) {        
        return text == null || Ext.isEmpty(text);
    }
});
