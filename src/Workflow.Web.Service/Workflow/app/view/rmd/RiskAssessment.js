/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.rmd.RiskAssessment", {
    extend: "Workflow.view.FormComponent",
    xtype: 'rmd-riskAssessment-view',
    modelName: 'riskAssessment',
    loadData: function (data, viewSetting) {
        var me = this,
            viewmodel = me.getViewModel(),
            reference = me.getReferences();

        if (data) {
            viewmodel.set('riskAssessment', data.riskAssessment);
        }
    },
    buildComponent: function () {
        var me = this,
            parent = me.mainView;
        
        return [{
            xtype: 'textfield',
            fieldLabel: 'Business Unit/Department',
            allowBlank: true,
            bind: {
                value: '{riskAssessment.businessUnit}'
            }
        }, {
            xtype: 'textareafield',
            fieldLabel: 'Objective',
            allowBlank: false,
            bind: {
                value: '{riskAssessment.objective}'
            }
        }];
    }
});