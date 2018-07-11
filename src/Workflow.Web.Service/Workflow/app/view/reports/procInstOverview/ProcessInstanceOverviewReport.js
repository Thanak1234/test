Ext.define("Workflow.view.reports.procInstOverview.ProcessInstanceOverview", {
    xtype: 'report-processinstanceoverview',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-processinstanceoverview",
    report: {
        criteria: 'report-processinstanceoverviewcriteria',
        url: 'api/processinstantoverviews'
    },
    buildColumns: function (columns) {
        
        return columns;
    },
    buildExtraItems: function (items) {
        var grid = items[0];

        grid.listeners = {
            rowclick: 'onProcInstRowClicked'
        };

        grid.bind = {
            selection: '{procSelectedRow}'
        };

        items.push({
            xtype: 'grid',
            title: 'Instance Activity',
            reference: 'instActivity',
            flex: 1,
            store: {
                type: 'procInstAI'
            },
            bind: {
                selection: '{actSelectedRow}'
            },
            listeners: {
                //rowdblclick: 'onActivityDoubleClicked',
                rowclick: 'onActivityInstRowClicked'
            },
            tbar: [
                {
                    xtype: 'button',
                    text: 'Refresh',
                    iconCls: 'fa fa-refresh',
                    handler: 'onRefreshClick',
                    bind: {
                        disabled: '{!procSelectedRow}'
                    }
                }
            ],
            columns: [
                {
                    text: 'ACTIVITY NAME',
                    width: 120,
                    sortable: true,
                    flex: 1,
                    dataIndex: 'activityName'
                }, {
                    text: 'DATE ASSIGNED',
                    sortable: true,
                    flex: 1,
                    renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                    dataIndex: 'startDate'
                }, {
                    text: 'DATE COMPLETED',
                    sortable: true,
                    flex: 1,
                    renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                    dataIndex: 'finishDate'
                }, {
                    text: 'PRIORITY',
                    sortable: true,
                    flex: 1,
                    dataIndex: 'priority'
                }, {
                    text: 'DURATION',
                    sortable: true,
                    width: 200,
                    renderer: function (s) {
                        return Workflow.FormatUtils.toHMS(s);
                    },
                    dataIndex: 'duration'
                }, {
                    text: 'STATUS',
                    sortable: true,
                    flex: 1,
                    dataIndex: 'status'
                }
            ]
        });


        items.push({
            xtype: 'grid',
            title: 'User Performance',
            reference: 'userPerformance',
            flex: 1,
            store: {
                type: 'userPerformance'
            },
            tbar: [
                {
                    xtype: 'button',
                    text: 'Refresh',
                    iconCls: 'fa fa-refresh',
                    handler: 'onPerformanceRefreshClick',
                    bind: {
                        disabled: '{!actSelectedRow}'
                    }
                }
            ],
            columns: [
                {
                    text: 'USER',
                    width: 120,
                    sortable: true,
                    dataIndex: 'destinationUserDisplay'
                }, {
                    text: 'ACTION',
                    sortable: true,
                    width: 200,
                    dataIndex: 'finalAction'
                }, {
                    text: 'DURATION',
                    sortable: true,
                    flex: 1,
                    dataIndex: 'duration',
                    renderer: function (s) {
                        return Workflow.FormatUtils.toHMS(s);
                    }
                }, {
                    text: 'DATE ASSIGNED',
                    sortable: true,
                    flex: 1,
                    renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                    dataIndex: 'startDate'
                }, {
                    text: 'DATE COMPLETED',
                    sortable: true,
                    flex: 1,
                    renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                    dataIndex: 'finishDate'
                }
            ]
        });
    }
});
