/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.dashboard.TkItemPrmDhbModel', {
  extend: 'Ext.app.ViewModel',
  alias: 'viewmodel.ticket-dashboard-tkitemprmdhb',
  data: {
    chartFields : [],
    seriesFields: [],
    state : {
      itemType : 0, //0=ALL,1= CATE, 2=SUCATE, 3=ITEM
      timeFrame : 0 , //1=Daily, 2=Weekly, 3: Monthly, 4 Quatarly, 5 Yearly
      timeFilter : 101,
      FILTERS: [], //[] = NALL
      selectedTimeFilter: 'Current Month'
    },
    data : [
      {time:'Jan', keyPath: 'Casino==>K2==>Login', value: 35},
      {time:'Jan', keyPath: 'Casino==>K2==>Error', value: 56},
      {time:'Jan', keyPath: 'Hotel==>V1==>cannot Access', value: 415},
      {time:'Jan', keyPath: 'Infra==>AD==>Login', value: 43},
      {time:'Feb', keyPath: 'Casino==>K2==>Login', value: 445},
      {time:'Feb', keyPath: 'Casino==>K2==>Error', value: 62},
      {time:'Feb', keyPath: 'Hotel==>V1==>cannot Access',  value: 465},
      {time:'Feb', keyPath: 'Infra==>AD==>Login', value: 245},

      {time:'Mar', keyPath: 'Casino==>K2==>Login', value: 45},
      {time:'Mar', keyPath: 'Casino==>K2==>Error', value: 6},
      {time:'Mar', keyPath: 'Hotel==>V1==>cannot Access',  value: 5},
      {time:'Mar', keyPath: 'Infra==>AD==>Login', value: 2675},

      {time:'Apr', keyPath: 'Hotel==>V1==>cannot Access',  value: 5},
      {time:'Apr', keyPath: 'Infra==>AD==>Login', value: 56}


    ]
  },
  stores: {
    chartStore : {
        fields : [],
        data: {}
    },
    mainStore : {
      fields: ['time', 'k2', 'ICS', 'Ticket'],

      data: [
          {
              "time": 'Jan',
              "k2": "546.877",
              "ICS": "1444.45",
              "Ticket": "4040.70"
          },
          {
              "time": 'Feb',
              "k2": "640.568",
              "ICS": "1585.09",
              "Ticket": "4346.75"
          },
          {
              "time": 'Mar',
              "k2": "6.568",
              "ICS": "85.09",
              "Ticket": "436.75"
          }
      ]
    }
  }
});
