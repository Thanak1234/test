/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.reservation.crr.GuestViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.crr-guestview',
    data: {
        guest: Ext.create('Workflow.model.crrForm.Guest').getData(),
        selectedItem: null,
        viewSetting: null
    },
    stores: {
        dataStore: {
            model: 'Workflow.model.crrForm.Guest'
        }
    },
    formulas: {
        canAddRemove: function (get) {

            if (get('viewSetting') && get('viewSetting').guestBlock.addEdit) {
                return true
            } else {
                return false;
            }            
        },

        editable: function (get) {
            if (get('selectedItem') && get('viewSetting') && get('viewSetting').guestBlock.addEdit) {
                return true;
            } else {
                return false;
            }            
        },

        canView: function (get) {
            if (get('selectedItem')) {
                return true;
            } else {
                return false;
            }

        },
        canAdd: function (get) {
            if (get('sessionSelection')) {
                return true;
            } else {
                return false;
            }
        }
    }

});
