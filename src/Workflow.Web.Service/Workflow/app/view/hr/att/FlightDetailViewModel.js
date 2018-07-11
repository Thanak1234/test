Ext.define('Workflow.view.hr.att.FlightDetailViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.att-flightdetailview',
    data: {
        FlightDetail: Ext.create('Workflow.model.attForm.FlightDetail').getData(),
        selectedItem: null,
        viewSetting: null
    },
    stores: {
        dataStore: {
            model: 'Workflow.model.attForm.FlightDetail'
        }
    },
    formulas: {
        canAddRemoveFlightDetail: function (get) {

            if (get('viewSetting') && get('viewSetting').flightDetailBlock.addEdit) {
                return true
            } else {
                return false;
            }            
        },

        editableFlightDetail: function (get) {
            if (get('selectedItem') && get('viewSetting') && get('viewSetting').flightDetailBlock.addEdit) {
                return true;
            } else {
                return false;
            }            
        },

        canViewFlightDetail: function (get) {
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
