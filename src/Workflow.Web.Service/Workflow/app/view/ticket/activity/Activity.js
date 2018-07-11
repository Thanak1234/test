/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.activity.Activity',{
    extend: 'Workflow.view.common.requestor.AbstractWindowDialog',
    scrollable: 'y',
    resizable : false,
    requires: [
        'Workflow.view.ticket.activity.ActivityController',
        'Workflow.view.ticket.activity.ActivityModel'
    ],
    controller: 'ticket-activity-activity',
    viewModel: {
        type: 'ticket-activity-activity'
    },

    initComponent: function () {
        var me = this;
        me.items = [];
        me.items.push(me.getForm());
        me.items.push({ xtype: 'ticket-attach-file-panel', reference: 'attachmentList'});
        
        this.callParent(arguments);
    },

    getForm: function () {
        var me = this;
        return {
            margin: 5,
            xtype: 'form',
            header : false,
            reference: 'form',
            collapsible: true,
            width: '100%',
            bodyPadding: '10 10 0',
            defaultType: 'textfield',
            items: me.getFormItems()
        }
    },
    getFormItems: function () {
        var me = this;
        var items = [];
        var mainItems = me.getMainItems();
        if (mainItems){

            mainItems.forEach(function (item) {
                items.push(item);
            });
        }
        items.push({
            //fieldLabel: 'Comment <span style="color:red;">*</span>',
            xtype: 'htmleditor',
            allowBlank: false,
            labelAlign: 'top',
            bind: {
                value: '{description}',
                fieldLabel: '{commentLabel}'
            }//,
            //border: '0 0 2 0',
            // style: {
            //     borderColor: 'blue',
            //     borderStyle: 'solid'
            // }
        });
        return items;
    },

    getMainItems: function (actIdentifier) {
        return null; 

    }



});
