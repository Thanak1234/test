Ext.define("Workflow.view.atd.attandanceInformation.AttandanceDetailModel", {
    extend: "Ext.app.ViewModel",
    alias: "viewmodel.atd-attandance-detail",

    data: {        
        viewSetting: null,
        
        Attandance: {
            
        },
        DetailType: null
    },
    stores: {
        types: {
            type: 'store',
            autoLoad: true,
            model: 'Workflow.model.atdform.DetailType'
        }
    }
});