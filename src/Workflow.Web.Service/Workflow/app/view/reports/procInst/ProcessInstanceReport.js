Ext.define("Workflow.view.reports.procInst.ProcessInstanceReport", {
    xtype: 'report-processinstancereport',
    extend: "Ext.panel.Panel",
    controller: "report-processinstancereport",
    viewModel: {
        type: "report-processinstancereport"
    },
    report: {
        criteria: 'report-processinstancecriteria',
        url: 'api/processinstant'
    },
    bodyPadding: 0,
    layout: 'border',
    ngconfig: {
        layout: 'fullScreen'
    },
    defaults: {
        collapsible: false
    },
    width: '100%',
    margin: 0,
    border: false,
    loadMask: true,
    initComponent: function () {
        var me = this;
        me.items = [];

        // Add criteria build
        me.items.push(
            me.criteriaBuilder(function (criteria) {
                return criteria;
            })
        );

        me.items.push(
            me.gridBuilder(function (columns) {
                var columnNames = new Ext.util.HashMap();
                columns = me.buildColumns(columns, columnNames); // update column and column name.
                Ext.Array.sort(columns, function (col1, col2) {
                    if (col1.visibleIndex > col2.visibleIndex)
                        return 1;
                    else
                        return -1;
                });
                me.renameColumns(columns, columnNames) // apply new name of column
                return columns;
            })
        );

        me.callParent(arguments);
    },
    renameColumns: function (columns, columnNames) {
        columnNames.each(function (key, value) {
            Ext.each(columns, function (column) {
                if (column.text == key) {
                    column.text = value;
                }
            });
        });
        return columns;
    },
    buildColumns: function (columns, columnNames) {
        return columns;
    },
    criteriaBuilder: function (fn) {
        var criteria = {
            xtype: this.report.criteria,
            reference: 'processInstanceCriteria',
            width: 1900,
            //folio: this.folio,
            region: 'north'
        };

        return fn(criteria);
    },
    gridBuilder: function (fn) {
        me = this;
        var columns = [{
                align: 'center',
                visibleIndex: 10,
                locked: true,
                dataIndex: 'workflowUrl',
                width: 35,
                renderer: function (workflowUrl, metaData, record) {
                    return '<a href="' + workflowUrl + '"><i class="fa fa-sitemap"></i></a>';
                }
            }, {
                text: 'FOLIO',
                visibleIndex: 20,
                width: 120,
                locked: true,
                dataIndex: 'folio',
                renderer: function (value, metaData, record) {
                    var formUrl = encodeURIComponent(record.get('formUrl'));
                    formUrl = formUrl.replace(/\./g, '__');
                    formUrl = '#k2/form/' + formUrl + '/' + value;
                    if (!record.get('noneK2')) {
                        formUrl = record.get('formUrl');
                    } 
                    //return '<a href="' + formUrl + '">' + value + '</a>'; // deprecated
                    return '<a href="' + record.get('formUrl') + '">' + value + '</a>';
                }
            }, {
                xtype: 'gridcolumn',
                visibleIndex: 30,
                text: 'REQUEST FORM',
                width: 200,
                dataIndex: 'application',
                bind: {
                    hidden: '{!showField.AppName}'
                }
            }, {
                text: 'SUBMITTER',
                visibleIndex: 40,
                sortable: true,
                width: 200,
                dataIndex: 'originator'
            }, {
                text: 'REQUESTOR',
                visibleIndex: 50,
                sortable: true,
                width: 200,
                dataIndex: 'requestor'
            }, {
                text: 'SUBMIT DATE',
                visibleIndex: 60,
                sortable: true,
                width: 150,
                renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                dataIndex: 'submitDate'
            }, {
                text: 'LAST ACTIVITY',
                visibleIndex: 70,
                sortable: true,
                width: 250,
                dataIndex: 'lastActivity'
            }, {
                text: 'LAST ACTION BY',
                visibleIndex: 80,
                sortable: true,
                width: 200,
                dataIndex: 'lastActionBy'
            }, {
                text: 'LAST ACTION DATE',
                visibleIndex: 90,
                sortable: true,
                renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                width: 150,
                dataIndex: 'lastActionDate'
            }, {
                text: 'ACTION',
                visibleIndex: 100,
                sortable: true,
                width: 100,
                dataIndex: 'action'
            }/*, {
                text: 'ACTION',
                sortable: true,
                width: 100,
                dataIndex: 'action'
            }*/
        ];
        
        var reportStore = me.getViewModel().storeInfo['report'];

        
        
        var grid = {
            xtype: 'grid',
            reference: 'processInstanceReport',
            controller: 'report-processinstancereport',
            viewModel: {
                type: 'report-processinstancereport'
            },
            viewConfig: {
                stripeRows: true,
                listeners: {
                    itemcontextmenu: function (view, rec, node, index, e) {
                        e.stopEvent();
                        var contextMenu = new Ext.menu.Menu({
                            items: [{
                                name: 'cx-resolve',
                                //iconCls: 'fa fa-wrench',
                                text: 'Tags',
                                handler: function (ctxItem) {
                                    var data = rec.getData();
                                    Ext.Ajax.request({
                                        url: Workflow.global.Config.baseUrl + ('api/processinstant/tags-request?requestHeaderId=' + data.id),
                                        headers: { 'Content-Type': 'application/json' },
                                        method: 'GET',
                                        success: function (response) {
                                            var tagIds = [];
                                            var records = Ext.decode(response.responseText);
                                            Ext.each(records, function (data) {
                                                tagIds.push(data.id);
                                            })
                                            me.tagsWindow(data, ctxItem, tagIds);
                                        },
                                        failure: function (response) {

                                        }
                                    });
                                    
                                }
                            },
                            {
                                name: 'cx-resolve',
                                text: 'Audit Log',
                                handler: function (ctxItem) {
                                    var data = rec.getData();
                                    var dialog = Ext.create('Workflow.view.dashboard.AuditDialog', {
                                        title: Ext.String.format('Audit Log ( {0} )', data.folio),
                                        extraParam: {
                                            procInstId: data.procInstId
                                        }
                                    });
                                    dialog.show(node);
                                }
                            }]
                        });
                        contextMenu.showAt(e.getXY());
                        return false;
                    }
                }
            },
            collapsible: false,
            headerBorders: true,
            columnLines: true,
            border: false,
            url: me.report.url,
            store: reportStore,
            autoHeight: true,
            flex: 1,
            //region: 'center',
            columns: fn(columns),
            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: reportStore,
                dock: 'bottom',
                displayInfo: true
            }]
        }

        var container = {
            xtype: 'container',
            region: 'center',
            layout: {
                type: 'vbox',
                align: 'stretch',
                padding: 0
            },
            items: [
                grid
            ]
        };

        if (me.buildExtraItems) {
            me.buildExtraItems(container.items);
        }

        
        return container;
    },
    tagsWindow: function (data, ctxItem, tagIds) {
        var window = Ext.create('Ext.window.Window', {
            xtype: 'form-vboxlayout',
            title: 'TAG - (' + data.folio + ')',
            width: 300,
            layout: 'fit',
            plain: true,
            viewModel: true,
            closable: false,
            items: [{
                xtype: 'form',
                defaultType: 'textfield',
                fieldDefaults: {
                    labelWidth: 120
                },

                layout: {
                    type: 'vbox',
                    align: 'stretch'
                },

                bodyPadding: 15,
                border: false,

                items: [{
                    xtype: 'tagfield',
                    emptyText: 'CURRENT ACTIVITY <ALL>',
                    minWidth: 250,
                    store: Ext.create('Ext.data.Store', {
                        autoLoad: true,
                        proxy: {
                            type: 'rest',
                            url: Workflow.global.Config.baseUrl + 'api/processinstant/tags?reqCode=' + data.requestCode,
                            reader: {
                                type: 'json'
                            }
                        }
                    }),
                    value: null,
                    reference: 'activity',
                    displayField: 'label',
                    valueField: 'id',
                    name: 'tagIds',
                    filterPickList: true,
                    queryMode: 'local',
                    publishes: 'value',
                    bind: {
                        value: tagIds
                    }
                }]
            }],

            buttons: [{
                text: 'Save',
                handler: function () {
                    var form = window.down('form').getForm();
                    var dataField = form.getValues();

                    Ext.Ajax.request({
                        url: Workflow.global.Config.baseUrl + ('api/processinstant/SaveTags?requestHeaderId=' + data.id +
                            '&tagIds=' + dataField.tagIds.toString()),
                        headers: { 'Content-Type': 'application/json' },
                        method: 'POST',
                        success: function (response) {
                            window.close();
                        },
                        failure: function (response) {
                            console.log('response', response);
                        }
                    });
                }
            }, {
                text: 'Close',
                handler: function () {
                    window.close();
                }
            }]
        });

        window.show();
    }
});
