
Ext.define("Workflow.view.bcj.RequestItemWindow", {
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",

    requires: [
        "Workflow.view.bcj.RequestItemWindowController",
        "Workflow.view.bcj.RequestItemWindowModel"
    ],

    controller: "bcj-requestitemwindow",
    viewModel: {
        type: "bcj-requestitemwindow"
    },

    initComponent: function () {
        var me = this;
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
            items: [
                {
                    xtype: 'textfield',
                    fieldLabel: 'Item',
                    allowBlank: false,
                    bind: {
                        value: '{item}',
                        readOnly: '{readOnlyField}'
                    }
                }, {
                    xtype: 'currencyfield',
                    fieldLabel: 'Unit Price',
                    minValue: 0.000000001,
                    allowBlank: false,
                    bind: {
                        value: '{unitPrice}',
                        readOnly: '{readOnlyField}'
                    }
                }, {
                    xtype: 'numberfield',
                    fieldLabel: 'Quantity',
                    minValue: 0.000000001,
                    allowBlank: false,
                    allowDecimals: true,
                    bind: {
                        value: '{quantity}',
                        readOnly: '{readOnlyField}'
                    }
                }
            ]
        }];

        me.callParent(arguments);
    }
});
