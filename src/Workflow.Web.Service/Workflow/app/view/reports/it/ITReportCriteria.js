Ext.define("Workflow.view.reports.it.ITReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-itCriteria',
    title: 'IT - Criteria',
    viewModel: {
        type: "report-it"
    },
    buildFields: function (fields) {
        var me = this;

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Section',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [
                {
                    xtype: 'combo',
                    name: 'section',
                    queryMode: 'local',
                    reference: 'cboSession',
                    editable: false,
                    fieldLabel: 'SECTION',
                    displayField: 'sessionName',
                    emptyText: 'SECTION <ALL>', 
                    valueField: 'id',
                    publishes: 'value',
                    margin: '0 5 0 0',
                    bind: {
                        selection: '{sessionSelection}',
                        store: '{sessionStore}',
                        value: '{criteria.ITSectionId}'
                    },
                    listeners: {
                        change: 'onSessionChanged'
                    },
                    triggers: {
                        clear: {
                            weight: 1,
                            cls: Ext.baseCSSPrefix + 'form-clear-trigger',
                            hidden: true,
                            handler: 'onClearClick'
                        }
                    }
                },
                {
                    xtype: 'combo',
                    name: 'ITEM',
                    queryMode: 'local',
                    displayField: 'itemName',
                    editable: false,
                    publishes: 'value',
                    reference: 'it_item',
                    valueField: 'id',
                    fieldLabel: 'ITEM',
                    emptyText: 'ITEM <ALL>',
                    margin: '0 5 0 0',
                    bind: {
                        selection: '{reqItem}',
                        value: '{criteria.ITItem}',
                        store: '{itemStore}',
                        disabled: '{!cboSession.value}'
                    },
                    listeners: {
                        change: 'onItemChanged'
                    },
                    triggers: {
                        clear: {
                            weight: 1,
                            cls: Ext.baseCSSPrefix + 'form-clear-trigger',
                            hidden: true,
                            handler: 'onClearClick'
                        }
                    }
                },
                {
                    xtype: 'combo',
                    name: 'itemType',
                    displayField: 'itemTypeName',
                    editable: false,
                    valueField: 'id',
                    publishes: 'value',
                    queryMode: 'local',
                    fieldLabel: 'ITEM TYPE',
                    reference: 'it_item_type',
                    emptyText: 'ITEM TYPE <ALL>',
                    margin: '0 5 0 0',
                    bind: {
                        selection: '{itemType}',
                        store: '{itemTypeStore}',
                        value: '{criteria.ITItemType}',
                        disabled: '{!it_item.value}'
                    },
                    listeners: {
                        change: 'onItemTypeChanged'
                    },
                    triggers: {
                        clear: {
                            weight: 1,
                            cls: Ext.baseCSSPrefix + 'form-clear-trigger',
                            hidden: true,
                            handler: 'onClearClick'
                        }
                    }
                },
                {
                    xtype: 'combo',
                    name: 'role',
                    displayField: 'roleName',
                    editable: false,
                    valueField: 'id',
                    publishes: 'value',
                    reference: 'it_item_role',                    
                    queryMode: 'local',
                    fieldLabel: 'ROLE',
                    emptyText: 'ROLE <ALL>',
                    margin: '0 5 0 0',
                    bind: {
                        selection: '{itemRole}',
                        store: '{itemRoleStore}',
                        value: '{criteria.ITRoleId}',
                        readOnly: '{!editable}',
                        disabled: '{!it_item_type.value}'
                    },
                    triggers: {
                        clear: {
                            weight: 1,
                            cls: Ext.baseCSSPrefix + 'form-clear-trigger',
                            hidden: true,
                            handler: 'onClearClick'
                        }
                    },
                    listeners: {
                        change: 'onSelectChanged'
                    }
                }]
            });

        return fields;
    }
});