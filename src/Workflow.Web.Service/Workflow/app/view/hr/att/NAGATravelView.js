Ext.define("Workflow.view.hr.att.NAGATravelView", {
    extend: "Ext.form.Panel",
    xtype: 'att-nagatravel-view',
    requires: [
        "Workflow.view.hr.att.NAGATravelViewController",
        "Workflow.view.hr.att.NAGATravelViewModel"
    ],

    controller: "att-nagatravelview",
    viewModel: {
        type: "att-nagatravelview"
    },

    title: 'NAGA Travel',
    iconCls: 'fa fa-suitcase',
    formReadonly : false,
    minHeight: 100,
    autoWidth: true,
    frame: false,
    bodyPadding: '10',
    method: 'POST',
    defaults: {
        flex: 1,
        anchor: '100%',
        //defaultType: 'textfield',
        labelAlign: 'right',
        labelWidth: 250,
        layout: 'form',
        margin: '5 0 5 0'
    },
    layout: 'anchor',
    initComponent: function() {
        var me = this;
        me.items = [
        {
            xtype: 'att-flightdetailview',
            reference: 'flightDetailView'
        },
        {
            xtype: 'numberfield',
            fieldLabel: 'Cost of Ticket per Entitlement ($USD)',
            allowBlank: false,
            reference: 'costTicket',
            minValue: 0,
            bind: {
                value: '{costTicket}',
                readOnly: '{!editable}'
            }
        },
        {
            xtype: 'numberfield',
            fieldLabel: 'Extra Charge ($USD)',
            reference: 'extraCharge',
            allowBlank: false,
            minValue: 0,
            bind: {
                value: '{extraCharge}',
                readOnly: '{!editable}'
            }
        },
        {
            xtype: 'label',
            text: 'Remark:'
        },
        {
            xtype: 'textarea',
            reference: 'remark',
            bind: {
                value: '{remark}',
                readOnly: '{!editable}'
            }
        }
    ];
    me.callParent(arguments);        
    },
    minimumDate: function (field, eOpts) {
        field.setMinValue(new Date());
        field.validate();
        this.dateRangeMin = new Date();
    },
    defaultsFieldSet: function () {
        return {
            flex: 1,
            anchor: '100%',
            defaultType: 'textfield',
            labelAlign: 'right',
            labelWidth: 150,
            layout: 'form',
            margin: '5 0 5 0'
        };
    }
});
