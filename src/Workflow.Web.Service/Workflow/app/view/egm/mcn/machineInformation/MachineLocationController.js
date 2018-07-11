Ext.define("Workflow.view.mcn.machineInformation.MachineLocationController", {
    extend: "Ext.app.ViewController",
    alias: "controller.mcn-machine-location",

    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear: 'clearData'
            }
        }
    },

    loadData: function (data) {
        //debugger;
        var me = this,
            view = me.getView(),
            model = me.getView().getViewModel();

        //me.view.getForm().reset(); // clean form before bind model.
        console.log('load data here', data);
        if (data.Machine) {
            //console.log('data.Incident');

            model.set('Machine', data.Machine);

        }
        model.set('viewSetting', data.viewSetting);

    },

    clearData: function () {
        this.getView().getStore().removeAll();
        this.getView().reset();
    }

});