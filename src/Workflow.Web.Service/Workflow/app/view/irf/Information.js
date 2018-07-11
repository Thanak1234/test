Ext.define("Workflow.view.irf.Information", {
    extend: "Ext.form.Panel",
    xtype: 'irf-information',
    border: false,
    layout: {
        type: 'vbox',
        pack: 'start',
        align: 'stretch'
    },
    margin: '0 0 0 0',
    defaults: {
        xtype: 'panel',
        width: '100%',
        margin: '5 0',
        collapsible: true,
        iconCls: 'fa fa-cogs',
        border: true
    },
    initComponent: function () {
        var me = this;
        me.buildItems();
        me.callParent(arguments);
    },
    reset: function () {
        var me = this,
            items = me.items.items;
        Ext.each(items, function (item, index) {
            item.reset();
        });
    },
    validate: function(action) {
        var me = this,
             items = me.items.items,
            message = '';
        Ext.each(items, function (item, index) {
            var msg = item.validate(action);
            if (msg != '') {
                message = msg;
                return;
            }
        });
        return message;
    },
    buildItems: function() {
        var me = this;
        me.items = [{
                xtype: 'ngGridPanel',
                name: 'vendor',
                title: 'Vendor Information',
                require: true,
                crudWinSize: {
                    width: 600,
                    minHeight: 300
                },
                providedFields: [{
                    kind: 'lookup',
                    name: 'vendor',
                    editable: false,
                    reference: 'vendor',
                    allowBlank: false,
                    editable: true,
                    text: 'Vendor',
                    url: '/data/itirf-vendor.json',
                    displayField: 'vendor',
                    valueField: 'vendor',
                    onChange: function(cb, value){
                        var vm = Ext.getCmp('vendor-window-id').getViewModel();
                        vm.set('formInfo', vm.get('vendor.selection'));
                    }
                }, {
                    kind: 'phone',
                    name: 'contactNo',
                    allowBlank: false,
                    autoSizeColumn: true,
                    text: 'Contact Number'
                }, {
                    kind: 'email',
                    name: 'email',
                    autoSizeColumn: true,
                    allowBlank: true,
                    text: 'E-Mail',
                    bind: {
                        value: '{vendor.email}'
                    }
                }, {
                    kind: 'textarea',
                    name: 'address',
                    allowBlank: true,
                    width: 500,
                    text: 'Address'
                }]
            },
            {
                xtype: 'ngGridPanel',
                name: 'item',
                title: 'Items Information',
                require: true,
                crudWinSize: {
                    width: 600,
                    minHeight: 400
                },
                dataDefault: {
                    qty: 1
                },
                providedFields: [{
                    kind: 'lookup',
                    name: 'itemName',
                    reference: 'itemName',
                    allowBlank: false,
                    editable: true,
                    text: 'Item Name',
                    url: 'api/lookup/itirf/items',
                    displayField: 'name',
                    valueField: 'name'
                }, {
                    kind: 'lookup',
                    name: 'itemModel',
                    allowBlank: false,
                    editable: true,
                    text: 'Item Model',
                    url: 'api/lookup/itirf/item/models',
                    displayField: 'name',
                    valueField: 'name',
                    ref: {
                        name: 'itemName',
                        property: 'id',
                        filter: {
                            property: 'itemId'
                        }
                    }
                }, {
                    kind: 'text',
                    name: 'serialNo',
                    allowBlank: false,
                    text: 'Serial No'
                }, {
                    kind: 'text',
                    name: 'partNo',
                    allowBlank: false,
                    text: 'Part No'
                }, {
                    kind: 'numeric',
                    name: 'qty',
                    allowBlank: false,
                    minValue: 1,
                    text: 'Quantity'
                }, {
                    kind: 'date',
                    name: 'sendDate',
                    allowBlank: false,
                    text: 'Send Date'
                }, {
                    kind: 'datetime',
                    name: 'returnDate',
                    allowBlank: true,
                    text: 'Return Date',
                    ref: {
                        min: 'sendDate'
                    }
                }, {
                    kind: 'textarea',
                    name: 'remark',
                    allowBlank: true,
                    text: 'Remark'
                }]
            }
        ];
    }
});
