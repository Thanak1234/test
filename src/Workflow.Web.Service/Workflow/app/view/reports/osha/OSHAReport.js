Ext.define("Workflow.view.reports.osha.OSHAReport", {
    xtype: 'report-osha',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-osha",
    viewModel: {
        type: "report-osha"
    },
    report: {
        criteria: 'report-oshacriteria',
        url: 'api/oshareport'
    },
    buildColumns: function (columns, columnNames) {
        var me = this;

        columns.push({
            text: 'NATURE/TYPE OF THE ACCIDENT',
            sortable: true,
            visibleIndex: 101,
            width: 250,
            dataIndex: 'nta'
        });
        columns.push({
            text: 'LOCATION OF ACCIDENT/INCIDENT',
            sortable: true,
            visibleIndex: 102,
            width: 250,
            dataIndex: 'lai'
        });
        columns.push({
            text: 'DATE/TIME OF ACCIDENT/INCIDENT',
            sortable: true,
            visibleIndex: 103,
            width: 250,
            dataIndex: 'dta',
            renderer: Ext.util.Format.dateRenderer('d/m/Y')
        });
        columns.push({
            text: 'CAUSE OF ACCIDENT/INCIDENT',
            width: 250,
            sortable: true,
            visibleIndex: 104,
            width: 250,
            dataIndex: 'ca'
        });
        columns.push({
            text: "HOD/SUPERVISOR'S COMMENTS",
            sortable: true,
            visibleIndex: 105,
            width: 250,
            dataIndex: 'cmtSupervisor'
        });
        columns.push({
            text: 'OSHA - COMMENTS',
            sortable: true,
            visibleIndex: 106,
            width: 250,
            dataIndex: 'cmtOSHA'
        });
        columns.push({
            text: "ACTION TAKEN BY REQUESTOR'S HOD",
            sortable: true,
            visibleIndex: 107,
            width: 250,
            dataIndex: 'actReqHOD'
        });

        return columns;
    }
});


