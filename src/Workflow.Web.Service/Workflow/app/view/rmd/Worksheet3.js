/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.rmd.Worksheet3", {
    extend: "Workflow.view.GridComponent",
    xtype: 'rmd-worksheet3-view',
    modelName: 'worksheet3',
    collectionName: 'worksheet3s',
    header: false,
    tickData: {
        data : [
            {name: 'A',   label: 'A'},
            {name: 'B',   label: 'B'},
            {name: 'C',   label: 'C'},
            {name: 'D',   label: 'D'}
        ]
    },
    rateData: {
        data : [
            {name: 'E',   label: 'Extreme (E)'},
            {name: 'H',   label: 'High (H)'},
            {name: 'M',   label: 'Medium (M)'},
            {name: 'L',   label: 'Low (L)'}
        ]
    },
    controlData: {
        data : [
            //{name: 'E',   label: 'Very High (E)'},
            {name: 'H',   label: 'High (H)'},
            {name: 'M',   label: 'Medium (M)'},
            {name: 'L',   label: 'Low (L)'}
        ]
    },
    treatmentOpts: {
        data : [
            { name: 'Risk Reduction', label: 'Risk Reduction' },
            { name: 'Risk Transfer', label: 'Risk Transfer' },
            { name: 'Risk Acceptance', label: 'Risk Acceptance' },
            { name: 'Risk Advoidance', label: 'Risk Advoidance' }
        ]
    },
    onItemCheck: function(m, p2, p3){
        console.log(this);
    },
    listeners: { 
        afterrender: function() {
            var me = this;

            var headerCt = this.headerCt;
            var menu = this.headerCt.getMenu();
            menu.items.removeAll();
            
            menu.add([{
                xtype: 'combobox',
                fieldLabel: 'Risk Reduction',
                displayField: 'label',
                editable: false,
                width: 180,
                margin: '1 3',
                store: Ext.create('Ext.data.Store', me.tickData),
                bind: {
                    value: '{rre}'
                }
            },{
                xtype: 'combobox',
                fieldLabel: 'Risk Transfer',
                displayField: 'label',
                editable: false,
                width: 180,
                margin: '1 3',
                store: Ext.create('Ext.data.Store', me.tickData),
                bind: {
                    value: '{rtr}'
                }
            },{
                xtype: 'combobox',
                fieldLabel: 'Risk Acceptance',
                displayField: 'label',
                editable: false,
                width: 180,
                margin: '1 3',
                store: Ext.create('Ext.data.Store', me.tickData),
                bind: {
                    value: '{rac}'
                }
            },{
                xtype: 'combobox',
                fieldLabel: 'Risk Avoidance',
                displayField: 'label',
                editable: false,
                width: 180,
                margin: '1 3',
                store: Ext.create('Ext.data.Store', me.tickData),
                bind: {
                    value: '{rav}'
                }
            }]);
        }
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
            dataIndex: 'netRisk'
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
        },{
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
				if(value){
					return value + '%';	
				}
				return '';
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
        }];
    },
    buildWindowComponent: function (component) {
        var me = this;
        component.title = 'Risk Assessment Worksheet 3 - Risk Evaluation and Risk Treatment';
        component.width = '60%';
        component.height = '78%';
        component.labelWidth = 200;
        component.layout = 'fit';
        component.maximizable = true;

        var vm = this.getViewModel();
		
        
		
        if (vm) {
            // reset default form setting
            console.log('com', vm);
            vm.set('isCompletionDate', true);
        }
        
        return [{
            xtype: 'combobox',
            displayField: 'label',
            editable: false,
            maxWidth: 400,
            store: Ext.create('Ext.data.Store', me.rateData),
            
            fieldLabel: 'Gross Risk Rating',
            allowBlank: false,
            bind: {
                value: '{worksheet3.grossRisk}',
                emptyText: 'Gross Risk'
            },
			listeners: {
				change: function(cb, value){
					var vm = component.getViewModel();
					var control = vm.get('worksheet3.controlReview');
					vm.set('worksheet3.netRisk', me.calNetRisk(value, control));
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
                value: '{worksheet3.controlReview}',
                emptyText: 'Control Review'
            },
			listeners: {
				change: function(cb, value){
					var vm = component.getViewModel(),
						gross = vm.get('worksheet3.grossRisk');
						console.log('net', me.calNetRisk(gross, value));
					vm.set('worksheet3.netRisk', me.calNetRisk(gross, value));
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
                value: '{worksheet3.netRisk}',
				readOnly: true,
                emptyText: 'Net Risk'
            }
        }, {
            fieldLabel: 'Weakness or Gap',
            xtype: 'textarea',
            allowBlank: false,
            margin: '20 0 10', 
            bind: { 
                value: '{worksheet3.gap}'
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
                    value: '{worksheet3.riskTreatment}'
                },
                listeners: {
                    change: function (field, value) {
                        if (value) {
                            var store = field.getStore(),
                                rec = store.findRecord('name', value),
                                index = store.indexOf(rec);

                            var vm = component.getViewModel(),
                                tick = me.tickData.data[index];
                            vm.set('worksheet3.tick', tick.label);
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
                    value: '{worksheet3.tick}',
                    readOnly: true
                }
            }]
        }, {
            fieldLabel: `Action Plans (for Risk Reduction/Transfer)`,
            xtype: 'textarea',
            margin: '20 0 10', 
            bind: { 
                value: '{worksheet3.actionPlan}',
                emptyText: 'For Risk Reduction/Transfer'
            }
        }, {
            xtype: 'fieldcontainer',
            fieldLabel: `Date of Completion`,
            layout: 'hbox',
            items: [{
                xtype: 'textfield',
                maxWidth: 220,
                bind: { 
                    value: '{worksheet3.completion}'
                }
            }, {
                xtype: 'datefield',
                maxWidth: 35,
                margin: '0 0 0 0',
                fieldCls: 'textdate-field',
                bind: {
                    value: '{worksheet3.completedDate}'
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
                            vm.set('worksheet3.completion', Ext.Date.format(value, 'm/d/Y'));
                        }
                    }
                }
            }]
        }, {
            fieldLabel: 'Completion (%)',
            xtype: 'numberfield',
            minValue: 0.00000001,
            maxValue: 100,
            allowDecimals: false,
            bind: { 
                value: '{worksheet3.percentage}'
            }
        }, {
            fieldLabel: `Rationale for 
            <br/>i) Risk Acceptance
            <br/>ii) Risk Avoidance
            `,
            xtype: 'textarea',
            margin: '20 0 10', 
            bind: { 
                value: '{worksheet3.rational}',
                emptyText: 'Rationale'
            }
        }];
    },
	calNetRisk: function(gross, control){
		var net = null, exp = null;
		
		if(gross && control){
			exp = gross.substring(gross.length-2, gross.length - 1) + 
				control.substring(control.length-2, control.length - 1);
			
			switch(exp) {
				case 'EH' : net = 'L'; break;
				case 'EM' : net = 'M'; break;
				case 'EL' : net = 'E'; break;
				case 'HH' : net = 'L'; break;
				case 'HM' : net = 'M'; break;
				case 'HL' : net = 'H'; break;
				case 'MH' : net = 'L'; break;
				case 'MM' : net = 'M'; break;
				case 'ML' : net = 'M'; break;
				case 'LH' : net = 'L'; break;
				case 'LM' : net = 'L'; break;
				default: net = null; break;
			}
		}
		
		return net;
	}
});