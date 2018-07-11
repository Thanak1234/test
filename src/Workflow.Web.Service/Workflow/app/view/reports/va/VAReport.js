Ext.define("Workflow.view.reports.va.VAReport", {
    xtype: 'report-va',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-va",
    viewModel: {
        type: "report-va"
    },
    report: {
        criteria: 'report-vacriteria',
        url: 'api/iavafreport'
    },
    buildColumns: function (columns, columnNames) {
        var me = this;
        columns.push({
            text: 'TYPE OF ADJUSTMENT',
            sortable: false,
            visibleIndex: 101,
            width: 175,
            dataIndex: 'adjType'
        });

        columns.push({
            text: 'REMARK',
            sortable: false,
            visibleIndex: 102,
            width: 175,
            dataIndex: 'remark'
        });

        //columns.push({
        //    text: 'GAMING DATE',
        //    renderer: Ext.util.Format.dateRenderer('d/m/Y'),
        //    sortable: false,
        //    visibleIndex: 103,
        //    width: 175,
        //    dataIndex: 'gamingDate'
        //});

        //columns.push({
        //    text: 'INCIDENT REPORT REF',
        //    sortable: false,
        //    visibleIndex: 104,
        //    width: 175,
        //    dataIndex: 'incidentRptRef',
        //    renderer: function (value, metaData, record) {
        //        return value ? Ext.String.format('<a href="#icd-request-form/SN={0}_99999">{1}</a>', record.get('instanceId'), value) : null;                
        //    }
        //});

        //columns.push({
        //    text: 'MCID/LOCN',
        //    sortable: false,
        //    visibleIndex: 105,
        //    width: 175,
        //    dataIndex: 'mcidLocn'
        //});

        //columns.push({
        //    text: 'TYPE OF VARIANCE',
        //    sortable: false,
        //    visibleIndex: 106,
        //    width: 175,
        //    dataIndex: 'varianceType'
        //});

        //columns.push({
        //    text: 'AREA',
        //    sortable: false,
        //    visibleIndex: 107,
        //    width: 175,
        //    dataIndex: 'area'
        //});

        //columns.push({
        //    text: 'REPORT COMPARISON',
        //    sortable: false,
        //    visibleIndex: 108,
        //    width: 175,
        //    dataIndex: 'rptComparison'
        //});

        //columns.push({
        //    text: 'SUBJECT',
        //    sortable: false,
        //    visibleIndex: 109,
        //    width: 175,
        //    dataIndex: 'subject'
        //});

        //columns.push({
        //    text: 'AMOUNT(USD)',
        //    sortable: false,
        //    visibleIndex: 110,
        //    width: 175,
        //    renderer: function(value){
        //        return Ext.util.Format.number(value, '$0,000.00');
        //    },
        //    dataIndex: 'amount'
        //});

        //columns.push({
        //    text: 'COMMENT',
        //    sortable: false,
        //    visibleIndex: 111,
        //    width: 175,
        //    dataIndex: 'comment'
        //});

        return columns;
    }
});


