Ext.define("Workflow.view.admsr.Information", {
    extend: "Ext.form.Panel",
    xtype: 'admsr-information',
    border: true,
    layout: 'column',
    margin: '0 0 10 0',
    defaults: {
        xtype: 'textarea',
        width: '100%',
        margin: '5 10',
        columnWidth: 1,
        labelWidth: 88
    },
    width: '100%',
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'Request Information',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'label',
                html: 'Description of Request <span class="req" style="color:red">*</span>:'
            }, {
                xtype: 'textarea',
                allowBlank: false,
                hiddenLabel: true,
                bind: {
                    readOnly: '{config.information.readOnly}',
                    value: '{information.dr}'
                }
            }, {
                xtype: 'label',
                html: 'Detail of service request/justification <span class="req" style="color:red">*</span>:'
            }, {
                xtype: 'textarea',
                allowBlank: false,
                hiddenLabel: true,
                bind: {
                    readOnly: '{config.information.readOnly}',
                    value: '{information.dsrj}'
                }
            }, {
                xtype: 'checkbox',
                boxLabel: 'Send to Line of Department',
                bind: {
                    readOnly: '{config.information.readOnly}',
                    value: '{information.slod}'
                }
            }
        ];

        me.callParent(arguments);
    }
});
