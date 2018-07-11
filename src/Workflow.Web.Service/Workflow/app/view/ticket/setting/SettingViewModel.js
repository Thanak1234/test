Ext.define('Workflow.view.ticket.setting.SettingViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-settingview',
    data: {
        keyword: null,
        statusVal: null,
        initData: null
    },
    
    formulas: {
        isAgent: function (get) {
            var data=  get('initData');
            if (data && data.isAgent) {
                return true;
            } else {
                return false;
            }
        }
    }

});
