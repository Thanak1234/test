Ext.define("Workflow.view.common.worklists.ShareWindow", {
    extend: 'Workflow.view.common.requestor.AbstractWindowDialog',
    xtype: 'common-worklists-sharewindow',
    title: 'Share To Users',
    controller: 'common-worklist-sharewindow',
    layout: {
        type: 'vbox',
        align: 'stretch'
    },
    items: [{
        xtype: 'common.worklist.components.user-grid',
        height: 180,
        margin: 5,
        border: true,
        reference: 'destinationGridPanel',
        bind: {
            store: '{destinations}'
        }
    }, {
        xtype: 'textarea',
        margin: 5,
        emptyText: 'Comment',
        allowBlank: false,
        bind: {
            value: '{comment}'
        }
    }],
    buttons: [
        {
            text: 'Share',
            handler: 'onOkClickHandler'
        },
        {
            text: 'Cancel',
            handler: 'closeWindow'
        }
    ]
});