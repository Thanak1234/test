Ext.define("Workflow.view.hr.att.DestinationWindow", {
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",

    requires: [
        "Workflow.view.hr.att.DestinationWindowController",
        "Workflow.view.hr.att.DestinationWindowModel"
    ],
    iconCls: 'fa fa-plane',
    controller: "att-destinationwindow",
    viewModel: {
        type: "att-destinationwindow"
    },

    initComponent: function () {
        var me = this;
        me.items = [{
            xtype: 'form',
            
            bodyPadding: '10 10 0',
            reference: 'form',
            method: 'POST',
            defaultListenerScope: true,
            defaults: {
                flex: 1,
                anchor: '100%',
                msgTarget: 'side',
                labelWidth: 150,
                layout: 'form',
                xtype: 'textfield'
            },
            items: [
                       {
                           fieldLabel: 'From (Airport)',
                           width: 200,
                           sortable: true,
                           bind: { value: '{fromDestination}', readOnly: '{readOnlyField}' },
                           allowBlank: false
                       }, {
                           fieldLabel: 'To (Airport)',
                           width: 250,
                           sortable: true,
                           allowBlank: false,
                           bind: { value: '{toDestination}', readOnly: '{readOnlyField}' }

                       }, {
                           fieldLabel: 'Date',
                           xtype: 'datefield',
                           sortable: true,
                           allowBlank: false,
                           width: 100,
                           bind: { value: '{date}', readOnly: '{readOnlyField}' }
                       }, {
                           xtype: 'timefield',
                           bind: { value: '{time}', readOnly: '{readOnlyField}' },
                           fieldLabel: 'Time',
                           allowBlank: false,
                           //minValue: '6:00 AM',
                           //maxValue: '8:00 PM',
                           //increment: 30,
                           anchor: '100%'
                       }
            ]
        }];

        me.callParent(arguments);

    }
});
