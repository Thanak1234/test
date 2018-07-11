Ext.define("Workflow.view.rac.Issue", {
    extend: "Ext.form.Panel",
    xtype: 'rac-issue',
    border: true,
    layout: 'column',
    margin: '0 0 10 0',
    defaults: {
        xtype: 'textfield',
        width: '100%',
        margin: '5 10',
        columnWidth: .5,
        labelWidth: 125
    },
    width: '100%',
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'For Surveillance Use Only',
    initComponent: function () {
        var me = this;        

        me.items = [
            {
                xtype: 'datefield',
                allowBlank: false,
                altFormats: 'c',
                format: 'd/m/Y',
                fieldLabel: 'Date Issue',
                bind: {
                    readOnly: '{!editable}',
                    value: '{Information.IssueDate}'
                }
            },
            {
                fieldLabel: 'Serial No',
                allowBlank: false,
                bind: {
                    readOnly: '{!editable}',
                    value: '{Information.SerialNo}'
                }
            }
        ];

        me.callParent(arguments);
    }
});
