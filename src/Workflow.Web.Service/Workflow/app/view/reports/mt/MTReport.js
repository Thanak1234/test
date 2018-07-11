Ext.define("Workflow.view.reports.mt.MTReport", {
    xtype: 'report-mt',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-mt",
    viewModel: {
        type: "report-mt"
    },
    report: {
        criteria: 'report-mtCriteria',
        url: 'api/mtprocessinstant'
    },
    buildColumns: function (columns) {
        //columns.push({
        //    text: 'ITEM TYPE NAME',
        //    sortable: true,
        //    width: 150,
        //    dataIndex: 'itemTypeName'
        //});
        return columns;
    }
});
