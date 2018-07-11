Ext.define("Workflow.view.reports.av.AVReport", {
    xtype: 'report-av',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-av",
    viewModel: {
        type: "report-av"
    },
    report: {
        criteria: 'report-avCriteria',
        url: 'api/avjprocessinstant'
    },
    buildColumns: function (columns) {
        columns.push({
            text: 'ITEM TYPE NAME',
            sortable: true,
            width: 150,
            dataIndex: 'itemTypeName'
        });
        return columns;
    }
});
