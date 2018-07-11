Ext.define("Workflow.view.reports.n2mwo.N2MWOReport", {
    xtype: 'report-n2mwo',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-n2mwo",
    viewModel: {
        type: "report-n2mwo"
    },
    report: {
        criteria: 'report-n2mwocriteria',
        url: 'api/n2mwoprocessinstant'
    },
    buildColumns: function (columns) {
        Workflow.view.reports.n2mwo.N2MWOReport
       
        columns.push({
            text: 'MODE',
            sortable: true,
            width: 240,
            dataIndex: 'Mode',
            visibleIndex: 101
        }, {
            text: 'REQUEST TYPE',
            sortable: true,
            width: 240,
            dataIndex: 'RequestType',
            visibleIndex: 102
        }, {
            text: 'REFERENCE NUMBER',
            sortable: true,
            width: 200,
            dataIndex: 'ReferenceNumber',
            visibleIndex: 103
        }, {
            text: 'LOCATION',
            sortable: true,
            width: 180,
            dataIndex: 'Location',
            visibleIndex: 104
        }, {
            text: 'SUB LOCATION',
            sortable: true,
            width: 150,
            dataIndex: 'SubLocation',
            visibleIndex: 105
        }, {
            text: 'COST CHARGABLE TO DEPARTMENT',
            sortable: true,
            width: 150,
            dataIndex: 'CCD',
            visibleIndex: 106
        }, {
            text: 'REMARK',
            sortable: true,
            width: 150,
            dataIndex: 'Remark',
            visibleIndex: 107
        }, {
            text: 'WORK REQUEST/JOB DESCRIPTION',
            sortable: true,
            width: 150,
            dataIndex: 'WRJD',
            visibleIndex: 108
        }, {
            text: 'ASSIGN DATE',
            sortable: true,
            width: 120,
            renderer: Ext.util.Format.dateRenderer('d/m/Y'),
            dataIndex: 'AssignDate',
            visibleIndex: 109
        }, {
            text: 'TECHNICIAN NAME',
            sortable: true,
            width: 80,
            dataIndex: 'TechnicianName',
            visibleIndex: 110
        }, {
            text: 'INSTRUCTION',
            sortable: true,
            width: 180,
            dataIndex: 'Instruction',
            visibleIndex: 111
        }, {
            text: 'WORKTYPE',
            sortable: true,
            width: 180,
            dataIndex: 'WorkType',
            visibleIndex: 112
        }, {
            text: 'TECHNICIAN DESCRIPTION',
            sortable: true,
            width: 180,
            dataIndex: 'TechnicianDesc',
            visibleIndex: 113
        });

        return columns;
    }
});
