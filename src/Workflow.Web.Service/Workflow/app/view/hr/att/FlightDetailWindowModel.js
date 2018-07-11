Ext.define('Workflow.view.hr.att.FlightDetailWindowModel', {
    extend: 'Workflow.view.common.AbstractWindowModel',
    alias: 'viewmodel.att-flightdetailwindow',
    data: {
        fromDestination: null,
        toDestination: null,
        date: null,
        time: null,
        requestHeaderId: 0,
        viewSetting: null
    },
    
    formulas:{
        readOnlyField: function (get) {            
            if (get('selectedItem') && get('viewSetting') && get('viewSetting').flightDetailBlock.addEdit) {
                 return false;
            } else {
                 return true;
            }
            return true;
         }    
    }

});
