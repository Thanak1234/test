Ext.define("Workflow.view.osha.OSHA", {
    extend: "Ext.form.Panel",
    xtype: 'osha-panel',
    border: true,
    layout: 'column',
    margin: '0 0 10 0',
    defaults: {
        columnWidth: 1,
        margin: '5 10 20 10'
    },
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'OSHA',
    initComponent: function () {
        var me = this;
        me.buildItems();
        me.callParent(arguments);
    },
    buildItems: function () {
        this.items = [
            {
                xtype: 'label',
                text: 'Additional comments/notes/recommendation if any: '
            },
            {
                xtype: 'textarea',
                bind: {
                    value: '{information.acnr}',
                    readOnly: '{config.osha.readOnly}'
                }
            }
        ];
    }
});
