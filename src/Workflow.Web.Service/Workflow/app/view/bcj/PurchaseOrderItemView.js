/**********************
 Author : Yim Samaune 
***********************/
Ext.define("Workflow.view.bcj.PurchaseOrderItemView", {
    extend: "Ext.grid.Panel",
    xtype : "bcj-purchaseorderitem",
    title: 'Purchasing Use Only',
    viewModel: {
        data: {
            totalPoAmount: 0,
            pendingPoAmount: 0,
            totalAmount: 0,
            selectedItem: null,
            purchaseOrder: {},
            
            button: {
                tbar: { hidden: false },
                bbar: { hidden: false },
                reset: { hidden: false, disabled: false },
                add: { hidden: false, disabled: false },
                edit: { hidden: false, disabled: true, text: 'Edit' },
                remove: { hidden: false, disabled: false }
            }
        }
    },
    onDataClear: function(){
        this.getStore().removeAll();
    },
    loadData: function (data, project, viewSetting) {
        var me = this,
            viewmodel = me.getViewModel();

        if (viewSetting && viewSetting.purchaseOrderItemView) {
            var property = viewSetting.purchaseOrderItemView;
            me.setVisible(!property.hidden);
            viewmodel.set('button.tbar.hidden', property.readOnly);
            viewmodel.set('button.bbar.hidden', property.readOnly);
            viewmodel.set('button.reset.hidden', property.readOnly);
            viewmodel.set('button.add.hidden', property.readOnly);
            viewmodel.set('button.edit.hidden', property.readOnly);
            viewmodel.set('button.remove.hidden', property.readOnly);
        }
        
        me.store.each(function(record){
            console.log('record', record);
        });
        if (data && data.length > 0) {
            me.store.setData(data);
        }
        
        if (project && project.totalAmount > 0) {
			var totalAmountInc = ((project.totalAmount) * 0.1) + (project.totalAmount);
            viewmodel.set('totalAmount', totalAmountInc);
            me.calculate();
        }
    },
    bind: {
        selection   : '{selectedItem}'
    },
    viewConfig: {
        enableTextSelection: true
    },
    stateful: true,
    collapsible: true,
    headerBorders: false,
    isValid: function (button, state) {
        var me = this,
            viewmodel = me.getViewModel(),
            totalAmount = viewmodel.get('totalAmount'),
            totalPoAmount = 0,
            purchaseOrder = viewmodel.get('purchaseOrder');
        
        me.store.each(function (record) {
            totalPoAmount += record.get('poAmount');
        });
		var message = 'Please input the required field, before click ' + button.text + '!';
        if (purchaseOrder.poDate &&
            purchaseOrder.poNumber &&
            purchaseOrder.poAmount && purchaseOrder.poAmount > 0) {
				if(state == 'update'){
					var record = viewmodel.get('selectedItem');
					totalPoAmount = (totalPoAmount - record.get('poAmount'));
				}
				console.log(totalAmount, (totalPoAmount + purchaseOrder.poAmount));
				if (totalAmount >= (totalPoAmount + purchaseOrder.poAmount)) {
					return true;
				}else{
					message = 'Total PO Value must less than or equal to Total BCJ Amount.';
				}
        }

		Ext.MessageBox.show({
            title: 'Invalid',
            msg: message,
            buttons: Ext.MessageBox.OK,
            scope: this,
            animateTarget: button,
            icon: Ext.MessageBox['ERROR']
        });
        return false;
    },
    initComponent: function () {
        var me = this,
            viewmodel = me.getViewModel();
        me.store = new Ext.create('Ext.data.Store', {});
       
        me.tbar = [{
            xtype: 'datefield',
            emptyText: 'Date*',
            editable: false,
            width: 180,
            bind: {
                hidden: '{button.tbar.hidden}',
                value: '{purchaseOrder.poDate}'
            }
        }, {
            xtype: 'textfield',
            emptyText: 'PO No*',
            listeners: {
                blur: function () {
                    // restrict blank space
                    this.setValue(this.getValue().trim());
                }
            },
            width: 200,
            bind: {
                hidden: '{button.tbar.hidden}',
                value: '{purchaseOrder.poNumber}'
            }
        }, {
            xtype: 'currencyfield',
            emptyText: 'Value ($)*',
            minValue: 0,
            flex: 1,
            bind: {
                hidden: '{button.tbar.hidden}',
                value: '{purchaseOrder.poAmount}'
            }
        }, {
            xtype: 'button',
            //text: 'Reset',
            bind: {
                disabled: '{button.reset.disabled}',
                hidden: '{button.reset.hidden}'
            },
            iconCls: 'fa fa-refresh',
            handler: function () {
                viewmodel.set('purchaseOrder', {});
                viewmodel.set('selectedItem', null);
                viewmodel.set('button.edit.text', 'Edit');
                viewmodel.set('button.edit.disabled', true);
            }
        }, '->', {
            xtype   : 'button',
            text    : 'Add',
            iconCls : 'fa fa-plus-circle',
            bind    : {
                disabled: '{button.add.disabled}',
                hidden: '{button.add.hidden}'
            },
            handler: function (button) {
                var record = viewmodel.get('purchaseOrder');
               
                if (me.isValid(button, 'add')) {
                    me.store.add(record);
                    me.calculate();
                    viewmodel.set('purchaseOrder', {});
                }
            }
        },{
            xtype   : 'button',
            bind: {
                text: '{button.edit.text}',
                disabled: '{button.edit.disabled}',
                hidden: '{button.edit.hidden}'
            },
            width: 90,
            iconCls : 'fa fa-plus-circlefa fa-pencil-square-o',
            handler: function (button) {
                var isUpdate = (viewmodel.get('button.edit.text') == 'Update');
                var record = viewmodel.get('selectedItem');

                if (isUpdate && me.isValid(button, 'update')) {
                    record.set('poDate', viewmodel.get('purchaseOrder.poDate'));
                    record.set('poNumber', viewmodel.get('purchaseOrder.poNumber'));
                    record.set('poAmount', viewmodel.get('purchaseOrder.poAmount'));

                    me.calculate();
                    
                    // reset
                    viewmodel.set('purchaseOrder', {});
                    viewmodel.set('button.edit.text', 'Edit');
                    viewmodel.set('button.add.disabled', false);
                } else {
                    var data = record.getData();
                    viewmodel.set('purchaseOrder', data);
                    viewmodel.set('purchaseOrder.poDate', new Date(data.poDate));
                    viewmodel.set('button.edit.text', 'Update');
                    viewmodel.set('button.add.disabled', true);
                }
            }
        }];
        
        me.listeners = {
            selectionchange: function (model, records) {
                var record = records[0],
                    viewmodel = me.getViewModel();

                if (record) {
                    viewmodel.set('purchaseOrder', {});
                    viewmodel.set('button.edit.text', 'Edit');
                    viewmodel.set('button.add.disabled', false);

                    viewmodel.set('button.edit.disabled', false);
                }
            }
        };

        // BUILD COMPONENT - COLUMN & TOOLBAR
        me.columns = [{
            xtype: 'datecolumn',
            header: 'Date',
            dataIndex: 'poDate',
            width: 200,
            editor: {
                xtype: 'datefield',
                allowBlank: false,
                format: 'm/d/Y'
            }
        }, {
            text: 'PO No',
            width: 200,
            sortable: true,
            dataIndex: 'poNumber'
        }, {
            xtype: 'numbercolumn',
            text: 'Value ($)',
            flex: 1,
            sortable: true,
            dataIndex: 'poAmount',
            format: '$0,000.00'
        }, {
            menuDisabled: true,
            sortable: false,
            width: 50,
            xtype: 'actioncolumn',
            align: 'center',
            bind: {
                disabled: '{button.remove.disabled}',
                hidden: '{button.remove.hidden}'
            },
            items: [{
                iconCls: 'fa fa-trash-o',
                tooltip: 'Remove',
                width: 150,
                handler: function (grid, rowIndex, colIndex) {
                    var record = me.store.getAt(rowIndex),
                        selection = me.getSelectionModel();

                    me.store.remove(record);
                    me.calculate();
                }
            }]
        }];

        me.dockedItems = [{
            xtype: 'toolbar',
            dock: 'bottom',
            bind: {
                hidden: '{button.bbar.hidden}'
            },
            items: [
                '->', {
                    xtype: 'currencyfield',
                    margin: '0 0 0 0',
                    labelAlign: 'right',
                    fieldLabel: 'Total ($):',
                    bind: {
                        readOnly: true,
                        value: '{totalPoAmount}'
                    }
                }, {
                    xtype: 'currencyfield',
                    margin: '0 5 0 0',
                    labelAlign: 'right',
                    fieldLabel: 'Pending ($):',
                    bind: {
                        readOnly: true,
                        value: '{pendingPoAmount}'
                    }
                }
            ]
        }];

        me.callParent(arguments);
    },
    /*
    Display Pending Value (Pending Value=Total BCJ Value-Total PO Value), 
    Total PO Value must less than or equal to Total BCJ Value, 
    if Total PO Value greater than Total BCJ Value not allow to submit form
    */
    calculate: function () {
        var me = this,
            viewmodel = me.getViewModel(),
            totalPoAmount = 0;

        me.store.each(function (record) {
            totalPoAmount += record.get('poAmount');
        });
		
		viewmodel.set('totalPoAmount', totalPoAmount);
		viewmodel.set('pendingPoAmount', (viewmodel.get('totalAmount') - totalPoAmount));	
    }
    
});
