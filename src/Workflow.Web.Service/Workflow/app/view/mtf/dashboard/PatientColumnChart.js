Ext.define('Workflow.view.mtf.dashboard.store.Climate', {
    extend: 'Ext.data.Store',
    alias: 'store.climate',

    fields: [
        'month',
        'high',
        'low',
        {
            name: 'highF',
            calculate: function (data) {
                return data.high * 1.8 + 32;
            }
        },
        {
            name: 'lowF',
            calculate: function (data) {
                return data.low * 1.8 + 32;
            }
        }
    ],
    data: [
        { month: 'Jan', high: 14.7, low: 5.6 },
        { month: 'Feb', high: 16.5, low: 6.6 },
        { month: 'Mar', high: 18.6, low: 7.3 },
        { month: 'Apr', high: 20.8, low: 8.1 },
        { month: 'May', high: 23.3, low: 9.9 },
        { month: 'Jun', high: 26.2, low: 11.9 },
        { month: 'Jul', high: 27.7, low: 13.3 },
        { month: 'Aug', high: 27.6, low: 13.2 },
        { month: 'Sep', high: 26.4, low: 12.1 },
        { month: 'Oct', high: 23.6, low: 9.9 },
        { month: 'Nov', high: 17, low: 6.8 },
        { month: 'Dec', high: 14.7, low: 5.8 }
    ],

    counter: 0,

    generateData: function () {
        var data = this.config.data,
            i, result = [],
            temp = 15,
            min = this.counter % 2 === 1 ? 0 : temp;
        for (i = 0; i < data.length; i++) {
            result.push({
                month: data[i].month,
                high: min + temp + Math.random() * temp,
                low: min + Math.random() * temp
            });
        }
        this.counter++;
        return result;
    },

    refreshData: function () {
        this.setData(this.generateData());
    }

});

Ext.define('Workflow.view.mtf.dashboard.PatientChartColumnBarController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.patient-chart-columnbar',
    onDownload: function () {
        if (Ext.isIE8) {
            Ext.Msg.alert('Unsupported Operation', 'This operation requires a newer version of Internet Explorer.');
            return;
        }
        var chart = this.lookupReference('chart');
        if (Ext.os.is.Desktop) {
            chart.download({
                filename: 'Redwood City Climate Data Chart'
            });
        } else {
            chart.preview();
        }
    },

    onReloadData: function () {
        var chart = this.lookupReference('chart');
        chart.getStore().refreshData();
    },

    // The 'target' here is an object that contains information
    // about the target value when the drag operation on the column ends.
    onEditTipRender: function (tooltip, item, target, e) {
        tooltip.setHtml('Temperature °F: ' + target.yValue.toFixed(1));
    },

    onSeriesLabelRender: function (value) {
        return value.toFixed(1);
    },

    onColumnEdit: function (chart, data) {
        var threshold = 65,
            delta = 20,
            yValue = data.target.yValue,
            coldness;

        if (yValue < threshold) {
            coldness = Ext.Number.constrain((threshold - yValue) / delta, 0, 1);
            return {
                fillStyle: 'rgba(133, 231, 252, ' + coldness.toString() + ')'
            };
        } else {
            return {
                fillStyle: 'none'
            };
        }
    },

    onAfterRender: function () {
        var me = this,
            chart = this.lookupReference('chart'),
            axis = chart.getAxis(0),
            store = chart.getStore();

        function onAxisRangeChange() {
            me.onAxisRangeChange(axis);
        }

        store.on({
            datachanged: onAxisRangeChange,
            update: onAxisRangeChange
        });
    },

    onAxisRangeChange: function (axis, range) {
        // this.lookupReference('chart') will fail here,
        // as at the time of this call
        // the chart is not yet in the component tree,
        // so we have to use axis.getChart() instead.
        var chart = axis.getChart(),
            store = chart.getStore(),
            sum = 0,
            mean;

        store.each(function (rec) {
            sum += rec.get('highF');
        });

        mean = sum / store.getCount();

        axis.setLimits({
            value: mean,
            line: {
                title: {
                    text: 'Average high: ' + mean.toFixed(2) + '°F'
                },
                lineDash: [2, 2]
            }
        });
    },

    itemAnimationDuration: 0,

    // Disable item's animaton for editing.
    onBeginItemEdit: function (chart, interaction, item) {
        var itemsMarker = item.sprite.getMarker(item.category),
            fx = itemsMarker.getTemplate().fx; // animation modifier

        this.itemAnimationDuration = fx.getDuration();
        fx.setDuration(0);
    },

    // Restore item's animation when editing is done.
    onEndItemEdit: function (chart, interaction, item, target) {
        var itemsMarker = item.sprite.getMarker(item.category),
            fx = itemsMarker.getTemplate().fx;

        fx.setDuration(this.itemAnimationDuration);
    }

});

Ext.define('Workflow.view.mtf.dashboard.PatientCartesianPanel', {
    extend: 'Ext.Panel',
    xtype: 'patient-chart-columnbar',
    controller: 'patient-chart-columnbar',
    requires: ['Ext.chart.series.Area'],
    iconCls: 'fa fa-area-chart',
    tbar: [
        '->',
        {
            text: 'Preview',
            platformConfig: {
                desktop: {
                    text: 'Download'
                }
            },
            handler: 'onDownload'
        },
        {
            text: 'Reload Data',
            handler: 'onReloadData'
        }
    ],

    items: {
        xtype: 'cartesian',
        reference: 'chart',
        store: {
            type: 'climate'
        },
        insetPadding: {
            top: 40,
            bottom: 40,
            left: 20,
            right: 40
        },
        interactions: {
            type: 'itemedit',
            tooltip: {
                renderer: 'onEditTipRender'
            },
            renderer: 'onColumnEdit'
        },
        axes: [{
            type: 'numeric',
            position: 'left',
            minimum: 30,
            titleMargin: 20,
            title: {
                text: 'Temperature in °F'
            },
            listeners: {
                rangechange: 'onAxisRangeChange'
            }
        }, {
            type: 'category',
            position: 'bottom'
        }],
        animation: Ext.isIE8 ? false : true,
        series: {
            type: 'bar',
            xField: 'month',
            yField: 'highF',
            style: {
                minGapWidth: 20
            },
            highlight: {
                strokeStyle: 'black',
                fillStyle: 'gold'
            },
            label: {
                field: 'highF',
                display: 'insideEnd',
                renderer: 'onSeriesLabelRender'
            }
        },
        sprites: {
            type: 'text',
            text: 'Redwood City Climate Data',
            fontSize: 22,
            width: 100,
            height: 30,
            x: 40, // the sprite x position
            y: 20  // the sprite y position
        },
        listeners: {
            afterrender: 'onAfterRender',
            beginitemedit: 'onBeginItemEdit',
            enditemedit: 'onEndItemEdit'
        }
    }
});