Ext.define("Workflow.view.irf.IRFForm", {
    extend: "Workflow.view.common.GenericForm",
    title: 'IT Item Repair',
    viewModel: {
        type: 'irf'
    },
    formType: 'ITIRF_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/itirfrequest',
    buildComponents: function () {
        var me = this;
        return [
            {
                xtype: 'irf-information',
                reference: 'information',
                margin: 5
            }        
        ];
    },
    excludeProps: [],
    loadData: function (viewmodel) {
        var me = this;
        var refs = this.getReferences();
    },
    transformData: function (viewmodel) {
        var me = this;
        var refs = this.getReferences();
    },
    validate: function (data) {
        var action = data.action;
        var refs = this.getReferences();
        return refs.information.validate(action);
    },
    buildConfigs: function (curAct, lastAct, config) {
        var container = config.container;
       
        return config;
    },
    clearData: function () {
        var refs = this.getReferences();
        var viewmodel = this.getViewModel();
        refs.information.reset();
    }
});
