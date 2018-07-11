Ext.define("Workflow.view.vaf.Information", {
    extend: "Ext.form.Panel",
    xtype: 'vainfo',
    border: true,
    layout: {
        type: 'vbox',
        pack: 'start'
    },
    margin: '0 0 10 0',
    defaults: {
        xtype: 'textfield',
        width: '100%',
        margin: 10,
        labelWidth: 150
    },
    width: '100%',
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'Information',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'combo',
                fieldLabel: 'Type of Adjustment',
                allowBlank: false,
                editable: false,
                readOnly: false,
                store: ['Slot Operation','TR-Soft Count','TR-Table Drop'],
                bind: {
                    value: '{Information.AdjType}',
                    readOnly: '{readOnly}'
                },
                listeners: {
                    beforeselect: function (el, newValue, oldValue, eOpt) {
                        var count = me.parent.getReferences().outline.getStore().count();
                        if (count > 0) {
                            Ext.MessageBox.show({
                                title: 'Alert',
                                msg: 'Please clear all Outline Of Variance before changing Type of Adjustment.',
                                icon: Ext.MessageBox.INFO,
                                scope: this
                            });
                            return false;
                        }
                    }
                }
            },
            {
                xtype: 'textareafield',
                fieldLabel: 'Remarks',
                allowBlank: false,
                readOnly: false,
                bind: {
                    value: '{Information.Remark}',
                    readOnly: '{readOnly}'
                }
            }
        ];

        me.callParent(arguments);
    }
});
