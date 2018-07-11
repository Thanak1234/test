/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.dashboard.TkItemPrmDhbController', {
extend: 'Workflow.view.AbstractBaseController',
alias: 'controller.ticket-dashboard-tkitemprmdhb',

init : function(){
  this.initData();
},

initData: function(fnCb){
  var me = this;
  var vm = me.getRef().vm;
  var timeFrame = vm.get('state.timeFrame') + 1;
  var timeFilter = vm.get('state.timeFilter');

  Ext.Ajax.request({
      url: Workflow.global.Config.baseUrl + 'api/ticket/dashboard/item-performance',
      method: 'GET',
      params: {type: 1, timeFrame : timeFrame , timeFilter: timeFilter},
      success: function (response) {
        var data = Ext.JSON.decode(response.responseText);

        if(data.length==0){
          if(fnCb){
            fnCb({error:true});
            Ext.MessageBox.show({
                title: 'Warning',
                msg: "No data fetched",
                buttons: Ext.MessageBox.OK,
                icon: Ext.MessageBox.ERROR
            });
          }
          return;
        }

        vm.set('data', data)
        me.reloadChartData()
        if(fnCb){
          fnCb();
        }
      },
      failure: function (response) {
        if(fnCb){
          fnCb({error:true});
        }
         vm.set('data', null)
      }
  });

},

onDateFilterClick: function (el, e, eOpts) {
    var ref = this.getRef();
    var timeFilter = ref.refs.timeFilter;
    var menus = timeFilter.getMenu().items;

    var oldState = Object.assign({}, ref.vm.get('state'))

    ref.vm.set('state.timeFilter', el.statusId);
    this.initData(function(error){
      if(error){
        ref.vm.set('state.timeFilter', oldState.timeFilter);
        return;
      }

      ref.vm.set('state.selectedTimeFilter', el.text)
      menus.items.forEach(function (item) {
        if (item === el) {
          el.setIconCls('fa fa-check');
        } else {
          item.setIconCls('');
        }
      });

    })
},

onItemToggle : function(){
  this.reloadChartData();
  this.onAfterRender(true);

},

onTimeFrameToggle: function(){
  this.initData();
},

onRefreshHandler : function(){
  var me = this;
  var chart = me.lookupReference('chart');
  this.initData(function(error){
    if(error){
      return;
    }
    chart.redraw();
  })

},
reloadChartData : function(){
  var vm = this.getRef().vm
  var data = vm.get('data')
  var columnType = vm.get('state.itemType')
  var pivote = this.pivote(data, columnType)
  var fileModel = this.getModelFileds(pivote.fields)

  var store = vm.getStore('chartStore')
  vm.set('chartFields', fileModel.chartFields )
  vm.set('seriesFields', fileModel.seriesFields )
  store.setFields(fileModel.storeFields)
  store.setData(pivote.data)

  this.reloadGrid();

},

reloadGrid : function(){

  var me = this;
  var ref = this.getRef();
  var grid = ref.refs.grid

  var fieldModels = ref.vm.get('seriesFields')

  var cols = []
  cols.push({
    xtype: 'rownumberer'
  });

  cols.push({
    text: 'Time',
    flex: 1,
    sortable: true,
    dataIndex: 'time'
  });

  fieldModels.forEach(function(item){
    cols.push(me.generateGridColumn(item))
  });

  cols.push({
    text: 'Total',
    width: 120,
    align : 'right',
    sortable: true,
    renderer : function(value, metaData, record, rowIdx, colIdx, store, view){
      var total =0
      var dataRec = record.getData()
      for (var property in dataRec) {
        if( 'id' !== property  && 'time' !== property )
        total += dataRec[property]
      }
      return total;
    },
    summaryType: 'sum',
    summaryType: function(records){
      var total =0

      records.forEach(function(rec){
        var dataRec = rec.getData()
        for (var property in dataRec) {
          if( 'id' !== property  && 'time' !== property )
          total += dataRec[property]
        }
      });
      return total;
    }
  });

  grid.reconfigure(ref.vm.get('chartStore'), cols);
},

generateGridColumn : function(fileModel){
  return {
    text: fileModel.display,
    width: 120,
    align : 'right',
    itemName: fileModel.field,
    dataIndex:  fileModel.field,
    renderer : function(value){
      return value || 0;
    },
    summaryType: 'sum'
  }
},

getSeriesConfig: function (field, title) {
  return {
    type: 'line',
    title: title,
    xField: 'time',
    yField: field,
    grid: true,
    marker: {
      type: 'square',
      fx: {
        duration: 200,
        easing: 'backOut'
      }
    },
    highlightCfg: {
      scaling: 2
    },
    tooltip: {
      trackMouse: true,
      renderer: function (tooltip, record, item) {
        tooltip.setHtml(title + ' (' + record.get('time') + '): ' + record.get(field));
      }
    }
  };
},


onAxisLabelRender: function (axis, label, layoutContext) {
    var value = layoutContext.renderer(label);
    return value !== '0' ? (value / 1000 + ',000') : value;
},
onAfterRender: function (forceRedraw) {
  var me = this,
      vm = me.getRef().vm,
      chart = me.lookupReference('chart');

  var fields = vm.get('seriesFields')
  var series = []
  fields.forEach(function(item){
    series.push(me.getSeriesConfig(item.field, item.display));
  })

  chart.setSeries(series);
  if(forceRedraw){
    chart.redraw();
  }
},


onSeriesTooltipRender: function (tooltip, record, item) {
    var title = item.series.getTitle();

    tooltip.setHtml(title + ' on ' + record.get('month') + ': ' +
        record.get(item.series.getYField()) + '%');
},

pivote : function(data,level){
  var me = this;
  var result = data.reduce(function(group, curr){
    var time = curr.time
    var itemName = me.getPathLevel(curr.keyPath,level)
    var value = curr.value
    var key =  itemName.replace(/\s+/g, '-')
    if(!group.item[time]) {
      group.item[time] = {};
      group.item[time][key]=value;
    }else if(!group.item[time][key]){
      group.item[time][key] = value;
    }else{
      group.item[time][key] += value;
    }


    var fieldName = me.getPathText(itemName)

    if(!group.fields[key]) group.fields[key] = fieldName;

    return group;
  }, {item:{}, fields:{}});

  //convert to array
  var finalResults = []
  for (var prop in result.item) {
      var item ={ time: prop }
      for (var property in result.item[prop]) {
        item[property] = result.item[prop][property];
      }

      finalResults.push(item);
  }

  return {
    data: finalResults,
    fields: result.fields
  };
},

  getPathLevel : function(str,level){
    if(level <=0){
      return 'Ticket count';
    }
    var values = str.split("==>",level);
    return values.concat([]).join("==>")
  },

  getPathText : function(str){
    var values = str.split("==>");
    return values? values.pop(): str ;

  },

  getModelFileds : function(fields){
    var storeFields = [];
    var chartFields = [];
    var seriesFields = [];

    for (var key in fields) {
        chartFields.push(key);
        storeFields.push({name : key, type: 'int'});
        seriesFields.push({field:key, display : fields[key]});
    }
    return {storeFields: storeFields, chartFields: chartFields, seriesFields: seriesFields };
  }
});
