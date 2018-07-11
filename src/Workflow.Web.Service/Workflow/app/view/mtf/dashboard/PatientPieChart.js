Ext.define('Workflow.view.mtf.dashboard.PatientPieChart', {
    extend: 'Ext.panel.Panel',
    xtype: 'patient-pie-chart',
    requires: [
        'Ext.chart.interactions.Rotate',
        'Ext.chart.series.Pie',
        'Ext.chart.PolarChart'
    ],
    iconCls: 'fa fa-pie-chart',
    bodyPadding: 0,
    items: [{
        xtype: 'polar',
        itemId: 'patient-status-polar',
        width: '100%',
        height: 250,
        insetPadding: 30,
        innerPadding: 20,
        store: new Ext.create('Ext.data.Store', {
            autoLoad: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/mtfrequest/patient-dashboard-piechart',
                reader: {
                    type: 'json'
                }
            }
        }),
        legend: {
            docked: 'right'
        },
        interactions: ['rotate'],
        sprites: [{
            type: 'text',
            text: 'Form state on ' + (Ext.Date.format(new Date(), 'Y-m-d')),
            x: 12,
            y: 24
        }],
        series: [{
            type: 'pie',
            angleField: 'COUNT',
            label: {
                field: 'QUEUE_STATUS',
                calloutLine: {
                    length: 60,
                    width: 3
                }
            },
            highlight: true,
            tooltip: {
                trackMouse: true,
                renderer: function (tooltip, record, item) {
                    tooltip.setHtml(record.get('QUEUE_STATUS') + ': ' + record.get('COUNT') + ' patient(s)');
                }
            }
        }]
    }]

});