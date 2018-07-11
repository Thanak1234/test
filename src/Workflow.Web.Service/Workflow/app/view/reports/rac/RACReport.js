Ext.define("Workflow.view.reports.rac.RACReport", {
    xtype: 'report-rac',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-rac",
    viewModel: {
        type: "report-rac"
    },
    report: {
        criteria: 'report-raccriteria',
        url: 'api/racreport'
    },
    buildColumns: function (columns, columnNames) {
        var me = this;
        columns.push({
            text: 'ITEM',
            sortable: false,
            visibleIndex: 101,
            width: 175,
            dataIndex: 'item'
        });

        columns.push({
            text: 'REASON',
            sortable: false,
            visibleIndex: 102,
            width: 175,
            dataIndex: 'reason'
        });

        columns.push({
            text: 'REMARK',
            sortable: false,
            visibleIndex: 103,
            width: 175,
            dataIndex: 'remark'
        });

        columns.push({
            text: 'DATE ISSUE',
            sortable: false,
            visibleIndex: 104,
            width: 175,
            renderer: Ext.util.Format.dateRenderer('d/m/Y'),
            dataIndex: 'dateissue'
        });

        columns.push({
            text: 'SERIAL NO',
            sortable: false,
            visibleIndex: 105,
            width: 175,
            dataIndex: 'serialno'
        });

        return columns;
    }
});


