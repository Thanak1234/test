Ext.define('Workflow.view.notification.NotificationView',{
    extend: 'Ext.window.Window',

    requires: [
        'Workflow.view.notification.NotificationViewController',
        'Workflow.view.notification.NotificationViewModel'
    ],

    controller: 'notification-notificationview',
    viewModel: {
        type: 'notification-notificationview'
    },

    title: 'Notification',
    height: 500,
    width: 400,
    closable : false,
    resizable : false,
    layout: 'fit',
    border : false,
    listeners: {
        show: 'onShow'
    },
    cbFn: null,
    iconCls: 'fa fa-bell',
    initComponent: function () {
        var me = this;
        var items = [];

        this.items = items;
        items.push(this.gridComp());
        me.callParent(arguments);
    },

    gridComp: function(){
        return {
            xtype : 'grid',
            border : false,
            requires: ['Ext.ux.PreviewPlugin'],
            hideHeaders: true,
            reference : 'notificationList',
            viewConfig: {
                plugins: [{
                    pluginId: 'preview',
                    ptype: 'preview',
                    bodyField: 'description',
                    previewExpanded: true
                }],
                listeners: {
                    //cellclick: 'onAttachmentClickHandler'
                },
                stripeRows: true,

                getRowClass: function(rec, idx, rowPrms, ds) {
                  return rec.get('STATUS') === 'READ' ? 'notification-read' : '';
                }
            },
            bind : {
                store: '{notificationStore}'
            },
            columns : [{
                dataIndex: 'subject',
                flex: 1,
                renderer: this.rendererTitle,
                sortable: false
            },{
                menuDisabled: true,
                sortable: false,
                xtype: 'actioncolumn',
                width: 50,
                items: [{
                    iconCls: 'fa fa-external-link',
                    handler: 'onViewTicket'
                }]
            }]

        };
    },

    rendererTitle : function(value, p, record){
        var subect = '<div class="topic"><b>{0}</b><span class="author">{1}</span></div>' ;
        var date = record.get('createdDate');
        var now = new Date(),
            d = Ext.Date.clearTime(now, true),
            notime = Ext.Date.clearTime(date, true).getTime();

        var dateVal = '';
        if (notime === d.getTime()) {
            dateVal = 'Today ' + Ext.Date.format(date, 'g:i a');
        }

        d = Ext.Date.add(d, 'd', -6);
        if (d.getTime() <= notime) {
            dateVal = Ext.Date.format(date, 'D g:i a');
        } else {
            dateVal = Ext.Date.format(date, 'Y/m/d g:i a');
        }
        return Ext.String.format(subect, value, dateVal );

    }
});
