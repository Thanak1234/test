Ext.define("Workflow.view.wm.WorklistManagementCriteria", {
    extend: "Ext.panel.Panel",
    xtype: 'wm-criteria',
    viewModel: {
        type: 'worklistmanagement'
    },
    layout: 'hbox',
    defaults: {
        border: false,
        xtype: 'panel',
        flex: 1,
        layout: 'vbox'
    },
    title: 'Worklist Criteria',
    frame: true,
    split: true,
    collapsible: true,
    bodyPadding: 10,
    initComponent: function () {
        var me = this;
        me.callParent(arguments);
    },
    items: [
        {
            defaults: {
                xtype: 'textfield',
                width: '100%',
                padding: 5
            },
            items: [
                {
                    fieldLabel: 'Request Code',
                    emptyText: 'Request Code',
                    bind: {
                        value: '{criteria.folio}'
                    }
                },
                {
                    xtype: 'combo',
                    reference: 'cmbProcess',
                    fieldLabel: 'Form',
                    emptyText: 'Form',
                    publishes: 'value',
                    forceSelection: true,
                    displayField: 'RequestDesc',
                    valueField: 'ProcessPath',
                    bind: {
                        value: '{criteria.form}',
                        store: '{forms}'
                    }
                },
                {
                    xtype: 'combo',
                    reference: 'cmbActivity',
                    emptyText: 'Activity',
                    fieldLabel: 'Activity',
                    displayField: 'DisplayName',
                    valueField: 'Name',
                    queryMode: 'remote',
                    forceSelection: true,
                    bind: {
                        disabled: '{!cmbProcess.value}',
                        value: '{criteria.activity}',
                        filters: {
                            property: 'ProcessPath',
                            value: '{cmbProcess.value}'
                        },
                        store: '{activities}'
                    }
                }
            ]
        },
        {
            defaults: {
                xtype: 'textfield',
                width: '100%',
                padding: 5
            },
            items: [
                {
                    fieldLabel: 'Destination',
                    emptyText: 'Destination',
                    xtype: 'employeePickup',
                    loadCurrentUser: true,
                    reference: 'employee',
                    bind: {
                        value: '{criteria.requestor}'
                    }
                },
                {
                    xtype: 'datefield',
                    fieldLabel: 'Worklist Date',
                    emptyText: 'Worklist Date',
                    bind: {
                        value: '{criteria.date}'
                    }
                }
            ]
        }
    ],
    buttons: [
        {
            xtype: 'button',
            iconCls: 'fa fa-search',
            text: 'Search',
            handler: 'onSearch'
        }
    ]
});