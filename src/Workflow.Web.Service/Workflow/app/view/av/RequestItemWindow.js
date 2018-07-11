/**
 *Author : Phanny 
 *
 */
Ext.define("Workflow.view.av.RequestItemWindow", {
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",

    requires: [
        "Workflow.view.av.RequestItemWindowController",
        "Workflow.view.av.RequestItemWindowModel"
    ],

    controller: "av-requestitemwindow",
    viewModel: {
        type: "av-requestitemwindow"
    },

    initComponent: function () {
        var me = this;
        //

        me.items = [{
            xtype: 'form',
            frame: false,
            bodyPadding: '10 10 0',
            reference: 'form',
            method: 'POST',
            defaultListenerScope: true,
            defaults: {
                flex: 1,
                anchor: '100%',
                msgTarget: 'side',
                labelWidth: 150,
                layout: 'form',
                xtype: 'container'
            },
            items: [{
                    fieldLabel: 'Item',
                    displayField: 'itemName',
                    valueField: 'id',
                    typeAhead: false,
                    reference: 'item',
                    xtype: 'combo',
                    allowBlank: false,
                    forceSelection: true,
                    bind: {
                        store: '{itemStore}',
                        value: '{itemId}',
                        rawValue: '{itemName}',
                        readOnly: '{readOnlyField}'
                    }
                }, {
                    xtype: 'numberfield',
                    fieldLabel: 'Quantity',
                    minValue: 1,
                    allowBlank: false,
                    allowDecimals: false,
                    bind: {
                        value: '{quantity}',
                        readOnly: '{readOnlyField}'
                    }
                }, {
                    fieldLabel: 'Comment',
                    xtype: 'textarea',
                    allowBlank: true,
                    bind: { value: '{comment}', readOnly: '{readOnlyField}' }
                },
                    {
                        xtype: 'label',
                        text: '* If characters more than 2000, please attach your file for detail',
                        margin: '0 150'
                    }
            ]
        }];

        me.callParent(arguments);
        var viewmodel = me.getViewModel(),
            itemStore = viewmodel.getStore('itemStore'),
            scopeType = viewmodel.get('scopeType');
        
        if (scopeType) {
            itemStore.getProxy().extraParams = {
                itemTypeName: scopeType.toUpperCase()
            };
            itemStore.load();
        }
    }
});
