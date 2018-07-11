Ext.define("Workflow.view.common.worklists.SleepWindow", {
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",
    xtype: 'common-worklists-sleepwindow',    
    controller: "common-worklists-sleepwindow",
    viewModel: {
        type: "common-worklists-sleepwindow"
    },
    title: 'Sleep',
    width: 450,
    items: [
        {
            xtype: 'radiogroup',
            reference: 'optionRadio',
            regoin: 'center',
            defaults: {
                padding: 10
            },
            allowBlank: false,
            items: [
                {
                    xtype: 'panel',
                    width: 450,
                    defaults: {
                        padding: 5
                    },
                    layout: {
                        type: 'table',
                        columns: 3,
                        tableAttrs: {
                            style: {
                                width: '100%'
                            }
                        }
                    },
                    items: [
                        {
                            xtype: 'label',
                            colspan: 3,
                            style: 'font-weight: bold',
                            text: 'Please select a sleep option'
                        },
                        {
                            colspan: 3,
                            html:'--------------------------------------------------------------',
                            width: '100%'
                        },
                        {
                            xtype: 'radio',
                            name: 'option',
                            allowBlank:false,
                            colspan: 3,
                            checked: true,
                            style: 'font-weight: bold',
                            boxLabel: 'Basic',
                            inputValue: true
                        },
                        {
                            width: 10
                        },
                        {
                            xtype: 'combobox',
                            colspan: 2,
                            width: '90%',
                            reference: 'cmbDay',
                            publishes: 'value',
                            displayField: 'Name',
                            valueField: 'Duration',
                            minChars: 0,
                            queryMode: 'local',
                            typeAhead: true,
                            bind: {
                                store: '{options}'
                            }
                        }, {
                            xtype: 'radio',
                            style: 'font-weight: bold',
                            name: 'option',
                            colspan: 3,
                            boxLabel: 'Date',
                            inputValue: false
                        },
                        {
                            width: 10
                        },
                        {
                            xtype: 'datefield',                            
                            allowBlank: false,
                            hiddenLabel: true,
                            reference: 'dfSleep',
                            width: '90%',
                            colspan: 2,
                            listeners: {
                                expand: function (field, eOpts) {
                                    var date = new Date().addDays(1);
                                    field.setMinValue(date);
                                    field.validate();
                                    this.dateRangeMin = date;
                                }
                            }
                        }
                    ]
                }
            ],
            listeners: {
                change: 'onOptionChange'
            }
    }],
    buttons: [
        {
            text: 'OK',
            handler: 'onOkClickHandler'
        },
        {
            text: 'Cancel',
            handler: 'closeWindow'
        }
    ]
});

Date.prototype.addDays = function (days) {
    this.setDate(this.getDate() + parseInt(days));
    return this;
};