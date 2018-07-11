/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.rmd.Worksheet2", {
    extend: "Workflow.view.GridComponent",
    xtype: 'rmd-worksheet2-view',
    modelName: 'worksheet2',
    collectionName: 'worksheet2s',
    header: false,
    tickData: {
        data: [
            { name: 'A', label: 'A' },
            { name: 'B', label: 'B' },
            { name: 'C', label: 'C' },
            { name: 'D', label: 'D' }
        ]
    },
    rateData: {
        data: [
            { name: 'E', label: 'Extreme (E)' },
            { name: 'H', label: 'High (H)' },
            { name: 'M', label: 'Medium (M)' },
            { name: 'L', label: 'Low (L)' }
        ]
    },
    controlData: {
        data: [
            //{name: 'E',   label: 'Very High (E)'},
            { name: 'H', label: 'High (H)' },
            { name: 'M', label: 'Medium (M)' },
            { name: 'L', label: 'Low (L)' }
        ]
    },
    treatmentOpts: {
        data: [
            { name: 'Risk Reduction', label: 'Risk Reduction' },
            { name: 'Risk Transfer', label: 'Risk Transfer' },
            { name: 'Risk Acceptance', label: 'Risk Acceptance' },
            { name: 'Risk Advoidance', label: 'Risk Advoidance' }
        ]
    },
    riskCat: {
        data: [
            { label: 'Business Continuity' },
            { label: 'Customer Relationship' },
            { label: 'Credit/Counterparty' },
            { label: 'Environmental' },
            { label: 'Execution' },
            { label: 'Information/Information Security' },
            { label: 'Legal' },
            { label: 'Liquidity' },
            { label: 'Market' },
            { label: 'People' },
            { label: 'Physical Security' },
            { label: 'Political' },
            { label: 'Processing' },
            { label: 'Regulatory' },
            { label: 'Reputation' },
            { label: 'Technology' },
            { label: 'Settlement' }
        ]
    },
    actionListeners: {
        beforeAdd: function (grid, datamodel) {

        },
        add: function (grid, store, record) {
            var newRecord = store.createModel(record);
            store.add(newRecord);
        }
    },
    afterSaveChange: function (grid) {
        // TODO: SOMTHING
    },
    buildGridComponent: function (component) {
        var me = this;

        return [{
            header: 'Business process/activity',
            width: 180,
            hideable: false,
            menuDisabled: true,
            sortable: true,
            dataIndex: 'riskActivity'
        }, {
            header: 'Risk category/<br/>sub risk category',
            width: 120,
            hideable: false,
            menuDisabled: true,
            dataIndex: 'riskCategory'
        }, {
            header: 'Risk Description',
            width: 120,
            hideable: false,
            menuDisabled: true,
            sortable: true,
            dataIndex: 'descr'
        }, {
            header: 'Key risk indicator(s)',
            width: 150,
            hideable: false,
            menuDisabled: true,
            sortable: true,
            dataIndex: 'keyRiskIndicator'
        }, {
            text: 'Consequence',
            columns: [{
                text: `Quantifiable/Non-quantifiable
                <br/>(if quantifiable please <br/>indicate)`,
                width: 220,
                hideable: false,
                menuDisabled: true,
                dataIndex: 'consequence'
            }]
        },{ 
            text: 'Control Activities',
            columns: [{
                text: 'Control Standards/Mitigating Measures',
                width: 250,
                hideable: false,
                menuDisabled: true,
                dataIndex: 'controlActivity'
            },{
                text: 'Responsibility',
                width: 100,
                hideable: false,
                menuDisabled: true,
                dataIndex: 'responsibility'
            }]
        }, {
            header: `Monitoring Process 
            <br/>e.g. Management reports/
            <br/>Performance management`,
            width: 180,
            hideable: false,
            menuDisabled: true,
            sortable: true,
            dataIndex: 'monitoringProcess'
        }, { // WORKSHEET 3 - MERGED
            header: 'Gross Risk Rating<br/>(refer to risk measurement matrix)',
            width: 210,
            menuDisabled: true,
            sortable: true,
            hideable: false,
            dataIndex: 'grossRisk'
        }, {
            header: 'Control Review Rating<br/>(refer to control rating table)',
            width: 200,
            menuDisabled: true,
            hideable: false,
            dataIndex: 'controlReview'
        }, {
            header: 'Net Risk Rating <br/>(refer to risk measurement matrix)',
            width: 210,
            menuDisabled: true,
            hideable: false,
            sortable: true,
            dataIndex: 'netRisk',
            renderer: function (value) {
                var net = '';
                switch (value) {
                    case 'E': net = 'Extreme (E)'; break;
                    case 'H': net = 'High (H)'; break;
                    case 'M': net = 'Medium (M)'; break;
                    case 'L': net = 'Low (L)'; break;
                    default: net = ''; break;
                }
                return net;
            }
        }, {
            header: 'Weakness or Gap',
            width: 150,
            menuDisabled: true,
            hideable: false,
            sortable: true,
            dataIndex: 'gap'
        }, {
            text: 'Risk Treatment Options',
            menuDisabled: true,
            hideable: false,
            columns: [{
                text: `Risk Reduction`,
                menuDisabled: true,
                columns: [{
                    text: `Risk Transfer`,
                    menuDisabled: true,
                    columns: [{
                        text: `Risk Acceptance`,
                        menuDisabled: true,
                        columns: [{
                            text: `Risk Avoidance`,
                            menuDisabled: true,
                            width: 180,
                            dataIndex: 'riskTreatment'
                        }]
                    }]
                }]
            }]
        }, {
            text: 'Tick',
            hideable: false,
            menuDisabled: true,
            name: 'tick',
            columns: [{
                menuDisabled: true,
                bind: {
                    text: 'A'
                },
                columns: [{
                    menuDisabled: true,
                    bind: {
                        text: 'B'
                    },
                    columns: [{
                        menuDisabled: true,
                        bind: {
                            text: 'C'
                        },
                        columns: [{
                            menuDisabled: true,
                            width: 90,
                            dataIndex: 'tick',
                            align: 'center',
                            bind: {
                                text: 'D'
                            }
                        }]
                    }]
                }]
            }]
        }, {
            header: `Action Plans (for Risk Reduction/Transfer)`,
            width: 250,
            menuDisabled: true,
            hideable: false,
            sortable: true,
            dataIndex: 'actionPlan'
        }, {
            header: `Date of completion`,
            width: 150,
            menuDisabled: true,
            hideable: false,
            sortable: true,
            dataIndex: 'completion'
        }, {
            header: `Completion (%)`,
            width: 120,
            menuDisabled: true,
            hideable: false,
            sortable: true,
            dataIndex: 'percentage',
            renderer: function (value) {
                if (value && value > 0) {
                    return value + '%';
                }
                return '0%';
            }
        }, {
            header: `Rationale for
            <br/>i) Risk Acceptance
            <br/>ii) Risk Avoidance
            `,
            width: 150,
            menuDisabled: true,
            hideable: false,
            sortable: true,
            dataIndex: 'rational'
        }, {
            header: 'Comment',
            width: 250,
            menuDisabled: true,
            hideable: false,
            sortable: true,
            dataIndex: 'comment'
        }];
    },
	busActData: {
        data : []
    },
	bindActStore: function(){
		var me = this;
        var parent = me.mainView,
            refs = parent.getReferences();
			
		var ws1 = refs.worksheet1,
            ws1_store = ws1.getStore();

		me.busActData.data = [];
		ws1_store.each(function(rec){
			var data = rec.getData();
			var acts = data.activities.split('|');
			Ext.each(acts, function(act){
				if(act){
					var actName = (data.businessProcess + '/' + act);
					me.busActData.data.push({
						name: actName,
						label: actName
					});
				}
			});
		});
	},
    buildWindowComponent: function (component) {
        var me = this;

        component.width = '60%';
        component.height = '95%';
        component.labelWidth = 200;
        component.layout = 'fit';
        component.maximizable = true;

        var vm = this.getViewModel();

        if (vm) {
            vm.set('isCompletionDate', true);
        }

        me.bindActStore();
		
        return [{
            xtype: 'combobox',
            displayField: 'label',
            maxWidth: 500,
			store: Ext.create('Ext.data.Store', me.busActData),
            fieldLabel: 'Business process/activity',
            allowBlank: false,
            bind: {
                value: '{worksheet2.riskActivity}'
            }
        }, {
            xtype: 'combobox',
            displayField: 'label',
            //editable: false,
            maxWidth: 500,
            store: Ext.create('Ext.data.Store', me.riskCat),
            fieldLabel: 'Risk Category/Sub Risk Category',
            allowBlank: false,
            bind: {
                value: '{worksheet2.riskCategory}'
            }
        }, {
            fieldLabel: 'Risk Description',
            allowBlank: false,
            xtype: 'textarea',
            margin: '20 0 10', 
            bind: { 
                value: '{worksheet2.descr}'
            }
        }, {
            fieldLabel: 'Key risk indicator(s)',
            xtype: 'textarea',
            allowBlank: false,
            margin: '20 0 10', 
            bind: { 
                value: '{worksheet2.keyRiskIndicator}'
            }
        }, {
            xtype: 'fieldset',
            title: 'Consequence',
            defaultType: 'textfield',
            items: [{
                fieldLabel: 'Quantifiable/Non-quantifiable <br/>(if quantifiable please indicate)',
                xtype: 'textarea',
                flex: 1,
                margin: '20 0 10', 
                bind: { 
                    value: '{worksheet2.consequence}',
                    emptyText: 'Quantifiable/Non-quantifiable (if quantifiable please indicate)'
                }
            }]
        }, {
            xtype: 'fieldset',
            title: 'Control Activities',
            defaultType: 'textfield',
            items: [{
                fieldLabel: 'Control Standards/Mitigating <br/>Measures',
                xtype: 'textarea',
                allowBlank: false,
                flex: 1,
                margin: '20 0 10', 
                bind: { 
                    value: '{worksheet2.controlActivity}',
                    emptyText: 'Control Standards/Mitigating Measures'
                }
            },{
                xtype: 'textfield',
                fieldLabel: 'Responsibility',
                allowBlank: false,
                flex: 1,
                bind: {
                    value: '{worksheet2.responsibility}'
                }
            }]
        }, {
            fieldLabel: `Monitoring Process 
            <br/>e.g. Management reports/
            <br/>Performance management`,
            xtype: 'textarea',
            margin: '20 0 10', 
            bind: { 
                value: '{worksheet2.monitoringProcess}',
                emptyText: 'Monitoring Process e.g. Management reports/Performance management'
            }
        }, { // WORKSHEET 3 - MERGED
            xtype: 'combobox',
            displayField: 'label',
            editable: false,
            maxWidth: 400,
            store: Ext.create('Ext.data.Store', me.rateData),

            fieldLabel: 'Gross Risk Rating',
            allowBlank: false,
            bind: {
                value: '{worksheet2.grossRisk}',
                emptyText: 'Gross Risk'
            },
            listeners: {
                change: function (cb, value) {
                    var vm = component.getViewModel();
                    var control = vm.get('worksheet2.controlReview');
                    vm.set('worksheet2.netRisk', me.calNetRisk(value, control));
                }
            }
        }, {
            xtype: 'combobox',
            displayField: 'label',
            editable: false,
            maxWidth: 400,
            store: Ext.create('Ext.data.Store', me.controlData),

            fieldLabel: 'Control Review Rating',
            allowBlank: false,
            bind: {
                value: '{worksheet2.controlReview}',
                emptyText: 'Control Review'
            },
            listeners: {
                change: function (cb, value) {
                    var vm = component.getViewModel(),
						gross = vm.get('worksheet2.grossRisk');
                    console.log('net', me.calNetRisk(gross, value));
                    vm.set('worksheet2.netRisk', me.calNetRisk(gross, value));
                }
            }
        }, {
            xtype: 'combobox',
            displayField: 'label',
            editable: false,
            maxWidth: 400,
            store: Ext.create('Ext.data.Store', me.rateData),
            fieldLabel: 'Net Risk Rating',
            allowBlank: false,
            bind: {
                value: '{worksheet2.netRisk}',
                readOnly: true,
                emptyText: 'Net Risk'
            },
            listeners: {
                change: function (cb, value) {
                    var vm = component.getViewModel(),
						gap = vm.get('worksheet2.gap');
					
					if(value == 'L'){
						vm.set('worksheet2.gap', 'N/A');
					}
					
					vm.set('worksheet2.disabled_gap', (value == 'L'));
					
                }
            }
        }, {
            fieldLabel: 'Weakness or Gap',
            xtype: 'textarea',
            allowBlank: false,
            margin: '20 0 10',
            bind: {
                value: '{worksheet2.gap}',
				disabled: '{worksheet2.disabled_gap}'
            }
        }, {
            xtype: 'fieldcontainer',
            fieldLabel: 'Risk Treatment Options',
            layout: 'hbox',
            items: [{
                xtype: 'combobox',
                allowBlank: false,
                editable: false,
                displayField: 'label',
                margin: '0 10 0 0',
                labelWidth: 5,
                maxWidth: 250,
                store: Ext.create('Ext.data.Store', me.treatmentOpts),
                bind: {
                    value: '{worksheet2.riskTreatment}'
                },
                listeners: {
                    change: function (field, value) {
                        if (value) {
                            var store = field.getStore(),
                                rec = store.findRecord('name', value),
                                index = store.indexOf(rec);

                            var vm = component.getViewModel(),
                                tick = me.tickData.data[index];
                            vm.set('worksheet2.tick', tick.label);
                        }
                    }
                }
            }, {
                xtype: 'combobox',
                fieldLabel: 'Tick',
                displayField: 'label',
                editable: false,
                labelWidth: 30,
                maxWidth: 80,
                store: Ext.create('Ext.data.Store', me.tickData),
                bind: {
                    value: '{worksheet2.tick}',
                    readOnly: true
                },
                listeners: {
                    change: function (field, value) {
                        var isNA = (value == 'C' || value == 'D');
                        var vm = component.getViewModel();

                        if (isNA) {
                            vm.set('worksheet2.actionPlan', 'N/A');
                            vm.set('worksheet2.completedDate', null);
                            vm.set('worksheet2.completion', 'N/A');
                            vm.set('worksheet2.rational', 'N/A');
                        }
                        vm.set('isNA', isNA);
                    }
                }
            }]
        }, {
            fieldLabel: `Action Plans (for Risk Reduction/Transfer)`,
            xtype: 'textarea',
            margin: '20 0 10',
            bind: {
                value: '{worksheet2.actionPlan}',
                disabled: '{isNA}',
                emptyText: 'For Risk Reduction/Transfer'
            }
        }, {
            xtype: 'fieldcontainer',
            fieldLabel: `Date of Completion`,
            layout: 'hbox',
            bind: {
                disabled: '{isNA}'
            },
            items: [{
                xtype: 'textfield',
                maxWidth: 220,
                bind: {
                    value: '{worksheet2.completion}',
                    disabled: '{isNA}'
                }
            }, {
                xtype: 'datefield',
                maxWidth: 35,
                margin: '0 0 0 0',
                fieldCls: 'textdate-field',
                bind: {
                    value: '{worksheet2.completedDate}',
                    disabled: '{isNA}'
                },
                listeners: {
                    change: function (field) {
                        if (vm) {
                            if (vm.get('action') == 'VIEW') {
                                field.hide();
                            }
                        }
                    },
                    select: function (field, value) {
                        var vm = component.getViewModel();

                        if (value) {
                            vm.set('worksheet2.completion', Ext.Date.format(value, 'm/d/Y'));
                        }
                    }
                }
            }]
        }, {
            fieldLabel: 'Completion (%)',
            xtype: 'numberfield',
            minValue: 0,
            maxValue: 100,
            allowDecimals: false,
            bind: {
                value: '{worksheet2.percentage}'
            }
        }, {
            fieldLabel: `Rationale for
            <br/>i) Risk Acceptance
            <br/>ii) Risk Avoidance
            `,
            xtype: 'textarea',
            margin: '20 0 10',
            bind: {
                value: '{worksheet2.rational}',
                disabled: '{isNA}',
                emptyText: 'Rationale'
            }
        }, {
            fieldLabel: 'Comment',
            xtype: 'textarea',
            margin: '20 0 10',
            bind: {
                value: '{worksheet2.comment}'
            }
        }];
    },
    calNetRisk: function (gross, control) {
        var net = null, exp = null;

        if (gross && control) {
            exp = gross.substring(gross.length - 2, gross.length - 1) +
				control.substring(control.length - 2, control.length - 1);

            switch (exp) {
                case 'EH': net = 'L'; break;
                case 'EM': net = 'M'; break;
                case 'EL': net = 'E'; break;
                case 'HH': net = 'L'; break;
                case 'HM': net = 'M'; break;
                case 'HL': net = 'H'; break;
                case 'MH': net = 'L'; break;
                case 'MM': net = 'M'; break;
                case 'ML': net = 'M'; break;
                case 'LH': net = 'L'; break;
                case 'LM': net = 'L'; break;
                default: net = null; break;
            }
        }

        return net;
    }
});