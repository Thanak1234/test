/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.reservation.fnfbr.ReservationViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.fnfbr-reservation',
    data: {
        reservation: Ext.create('Workflow.model.fnfForm.Reservation').getData(),
        viewSetting : null
    },
    formulas:{
        editable: function (get) {
            if (get('viewSetting') && get('viewSetting').reservationForm.readOnly) {
                return false;
            }else{
                return true;
            }
        },
        onlyReservation: function (get) {
            return get('viewSetting') && get('viewSetting').reservationForm.reservationOnly;
        },
        reservationReadOnly: function (get) {
            if (get('viewSetting') && get('viewSetting').reservationForm.reservationReadOnly) {
                return false;
            } else {
                return true;
            }
        },
        reservationRequired: function (get) {
            if (get('viewSetting') && get('viewSetting').reservationForm.require) {
                return true;
            } else {
                return false;
            }
        }
    }
});
