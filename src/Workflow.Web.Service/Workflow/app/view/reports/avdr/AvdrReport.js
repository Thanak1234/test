Ext.define("Workflow.view.reports.avdr.AvdrReport", {
    xtype: 'report-avdr',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-avdr",
    viewModel: {
        type: "report-avdr"
    },
    report: {
        criteria: 'report-avdrCriteria',
        url: 'api/avrdreport'
    },
    buildColumns: function (columns, columnNames) {
        var me = this;
        columnNames.add('REQUESTOR', 'RECEIVED BY');

        columns.push({
            text: 'INCIDENT DATE',
            sortable: true,
            visibleIndex: 2,
            width: 175,
            renderer: Ext.util.Format.dateRenderer('d/m/Y'),
            dataIndex: 'incidentDate'
        });
        
        columns.push({
            text: 'LOCATION',
            sortable: true,
            visibleIndex: 3,
            width: 175,
            dataIndex: 'elocation'
        });

        columns.push({
            text: 'EQUIPMENT',
            sortable: true,
            visibleIndex: 4,
            width: 175,
            dataIndex: 'dle'
        });

        columns.push({
            text: 'FAULTY DESCRIPTION',
            sortable: true,
            visibleIndex: 5,
            width: 175,
            dataIndex: 'hedl'
        });

        columns.push({
            text: 'REMARK',
            sortable: true,
            visibleIndex: 6,
            width: 175,
            dataIndex: 'at'
        });

        return columns;
    }
});
