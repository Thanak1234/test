Ext.define("Workflow.view.osha.ActionTaken", {
    extend: "Ext.form.Panel",
    xtype: 'osha-actionTaken',
    border: true,
    layout: 'column',
    margin: '0 0 10 0',
    defaults: {
        columnWidth: 1,
        margin: '20 10'
    },
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'Action Taken',
    initComponent: function () {
        var me = this;
        me.buildItems();
        me.callParent(arguments);
    },
    buildItems: function () {
        this.items = [
            {
                xtype: 'textarea',
                bind: {
                    value: '{information.at}',
                    readOnly: '{config.actionTaken.readOnly}'
                }
            }
        ];
    }
});
