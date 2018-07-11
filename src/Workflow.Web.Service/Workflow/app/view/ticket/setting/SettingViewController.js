Ext.define('Workflow.view.ticket.setting.SettingViewController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.ticket-setting-settingview',
    init: function () {},
    loadTabData: function(elm, eOpts){
      var data = elm.viewModel.data, query = null, status = null;
      query = data == null ? query : data.query;
      status = data == null ? status : data.status;
      var param = {'query': query, 'status': status};
      elm.getController().loadData(param);
    }
});
