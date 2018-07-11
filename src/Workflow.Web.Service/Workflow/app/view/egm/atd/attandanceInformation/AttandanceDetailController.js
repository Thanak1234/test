Ext.define("Workflow.view.atd.attandanceInformation.AttandanceDetailController", {
    extend: "Ext.app.ViewController",
    alias: "controller.atd-attandance-detail",

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
        if (data.Attandance) {
            //console.log('data.Incident');

            model.set('Attandance', data.Attandance);

        }
        model.set('viewSetting', data.viewSetting);

    },

    clearData: function () {
        this.getView().getStore().removeAll();
        this.getView().reset();
    }

});