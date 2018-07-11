Ext.define("Workflow.view.osha.OSHAForm", {
    extend: "Workflow.view.common.GenericForm",
    title: 'Accident/Incident Form',
    viewModel: {
        type: 'osha'
    },
    formType: 'OSHA_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/osharequest',
    buildExtraButtons: function () {
        var me = this;
        return [{
            xtype: 'button',
            text: 'Download Human Grid Diagram',
            href: '/documents/OSHA - Human Grid Diagram.pdf',
            target: '_blank',
            hrefTarget: '_blank'
        }];
    },
    buildComponents: function () {
        var me = this;
        return [
            {
                xtype: 'osha-information',
                parent: me,
                reference: 'information',
                margin: 5,
                bind: {
                    hidden: '{config.information.hidden}'
                }
            },
            {
                xtype: 'osha-panel',
                reference: 'osha',
                parent: me,
                margin: 5,
                bind: {
                    hidden: '{config.osha.hidden}'
                }
            },
            {
                xtype: 'osha-actionTaken',
                reference: 'actionTaken',
                parent: me,
                margin: 5,
                bind: {
                    hidden: '{config.actionTaken.hidden}'
                }
            }
        ];
    },    
    excludeProps: [],
    loadData: function (viewmodel) {
        var me = this;
        var refs = me.getReferences();
        var data = viewmodel.getData();
        var victims = refs.victims;
        var withness = refs.withness;

        victims.fireEvent('loadData', { data: data && data.victims ? data.victims : null, config: data.viewSetting.container.information.victims });
        withness.fireEvent('loadData', { data: data && data.withness ? data.withness : null, config: data.viewSetting.container.information.withness });

        if (data && data.activity == "Submission") {
            viewmodel.set('information', {});
        }

        if (data && data.information) {
            viewmodel.set('rdiegsd.diegsd', data.information.diegsd);
            viewmodel.set('re1.e1', data.information.e1);
            viewmodel.set('re2.e2', data.information.e2);
            viewmodel.set('rg3.g3', data.information.g3);
            viewmodel.set('rg4.g4', data.information.g4);
            viewmodel.set('rhcat.hcat', data.information.hcat);
        }
    },
    transformData: function (viewmodel) {
        var me = this;
        var refs = me.getReferences();
        var data = viewmodel.getData();
        var victims = refs.victims;
        var withness = refs.withness;

        var victimStore = victims.getStore();
        var withnessStore = withness.getStore();

        viewmodel.set('information.diegsd', viewmodel.get('rdiegsd.diegsd'));
        viewmodel.set('information.e1', viewmodel.get('re1.e1'));
        viewmodel.set('information.e2', viewmodel.get('re2.e2'));
        viewmodel.set('information.g3', viewmodel.get('rg3.g3'));
        viewmodel.set('information.g4', viewmodel.get('rg4.g4'));
        viewmodel.set('information.hcat', viewmodel.get('rhcat.hcat'));

        viewmodel.set('addVictims', me.getOriginDataFromCollection(victimStore.getNewRecords()));
        viewmodel.set('editVictims', me.getOriginDataFromCollection(victimStore.getUpdatedRecords()));
        viewmodel.set('removeVictims', me.getOriginDataFromCollection(victimStore.getRemovedRecords()));

        viewmodel.set('addWithness', me.getOriginDataFromCollection(withnessStore.getNewRecords()));
        viewmodel.set('editWithness', me.getOriginDataFromCollection(withnessStore.getUpdatedRecords()));
        viewmodel.set('removeWithness', me.getOriginDataFromCollection(withnessStore.getRemovedRecords()));
    },
    validate: function (data) {
        var me = this;
        var viewmodel = me.getViewModel();
        var information = viewmodel.get('information');
        var refs = me.getReferences();
        var form = refs.information;
        var victims = refs.victims;
        var withness = refs.withness;

        var actionTaken = refs.actionTaken,
            osha = refs.osha;

        if (!form.isValid()) {
            return "Please input the required(*) field(s) before you take an action.";
        }
        
        if (!victims.getStore().getAt(0)) {
            return "Please add victim to list before you submit.";
        }

        if (osha.isVisible() && !information.acnr) {
            return "Please input the OSHA field before you take an action.";
        }
        if (actionTaken.isVisible() && !information.at) {
            return "Please input the Action Taken field before you take an action.";
        }
    },
    buildConfigs: function (curAct, lastAct, config) {
        
        return config;
    },
    clearData: function () {
        var me = this;
        var refs = me.getReferences();
        var information = refs.information;
        var victims = refs.victims;
        var withness = refs.withness;

        victims.getStore().removeAll();
        withness.getStore().removeAll();
        information.reset();
    }
});
