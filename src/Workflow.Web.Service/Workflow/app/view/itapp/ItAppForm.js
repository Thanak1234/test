Ext.define("Workflow.view.itapp.ItAppForm", {
    extend: "Workflow.view.common.GenericForm",
    title: 'Software Development Request Form',
    viewModel: {
        type: 'itapp'
    },
    formType: 'ITSWD_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/itapprequest',
    buildComponents: function() {
        return [
            {
                xtype: 'projectinit',
                reference: 'projectinit',
                margin: 5

            },
            {
                xtype: 'projectapproval',
                reference: 'projectapproval',
                margin: 5,
                bind: {
                    hidden: '{hideProjectApproval}'
                }
            },
            {
                xtype: 'development',
                reference: 'development',
                margin: 5,
                bind: {
                    hidden: '{hideDev}'
                }
            },
            {
                xtype: 'qa',
                reference: 'qa',
                margin: 5,
                bind: {
                    hidden: '{hideQA}'
                }
            }
        ];
    },
    validate: function (data) {
        var actName = data.activity;
        var refs = this.getReferences();

        if ((actName.toLowerCase() == "Submission".toLocaleLowerCase() || actName.toLowerCase() == 'Requestor Rework') && !refs.projectinit.form.isValid()) {
            return 'Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.';
        }

        if ((actName.toLowerCase() == "Submission".toLocaleLowerCase() || actName.toLowerCase() == 'Requestor Rework') && !this.isValid()) {
            return 'Please check at least one Benefits of Change or input others.';
        }

        if (actName.toLowerCase() == "IT App Manager".toLocaleLowerCase() && data.action == 'Approved' && !refs.projectapproval.form.isValid()) {
            return 'Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.';
        }

        if (actName.toLowerCase() == "Development".toLocaleLowerCase() && !refs.development.form.isValid()) {
            return 'Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.';
        }

        if (actName.toLowerCase() == "IT App Manager QA".toLocaleLowerCase() && data.action == 'Accepted' && !refs.qa.form.isValid()) {
            return 'Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.';
        }

        if (actName.toLowerCase() == "Input Go Live Date".toLocaleLowerCase() && this.isUndefine(refs.golivedf.getValue())) {            
            return 'Go Live date field is required. Please input the required field(s) before you click the Submit button.';
        }
    },
    isValid: function() {
        var refs = this.getReferences();
        var viewmodel = this.getViewModel();

        var benefitCS = viewmodel.get('ProjectInit.BenefitCS'), benefitIIS = viewmodel.get('ProjectInit.BenefitIIS'), benefitRM = viewmodel.get('ProjectInit.BenefitRM'), benefitOther = viewmodel.get('ProjectInit.BenefitOther');

        if (benefitCS == 1 || benefitIIS == 1 || benefitRM == 1 || !Ext.isEmpty(Ext.util.Format.trim(benefitOther)))
            return true;

        return false;
    },
    isUndefine: function(prop) {
        if (prop == null || prop == undefined)
            return true;
        return false;
    },
    buildConfigs: function (curAct, lastAct, config) {
        var container = config.container;
        if (curAct == 'Form View' && (lastAct == "Submission" || lastAct == "HOD Approval")) {
            container.hideProjectApproval = true;
            container.readOnlyProjectApproval = true;

            container.hideDev = true;
            container.readOnlyDev = true;

            container.hideQA = true;
            container.readOnlyQA = true;

            container.hideGoLiveDate = true;
            container.readOnlyGoLiveDate = true;
        } else if (curAct == 'Form View' && (lastAct == "IT App Manager" || lastAct == 'IT HOD Approval')) {
            container.hideProjectApproval = false;
            container.readOnlyProjectApproval = true;

            container.hideDev = true;
            container.readOnlyDev = true;

            container.hideQA = true;
            container.readOnlyQA = true;

            container.hideGoLiveDate = true;
            container.readOnlyGoLiveDate = true;
        } else if (curAct == 'Form View' && (lastAct == "Development")) {
            container.hideProjectApproval = false;
            container.readOnlyProjectApproval = true;

            container.hideDev = false;
            container.readOnlyDev = true;

            container.hideQA = true;
            container.readOnlyQA = true;

            container.hideGoLiveDate = true;
            container.readOnlyGoLiveDate = true;
        } else if (curAct == 'Form View' && (lastAct == "IT App Manager QA" || lastAct == "Submitter Agreement")) {
            container.hideProjectApproval = false;
            container.readOnlyProjectApproval = true;

            container.hideDev = false;
            container.readOnlyDev = true;

            container.hideQA = false;
            container.readOnlyQA = true;

            container.hideGoLiveDate = true;
            container.readOnlyGoLiveDate = true;
        } else if (curAct == 'Form View' && (lastAct == "Input Go Live Date" || lastAct == "HOD Agreement")) {
            container.hideProjectApproval = false;
            container.readOnlyProjectApproval = true;

            container.hideDev = false;
            container.readOnlyDev = true;

            container.hideQA = false;
            container.readOnlyQA = true;

            container.hideGoLiveDate = false;
            container.readOnlyGoLiveDate = true;
        }

        return config;
    },
    clearData: function () {
        var refs = this.getReferences();
        refs.container.reset();
    }
});
