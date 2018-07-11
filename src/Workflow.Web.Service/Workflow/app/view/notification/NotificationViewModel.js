Ext.define('Workflow.view.notification.NotificationViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.notification-notificationview',
    data: {
        name: 'Workflow'
    },

    stores : {
    	notificationStore: {
            model: 'Workflow.model.notification.Notification',
            autoLoad: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/notification',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }

});
