Ext.define('Workflow.view.reservation.fnfbr.FnFBookingRequestFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.fnfbr-requestform',
    actionUrl: Workflow.global.Config.baseUrl + 'api/fnfrequest',
    renderSubForm: function (data) {
        var me = this,
            view = me.getView(),
            reservationView = view.getReferences().reservationView,
            occupancyView = view.getReferences().occupancyView,
            viewSetting = view.getWorkflowFormConfig();

        reservationView.fireEvent('loadData', { reservation: data && data.reservation ? data.reservation : null, viewSetting: viewSetting });
        occupancyView.fireEvent('loadData', { data: data && data.occupancies ? data.occupancies : null, viewSetting: viewSetting });

    },
    validateForm: function () {
        var me = this,
            view = me.getView(),
            reservationView = view.getReferences().reservationView,
            model = reservationView.getViewModel(),
            currentActivity = view.getViewModel().get("activity");

        if (!model.get('reservation.agree')) {
            return 'You must agree to the Terms and Conditions.';
        }

        if ((currentActivity.toLowerCase() === 'Submission'.toLowerCase() || currentActivity.toLowerCase() === 'Requestor Rework'.toLowerCase()) && model.get('reservation.totalRoomTaken') < 0) {
            return 'Your total room night taken is over limit.';
        }

        if (model.get('reservationRequired')) {
            if (!model.get('reservation.receiveDate') && !model.get('reservation.confirmationNumber')) {
                return "You are required to input [Received Date] and [Confirmation Number].";
            } else if (!model.get('reservation.receiveDate')) {
                return "You are required to input [Received Date].";
            } else if (!model.get('reservation.confirmationNumber')) {
                return "You are required to input [Confirmation Number].";
            }
        }
        if (!reservationView.form.isValid()) {
            return "Some field(s) of reservation form is required. Please input the required field(s) before you click the Submit button.";
        }
    },
    getRequestItem : function(){
        var me = this,
            view = me.getView(),
            reservationView = view.getReferences().reservationView,
            occupancyView = view.getReferences().occupancyView,
            reservationModel = reservationView.getViewModel();

        var reservation = reservationModel.getData().reservation;
        var occupancyStore = occupancyView.getStore();
        
        if (reservation) {
            reservation.extraBed = (reservation.extraBed ? reservation.extraBed.value : reservation.extraBed);
        }
            
        var data = {
            reservation: reservation,
            editoccupancies: me.getOriginDataFromCollection(occupancyStore.getUpdatedRecords())
        }
        
        return data;
    },
    clearData: function () {
        var me = this,
            view = me.getView(),
            reservationView = view.getReferences().reservationView;

        reservationView.getForm().reset();
    }
});
