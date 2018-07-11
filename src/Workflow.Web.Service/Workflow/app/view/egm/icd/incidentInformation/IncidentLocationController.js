Ext.define("Workflow.view.icd.incidentInformation.IncidentLocationController", {
    extend: "Ext.app.ViewController",
    alias: "controller.icd-incident-location",

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
        if (data.Incident) {
            //console.log('data.Incident');

            model.set('Incident', data.Incident);

        }
        model.set('viewSetting', data.viewSetting);

    },

    clearData: function () {
        this.getView().getStore().removeAll();
        this.getView().reset();
    }

});