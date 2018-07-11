Ext.define("Workflow.view.itapp.Development", {
    extend: "Ext.form.Panel",
    xtype: 'development',
    border: true,
    layout: 'column',
    defaults: {
        xtype: 'datefield',
        margin: 10
    },
    margin: '0 0 10 0',
    width: '100%',
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'Development',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                columnWidth: 0.5,
                fieldLabel: 'Start Date',
                altFormats: 'c',
                format: 'd/m/Y',
                allowBlank: false,
                bind: {
                    value: '{ProjectDev.StartDate}',
                    readOnly: '{readOnlyDev}'
                }

            },
            {
                columnWidth: 0.5,
                fieldLabel: 'End Date',
                altFormats: 'c',
                format: 'd/m/Y',
                allowBlank: false,
                bind: {
                    value: '{ProjectDev.EndDate}',
                    readOnly: '{readOnlyDev}'
                }

            },
            {
                xtype: 'textarea',
                columnWidth: 1,
                fieldLabel: 'Remark',
                bind: {
                    value: '{ProjectDev.Remark}',
                    readOnly: '{readOnlyDev}'
                }
            }
        ];

        me.callParent(arguments);
    }
});
