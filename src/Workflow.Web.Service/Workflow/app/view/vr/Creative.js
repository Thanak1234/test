Ext.define("Workflow.view.vr.Creative", {
    extend: "Ext.form.Panel",
    xtype: 'vr-creative',
    border: true,
    layout: 'column',
    margin: '0 0 10 0',
    defaults: {
        xtype: 'textfield',
        width: '100%',
        margin: '5 10',
        columnWidth: .5,
        labelWidth: 200
    },
    width: '100%',
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'Creative Review',
    initComponent: function () {
        var me = this;        

        me.items = [
            {
                xtype: 'radiogroup',
                fieldLabel: 'Job to be Done by',
                cls: 'x-check-group-alt',
                name: 'DoneByCreative',
                bind: {
                    value: '{JobDoneBy}',
                    readOnly: '{readOnlyCreative}'
                },
                items: [
                    { boxLabel: 'Creative', inputValue: true, checked: true },
                    { boxLabel: 'Outside Vendor', inputValue: false }
                ]
            }
        ];

        me.callParent(arguments);
    }
});
