Ext.define('Workflow.view.reservation.crr.RequestFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.crr-requestform',
    actionUrl: Workflow.global.Config.baseUrl + 'api/crrrequest',
    renderSubForm: function (data) {
        
        var me = this,
            view = me.getView(),
            formView = view.getReferences().formView,
            guestView = view.getReferences().guestView,
            viewSetting = view.getWorkflowFormConfig();

        console.log('from main controller', data);

        formView.fireEvent('loadData', {
            complimentary: data && data.complimentary ? data.complimentary : null,
            viewSetting: viewSetting,
            checkExpenseItem: data && data.CheckExpenseItem ? data.CheckExpenseItem : null,
            checkExpense: data && data.checkExpense ? data.checkExpense : null
        });

        guestView.fireEvent('loadData', { data: data && data.guests ? data.guests : null, viewSetting: viewSetting });
                
        console.log(formView);
    },
    validateForm: function (data) {
        var me = this,
           view = me.getView(),
           ref = me.getReferences(),
           formView = view.getReferences().formView,
           model = formView.getViewModel(),
           guestList = view.getReferences().guestView.getStore();;

        console.log('arrival ', formView.getReferences().arrivalTransfer);
        console.log('vehicle ', formView.getReferences().arrivalVehicleTypeId);
        

        if (model.get('complimentaryEditConfirmationNumber') &&
            me.isTextFieldBlank(model.get('complimentary.confirmationNumber')) && 
            data.action == 'Done') {
            return 'Confirmation Number is required...';
        } else if (guestList.getCount() == 0) {
            return "There is no Guest item in this request. Please add at least one item before you click the button submit.";
        } else if (!model.get('complimentary.arrivalDate')) {
            return 'Arrival Date is required...';
        } else if (!model.get('complimentary.departureDate')) {
            return 'Departure Date is required...';
        } else if (formView.getReferences().arrivalTransfer.getValue().arrivalTransfer
            && !formView.getReferences().arrivalVehicleTypeId.getValue()) {
            return 'Type of Vehicle of Arrival is required...';
        } else if (formView.getReferences().departureTransfer.getValue().departureTransfer
            && !formView.getReferences().departureVehicleTypeId.getValue()) {
            return 'Type of Vehicle of Departure is required...';
        } else if (!model.get('complimentary.room')) {
            return 'No. of Room is required...';
        } else if (!model.get('complimentary.adult')) {
            return 'No. of Guest is required...';
        } else if (!model.get('complimentary.roomCategoryId')) {
            return 'Room Category is required...';
        }
        //else if (!model.get('complimentary.vipStatusId')) {
        //    return 'VIP Status is required...';
        //}
        else if (!model.get('complimentary.purposeId')) {
            return 'Purpose is required...';
        } else if (ref.formView.getReferences().mealExcludingAlcohol.getValue().mealExcludingAlcohol == null) {
            return "Meal Excluding Alcohol is required.";
        } else if (ref.formView.getReferences().alcohol.getValue().alcohol == null) {
            return "Alcohol is required.";
        } else if (ref.formView.getReferences().tobacco.getValue().tobacco == null) {
            return "Tobacco is required.";
        } else if (ref.formView.getReferences().spa.getValue().spa == null) {
            return "Spa is required";
        } else if (ref.formView.getReferences().souvenirShop.getValue().souvenirShop == null) {
            return "Souvenir Shop is required";
        } else if (ref.formView.getReferences().airportTransfers.getValue().airportTransfers == null) {
            return "Airport Transfer is required.";
        } else if (ref.formView.getReferences().otherTransportwithinCity.getValue().otherTransportwithinCity == null) {
            return "Other Transport within City is required.";
        } else if (ref.formView.getReferences().extraBeds.getValue().extraBeds == null) {
            return "Extra Bed is required.";
        }


    },
    getRequestItem: function () {

        var me = this,
            view = me.getView(),
            formView = view.getReferences().formView,
            formModel = formView.getViewModel(),
            ref = me.getReferences(),
            guestView = view.getReferences().guestView,
            guestStore = guestView.getStore();

        //-----------------------------------------------------------------------------------------------------------------------     
        var checkExpense = formModel.getData().checkExpense;

        //var a = ref.formView.getReferences().mealExcludingAlcohol.getValue().mealExcludingAlcohol;
        checkExpense.mealExcludingAlcohol = ref.formView.getReferences().mealExcludingAlcohol.getValue().mealExcludingAlcohol;
        checkExpense.alcohol = ref.formView.getReferences().alcohol.getValue().alcohol;
        checkExpense.tobacco = ref.formView.getReferences().tobacco.getValue().tobacco;
        checkExpense.spa = ref.formView.getReferences().spa.getValue().spa;
        checkExpense.souvenirShop = ref.formView.getReferences().souvenirShop.getValue().souvenirShop;
        checkExpense.airportTransfers = ref.formView.getReferences().airportTransfers.getValue().airportTransfers;
        checkExpense.otherTransportwithinCity = ref.formView.getReferences().otherTransportwithinCity.getValue().otherTransportwithinCity;
        checkExpense.extraBeds = ref.formView.getReferences().extraBeds.getValue().extraBeds;
        //-----------------------------------------------------------------------------------------------------------------------

        var complimentary = formModel.getData().complimentary;
        complimentary.arrivalTransfer = ref.formView.getReferences().arrivalTransfer.getValue().arrivalTransfer;
        complimentary.departureTransfer = ref.formView.getReferences().departureTransfer.getValue().departureTransfer;
        complimentary.roomCharge = ref.formView.getReferences().roomCharge.getValue().roomCharge;
        complimentary.extraBed = ref.formView.getReferences().extraBed.getValue().extraBed;

        var data = {
            complimentary: complimentary,
            checkExpense : checkExpense,
            delRequestGuests: me.getOriginDataFromCollection(guestStore.getRemovedRecords()),
            editRequestGuests: me.getOriginDataFromCollection(guestStore.getUpdatedRecords()),
            addRequestGuests: me.getOriginDataFromCollection(guestStore.getNewRecords())
        }        
        return data;
    },
    clearData: function () {
        var me = this,
            view = me.getView(),
            formView = view.getReferences().formView;
        formView.getForm().reset();
    },
    isTextFieldBlank: function (text) {        
        return text == null || Ext.isEmpty(text);
    }
});
