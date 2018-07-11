Ext.define("Workflow.view.reports.admsr.ADMSRReport", {
    xtype: 'report-admsr',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-admsr",
    viewModel: {
        type: "report-admsr"
    },
    report: {
        criteria: 'report-admsrcriteria',
        url: 'api/admsrreport'
    },
    buildColumns: function (columns, columnNames) {
        var me = this;

        columns.push({
            text: 'DESCRIPTION OF REQUEST',
            sortable: false,
            visibleIndex: 101,
            width: 175,
            dataIndex: 'dr'
        });

        columns.push({
            text: 'DETAIL OF SERVICE REQUEST/JUSTIFICATION',
            sortable: false,
            visibleIndex: 102,
            width: 175,
            dataIndex: 'dsrj'
        });

        columns.push({
            text: 'SEND TO LINE OF DEPARTMENT',
            sortable: false,
            visibleIndex: 103,
            width: 175,
            dataIndex: 'slod'
        });

        columns.push({
            text: 'SEND TO ADMIN\'S LINE OF DEPARTMENT',
            visibleIndex: 104,
            sortable: false,
            width: 250,
            dataIndex: 'salod'
        });

        columns.push({
            text: 'EMAIL NOTIFICATION - FINANCE',
            visibleIndex: 105,
            sortable: false,
            width: 175,
            dataIndex: 'efinance'
        });

        columns.push({
            text: 'EMAIL NOTIFICATION - COST CONTROL',
            visibleIndex: 106,
            sortable: false,
            width: 250,
            dataIndex: 'ecc'
        });

        columns.push({
            text: 'EMAIL NOTIFICATION - PURCHASING',
            visibleIndex: 107,
            sortable: false,
            width: 350,
            dataIndex: 'epurchasing'
        });
        
        return columns;
    }
});
