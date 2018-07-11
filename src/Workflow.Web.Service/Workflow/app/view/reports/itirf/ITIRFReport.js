Ext.define("Workflow.view.reports.itirf.ITIRFReport", {
    xtype: 'report-itirf',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-itirf",
    viewModel: {
        type: "report-itirf"
    },
    report: {
        criteria: 'report-itirfCriteria',
        url: 'api/itirfprocessinstant'
    },
    buildColumns: function (columns) {
       
        // columns.push({
        //     text: 'ITEM NAME',
        //     sortable: true,
        //     width: 240,
        //     dataIndex: 'ItemName',
        //     visibleIndex: 101
        // }, {
        //     text: 'ITEM MODEL',
        //     sortable: true,
        //     width: 240,
        //     dataIndex: 'ItemModel',
        //     visibleIndex: 102
        // }, {
        //     text: 'SERIAL NO',
        //     sortable: true,
        //     width: 200,
        //     dataIndex: 'SerialNo',
        //     visibleIndex: 103
        // }, {
        //     text: 'PART NO',
        //     sortable: true,
        //     width: 180,
        //     dataIndex: 'PartNo',
        //     visibleIndex: 104
        // }, {
        //     text: 'QTY',
        //     sortable: true,
        //     width: 150,
        //     dataIndex: 'Qty',
        //     visibleIndex: 105
        // }, {
        //     text: 'SEND DATE',
        //     sortable: true,
        //     width: 120,
        //     renderer: Ext.util.Format.dateRenderer('d/m/Y'),
        //     dataIndex: 'SendDate',
        //     visibleIndex: 106
        // }, {
        //     text: 'RETURN DATE',
        //     sortable: true,
        //     width: 120,
        //     renderer: Ext.util.Format.dateRenderer('d/m/Y'),
        //     dataIndex: 'ReturnDate',
        //     visibleIndex: 107
        // }, {
        //     text: 'VENDOR NAME',
        //     sortable: true,
        //     width: 150,
        //     dataIndex: 'Vendor',
        //     visibleIndex: 108
        // }, {
        //     text: 'EMAIL',
        //     sortable: true,
        //     width: 120,
        //     dataIndex: 'Email',
        //     visibleIndex: 109
        // }, {
        //     text: 'ADDRESS',
        //     sortable: true,
        //     width: 120,
        //     dataIndex: 'Address',
        //     visibleIndex: 110
        // }, {
        //     text: 'REMARK',
        //     sortable: true,
        //     width: 180,
        //     dataIndex: 'Remark',
        //     visibleIndex: 111
        // });

        return columns;
    }
});
