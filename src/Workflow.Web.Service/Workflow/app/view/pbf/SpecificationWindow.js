/**
 *Author : Phanny 
 *
 */
Ext.define("Workflow.view.pbf.SpecificationWindow", {
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",

    requires: [
        "Workflow.view.pbf.SpecificationWindowController",
        "Workflow.view.pbf.SpecificationWindowModel"
    ],
    iconCls: 'fa fa-gears',
    controller: "pbf-specificationwindow",
    viewModel: {
        type: "pbf-specificationwindow"
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
                labelWidth: 180,
                layout: 'form',
                xtype: 'container'
            },
            items: [{
                    xtype: 'lookupfield',
                    fieldLabel: 'Item Name',
                    namespace: '[EVENT].[SPECIFICATION].[NAME]',
                    isChild: true,
                    bind: {
                        value: '{itemId}',
                        readOnly: '{!editable}'
                    },
                    listeners: {
                        select: function(combo){
                            me.getViewModel().set('name', combo.getRawValue());
                        },
                        beforerender: function (elm, eOpts) {
                            var me = this;
                            var store = this.getStore();
                            store.getProxy().extraParams = {
                                name: me.namespace,
                                parentId: -1
                            };
                            store.load();
                        }
                    }
                },{
                    xtype: 'textfield',
                    fieldLabel: 'Specificity (size, format, etc.)',
                    allowBlank: false,
                    bind: {
                        value: '{description}',
                        readOnly: '{readOnlyField}'
                    }
                }, {
                    xtype: 'numberfield',
                    fieldLabel: 'QTY',
                    minValue: 1,
                    allowBlank: false,
                    bind: {
                        value: '{quantity}',
                        readOnly: '{readOnlyField}'
                    }
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'Item Nr.',
                    readOnly: true,
                    bind: {
                        value: '{itemRef}',
                        hidden: '{!visibleNr}'
                    }
                }
            ]
        }];

        me.callParent(arguments);
    }
});
