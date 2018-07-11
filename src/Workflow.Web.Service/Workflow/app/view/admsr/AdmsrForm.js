Ext.define("Workflow.view.admsr.AdmsrForm", {
    extend: "Workflow.view.common.GenericForm",
    title: 'Admin - Service Request Form',
    viewModel: {
        type: 'admsr'
    },
    formType: 'ADMSR_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/admsrrequest',
    buildComponents: function () {
        var me = this;
        return [
            {
                xtype: 'admsr-information',
                reference: 'information',
                parent: me,
                margin: 5
            }, {
                xtype: 'panel',
                title: 'Administrator Department',
                border: true,
                layout: 'column',
                margin: 5,
                width: '100%',
                collapsible: true,
                iconCls: 'fa fa-cogs',
                defaults: {
                    xtype: 'textarea',
                    width: '100%',
                    margin: '5 10',
                    columnWidth: 1,
                    labelWidth: 88
                },
                bind: {
                    hidden: '{config.admin.hidden}'
                },
                items: [
                    {
                        minHeight: 200,
                        border: true,
                        xtype: 'admsr-company',
                        reference: 'company',
                        mainView: me
                    },
                    {
                        xtype: 'label',
                        text: 'Administrator Department Comment'
                    },
                    {
                        xtype: 'textarea',
                        bind: {
                            readOnly: '{config.admin.readOnly}',
                            value: '{information.adc}'
                        }
                    }, {
                        xtype: 'checkbox',
                        boxLabel: 'Send to Admin\'s Line Of Department',
                        columnWidth: 1,
                        hideLabel: true,
                        checked: false,
                        bind: {
                            readOnly: '{config.admin.readOnly_salod}',
                            value: '{information.salod}',
                            hidden: '{config.admin.hidden_salod}'
                        }
                    }, {
                        xtype: 'fieldset',
                        title: 'Email Notification',
                        defaultType: 'checkbox',
                        layout: 'hbox',
                        allowBlank: false,
                        defaults: {
                            margin: '0 30 0 0'
                        },
                        items: [{
                            boxLabel: 'Finance',
                            bind: {
                                readOnly: '{config.admin.readOnly}',
                                value: '{information.efinance}'
                            }
                        }, {
                            boxLabel: 'Cost Control',
                            bind: {
                                readOnly: '{config.admin.readOnly}',
                                value: '{information.ecc}'
                            }
                        }, {
                            boxLabel: 'Purchasing',
                            bind: {
                                readOnly: '{config.admin.readOnly}',
                                value: '{information.epurchasing}'
                            }
                        }]
                    }
                ]
            }
        ];
    },
    excludeProps: [],
    loadData: function (viewmodel) {
        var me = this;
        var refs = this.getReferences();
        var company = refs.company;
        if (company) {
            var noQuote = company.getReferences()['noQuote'];
            console.log('viewmodel.getData()', viewmodel.getData());
            
            var vm = company.getViewModel(),
                data = viewmodel.getData();
            noQuote.setValue(!(data.companies && data.companies.length > 0))
            
            vm.set('companyProperty', viewmodel.get('viewSetting.companyProperty'));
            company.fireEvent('loadData', viewmodel.getData(), viewmodel.get('viewSetting'));
        }
    },
    transformData: function (viewmodel) {
        var me = this;
        var refs = this.getReferences();
        var company = refs.company;
        var store = company.getStore();

        var newCompanies = me.getOriginDataFromCollection(store.getNewRecords());
        if (newCompanies != null && newCompanies.length > 0)
            viewmodel.set('newCompanies', newCompanies);

        var modifiedCompanies = me.getOriginDataFromCollection(store.getUpdatedRecords());
        if (modifiedCompanies != null && modifiedCompanies.length > 0)
            viewmodel.set('modifiedCompanies', modifiedCompanies);

        var deletedCompanies = me.getOriginDataFromCollection(store.getRemovedRecords());
        if (deletedCompanies != null && deletedCompanies.length > 0)
            viewmodel.set('deletedCompanies', deletedCompanies);
    },
    validate: function (data) {
        var actName = data.activity;
        var refs = this.getReferences();
        var store = refs.company.getStore();
        var noQuote = refs.company.getReferences()['noQuote'];


        if (!refs.container.form.isValid()) {
            return 'Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.';
        }

        if (actName == 'Admin Review' &&
            !store.getAt(0) && 
            (noQuote && !noQuote.getValue())) {
            return 'Please add item to Company/Contractor List before you take action.';
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
        refs.company.fireEvent('onDataClear');
    }
});
