Ext.define("Workflow.view.reservation.crr.GuestWindow", {
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",

    requires: [
        "Workflow.view.reservation.crr.GuestWindowController",
        "Workflow.view.reservation.crr.GuestWindowModel"
    ],
    iconCls: 'x-fa fa-user',
    controller: "crr-guestwindow",
    viewModel: {
        type: "crr-guestwindow"
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
                           fieldLabel: 'Guest Name',
                           width: 200,
                           sortable: true,
                           bind: { value: '{name}', readOnly: '{readOnlyField}' },
                           allowBlank: false
                       }, {
                           fieldLabel: 'Position',
                           width: 250,
                           sortable: true,
                           bind: { value: '{title}', readOnly: '{readOnlyField}' }

                       }, {
                           fieldLabel: 'Company Name',
                           sortable: true,
                           flex: 1,
                           bind: { value: '{companyName}', readOnly: '{readOnlyField}' }
                       }
            ]
        }];

        me.callParent(arguments);

    }
});
