Ext.define('Workflow.view.hr.att.NAGATravelViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.att-nagatravelview',
    data: {
        flightDetail: Ext.create('Workflow.model.attForm.FlightDetail').getData(),
        viewSetting: null,
        costTicket: 0,
        extraCharge: 0,
        remark: null
    },    
    formulas: {
        editable: function (get) {
            if (get('viewSetting') && get('viewSetting').nagaTravelForm.readOnly
                ) {
                return false;
            }else{
                return true;
            }
        },
        onlyflightDetail: function (get) {
            return get('viewSetting');// && get('viewSetting').flightDetailForm.flightDetailOnly;
        },
        flightDetailReadOnly: function (get) {
            if (get('viewSetting')
                //&& get('viewSetting').flightDetailForm.flightDetailReadOnly
                ) {
                return false;
            } else {
                return true;
            }
        },
        flightDetailRequired: function (get) {
            if (get('viewSetting')
                //&& get('viewSetting').flightDetailForm.require
                ) {
                return true;
            } else {
                return false;
            }
        },
        visible: function (get) {
            if (get('viewSetting') && get('viewSetting').travelDetailForm.visible) {
                return true;
            } else {
                return false;
            }
        }
    }
});
