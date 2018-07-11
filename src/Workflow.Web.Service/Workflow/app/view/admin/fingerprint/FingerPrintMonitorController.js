Ext.define('Workflow.view.admin.FingerPrintMonitorController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.admin-fingerprintmonitor',
    init: function() {
        var me = this;
        setTimeout(function () {
            me.IsServiceStarted();
        }, 5000);        
    },
    IsServiceStarted: function () {
        var me = this;
        var viewmodel = me.getViewModel();
        Ext.Ajax.request({
            url: '/api/fingerprints/IsServiceStarted',
            method: 'GET',
            success: function (response) {
                viewmodel.set('disabled', false);
                var isCompleted = response.responseText === 'true';
                if (isCompleted) {
                    viewmodel.set('buttonText', 'STOP SERVICE');                    
                } else {
                    viewmodel.set('buttonText', 'START SERVICE');
                }
            },
            failure: function (response) {
                Ext.Msg.show({
                    title: 'System error',
                    message: response.responseText,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        });
    },
    onActionClick: function (el, v) {
        var me = this;
        Ext.Msg.show({
            title: 'Action Confirm',
            message: 'Would you like to take this action?',
            buttons: Ext.Msg.YESNO,
            icon: Ext.Msg.QUESTION,
            fn: function (btn) {
                if (btn === 'yes') {
                    Ext.Ajax.request({
                        url: '/api/fingerprints',
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        jsonData: el.jsonData,
                        success: function (response) {
                            me.loadData();
                        },
                        failure: function (response) {
                            Ext.Msg.show({
                                title: 'System error',
                                message: response.responseText,
                                buttons: Ext.Msg.OK,
                                icon: Ext.Msg.ERROR
                            });
                        }
                    });
                }
            }
        });
    },
    onRestartClick: function() {
        var me = this;
        var viewmodel = me.getViewModel();
        Ext.Msg.show({
            title: 'Action Confirm',
            message: 'Would you like to take action?',
            buttons: Ext.Msg.YESNO,
            icon: Ext.Msg.QUESTION,
            fn: function (btn) {
                if (btn === 'yes') {
                    viewmodel.set('buttonText', 'PROCESSING...');
                    viewmodel.set('disabled', true);
                    Ext.Ajax.request({
                        url: '/api/fingerprints/restart',
                        method: 'GET',
                        success: function (response) {
                            setTimeout(function () {
                                me.IsServiceStarted();
                                me.loadData();
                            }, 7000);
                        },
                        failure: function (response) {
                            Ext.Msg.show({
                                title: 'System error',
                                message: response.responseText,
                                buttons: Ext.Msg.OK,
                                icon: Ext.Msg.ERROR
                            });
                        }
                    });
                }
            }
        });
    },
    onRefreshClick: function () {
        var me = this;
        me.loadData();
    },
    loadData: function () {
        var me = this;
        var viewmodel = me.getView().getViewModel();
        var store = viewmodel.get('fingerprintStore');
        try {
            store.reload();
        } catch (ex) {
            console.log(ex);
        }
    }
});
