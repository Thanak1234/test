Ext.define("Workflow.view.rac.RACForm", {
    extend: "Workflow.view.common.GenericForm",
    title: 'Request For Access Card',
    viewModel: {
        type: 'rac'
    },
    formType: 'RAC_REQ',
    hasSaveDraft: true,
    actionUrl: Workflow.global.Config.baseUrl + 'api/racrequest',
    buildComponents: function () {
        var me = this;
        return [
            {
                xtype: 'rac-information',
                reference: 'information',
                parent: me,
                margin: 5
            },
            {
                xtype: 'rac-issue',
                reference: 'issue',
                margin: 5,
                parent: me,
                bind: {
                    hidden: '{!isIssue}',
                    disabled: '{!isIssue}'
                }
            }            
        ];
    },
    excludeProps: ['itemSelected', 'itemStore', 'reasonStore'],
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
        var information = refs.information;
        var issue = refs.issue;        

        if ((actName == 'Submission' || actName == 'Requestor Rework' || actName == 'Draft') && !information.form.isValid()) {
            return 'Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.';
        }

        if (actName == 'Surveilance Issues' && !issue.form.isValid()) {
            return 'Some field(s) of Information is required. Please input the required field(s) before you click the Done button.';
        }
    },
    buildConfigs: function (curAct, lastAct, config) {
        var container = config.container;
        if (curAct == 'Form View' && lastAct == 'Surveillance Issues') {
            container.isIssue = true;
        }
        return config;
    },
    clearData: function () {
        var refs = this.getReferences();
        refs.container.reset();
    }
});
