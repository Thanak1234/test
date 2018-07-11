Ext.define("Workflow.view.hr.att.FormView", {
    extend: "Ext.form.Panel",
    xtype: 'att-form-view',
    requires: [
        "Workflow.view.hr.att.FormViewController",
        "Workflow.view.hr.att.FormViewModel"
    ],

    controller: "att-form",
    viewModel: {
        type: "att-form"
    },

    title: 'Travel Detail',
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
        labelAlign: 'left',
        labelWidth: 190,
        layout: 'form',
        margin: '5 0 5 0'
    },
    layout: 'anchor',
    initComponent: function() {
        var me = this;
        me.items = [{ 
            xtype: 'radiogroup',
            layout: { autoFlex: false },
            fieldLabel: 'Class of Travel as per Entitlement',
            value: 0,
            reference: 'classTravelEntitlement',
            defaults: {
                name: 'classTravelEntitlement',
                margin: '0 15 0 0'
            },
            bind: {
                value: { classTravelEntitlement: '{travelDetail.classTravelEntitlement}' },
                readOnly: '{!editable}'
            },
            items: [{
                xtype: 'radiofield',
                inputValue: 1,
                boxLabel: 'Economy',
                checked: true
            }, {
                xtype: 'radiofield',
                inputValue: 2,
                boxLabel: 'Business'
            }, {
                xtype: 'radiofield',
                inputValue: 3,
                boxLabel: 'Premium'
            }]
        }, {
            xtype: 'radiogroup',
            layout: { autoFlex: false },
            fieldLabel: 'Class of Travel Requested',
            value: 0,
            reference: 'classTravelRequest',
            defaults: {
                name: 'classTravelRequest',
                margin: '0 15 0 0'
            },
            bind: {
                value: { classTravelRequest: '{travelDetail.classTravelRequest}' },
                readOnly: '{!editable}'
            },
            items: [{
                xtype: 'radiofield',
                inputValue: 1,
                boxLabel: 'Economy',
                checked: true
            }, {
                xtype: 'radiofield',
                inputValue: 2,
                boxLabel: 'Business'
            }, {
                xtype: 'radiofield',
                inputValue: 3,
                boxLabel: 'Premium'
            }]
        },
        {
            xtype: 'label',
            text: 'Reason for Exception:'
        },
        {
            xtype: 'textarea',
            bind: {
                value: '{travelDetail.reasonException}',
                readOnly: '{!editable}'
            }
        },
        {
            xtype: 'container',
            layout: 'column',
            items: [
                {
                    xtype: 'radiogroup',
                    layout: { autoFlex: false },
                    fieldLabel: 'Purpose of Travel',
                    columnWidth: 0.5,
                    value: 0,
                    reference: 'purposeTravel',
                    defaults: {
                        name: 'purposeTravel',
                        margin: '0 15 0 0'
                    },
                    bind: {
                        value: { purposeTravel: '{travelDetail.purposeTravel}' },
                        readOnly: '{!editable}'
                    },
                    items: [{
                        xtype: 'radiofield',
                        inputValue: 0,
                        boxLabel: 'Business Trip',
                        checked: true
                    }, {
                        xtype: 'radiofield',
                        inputValue: 1,
                        boxLabel: 'Personal/Home Leave'
                    }],
                    listeners: {
                        change: 'onPurposeChanged'
                    }
                },
                {
                    xtype: 'textfield',
                    fieldLabel: 'Travel Taken This Year',
                    labelWidth: 180,
                    columnWidth: 0.5,
                    readOnly: true,
                    minValue: 0,
                    bind: {
                        value: '{travelDetail.noRequestTaken}'                        
                    }
                }
            ]
        },
        {
            xtype: 'label',
            text: 'Reason for Travel:'
        },
        {
            xtype: 'textarea',
            bind: {
                value: '{travelDetail.reasonTravel}',
                readOnly: '{!editable}'
            }
        },
        {
            xtype: 'att-destinationview',
            reference: 'destinationView'
        },
        {
            xtype: 'numberfield',
            fieldLabel: 'Estimated Cost of Ticket ($USD)',
            allowBlank: false,
            minValue: 0,
            bind: {
                value: '{travelDetail.estimatedCostTicket}',
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
    },
    getTokenInThisYear: function (requestorId, purposeId) {
        var response = Ext.Ajax.request({
            async: false,
            url: Workflow.global.Config.baseUrl + 'api/attitem/tokenByRequestor/' + requestorId + '/' + purposeId
        });
        return response.responseText;
    }
});
