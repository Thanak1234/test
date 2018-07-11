Ext.define("Workflow.view.reports.gmu.GMUReport", {
    xtype: 'report-gmu',
    extend: "Workflow.view.reports.procInst.ProcessInstanceReport",
    controller: "report-gmu",
    viewModel: {
        type: "report-gmu"
    },
    report: {
        criteria: 'report-gmucriteria',
        url: 'api/gmureport'
    },
    buildColumns: function (columns, columnNames) {
        var me = this;
        columns = [{
            align: 'center',
            visibleIndex: 1,
            locked: true,
            dataIndex: 'workflowUrl',
            width: 35,
            renderer: function (workflowUrl, metaData, record) {
                return '<a href="' + workflowUrl + '"><i class="fa fa-sitemap"></i></a>';
            }
        }, {
            text: 'FOLIO',
            visibleIndex: 2,
            width: 120,
            locked: true,
            dataIndex: 'folio',
            renderer: function (value, metaData, record) {
                var formUrl = encodeURIComponent(record.get('formUrl'));
                formUrl = formUrl.replace(/\./g, '__');
                formUrl = '#k2/form/' + formUrl + '/' + value;
                if (!record.get('noneK2')) {
                    formUrl = record.get('formUrl');
                }
                //return '<a href="' + formUrl + '">' + value + '</a>'; // deprecated
                return '<a href="' + record.get('formUrl') + '">' + value + '</a>';
            }
        }, {
            text: 'REQUESTOR',
            visibleIndex: 3,
            sortable: true,
            width: 200,
            dataIndex: 'requestor'
        }, {
            text: 'SUBMITTED BY',
            visibleIndex: 4,
            sortable: true,
            width: 200,
            dataIndex: 'originator'
        }, {
            text: 'SUBMITTED DATE',
            visibleIndex: 5,
            sortable: true,
            width: 150,
            renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
            dataIndex: 'submitDate'
        }, {
            text: 'TECHNICAL MANAGER APPROVAL',
            visibleIndex: 6,
            sortable: true,
            width: 250,
            dataIndex: 'm'
        }, {
            text: 'TECHNICAL MANAGER APPROVAL DATE',
            visibleIndex: 7,
            sortable: true,
            width: 250,
            renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
            dataIndex: 'mpd'
        }, {
            text: 'HOD APPROAL',
            visibleIndex: 8,
            sortable: true,
            width: 200,
            dataIndex: 'h'
        }, {
            text: 'HOD APPROVAL DATE',
            visibleIndex: 9,
            sortable: true,
            width: 250,
            renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
            dataIndex: 'hpd'
        }, {
            text: 'CONFIGURATION',
            visibleIndex: 10,
            sortable: true,
            width: 200,
            dataIndex: 'c'
        }, {
            text: 'CONFIGURATION DATE',
            visibleIndex: 11,
            sortable: true,
            width: 250,
            renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
            dataIndex: 'cpd'
        }, {
            text: 'VERIFICATION',
            visibleIndex: 12,
            sortable: true,
            width: 200,
            dataIndex: 'v'
        }, {
            text: 'VERIFICATION DATE',
            visibleIndex: 13,
            sortable: true,
            width: 250,
            renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
            dataIndex: 'vpd'
        }, {
            text: 'PROPERTY',
            width: 125,
            visibleIndex: 14,
            dataIndex: 'props'
        }, {
            text: 'GMID',
            width: 125,
            visibleIndex: 15,
            dataIndex: 'gmid'
        }, {
            text: 'GMU MAC',
            width: 125,
            visibleIndex: 16,
            dataIndex: 'macAddress'
        }, {
            text: 'IP',
            width: 180,
            visibleIndex: 17,
            dataIndex: 'ip'
        }, {
            text: 'Remark',
            width: 125,
            visibleIndex: 18,
            dataIndex: 'remark'
        }, {
            text: 'GMU',
            width: 700,
            visibleIndex: 19,
            dataIndex: 'gmus'
        }, {
            text: 'OTHER (Description)',
            width: 150,
            visibleIndex: 20,
            dataIndex: 'descr'
        }, {
            text: 'ACTION',
            visibleIndex: 21,
            sortable: true,
            width: 100,
            dataIndex: 'action'
        }, {
            text: 'LAST ACTION DATE',
            visibleIndex: 22,
            sortable: true,
            renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
            width: 150,
            dataIndex: 'lastActionDate'
        }];
        return columns;
    }
});


