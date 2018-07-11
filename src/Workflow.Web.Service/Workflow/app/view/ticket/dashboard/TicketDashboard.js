/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.dashboard.TicketDashboard',{
    //extend: 'Ext.panel.Panel',
    extend: 'Ext.tab.Panel',
    ui: 'ng-tab-panel-ui',
    cls: 'ng-tab-panel-ui',
    requires: [
        'Workflow.view.ticket.dashboard.TicketDashboardController',
        'Workflow.view.ticket.dashboard.TicketDashboardModel',
        'Ext.chart.CartesianChart',
        'Ext.chart.axis.Numeric',
        'Ext.chart.axis.Category',
        'Ext.chart.series.Bar',
        'Ext.chart.interactions.ItemHighlight',
        'Ext.chart.plugin.ItemEvents'
    ],
    ngconfig: {
        layout: 'fullScreen'
    },
    controller: 'ticket-dashboard-ticketdashboard',
    viewModel: {
        type: 'ticket-dashboard-ticketdashboard'
    },
    border : false,
    margin : 0,
    initComponent: function () {
        var me = this;
        me.items = [];


        var activeTicketTab ={
            xtype : 'panel',
            title : 'Active Tickets',
            border: false,
            cls: 'ng-tab-item-ui',
            //width: 1010,
            scrollable : 'y',
            items : [
                me.itemChartPanel(),
                me.itemGrid()
            ]
        }

        var performanceTab ={
            // xtype : 'panel',
            // scrollable : 'y',
            // title : 'Performance',
            xtype : 'TkItemPrmDhb',
            border: false,
            cls: 'ng-tab-item-ui'
            //width: 1010,
        }
        me.items.push(activeTicketTab);
        me.items.push(performanceTab);
        me.callParent(arguments);
    },


    //Active Ticket


    itemChartPanel : function(){
    return {
            xtype :'panel',
            items: [ this.itemChart() ],
            tbar: [
                {
                    xtype: 'segmentedbutton',
                    width: 500,
                    defaults: { ui: 'default-toolbar' },
                    items: [
                        {
                            iconCls: 'fa fa-street-view',
                            text: 'Agent',
                            pressed: true,
                            width: 80
                        },
                        {
                            iconCls: 'fa fa-qrcode',
                            text: 'Team',
                            width: 80
                        },
                        {
                            iconCls: 'fa fa-object-group',
                            text: 'Category',
                            width: 100
                        },
                        {
                            iconCls: 'fa fa-object-ungroup',
                            text: 'Sub Category',
                            width: 120
                        },
                        {
                            iconCls: 'fa fa-plug',
                            text: 'Item',
                            width: 80
                        },
                        {
                            text: 'Source',
                            width: 80
                        }
                    ],
                    listeners: {
                        toggle: 'onItemToggle'
                    }
                },
                '->',
                {
                    xtype: 'segmentedbutton',
                    width: 200,
                    defaults: { ui: 'default-toolbar' },
                    items: [
                        {
                            text: 'Stack',
                            pressed: true
                        },
                        {
                            text: 'Group'
                        }
                    ],
                    listeners: {
                        toggle: 'onStackGroupToggle'
                    }
                }

            ]
        };
    },
    itemChart: function(){

        var item = {

            xtype: 'cartesian',
            reference : 'itemChart',
            plugins: {
                ptype: 'chartitemevents',
                moveEvents: true
            },
            //width: 1010,
            height: 460,
            bind : {
                store : '{ticketItemStore}'
            },
            legend: {
                docked: 'right'
            },

            insetPadding: {
                top: 40,
                left: 40,
                right: 40,
                bottom: 40
            },
            sprites: [{
                type: 'text',
                text: 'Ticket Item summary: This data shows only active tickets ',
                fontSize: 22,
                width: 100,
                height: 30,
                x: 40, // the sprite x position
                y: 20  // the sprite y position
            }],
            axes: [{
                type: 'numeric',
                position: 'left',
                adjustByMajorUnit: true,
                grid: true,
                fields: ['opened'],
                //renderer: 'onAxisLabelRender',
                minimum: 0
            }, {
                type: 'category',
                position: 'bottom',
                grid: true,
                fields: ['itemName'],
                label: {
                    rotate: {
                        degrees: -45
                    }
                }
            }],
            series: [{
                type: 'bar',
                title: [ 'Unassigned','Opened',  'On Hold', 'Over Due' ],
                xField: 'itemName',
                yField: [ 'unAssigned', 'opened',  'onHold', 'overDue' ],
                stacked: true,
                marker: true,
                style: {
                    opacity: 0.80
                },
                highlight: {
                    fillStyle: 'yellow'
                },
                tooltip: {
                    renderer: 'onBarTipRender'
                }
            }],

            listeners: { // Listen to itemclick events on all series.
                itemclick: 'onItemClickHandler'
            }
        };


        return item;

    },

    itemGrid : function(){
        var item ={
            xtype: 'grid',
            border: true,
            //width: 1010,
            height: 460,
            bind: {
                store: '{ticketItemStore}'
            },
            features: [{
                ftype: 'summary'
            }],
            columns: [
                {
                    xtype: 'rownumberer'
                },
                {
                    text: 'Item Name',
                    flex: 1,
                    sortable: true,
                    dataIndex: 'itemName'
                },
                {
                    text: 'Unassigned',
                    width: 120,
                    align : 'right',
                    itemName:'unAssigned',
                    sortable: true,
                    dataIndex: 'unAssigned',
                    summaryType: 'sum'
                },
                 {
                    text: 'Opened',
                    width: 120,
                    align : 'right',
                    sortable: true,
                    dataIndex: 'opened',
                    summaryType: 'sum'
                },
                 {
                    text: 'On Hold',
                    width: 120,
                    align : 'right',
                    sortable: true,
                    dataIndex: 'onHold',
                    summaryType: 'sum'
                },
                 {
                    text: 'Overdue',
                    width: 120,
                    align : 'right',
                    sortable: true,
                    dataIndex: 'overDue',
                    summaryType: 'sum'
                },
                {
                    text: 'Total',
                    width: 120,
                    align : 'right',
                    sortable: true,
                    renderer : function(value, metaData, record, rowIdx, colIdx, store, view){
                        return record.get('unAssigned') +  record.get('opened')  +  record.get('onHold') +  record.get('overDue') ;
                    },
                    summaryType: 'sum',
                    summaryRenderer:function(value, summaryData, dataIndex){
                        var total =0;
                        for (property in summaryData) {
                              total += summaryData[property] || 0;
                        }
	                    return Ext.String.format('<label style="font-weight:bold;">{0}</label>',total);
                    }
                    //dataIndex: 'overDue'
                }
            ]
        };

        return item;
    },


    /////////////////////////////////***************Ticket Performance************************************/////////////////////////////////////

    ticketCateChart: function(){
      return {

      }
    },

    ticketSubCateChart : function(){

    },

    ticketItem : function (){

    }

});
