/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.fnfbr.OccupancyViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.fnf-occupancyview',
    data: {
        selectedItem: null,
        viewSetting: null
    },
    stores : {
        occupancy: {
            model: 'Workflow.model.fnfForm.Occupancy'
        }
    },
    formulas: {
        editable: function (get) {
            if (get('viewSetting') && get('viewSetting').occupancyBlock.addEdit) {
                return true;
            } else {
                return false;
            }
        },
        reservationOnly: function (get) {
            return get('viewSetting') && get('viewSetting').occupancyBlock.reservationOnly;
        }
    }
});
