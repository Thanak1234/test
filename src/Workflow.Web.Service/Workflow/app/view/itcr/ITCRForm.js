Ext.define("Workflow.view.itcr.ITCRForm", {
    extend: "Workflow.view.common.GenericForm",
    title: 'IT Change Request',
    viewModel: {
        type: 'itcr'
    },
    formType: 'ITCR_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/itcrrequest',
    buildComponents: function () {
        var me = this;
        return [
            {
                xtype: 'itcr-information',
                reference: 'information',
                parent: me,
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
        var actName = data.activity;
        var refs = this.getReferences();
        if (!refs.container.form.isValid()) {
            return 'Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.';
        }        
    },
    buildConfigs: function (curAct, lastAct, config) {
        var container = config.container;
        if (curAct == "Form View" && (lastAct == 'Manager Acknowledge')) {
            container.acknowledge = {
                readOnly: true,
                hidden: false,
                disabled: false
            };
        }
        return config;
    },
    clearData: function () {
        var refs = this.getReferences();
        var viewmodel = this.getViewModel();
        refs.container.reset();
    }
});
