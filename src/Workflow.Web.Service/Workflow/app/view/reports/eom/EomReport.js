Ext.define("Workflow.view.reports.eom.EomReport", {
    xtype: 'report-eom',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-eom",
    viewModel: {
        type: "report-eom"
    },
    report: {
        criteria: 'report-eomcriteria',
        url: 'api/eomprocessinstant'
    },
    buildColumns: function (columns, columnNames) {
        var me = this;

        columnNames.add('REQUESTOR', 'EMPLOYEE NAME');

        columns.push({
            text: 'POSITION',
            sortable: true,
            visibleIndex: 51,
            width: 175,
            dataIndex: 'position'
        });

        columns.push({
            text: 'CURRENT ACTIVITY',
            sortable: true,
            visibleIndex: 101,
            width: 175,
            dataIndex: 'activity'
        });

        columns.push({
            text: 'EMPLOYEE ID',
            sortable: true,
            visibleIndex: 102,
            width: 175,
            dataIndex: 'empNo'
        });

        columns.push({
            text: 'DEPARTMENT',
            sortable: true,
            visibleIndex: 103,
            width: 175,
            dataIndex: 'fullDeptName'
        });

        columns.push({
            text: 'MONTH',
            sortable: true,
            renderer: Ext.util.Format.dateRenderer('M/Y'),
            visibleIndex: 104,
            width: 175,
            dataIndex: 'month'
        });

        columns.push({
            text: 'TOTAL',
            sortable: true,
            visibleIndex: 105,
            width: 175,
            dataIndex: 'total'
        });

        columns.push({
            text: 'REASON',
            sortable: true,
            visibleIndex: 106,
            width: 175,
            dataIndex: 'reason'
        });
        
        return columns;
    }
});
