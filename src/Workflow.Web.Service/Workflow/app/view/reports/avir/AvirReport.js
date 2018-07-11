Ext.define("Workflow.view.reports.avir.AvirReport", {
    xtype: 'report-avir',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-avir",
    viewModel: {
        type: "report-avir"
    },
    report: {
        criteria: 'report-avirCriteria',
        url: 'api/avirreport'
    },
    buildColumns: function (columns, columnNames) {
        var me = this;
        columnNames.add('REQUESTOR', 'RECEIVED BY');

        columns.push({
            text: 'INCIDENT DATE',
            sortable: true,
            visibleIndex: 2,
            width: 175,
            renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
            dataIndex: 'incidentDate'
        });
        
        columns.push({
            text: 'LOCATION',
            sortable: true,
            visibleIndex: 3,
            width: 175,
            dataIndex: 'location'
        });

        columns.push({
            text: 'ISSUE',
            sortable: true,
            visibleIndex: 4,
            width: 175,
            dataIndex: 'complaintRegarding'
        });

        columns.push({
            text: 'COMPLAINT RECEIVED',
            sortable: true,
            visibleIndex: 5,
            width: 175,
            dataIndex: 'reporterName'
        });

        columns.push({
            text: 'REMARK',
            sortable: true,
            visibleIndex: 6,
            width: 175,
            dataIndex: 'complaint'
        });
        
        return columns;
    }
});
