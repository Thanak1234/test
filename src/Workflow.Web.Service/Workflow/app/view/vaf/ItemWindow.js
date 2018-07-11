Ext.define("Workflow.view.vaf.ItemWindow", {
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",
    controller: "va-itemwindow",
    viewModel: {
        type: "va-itemwindow"
    },
    layout: 'fit',
    width: 1010,
    height: 400,
    resizable: false,
    isUndefine: function(v) {
        return v == undefined || v == null || v == 0;
    },
    initComponent: function () {
        var me = this;
        me.items = [
            {
            xtype: 'form',
            frame: false,
            bodyPadding: '10 10 0',
            reference: 'form',
            layout: 'column',
            defaults: {
                labelWidth: 125,
                xtype: 'textfield',
                columnWidth: 0.5,
                margin: 5,
                triggers: {
                    clear: {
                        type: 'clear',
                        weight: -1,
                        hideWhenEmpty: false
                    }
                }
            },
            items: [
                {
                    xtype: 'datefield',
                    fieldLabel: 'Gaming Date',
                    readOnly: true,
                    allowBlank: false,
                    bind: {
                        readOnly: '{readOnly}',
                        value: '{outline.GamingDate}'
                    }
                },
                {
                    xtype: 'combo',
                    fieldLabel: 'Incident Report Ref',
                    reference: 'incidentRef',
                    allowBlank: true,
                    readOnly: true,
                    displayField: 'Title',
                    valueField: 'ProcessInstanceId',
                    mode: 'remote',
                    forceSelection: true,
                    pageSize: 10,
                    minChars: 2,
                    bind: {
                        store: '{incidentRefStore}',
                        readOnly: '{readOnly}',
                        selection: '{selection}',
                        value: '{outline.ProcessInstanceId}'
                    },
                    listConfig: {
                        resizable: false,
                        loadingText: 'Searching...',
                        emptyText: 'No matching posts found.',
                        itemSelector: '.search-outline',
                        itemTpl: [
                            '<a class="tpl-list-employee">',
                                '{Title} ({Requestor})',
                            '</a>'
                        ]
                    },
                    listeners: {
                        change: function (el, n, o) {
                            if (Ext.isEmpty(n) || el.readOnly) {
                                el.getTrigger('edit').setHidden(true);
                            } else {
                                el.getTrigger('edit').setHidden(false);
                            }
                        },
                        beforequery: function (queryPlan, eOpts) {
                            if (!queryPlan.query) {
                                queryPlan.query = queryPlan.combo.rawValue;
                            }
                        }
                    },
                    triggers: {
                        edit: {
                            weight: 1,
                            cls: Ext.baseCSSPrefix + 'form-link-trigger',
                            hidden: true,
                            handler: 'onFormView'
                        }
                    }
                },
                {
                    allowBlank: false,
                    readOnly: true,
                    fieldLabel: 'MCID/Locn',
                    bind: {
                        readOnly: '{readOnly}',
                        value: '{outline.McidLocn}'
                    }
                },
                {
                    xtype: 'combo',
                    allowBlank: false,
                    fieldLabel: 'Area',
                    queryMode: 'local',
                    typeAhead: true,
                    displayField: 'value',
                    valueField: 'value',
                    bind: {
                        store: '{areaStore}',
                        readOnly: '{readOnly}',
                        value: '{outline.Area}',
                        filters: {
                            exactMatch: true,
                            property: 'filter',
                            value: '{adjustType}'
                        }
                    }
                },
                {
                    xtype: 'combo',
                    allowBlank: false,
                    fieldLabel: 'Variance Type',
                    queryMode: 'local',
                    typeAhead: true,
                    displayField: 'value',
                    valueField: 'value',
                    bind: {
                        store: '{varianceTypeStore}',
                        readOnly: '{readOnly}',
                        value: '{outline.VarianceType}',
                        selection: '{varianceType}',
                        filters: {
                            exactMatch: true,
                            property: 'filter',
                            value: '{adjustType}'
                        }
                    }
                },
                {
                    xtype: 'combo',
                    allowBlank: false,
                    fieldLabel: 'Report Comparison',
                    queryMode: 'local',
                    typeAhead: true,
                    displayField: 'value',
                    valueField: 'value',
                    bind: {
                        store: '{reportsComparisonStore}',
                        readOnly: '{readOnly}',
                        value: '{outline.RptComparison}',
                        filters: {
                            exactMatch: true,
                            property: 'filter',
                            value: '{varianceType.id}'
                        }
                    }
                },
                {
                    xtype: 'combo',
                    allowBlank: false,
                    fieldLabel: 'Subject',
                    queryMode: 'local',
                    typeAhead: true,
                    displayField: 'value',
                    valueField: 'value',
                    bind: {
                        store: '{subjectStore}',
                        readOnly: '{readOnly}',
                        value: '{outline.Subject}',
                        filters: {
                            exactMatch: true,
                            property: 'filter',
                            value: '{varianceType.id}'
                        }
                    }
                },
                {
                    xtype: 'currencyfield',
                    fieldLabel: 'Amount(USD)',
                    format: '$0,000.00',
                    allowBlank: false,
                    readOnly: true,
                    bind: {
                        readOnly: '{readOnly}',
                        value: '{outline.Amount}'
                    }
                },
                {
                    xtype: 'textarea',
                    fieldLabel: 'Comment',
                    readOnly: true,
                    columnWidth: 1,
                    bind: {
                        readOnly: '{readOnly}',
                        value: '{outline.Comment}'
                    }
                }
            ]
        }];

        me.callParent(arguments);

        me.boundData();
    },
    boundData: function () {
        var me = this;
        var viewmodel = me.getViewModel();
        var itemStore = viewmodel.get('incidentRefStore');
        var query = viewmodel.get('outline.IncidentRptRef');
        if (query) {
            Ext.apply(itemStore.getProxy().extraParams, {
                query: query
            });
            itemStore.load();
        }
    }
});
