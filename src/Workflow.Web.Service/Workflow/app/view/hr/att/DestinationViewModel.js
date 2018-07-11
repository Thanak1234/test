Ext.define('Workflow.view.hr.att.DestinationViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.att-destinationview',
    data: {
        destination: Ext.create('Workflow.model.attForm.Destination').getData(),
        selectedItem: null,
        viewSetting: null
    },
    stores: {
        dataStore: {
            model: 'Workflow.model.attForm.Destination'
        }
    },
    formulas: {
        canAddRemoveDestination: function (get) {

            if (get('viewSetting') && get('viewSetting').destinationBlock.addEdit) {
                return true
            } else {
                return false;
            }            
        },

        editableDestination: function (get) {
            
            if (get('selectedItem') && get('viewSetting') && get('viewSetting').destinationBlock.addEdit) {

                return true;
            } else {
                console.log('setting ', get('viewSetting'), get('viewSetting').destinationBlock.addEdit);
                return false;
            }            
        },

        canViewDestination: function (get) {
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
