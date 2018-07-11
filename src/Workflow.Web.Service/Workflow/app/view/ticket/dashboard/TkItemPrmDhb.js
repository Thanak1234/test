/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.dashboard.TkItemPrmDhb',{
    extend: 'Ext.panel.Panel',
    xtype: 'TkItemPrmDhb',

    requires: [
        'Workflow.view.ticket.dashboard.TkItemPrmDhbController',
        'Workflow.view.ticket.dashboard.TkItemPrmDhbModel',
        'Ext.chart.CartesianChart',
        'Ext.chart.axis.Numeric',
        'Ext.chart.axis.Category',
        'Ext.chart.series.Bar',
        'Ext.chart.series.Line',
        'Ext.chart.interactions.ItemHighlight',
        'Ext.chart.plugin.ItemEvents'

    ],
    scrollable : 'y',
    title : 'Ticket Item Performance',
    controller: 'ticket-dashboard-tkitemprmdhb',
    viewModel: {
        type: 'ticket-dashboard-tkitemprmdhb'
    },

    tbar: [
      {
        xtype: 'segmentedbutton',
        width: 450,
        defaults: { ui: 'default-toolbar' },
        bind : '{state.itemType}',
        listeners: {
            toggle: 'onItemToggle'
        },
        items: [
            {
              text: 'All Tickets',
              pressed: true,
              width: 120
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
            }
        ]
      },
      {
        xtype: 'segmentedbutton',
        width: 400,
        defaults: { ui: 'default-toolbar' },
        bind : '{state.timeFrame}',
        listeners: {
            toggle: 'onTimeFrameToggle'
        },
        items: [
            {
              text: 'Daily',
              pressed: true
            },
            {
                text: 'Weekly'
            },
            {
                text: 'Monthly'
            },
            {
                text: 'Quatarly'
            },
            {
                text: 'Yearly'
            }
        ]
      },
      {
        //text: 'Current Month',
        bind :{
          text : '{state.selectedTimeFilter}'
        },
        reference: 'timeFilter',
        showText: true,
        width: 200,
        textAlign: 'left',

        menu: [
          {
            text: 'Current Month',
            statusId: 101,
            iconCls: 'fa fa-check',
            handler: 'onDateFilterClick',
            scope: 'controller'
          },
          {
            text: 'Last Month',
            statusId: 102,
            iconCls:  'a',
            handler: 'onDateFilterClick',
            scope: 'controller'
          },
          {
            text: 'Last Month Today',
            statusId: 103,
            iconCls:  'a',
            handler: 'onDateFilterClick',
            scope: 'controller'
          },
          '-',
          {
            text: 'Current Quater',
            statusId: 201,
            iconCls:  'a',
            handler: 'onDateFilterClick',
            scope: 'controller'
          },
          {
            text: 'Last Quater',
            statusId: 202,
            iconCls:  'a',
            handler: 'onDateFilterClick',
            scope: 'controller'
          },
          {
            text: 'Last Quater-Today',
            statusId: 203,
            iconCls:  'a',
            handler: 'onDateFilterClick',
            scope: 'controller'
          },
          '-',
          {
            text: 'Current Year',
            statusId: 301,
            iconCls:  'a',
            handler: 'onDateFilterClick',
            scope: 'controller'
          },
          {
            text: 'Last Year',
            statusId: 302,
            iconCls:  'a',
            handler: 'onDateFilterClick',
            scope: 'controller'
          }

        ]
          },
      '->',
      {
        text: 'Refresh',
        handler: 'onRefreshHandler',
        iconCls: 'fa fa-refresh'
      }

    ],

    initComponent : function(){
      var me = this;
      me.items = [];
      me.items.push(me.getCateChart())
      me.items.push(me.getGridPanel())
      me.callParent(arguments);
    },

    getCateChart : function(){
      return {
          reference: 'chart',
          xtype: 'cartesian',
          bind :{
            store : '{chartStore}'
          },
          background: 'white',
          height: 400,
          insetPadding: '40 40 40 40',
          legend: {
               docked: 'bottom'
           },
           sprites: [{
                type: 'text',
                text: 'Ticket Item Performance By Time Frame',
                fontSize: 22,
                width: 100,
                height: 30,
                x: 40, // the sprite x position
                y: 20  // the sprite y position
            }],
            axes: [{
                type: 'numeric',
                 position: 'left',
                 //fields: ['china', 'japan', 'usa'],
                 //fields: ['k2','ICS', 'Ticket'],
                 title: 'Task Count Based on Time Frame',
                 grid: true,
                 minimum: 0,
                 adjustByMajorUnit: true,
                 majorTickSteps: 10
              }, {
                  type: 'category',
                  position: 'bottom',
                  label: {
                     rotate: {
                         degrees: -45
                     }
                   }
              }],
              listeners: {
                afterrender: 'onAfterRender'
              }
        }
    },

    getGridPanel : function(){
      return{
          xtype: 'grid',
          autoRender: true,
          reference : 'grid',
          border: true,
          height: 300,
          bind: {
              store: '{chartStore}'
          },
          features: [{
              ftype: 'summary'
          }],
          columns: [
              {
                  xtype: 'rownumberer'
              }
          ]
      }
    }

});
