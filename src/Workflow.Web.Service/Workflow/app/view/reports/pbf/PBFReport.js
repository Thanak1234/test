Ext.define("Workflow.view.reports.pbf.PBFReport", {
    xtype: 'report-pbf',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-pbf",
    viewModel: {
        type: "report-pbf"
    },
    report: {
        criteria: 'report-pbfCriteria',
        url: 'api/pbfreport'
    },
    buildColumns: function (columns, columnNames) {
        // add new column
        columns.push({
            text: 'PROJECT REF',
            visibleIndex: 4,
            sortable: true,
            width: 120,
            dataIndex: 'projectReference'
        }, {
            text: 'PROJECT NAME',
            visibleIndex: 5,
            sortable: true,
            width: 150,
            dataIndex: 'projectName'
        });

        return columns;
    }
});
