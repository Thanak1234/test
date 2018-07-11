Ext.define("Workflow.view.reports.admcpp.AdmCppReport", {
    xtype: 'report-admcpp',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-admcpp",
    viewModel: {
        type: "report-admcpp"
    },
    report: {
        criteria: 'report-admcppcriteria',
        url: 'api/admcppprocessinstant'
    },
    buildColumns: function (columns, columnNames) {
        var me = this;

        columns.push({
            text: 'MODEL',
            sortable: true,
            visibleIndex: 101,
            width: 175,
            dataIndex: 'model'
        });

        columns.push({
            text: 'COLOR',
            sortable: true,
            visibleIndex: 102,
            width: 175,
            dataIndex: 'color'
        });

        columns.push({
            text: 'PLAT NO',
            sortable: true,
            visibleIndex: 103,
            width: 175,
            dataIndex: 'platNo'
        });

        columns.push({
            text: 'YEAR OF PRODUCTION',
            visibleIndex: 104,
            sortable: true,
            width: 250,
            dataIndex: 'yop'
        });

        columns.push({
            text: 'ISSUE DATE',
            visibleIndex: 105,
            sortable: true,
            width: 175,
            dataIndex: 'issueDate',
            renderer: Ext.util.Format.dateRenderer('d/m/Y')
        });

        columns.push({
            text: 'CAR PARK S/N',
            visibleIndex: 106,
            sortable: true,
            width: 250,
            dataIndex: 'cpsn'
        });

        columns.push({
            text: 'REMARK',
            visibleIndex: 107,
            sortable: true,
            width: 350,
            dataIndex: 'remark'
        });
        
        return columns;
    }
});
