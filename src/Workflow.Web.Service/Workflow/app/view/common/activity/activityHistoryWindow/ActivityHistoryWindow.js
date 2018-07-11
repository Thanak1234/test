/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.common.activity.activityHistoryWindow.ActivityHistoryWindow', {
    extend: 'Workflow.view.common.requestor.AbstractWindowDialog',

    viewModel: {
        type: 'activityHistoryWindow'
    },

    controller: 'activityHistoryWindow',
    items: [
        {
            xtype: 'form',
            frame: false,
            bodyPadding: '10 10 0',
            defaultListenerScope: true,
            defaults: {
                flex: 1,
                anchor: '100%',
                msgTarget: 'side',
                labelWidth: 150,
                layout: 'form',
                xtype: 'container'
            },
            items: [
                        {
                            fieldLabel: 'User',
                            xtype: 'textfield',
                            bind: '{item.appriverDisplayName}',
                            readOnly : true
                        }, {
                            fieldLabel: 'Date',
                            xtype: 'datefield',
                            bind: '{item.actionDate}',
                            format: 'Y-m-d H:i:s',
                            readOnly: true
                        }, {
                            fieldLabel: 'Activity',
                            xtype: 'textfield',
                            bind: '{item.activity}',
                            readOnly: true
                        }, {
                            fieldLabel: 'Decision',
                            xtype: 'textfield',
                            bind: '{item.decision}',
                            readOnly: true
                        },
                        {
                            fieldLabel: 'Comments',
                            xtype: 'textarea',
                            allowBlank: true,
                            bind: '{item.comment}',
                            readOnly: true
                        }
            ]
        }
    ]
});