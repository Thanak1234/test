Ext.define('Workflow.view.hr.att.FormViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.att-form',
    data: {
        travelDetail: Ext.create('Workflow.model.attForm.TravelDetail').getData(),
        viewSetting: null,
        requestorId: null
    },    
    formulas: {
        editable: function (get) {
            if (get('viewSetting') && get('viewSetting').travelDetailForm.readOnly) {
                return false;
            }else{
                return true;
            }
        },
        onlyTravelDetail: function (get) {
            return get('viewSetting');// && get('viewSetting').travelDetailForm.travelDetailOnly;
        },
        travelDetailReadOnly: function (get) {
            if (get('viewSetting')
                //&& get('viewSetting').travelDetailForm.travelDetailReadOnly
                ) {
                return false;
            } else {
                return true;
            }
        },
        travelDetailRequired: function (get) {
            if (get('viewSetting')
                //&& get('viewSetting').travelDetailForm.require
                ) {
                return true;
            } else {
                return false;
            }
        }
    }
});
