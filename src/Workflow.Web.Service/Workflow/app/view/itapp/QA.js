Ext.define("Workflow.view.itapp.QA", {
    extend: "Ext.form.Panel",
    xtype: 'qa',
    border: true,
    layout: 'column',
    defaults: {
        xtype: 'datefield',
        margin: 10
    },
    width: '100%',
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'IT Application Manager QA Test',
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
                    value: '{ProjectQA.StartDate}',
                    readOnly: '{readOnlyQA}'
                }

            },
            {
                columnWidth: 0.5,
                fieldLabel: 'End Date',
                format: 'd/m/Y',
                altFormats: 'c',
                allowBlank: false,
                bind: {
                    value: '{ProjectQA.EndDate}',
                    readOnly: '{readOnlyQA}'
                }

            },
            {
                xtype: 'textarea',
                columnWidth: 1,
                fieldLabel: 'Remark',
                bind: {
                    value: '{ProjectQA.Remark}',
                    readOnly: '{readOnlyQA}'
                }

            }
        ];

        me.callParent(arguments);
    }
});
