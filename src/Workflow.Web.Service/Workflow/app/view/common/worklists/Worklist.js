Ext.define('Workflow.view.common.worklist.Worklist', {
    extend: 'Ext.grid.Panel',
    xtype: 'common-worklist',
    requires: [
        'Workflow.view.common.worklist.OutOfficeWindow',
        'Ext.grid.feature.Feature'
    ],
    viewModel: {
        type: 'common-worklist'
    },
    controller: 'common-worklist',
    WorkflowStatus: {
        SLEEP: 'Sleep',
        OPEN: 'Open',
        AVAILABLE: 'Available',
        ALLOCATED: 'Allocated'
    },
    WorklistActions: {
        SHARE: 'Share',
        REDIRECT: 'Redirect'
    },
    ActionPrefix: {
        ACTION: 'ACTION',
        MORE: 'MORE',
        GENERAL: 'GENERAL'
    },
    stateful: true,
    bind: {
        selection: '{selectedRecord}'
    },
    listeners: {
        rowdblclick: 'viewItemAction',
        itemclick: 'onItemclick',
        sortchange: 'onSortChange',
        headerclick: 'onHeaderClick'
    },
    plugins: 'gridfilters',
    columnLines: true,
    initComponent: function () {
        var me = this;
        me.buildGridDockedItems();
        me.buildGridColumns();

        me.callParent(arguments);
    },
    buildGridColumns: function () {
        var me = this;

        function renderer(value, isBold) {
            var newValue = me.searchRegex ? value.toString().replace(me.searchRegex, '<span style="color:red;font-weight:500; ">$1</span>') : value;
            return newValue;
        }
        
        me.columns = [
            { xtype: 'rownumberer', locked: true, hidden: true },
            {
                width: 50,
                xtype: 'widgetcolumn',
                locked: true,
                menuDisabled: true,
                widget: {
                    xtype: 'button',
                    textAlign: 'left',
                    arrowCls: '',
                    iconCls: 'fa fa-chevron-right',
                    handler: function (btn) {
                        var rec = btn.getWidgetRecord();
                        me.setSelection(rec);
                    }
                },
                onWidgetAttach: function (column, widget, record) {
                    var data = record.getData();
                    var menus = me.buildOpenAction(data);
                    menus = menus.concat('-');
                    menus = menus.concat(me.buildActions(data));
                    menus = menus.concat(me.buildMoreAction(data));
                    widget.setMenu(menus);
                }
            },
            {
                xtype: 'actioncolumn',
                width: 28,
                sortable: false,
                locked: true,
                menuDisabled: true,
                align: 'center',
                dataIndex: 'status',
                items: [{
                    getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                        return v == me.WorkflowStatus.AVAILABLE ? 'fa fa-circle action-column-blue-color' : 'action-column-blue-color';
                    }
                }, {
                    getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                        var priority = r.get('priority');
                        return priority == 0 ? 'fa fa-exclamation action-column-red-color' : 'action-column-red-color';
                    }
                }]
            },
            {
                text: "FOLIO *",
                dataIndex: 'folio',
                width: 120,
                sortable: false,
                locked: true,
                renderer: function (value) {
                    return '<span style="color:blue;font-weight:500;">' + value + '</span>';
                }
            },{
                text: "REQUESTOR *",
                minWidth: 180,
                
                sortable: true,
                menuDisabled: true,
                dataIndex: 'requestor'
            },{
                text: "ACTIVITY NAME *",
                width: 160,
                sortable: true,
                //menuDisabled: true,
                dataIndex: 'activityName',
                filter: me.columnConfig.ACTIVITY_NAME.filter,
                hidden: me.columnConfig.ACTIVITY_NAME.hidden,
                renderer: renderer
            },
            {
                text: "WORKFLOW NAME *",
                flex: 1,
                sortable: true,
                menuDisabled: true,
                dataIndex: 'processName',
                filter: me.columnConfig.WORKFLOW_NAME.filter,
                hidden: me.columnConfig.WORKFLOW_NAME.hidden,
                renderer: renderer
            },{
                text: "OPENED BY *",
                width: 160,
                hidden: false,
                dataIndex: 'openedBy'
            },{
                text: "LAST ACTION DATE *",
                width: 150,
                sortable: true,
                //menuDisabled: true,
                dataIndex: 'activityStartDate',
                filter: me.columnConfig.LAST_ACTION_DATE.filter,
                hidden: me.columnConfig.LAST_ACTION_DATE.hidden,
                renderer: function (value) {
                    var date = Ext.util.Format.date(value, 'Y-m-d H:i:s');
                    return renderer(date);
                }
            },{
                text: "PRIORITY",
                width: 80,
                sortable: true,
                menuDisabled: true,
                dataIndex: 'priority',
                renderer: function (val) {

                    var priority = 'Low';

                    switch (val) {
                        case 0:
                            priority = '<span style="color:red">High</span>';
                            break;
                        case 1:
                            priority = '<span style="color:#FE9A2E">Medium</span>';
                            break;
                        case 2:
                            priority = '<span>Low</span>';
                            break;
                    }

                    return priority;
                }
            }
        ];
    },
    columnConfig: {
        EMP_NO: { hidden: true, filter: { type: 'string' } },
        OPENED_BY: { hidden: false, filter: { type: 'string' } },
        LAST_ACTION_DATE: { hidden: false, filter: { type: 'string' } },
        WORKFLOW_NAME: { hidden: false, filter: { type: 'string' } },
        ACTIVITY_NAME: { hidden: false, filter: { type: 'string' } }
    },

    buildGridDockedItems: function () {
        var me = this;

        me.dockedItems = [{
            dock: 'top',
            xtype: 'toolbar',
            items: [{
                xtype: 'button',
                iconCls: 'fa fa-refresh',
                text: 'Refresh',
                handler: 'onRefreshClickHandler'
            }, {
                xtype: 'button',
                iconCls: 'fa fa-unlock-alt',
                text: 'RELEASE <span style="color: red">*</span>',
                reference: 'releaseBtn',
                handler: 'onForceReleaseClick',
                bind: {
                    disabled: '{disableReleaseBtn}'
                }

            }, {
                xtype: 'button',
                iconCls: 'fa fa-users',
                text: 'Out of Office',
                reference: 'outOfficeBtn',
                handler: 'onOutOfficeClickHandler'
            }, '->', {
                xtype: 'combobox',
                fieldLabel: 'Status',
                labelWidth: 50,
                emptyText: 'Select Status',
                forceSelection: true,
                queryMode: 'Local',
                listeners: {
                    select: 'onStatusChangeHandler'
                },
                bind: {
                    store: '{statusStore}',
                    value: '{status}'
                },
                reference: 'cmbStatus',
                valueField: 'value',
                displayField: 'display'
            },
            {
                xtype: 'textfield',
                width: 275,
                emptyText: 'Search',
                enableKeyEvents: true,
                reference: 'searchTextBox',
                bind: {
                    value: '{searchText}'
                },
                triggers: {
                    clear: {
                        cls: 'x-form-clear-trigger',
                        handler: 'onSearchClearClick',
                        hidden: true
                    },
                    search: {
                        cls: 'x-form-search-trigger',
                        weight: 1,
                        handler: 'onSearchClick'
                    }
                },
                listeners: {
                    keypress: 'onSearchKeypress',
                    buffer: 0
                }
            }]
        }];
    },
    
    /* Quick Action - Builder */
    buildOpenAction: function (record) {
        var me = this;
        var menus = [];
        var status = record.status;
        var serial = record.serial;
        var folio = record.folio;
        var status = record.Status;
        var data = record.data;
        var prefix = me.ActionPrefix.GENERAL;
        
        return [{
            text: 'Open Form',
            iconCls: 'fa fa-folder-open',
            prefix: prefix,
            serial: serial,
            routeUrl: data,
            folio: folio,
            wItem: record,
            handler: 'onItemClick'
        }, {
            text: 'View Flow',
            prefix: me.ActionPrefix.GENERAL,
            serial: serial,
            viewFlow: record.viewFlow,
            folio: folio,
            iconCls: 'fa fa-sitemap',
            handler: 'onItemClick'
        }];
    },
    
    buildMoreAction: function (record) {
        var me = this, prefix = me.ActionPrefix.MORE;
        var isEscalatable = false;

        return [{
            prefix: prefix,
            serial: record.serial,
            payload: record,
            hidden: !record.isEscalatable,
            text: 'Redirect',
            iconCls: 'fa fa-user',
            handler: 'onItemClick'
        }, {
            prefix: prefix,
            serial: record.serial,
            payload: record,
            hidden: !record.isEscalatable,
            text: 'Share',
            iconCls: 'fa fa-share-alt-square',
            handler: 'onItemClick'
        }, {
            prefix: prefix,
            serial: record.serial,
            payload: record,
            hidden: (record.status != 'Open'),
            text: 'Release',
            iconCls: 'fa fa-unlock-alt',
            handler: 'onItemClick'
        }, {
			text: 'Audit Log',
			iconCls: 'fa fa-eye',
			handler: function (el) {
				var procInstId = parseInt(record.serial.split('_')[0]);
				var dialog = Ext.create('Workflow.view.dashboard.AuditDialog', {
					title: Ext.String.format('Audit Log ( {0} )', record.folio),
					extraParam: {
						procInstId: procInstId
					}
				});
				dialog.show(el);
			}
		}];
    },
    buildActions: function (record) {
        var me = this;
        var menus = [];
        var prefix = me.ActionPrefix.ACTION;
        var serial = record.serial;
        var actions = record.actions;
        
        for (var i = 0; i < actions.length; i++) {
            var name = actions[i];
            menus.push({
                text: name,
                prefix: prefix,
                serial: serial,
                hidden: (record.status == 'Open'),
                handler: 'onItemClick'
            });        
        }
        if(actions.length > 0){
            menus = menus.concat('-');
        }
        
        return menus;
    }
});