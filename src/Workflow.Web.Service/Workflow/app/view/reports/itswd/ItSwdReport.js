Ext.define("Workflow.view.reports.itswd.ItSwdReport", {
    xtype: 'report-itswd',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-itswd",
    viewModel: {
        type: "report-itswd"
    },
    report: {
        criteria: 'report-itswdcriteria',
        url: 'api/itswdreport'
    },
    buildColumns: function (columns, columnNames) {
        var me = this;

        columns.push({
            text: 'APPLICATION',
            sortable: true,
            visibleIndex: 101,
            width: 175,
            dataIndex: 'application'
        });
        columns.push({
            text: 'PROPOSED CHANGE',
            sortable: true,
            visibleIndex: 102,
            width: 175,
            dataIndex: 'proposedChange'
        });
        columns.push({
            text: 'DESCRIPTION',
            sortable: true,
            visibleIndex: 103,
            width: 175,
            dataIndex: 'description'
        });
        columns.push({
            xtype: 'checkcolumn',
            text: 'BENEFITS OF CHANGE(COST SAVINGS)',
            width: 250,
            disabled: true,
            visibleIndex: 104,
            dataIndex: 'benefitCs'
        });
        columns.push({
            xtype: 'checkcolumn',
            text: 'BENEFITS OF CHANGE(INCREASE IN SALES)',
            disabled: true,
            visibleIndex: 105,
            width: 250,
            dataIndex: 'benefitIIS'
        });
        columns.push({
            xtype: 'checkcolumn',
            text: 'BENEFITS OF CHANGE(RISK MANAGEMENT)',
            disabled : true,
            visibleIndex: 106,
            width: 250,
            dataIndex: 'benefitRM'
        });
        columns.push({
            text: 'BENEFITS OF CHANGE(OTHER)',
            sortable: true,
            visibleIndex: 107,
            width: 250,
            dataIndex: 'benefitOther'
        });
        columns.push({
            text: 'PRIORITY CONSIDERATION-BENEFITS IN USD (POTENTIAL COST SAVINGS OR INCREASE OF REVENUE)',
            sortable: true,
            visibleIndex: 108,
            width: 250,
            dataIndex: 'priorityConsideration'
        });
        columns.push({
            text: 'COST ESTIMATES(HARDWARE COST)',
            sortable: true,
            visibleIndex: 109,
            width: 250,
            dataIndex: 'hc'
        });
        columns.push({
            text: 'COST ESTIMATES(SOFTWARE LICENSE COST)',
            sortable: true,
            visibleIndex: 110,
            width: 250,
            dataIndex: 'slc'
        });
        columns.push({
            text: 'COST ESTIMATES(SERVICE COST IN MAN-DAYS)',
            sortable: true,
            visibleIndex: 111,
            width: 250,
            dataIndex: 'scmd'
        });
        columns.push({
            text: 'IMPACT(RESOURCE OR SCHEDULE IMPACT, IF ANY)',
            sortable: true,
            visibleIndex: 112,
            width: 250,
            dataIndex: 'rsim'
        });
        columns.push({
            text: 'IMPACT(RISK OR ASSUMPTION THAT WE MADE, IF ANY)',
            sortable: true,
            visibleIndex: 113,
            width: 250,
            dataIndex: 'rawm'
        });
        columns.push({
            text: 'TARGET DELIVERY DATE',
            sortable: true,
            visibleIndex: 114,
            width: 250,
            dataIndex: 'deliveryDate'
        });
        columns.push({
            text: 'GO LIVE DATE',
            sortable: true,
            visibleIndex: 115,
            width: 250,
            dataIndex: 'goLiveDate',
            renderer: Ext.util.Format.dateRenderer('d/m/Y')
        });
        columns.push({
            text: 'DEVELOPMENT(START DATE)',
            sortable: true,
            visibleIndex: 116,
            width: 250,
            dataIndex: 'devStartDate',
            renderer: Ext.util.Format.dateRenderer('d/m/Y')
        });
        columns.push({
            text: 'DEVELOPMENT(END DATE)',
            sortable: true,
            visibleIndex: 117,
            width: 250,
            dataIndex: 'devEndDate',
            renderer: Ext.util.Format.dateRenderer('d/m/Y')
        });
        columns.push({
            text: 'DEVELOPMENT(REMARK)',
            sortable: true,
            visibleIndex: 118,
            width: 250,
            dataIndex: 'devRemark'
        });
        columns.push({
            text: 'IT APPLICATION MANAGER QA TEST(START DATE)',
            sortable: true,
            visibleIndex: 119,
            width: 250,
            dataIndex: 'qaStartDate',
            renderer: Ext.util.Format.dateRenderer('d/m/Y')
        });
        columns.push({
            text: 'IT APPLICATION MANAGER QA TEST(END DATE)',
            sortable: true,
            visibleIndex: 120,
            width: 250,
            dataIndex: 'qaEndDate',
            renderer: Ext.util.Format.dateRenderer('d/m/Y')
        });
        columns.push({
            text: 'IT APPLICATION MANAGER QA TEST(REMARK)',
            sortable: true,
            visibleIndex: 121,
            width: 250,
            dataIndex: 'qaRemark'
        });

        return columns;
    }
});


