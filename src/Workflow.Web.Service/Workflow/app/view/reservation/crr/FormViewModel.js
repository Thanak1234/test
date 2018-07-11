/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.reservation.crr.FormViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.crr-form',
    data: {
        complimentary: Ext.create('Workflow.model.crrForm.Complimentary').getData(),
        checkExpenseItem : null,
        checkExpense: {
            //mealExcludingAlcohol: false,
            //alcohol:false,
            //tobacco:false,
            //spa:false,
            //souvenirShop: false,
            //airportTransfers:false,
            //otherTransportwithinCity: false,
            //extraBeds:false,                            
        },
        viewSetting : null
    },    
    formulas: {        
        arrivalRadioValue : {
            bind: '{complimentary.arrivalTransfer}',
            get: function (value) {                
                return {
                    arrivalTransfer: value
                };
            },
            set: function(value) {
                this.set('complimentary.arrivalTransfer', value.arrivalTransfer);
            }
        },
        editable: function (get) {
            if (get('viewSetting') && get('viewSetting').complimentaryForm.readOnly) {
                return false;
            }else{
                return true;
            }
        },
        disableArrivalVehicle: function (get) {            
            if (get('viewSetting') && get('viewSetting').complimentaryForm.readOnly) {
                return get('complimentary').arrivalTransfer ? false : true;                
            } else {
                return get('complimentary').arrivalTransfer ? false : true;
            }
        },
        disableDepartureVehicle: function (get) {
            console.log('c ', get('complimentary').departureTransfer);
            if (get('viewSetting') && get('viewSetting').complimentaryForm.readOnly) {
                return get('complimentary').departureTransfer ? false : true;
            } else {
                return get('complimentary').departureTransfer ? false : true;                
            }
        },
        onlyComplimentary: function (get) {
            return get('viewSetting') && get('viewSetting').complimentaryForm.complimentaryOnly;
        },
        complimentaryReadOnly: function (get) {
            if (get('viewSetting') && get('viewSetting').complimentaryForm.complimentaryReadOnly) {
                return false;
            } else {
                return true;
            }
        },
        complimentaryRequired: function (get) {
            if (get('viewSetting') && get('viewSetting').complimentaryForm.require) {
                return true;
            } else {
                return false;
            }
        },
        complimentaryShowConfirmationNumber: function (get) {
            if (get('viewSetting') && get('viewSetting').complimentaryForm.showConfirmationNumber) {
                return true;
            } else {
                return false;
            }
        },
        complimentaryEditConfirmationNumber: function (get) {
            if (get('viewSetting') && get('viewSetting').complimentaryForm.editConfirmationNumber) {
                return true;
            } else {
                return false;
            }
        }
    }
});
