Ext.define("Workflow.view.vaf.VAForm", {
    extend: "Workflow.view.common.GenericForm",
    title: 'Gaming IA-Variance Approval Form',
    viewModel: {
        type: 'vaf'
    },
    formType: 'IAVAF_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/vafrequest',
    buildComponents: function () {
        var me = this;
        return [
            {
                xtype: 'vainfo',
                reference: 'information',
                parent: me,
                margin: 5
            },
            {
                xtype: 'vaoutline',
                reference: 'outline',
                parent: me,
                margin: 5,
                bind: {
                    disabled: '{!Information.AdjType}',
                    store: '{outlineStore}'
                }
            }
        ];
    },    
    validate: function (data) {
        var actName = data.activity;
        var refs = this.getReferences();
        if (actName.toLowerCase() == "Submission".toLocaleLowerCase() && !refs.information.form.isValid()) {
            return 'Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.';
        }
    },
    buildConfigs: function (curAct, lastAct, config) {
        var container = config.container;
        return config;
    },
    clearData: function () {
        var refs = this.getReferences();
        refs.container.reset();
        refs.outline.getStore().removeAll();
        refs.outline.getViewModel().set('totalAmount', Ext.util.Format.number(0, '$0,000.00'));
        this.getViewModel().set('Information.AdjType', null);
    },
    loadData: function (viewmodel) {
        var refs = this.getReferences();        
        var outlineStore = viewmodel.get('outlineStore');
        var allOutlines = viewmodel.get('AllOutlines');
        if (!Ext.isEmpty(allOutlines)) {
            outlineStore.setData(allOutlines);
            var totalAmount = this.calcTotal(outlineStore);
            refs.outline.getViewModel().set('totalAmount', Ext.util.Format.number(totalAmount, '$0,000.00'));
        }
        var setting = viewmodel.get('viewsetting');
        if (setting && refs.outline) {
            refs.outline.getViewModel().set('viewsetting', setting);
        }
    },
    transformData: function (viewmodel) {        
        try {
            var refs = this.getReferences();
            var outlineStore = refs.outline.getStore();
            viewmodel.set('NewOutlines', this.getData(outlineStore.getNewRecords(), true));
            viewmodel.set('ModifiedOutlines', this.getData(outlineStore.getUpdatedRecords()));
            viewmodel.set('DeletedOutlines', this.getData(outlineStore.getRemovedRecords()));
        } catch (e) {
            console.log(e);
        }        
    },
    calcTotal: function (store) {
        var totalAmount = 0;
        store.each(function (record) {
            var amount = record.get('Amount');
            totalAmount += amount;
        });
        return totalAmount;
    }
});
