Ext.define("Workflow.view.hr.att.FlightDetailWindow", {
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",

    requires: [
        "Workflow.view.hr.att.FlightDetailWindowController",
        "Workflow.view.hr.att.FlightDetailWindowModel"
    ],
    iconCls: 'fa fa-plane',
    controller: "att-flightdetailwindow",
    viewModel: {
        type: "att-flightdetailwindow"
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
                           width: 100,
                           allowBlank: false,
                           bind: { value: '{date}', readOnly: '{readOnlyField}' }
                       }, {
                           xtype: 'timefield',
                           bind: { value: '{time}', readOnly: '{readOnlyField}' },
                           fieldLabel: 'Time',
                           //minValue: '6:00 AM',
                           //maxValue: '8:00 PM',
                           //increment: 30,
                           anchor: '100%',
                           allowBlank: false
                       }
            ]
        }];

        me.callParent(arguments);

    }
});
