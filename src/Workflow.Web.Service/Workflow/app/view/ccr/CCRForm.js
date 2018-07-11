Ext.define("Workflow.view.ccr.CCRForm", {
    extend: "Workflow.view.common.GenericForm",
    title: 'Contract Draft/Review Request',
    viewModel: {
        type: 'ccr'
    },
    formType: 'CCR_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/ccrrequest',
    buildComponents: function () {
        var me = this;
        return [
               {
                   xtype: 'ccr-section-one',
                   reference: 'section_one',
                   parent: me,
                   margin: '0 5'
               },
               {
                   xtype: 'ccr-section-two',
                   reference: 'section_two',
                   parent: me,
                   margin: '0 5'
               }
        ];
    },
    excludeProps: ['formpickup'],
    loadData: function (viewmodel) {
        var me = this;
        var refs = this.getReferences();
        var activity = viewmodel.get('activity');

        if ("Submission" == activity) return;
        
        var isCapex = viewmodel.get('ContractDraft.IsCapex');
        if (!Ext.isEmpty(isCapex)) {
            viewmodel.set('capex', { isCapex: isCapex });
        }

        var status = viewmodel.get('ContractDraft.Vis');
        if (!Ext.isEmpty(status)) {
            viewmodel.set('radVis', { vis: status });
        }

        var startDate = viewmodel.get('ContractDraft.StartDate');
        if (!Ext.isEmpty(startDate)) {
            viewmodel.set('ContractDraft.StartDate', new Date(startDate));
        }

        var endingDate = viewmodel.get('ContractDraft.EndingDate');
        if (!Ext.isEmpty(endingDate)) {
            viewmodel.set('ContractDraft.EndingDate', new Date(endingDate));
        }
    },
    transformData: function (viewmodel) {
        var me = this;        
        var refs = this.getReferences();
        var capex = viewmodel.get('capex');
        if (!Ext.isEmpty(capex))
            viewmodel.set('ContractDraft.IsCapex', capex.isCapex);

        var radVis = viewmodel.get('radVis');
        if (!Ext.isEmpty(radVis))
            viewmodel.set('ContractDraft.Vis', radVis.vis);
    },
    buildExtraButtons: function () {
        var me = this;
        return [{
            xtype: 'button',
            text: 'Download VIS Form',
            href: '/documents/Vendor Information Sheet.pdf',
            target: '_blank',
            hrefTarget: '_blank'
        }, {
            xtype: 'button',
            text: 'Download Crontract Review Process',
            href: '/documents/Process Flowchart.pdf',
            target: '_blank',
            hrefTarget: '_blank'
        }];
    },
    validate: function (data) {
        var actName = data.activity;
        var refs = this.getReferences();
        var contractDraft = data.dataItem.ContractDraft;
        var me = this;
        if (!refs.container.form.isValid()) {
            return 'Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.';
        }

        if (contractDraft.EndingDate < contractDraft.StartDate) {
            return 'Starting Date must be less than Ending Date.';
        }

        if (Ext.isEmpty(contractDraft.Vis)) {
            return 'Please choose Yes or No for Has the Vendor Information Sheet(VIS) been submitted to Legal?';
        }

        if (me.validAgreementType(contractDraft))
        {
            return 'Agreement Type must be required. Please check Agreement Type or input others.';
        }

        if (me.validStatus(contractDraft)) {
            return 'Status must be required. Please tick in approporiate status.';
        }
    },
    validAgreementType: function (contractDraft) {
        var me = this;
        if(me.isEmpty(contractDraft.AtSa) && 
           me.isEmpty(contractDraft.AtSpa) &&
           me.isEmpty(contractDraft.AtLa) &&
           me.isEmpty(contractDraft.AtCa) &&
           me.isEmpty(contractDraft.AtLea) &&
           me.isEmpty(contractDraft.AtEa) &&
           me.isEmpty(contractDraft.AtOther)) {
            return true;
        }
    },
    validStatus: function (contractDraft) {
        var me = this;
        if (me.isEmpty(contractDraft.StatusNew) &&
           me.isEmpty(contractDraft.StatusRenewal) &&
           me.isEmpty(contractDraft.StatusReplacement) &&
           me.isEmpty(contractDraft.StatusAddendum)) {
            return true;
        }
    },
    buildConfigs: function (curAct, lastAct, config) {
        var container = config.container;

        return config;
    },
    clearData: function () {
        var refs = this.getReferences();
        var viewmodel = this.getViewModel();
        refs.container.reset();
        viewmodel.set('capex', { isCapex: true });
    },
    isEmpty: function (v) {
        if (v == null || v == false) {
            return true;
        }
        return false;
    }
});
