/**
 *Author : Phanny 
 *
 */
Ext.define("Workflow.view.bcj.AnalysisItemWindow", {
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",

    requires: [
        "Workflow.view.bcj.AnalysisItemWindowController",
        "Workflow.view.bcj.AnalysisItemWindowModel"
    ],

    controller: "bcj-analysisItemwindow",
    viewModel: {
        type: "bcj-analysisItemwindow"
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
                    fieldLabel: 'Description',
                    allowBlank: false,
                    bind: {
                        value: '{description}',
                        readOnly: '{readOnlyField}'
                    }
                }, {
                    xtype: 'currencyfield',
                    fieldLabel: 'Revenue',
                    minValue: 0.000000001,
                    allowBlank: false,
                    bind: {
                        value: '{revenue}',
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
