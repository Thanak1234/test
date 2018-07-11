Ext.define("Workflow.view.reports.vr.VRReport", {
    xtype: 'report-vr',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-vr",
    viewModel: {
        type: "report-vr"
    },
    report: {
        criteria: 'report-vrcriteria',
        url: 'api/vrreport'
    },
    buildColumns: function (columns, columnNames) {
        var me = this;
        columns.push({
            text: 'VOUCHER TYPE',
            sortable: false,
            visibleIndex: 101,
            width: 175,
            dataIndex: 'voucherType'
        });

        columns.push({
            text: 'QUANTITY REQUESTED',
            sortable: false,
            visibleIndex: 102,
            width: 175,
            renderer: function (value) {
                return Ext.util.Format.number(value, '0,000');
            },
            dataIndex: 'qtyRequest'
        });

        columns.push({
            xtype: 'checkcolumn',
            text: 'HOTEL VOUCHER',
            disabled: true,
            sortable: false,
            visibleIndex: 102,
            width: 175,
            dataIndex: 'isHotelVoucher'
        });

        columns.push({
            xtype: 'checkcolumn',
            text: 'GAMING VOUCHER',
            disabled: true,
            sortable: false,
            visibleIndex: 102,
            width: 175,
            dataIndex: 'isGamingVoucher'
        });

        columns.push({
            text: 'DATE REQUIRED',
            renderer: Ext.util.Format.dateRenderer('d/m/Y'),
            sortable: false,
            visibleIndex: 103,
            width: 175,
            dataIndex: 'dateRequired'
        });

        columns.push({
            text: 'VOUCHER NUMBER',
            sortable: false,
            visibleIndex: 104,
            width: 175,
            dataIndex: 'voucherNo'
        });

        columns.push({
            text: 'AVAILABLE STOCK',
            sortable: false,
            visibleIndex: 105,
            width: 175,
            renderer: function (value) {
                return Ext.util.Format.number(value, '0,000');
            },
            dataIndex: 'availableStock'
        });

        columns.push({
            text: 'MONTHLY UTILSATION',
            sortable: false,
            visibleIndex: 106,
            width: 175,
            renderer: function (value) {
                return Ext.util.Format.number(value, '0,000');
            },
            dataIndex: 'monthlyUtilsation'
        });

        columns.push({
            xtype: 'checkcolumn',
            text: 'IS THIS A REPRINT?',
            disabled: true,
            sortable: false,
            visibleIndex: 107,
            width: 175,
            dataIndex: 'isReprint'
        });

        columns.push({
            text: 'HEADER ON VOUCHER',
            sortable: false,
            visibleIndex: 108,
            width: 175,
            dataIndex: 'headerOnVoucher'
        });

        columns.push({
            text: 'DETAILED DESCRIPTION',
            sortable: false,
            visibleIndex: 109,
            width: 175,
            dataIndex: 'detail'
        });

        columns.push({
            text: 'JUSTIFICATION FOR REQUEST',
            sortable: false,
            visibleIndex: 110,
            width: 175,            
            dataIndex: 'justification'
        });

        columns.push({
            text: 'VALID FROM',
            renderer: Ext.util.Format.dateRenderer('d/m/Y'),
            sortable: false,
            visibleIndex: 111,
            width: 175,
            dataIndex: 'validFrom'
        });

        columns.push({
            text: 'VALID TO',
            renderer: Ext.util.Format.dateRenderer('d/m/Y'),
            sortable: false,
            visibleIndex: 112,
            width: 175,
            dataIndex: 'validTo'
        });

        columns.push({
            text: 'VALIDITY DESCRIPTION',
            sortable: false,
            visibleIndex: 113,
            width: 175,
            dataIndex: 'validity'
        });

        columns.push({
            text: 'IF DISCOUNT VOUCHER % OF DISCOUNT',
            sortable: false,
            visibleIndex: 114,
            width: 175,
            renderer: function (value) {
                return Ext.util.Format.number(value, '0%');
            },
            dataIndex: 'discount'
        });

        columns.push({
            xtype: 'checkcolumn',
            disabled: true,
            text: 'JOB TO BE DONE BY CREATIVE',
            sortable: false,
            visibleIndex: 115,
            width: 175,
            dataIndex: 'doneByCreative'
        });

        columns.push({
            xtype: 'checkcolumn',
            disabled: true,
            text: 'JOB TO BE DONE BY OUTSIDE VENDOR',
            sortable: false,
            visibleIndex: 116,
            width: 175,
            dataIndex: 'doneByOutsideVendor'
        });

        return columns;
    }
});


