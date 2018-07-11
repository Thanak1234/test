/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.iteirq.EventInternetView", {
    extend: "Workflow.view.FormComponent",
    xtype: 'iteirq-eventInternet-view',
    modelName: 'eventInternet',
    cost: 3,
    loadData: function (data, viewSetting) {
        var me = this,
            viewmodel = me.getViewModel(),
            reference = me.getReferences();

        if (data) {
            viewmodel.set('eventInternet', data.eventInternet);
        }
    },
    buildComponent: function () {
        var me = this;

        var product = {
            dayNum: 0,
            cost: 0,
            bandwidth: 0
        };

        return [{
            xtype: 'textfield',
            fieldLabel: 'Event Name',
            allowBlank: true,
            bind: {
                value: '{eventInternet.subject}'
            }
        }, {
            xtype: 'container',
            layout: 'hbox',
            fieldLabel: 'Date',
            defaults: me.defaultsFieldSet,
            items: [{
                fieldLabel: 'Start Date',
                xtype: 'datefield',
                allowBlank: false,
                bind: {
                    value: '{eventInternet.startDate}'
                },
                listeners: {
                    change: function (field, startDate) {
                        var vm = me.getViewModel();
                        var endDate = vm.get('eventInternet.endDate');
                        if (startDate && endDate) {
							if(Ext.Date.format(endDate, 'mdY') != '01011970'){
								var days = Ext.Date.diff(startDate, endDate, "d") + 1;
								vm.set('eventInternet.dayNum', days);
								product.dayNum = days;
								me.calcTotalCost(product);
							}
                        }
                    }
                }
            }, {
				fieldLabel: 'No End Date',
				xtype		: 'checkbox',
				checked   	: false,
				maxWidth	: 100,
				labelWidth	: 80,
				inputValue  : false,
				reference	: 'ckforever',
				bind		: {
					value	: '{eventInternet.forever}'
				},
				listeners: {
                    change: function (field, val) {
						console.log('val', val);
						if(val){
                            me.resetCost();
						}
					}
				}
			}, {
                fieldLabel: 'End Date',
                xtype: 'datefield',
                allowBlank: true,
                bind: {
                    value: '{eventInternet.endDate}',
					disabled: '{eventInternet.forever}'
                },
                listeners: {
                    change: function (field, endDate) {
                        var vm = me.getViewModel();
                        var startDate = vm.get('eventInternet.startDate');
						
						if(!endDate || (Ext.Date.format(endDate, 'mdY') == '01011970')){
							vm.set('eventInternet.forever', true);
						}
						
                        if (startDate && endDate) {
							if(Ext.Date.format(endDate, 'mdY') != '01011970'){
								var days = Ext.Date.diff(startDate, endDate, "d") + 1;
								vm.set('eventInternet.dayNum', days);
								vm.set('eventInternet.forever', false);
								product.dayNum = days;
								me.calcTotalCost(product);
							}
                        }
                    }
                }
            }, {
                xtype: 'numberfield',
                fieldLabel: 'No. of day (s)',
                allowBlank: true,
                readOnly: true,
                bind: {
                    value: '{eventInternet.dayNum}',
					disabled: '{eventInternet.forever}'
                }
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            fieldLabel: 'Date',
            defaults: me.defaultsFieldSet,
            items: [{
                xtype: 'numberfield',
                fieldLabel: 'Bandwidth (Mbps)',
                minValue: 0,
                allowBlank: false,
                bind: {
                    value: '{eventInternet.bandwidth}'
					//disabled: '{eventInternet.forever}'
                },
                listeners: {
                    change: function (field, value) {
                        product.bandwidth = value;
                        me.calcTotalCost(product);
                    }
                }
            }, {
                xtype: 'currencyfield',
                fieldLabel: 'Est. Cost USD/Mb/Day',
                allowBlank: false,
                value: me.cost,
				minValue: 0.0000001,
                bind: {
                    value: '{eventInternet.cost}',
					disabled: '{eventInternet.forever}'
                },
                listeners: {
                    change: function (field, value) {
                        me.getViewModel().set('eventInternet.cost', value);
                        product.cost = value;
                        me.calcTotalCost(product);
                    }
                }
            }, {
                xtype: 'currencyfield',
                fieldLabel: 'Total Cost',
                readOnly: true,
                bind: {
                    value: '{eventInternet.totalCost}',
					disabled: '{eventInternet.forever}'
                }
            }]
        }, {
            xtype: 'textareafield',
            fieldLabel: 'Request Desription',
            allowBlank: false,
            bind: {
                value: '{eventInternet.requestDescr}'
            }
        }];
    },
	afterRender: function(){
		var vm = this.getViewModel();
		var endDate = vm.get('eventInternet.endDate');
		
		if((Ext.Date.format(endDate, 'mdY') == '01011970')){
			vm.set('eventInternet.forever', true);
		}else{
			vm.set('eventInternet.forever', false);
		}
		this.callParent(arguments);
	},
	resetCost: function(){
		var me = this;
        var vm = me.getViewModel();
		
		vm.set('eventInternet.endDate', null);
		vm.set('eventInternet.dayNum', 0);
		//vm.set('eventInternet.bandwidth', 0);
		vm.set('eventInternet.totalCost', 0);
	},
    calcTotalCost: function (product) {
        var me = this;
        var vm = me.getViewModel();

		
        if(product.dayNum > 0){
            product.cost = (product.cost == 0 ? me.cost : product.cost);
            vm.set('eventInternet.cost', product.cost);
			vm.set('eventInternet.totalCost', (product.dayNum * product.cost * product.bandwidth));
        }else{
			var endDate = vm.get('eventInternet.endDate');
			
			if(endDate && Ext.Date.format(endDate, 'mdY') != '01011970'){
				Ext.MessageBox.show({
					title: 'Invalide',
					msg: 'Invalid date range selection',
					buttons: Ext.MessageBox.OK,
					icon: Ext.Msg.ERROR
				});		
			}
            vm.set('eventInternet.totalCost', 0);
        }        
    }
});