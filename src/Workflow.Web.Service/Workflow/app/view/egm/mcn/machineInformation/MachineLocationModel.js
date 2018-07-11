Ext.define("Workflow.view.mcn.machineInformation.MachineLocationModel", {
    extend: "Ext.app.ViewModel",
    alias: "viewmodel.mcn-machine-location",

    data: {        
        viewSetting: null,
        
        Machine: {
            
        },
        IssueType:null

    },
        
    stores: {
        types: {
            type: 'store',
            autoLoad: true,
            model: 'Workflow.model.mcnform.MachineType'
        }
    }

});