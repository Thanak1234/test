Ext.define("Workflow.view.maintenace.Technician", {
    extend: "Ext.form.Panel",
    xtype: 'maintenace-technician',
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
    title: 'Technician',
    initComponent: function () {
        var me = this;        

        me.items = [
            {
                xtype: 'label',
                html: 'Description <span style="color:red">*<span>',
                columnWidth: 1
            },
            {
                xtype: 'textarea',
                labelWidth: 180,
                allowBlank: false,
                hiddenLabel: true,
                columnWidth: 1,
                bind: {
                    value: '{information.tcDesc}',
                    readOnly: '{viewSetting.container.technician.readOnly}'
                }
            }
        ];

        me.callParent(arguments);
    }
});
