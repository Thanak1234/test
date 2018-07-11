Ext.define('Workflow.view.mtf.dashboard.GDP', {
    extend: 'Ext.data.Store',
    alias: 'store.gdp',
    autoLoad: true,
    proxy: {
        type: 'ajax',
        url: Workflow.global.Config.baseUrl + 'api/mtfrequest/patient-dashboard-linechart',
        reader: {
            type: 'json'
        }
    },
    fields: ['DATEPATH', 'FIT_TO_WORK', 'UNFIT_TO_WORK', 'ALL_PATIENTS']
});

Ext.define('Workflow.view.mtf.dashboard.PatientLineChartController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.patient-line-chart',

    onAxisLabelRender: function (axis, label, layoutContext) {
        var value = layoutContext.renderer(label);
        return value;
    },
    
    getSeriesConfig: function (field, title) {
        return {
            type: 'area',
            title: title,
            xField: 'DATEPATH',
            yField: field,
            style: {
                opacity: 0.60
            },
            marker: {
                opacity: 0,
                scaling: 0.01,
                fx: {
                    duration: 200,
                    easing: 'easeOut'
                }
            },
            highlightCfg: {
                opacity: 1,
                scaling: 1.5
            },
            tooltip: {
                trackMouse: true,
                renderer: function (tooltip, record, item) {
                    tooltip.setHtml(title + ' (' + record.get('DATEPATH') + '): ' + record.get(field));
                }
            }
        };
    },

    onAfterRender: function () {
        var me = this,
            chart = me.lookupReference('chart');

        chart.setSeries([
            me.getSeriesConfig('ALL_PATIENTS', 'All Patients'),
            me.getSeriesConfig('FIT_TO_WORK', 'Fit To Work'),
            me.getSeriesConfig('UNFIT_TO_WORK', 'Unfit To Work')
        ]);
    }

});

Ext.define('Workflow.view.mtf.dashboard.PatientLineChartPanel', {
    extend: 'Ext.Panel',
    xtype: 'patient-line-chart',
    controller: 'patient-line-chart',
    requires: ['Ext.chart.series.Area'],
    iconCls: 'fa fa-area-chart',
    items: [{
        xtype: 'cartesian',
        reference: 'chart',
        width: '100%',
        height: 380,
        insetPadding: '40 40 40 40',
        store: {
            type: 'gdp'
        },
        legend: {
            docked: 'bottom'
        },
        sprites: [{
            type: 'text',
            text: 'Patient take leave of fit/unfit to work in 12 months',
            fontSize: 14,
            width: 100,
            height: 80,
            x: 40, // the sprite x position
            y: 20  // the sprite y position
        }, {
            type: 'text',
            text: 'Data: Gross domestic product based on purchasing-power-parity (PPP) valuation of country GDP. Figures for FY2014 are forecasts.',
            fontSize: 10,
            x: 12,
            y: 525
        }, {
            type: 'text',
            text: 'Source: http://www.imf.org/ World Economic Outlook Database October 2014.',
            fontSize: 10,
            x: 12,
            y: 540
        }],
        axes: [{
            type: 'numeric',
            position: 'left',
            fields: ['FIT_TO_WORK', 'UNFIT_TO_WORK', 'ALL_PATIENTS'],
            title: 'Number of Patients',
            grid: true,
            //minimum: 100,
            //maximum: 3000,
            majorTickSteps: 10,
            renderer: 'onAxisLabelRender'
        }, {
            type: 'category',
            position: 'bottom',
            fields: 'DATEPATH',
            label: {
                rotate: {
                    degrees: -45
                }
            }
        }]
        // No 'series' config here,
        // as series are dynamically added in the controller.
    }],

    listeners: {
        afterrender: 'onAfterRender'
    }

});