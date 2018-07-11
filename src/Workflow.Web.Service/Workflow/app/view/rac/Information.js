Ext.define("Workflow.view.rac.Information", {
    extend: "Ext.form.Panel",
    xtype: 'rac-information',
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
    title: 'Request Information',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'combo',
                fieldLabel: 'Items',
                editable: false,
                allowBlank: false,
                displayField: 'value',
                valueField: 'value',
                store: Ext.create('Ext.data.Store', {
                    fields: [
                        'filter',
                        'value'
                    ],
                    data: [
                        { filter: 1, value: 'New Access Card' },
                        { filter: 2, value: 'Replacement' },
                    ]
                }),
                listeners: {
                    select: function () {
                        var viewmodel = me.parent.getViewModel();
                        viewmodel.set('Information.Reason', null);
                    }
                },
                bind: {
                    readOnly: '{readOnly}',
                    value: '{Information.Item}',
                    selection: '{itemSelected}'
                }
            },
            {
                xtype: 'combo',
                editable: false,
                allowBlank: false,
                fieldLabel: 'Reasons',
                displayField: 'value',
                valueField: 'value',
                store: Ext.create('Ext.data.Store', {
                    fields: [
                        'filter',
                        'value'
                    ],
                    data: [
                        { filter: 1, value: 'New Access Card' },
                        { filter: 2, value: '1st Damage' },
                        { filter: 2, value: '2nd Damage' },
                        { filter: 2, value: '3rd and more Demage' },
                        { filter: 2, value: '1st Loss' },
                        { filter: 2, value: '2nd Loss' },                        
                        { filter: 2, value: '3rd and more Loss' }
                    ]
                }),
                bind: {
                    readOnly: '{readOnly}',
                    value: '{Information.Reason}',
                    disabled: '{!itemSelected}',
                    filters: {
                        exactMatch: true,
                        property: 'filter',
                        value: '{itemSelected.filter}'
                    }
                }
            },
            {
                xtype: 'textarea',
                fieldLabel: 'Remark',
                allowBlank: true,
                columnWidth: 1,
                bind: {
                    readOnly: '{readOnly}',
                    value: '{Information.Remark}'
                }
            },
            {
                xtype: 'label',
                columnWidth: 1,
                html: 'I understand & agree that the amount of US$5 & US$10 will be charged for the 1st & 2nd onwards for replacement of lost Access Card.<br/>Group Human Resource is authorized to deduct the amount above from my salary.'
            }
        ];

        me.callParent(arguments);
    }
});
