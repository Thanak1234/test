Ext.define('Workflow.view.hr.att.DestinationWindowModel', {
    extend: 'Workflow.view.common.AbstractWindowModel',
    alias: 'viewmodel.att-destinationwindow',
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
            console.log('guest windows model ', get('selectedItem'));
            if (get('selectedItem') && get('viewSetting') && get('viewSetting').destinationBlock.addEdit) {
                 return false;
            } else {
                 return true;
            }
            return true;
         }    
    }

});
