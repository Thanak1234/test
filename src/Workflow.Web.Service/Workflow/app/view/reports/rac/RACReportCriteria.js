Ext.define("Workflow.view.reports.rac.RACReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-raccriteria',
    title: 'Request For Access Card - Criteria',
    viewModel: {
        type: "report-rac"
    },
    buildFields: function (fields) {
        var me = this;

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Information',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250,
                triggers: {
                    clear: {
                        type: 'clear',
                        weight: -1
                    }
                }
            },
            items: [
                {
                    xtype: 'combo',
                    fieldLabel: 'Information',
                    emptyText: 'ITEM',
                    margin: '0 5 0 0',
                    editable: false,
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
                    bind: {
                        selection: '{itemSelected}',
                        value: '{criteria.Item}'
                    }
                },
                {
                    xtype: 'combo',
                    editable: false,
                    forceSelect: true,
                    allowBlank: false,
                    emptyText: 'REASON',
                    fieldLabel: 'Reasons',
                    displayField: 'value',
                    valueField: 'value',
                    margin: '0 5 0 0',
                    disabled: true,
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
                        value: '{criteria.Reason}',
                        disabled: '{!itemSelected}',
                        filters: {
                            exactMatch: true,
                            property: 'filter',
                            value: '{itemSelected.filter}'
                        }
                    }
                },
                {
                    xtype: 'textfield',
                    emptyText: 'SERIAL NO',
                    bind: {
                        value: '{criteria.SerialNo}',
                    }
                }
            ]
        });

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Date Issue',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250,
                triggers: {
                    clear: {
                        type: 'clear',
                        weight: -1
                    }
                }
            },
            items: [
                {
                    xtype: 'datefield',
                    emptyText: 'START DATE',
                    margin: '0 5 0 0',
                    bind: {
                        value: '{criteria.DIStart}'
                    }
                },
                {
                    xtype: 'datefield',
                    emptyText: 'END DATE',
                    bind: {
                        value: '{criteria.DIEnd}'
                    }
                }
            ]
        });

        return fields;
    }
});