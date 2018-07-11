/**
 *Author : Phanny 
 *
 */

Ext.define("Workflow.view.reservation.fnfbr.ReservationView", {
    extend: "Ext.form.Panel",
    xtype: 'fnfbr-reservation-view',
    requires: [
        "Workflow.view.reservation.fnfbr.ReservationViewController",
        "Workflow.view.reservation.fnfbr.ReservationViewModel"
    ],

    controller: "fnfbr-reservation",
    viewModel: {
        type: "fnfbr-reservation"
    },

    title: 'Reservation',
    iconCls: 'fa fa-suitcase',
    formReadonly : false,
    minHeight: 100,
    autoWidth: true,
    frame: false,
    bodyPadding: '10 10 10 10',
    method: 'POST',
    //defaultListenerScope: true,
    defaults: {
        flex: 1,
        anchor: '100%',
        defaultType: 'textfield',
        labelAlign: 'right',
        labelWidth: 150,
        layout: 'form',
        margin: '5 0 5 0'
    },
    layout: 'anchor',
    initComponent: function() {
        var me = this;
        me.items = [{
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                xtype: 'textfield',
                fieldLabel: 'Guest Full Name',
                regex: /.*\S.*/,
                allowBlank: false,
                bind: {
                    value: '{reservation.guestFullName}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'datefield',
                fieldLabel: 'Check-in Date',
                emptyText: 'MM/DD/YYY',
                itemId: 'reservation-checkindt',
                vtype: 'daterange',
                endDateField: 'reservation-checkoutdt',
                name: 'checkInDate',
                allowBlank: false,
                listeners: {
                    select: 'calculateRoomTaken',
                    expand: me.minimumDate
                },
                bind: {
                    value: '{reservation.checkInDate}',
                    readOnly: '{!editable}'
                }
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                xtype: 'textfield',
                fieldLabel: 'I/D or Passport No',
                bind: {
                    value: '{reservation.passportNo}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'datefield',
                fieldLabel: 'Check-out Date',
                emptyText: 'MM/DD/YYY',
                itemId: 'reservation-checkoutdt',
                name: 'checkOutDate',
                vtype: 'daterange',
                startDateField: 'reservation-checkindt',
                allowBlank: false,
                listeners: {
                    select: 'calculateRoomTaken',
                    expand: me.minimumDate
                },
                bind: {
                    value: '{reservation.checkOutDate}',
                    readOnly: '{!editable}'
                }
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                xtype: 'textfield',
                fieldLabel: 'Relationship',
                bind: {
                    value: '{reservation.relationship}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'numberfield',
                fieldLabel: 'No of Room',
                name: 'numberOfRoom',
                allowBlank: false,
                minValue: 1,
                listeners: {
                    change: 'calculateRoomTaken',
                    spin: 'calculateRoomTaken'
                },
                bind: {
                    value: '{reservation.numberOfRoom}',
                    readOnly: '{!editable}'
                }
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                xtype: 'lookupfield',
                fieldLabel: 'Room Category',
                allowBlank: false,
                editable: false,
                reference: 'roomCategory',
                publishes: 'value',
                namespace: '[RESERVATION].[FRIEND_FAMILY_BOOKING].[ROOM_CATEGORY]',
                bind: {
                    value: '{reservation.roomCategoryId}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaults: {
                    flex: 1,
                    labelAlign: 'right',
                    labelWidth: 50,
                    layout: 'form'
                },
                items: [{
                    xtype: 'label',
                    padding: '8 5 0 78',
                    html: 'No of Paxs <span class="req" style="color:red">*</span>:'
                }, {
                    xtype: 'numberfield',
                    fieldLabel: 'Adult',
                    allowBlank: false,
                    minValue: 1,
                    bind: {
                        value: '{reservation.paxsAdult}',
                        readOnly: '{!editable}'
                    }
                }, {
                    xtype: 'numberfield',
                    fieldLabel: 'Child',
                    minValue: 0,
                    bind: {
                        value: '{reservation.paxsChild}',
                        readOnly: '{!editable}'
                    }
                }]
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                xtype: 'radiogroup',
                layout: {
                    autoFlex: false
                },
                fieldLabel: 'Extra Bed',
                defaults: {
                    name: 'value',
                    margin: '0 15 0 0'
                },
                bind: {
                    value: '{reservation.extraBed}',
                    readOnly: '{!editable}'
                },
                items: [{
                    xtype: 'radiofield',
                    inputValue: false,
                    boxLabel: 'No',
                    checked: true
                }, {
                    xtype: 'radiofield',
                    inputValue: true,
                    boxLabel: 'Yes'
                }]
            }, {
                xtype: 'numberfield',
                fieldLabel: 'Total Room Night Taken',
                readOnly: true,
                bind: {
                    value: '{reservation.totalRoomCount}'
                }
            }]
        }, {
            xtype: 'label',
            margin: '-10 0 10 155',
            html: '<font style="color:#ff0000; font-size:11px; font-style: italic;">Request for Extra Bed at USD $40 net per unit per night (max. 1 unit per room)</font>'
        }, {
            xtype: 'textarea',
            margin: '5 0 20 0',
            fieldLabel: 'Remarks',
            bind: {
                value: '{reservation.remark}',
                readOnly: '{!editable}'
            }
        },{
            xtype: 'textarea',
            fieldLabel: 'Terms and Conditions',
            readOnly: true,
            cls: 'terms-condition-text',
            height: 120,
            annotation: false,
            scrollable: true,
            value:  '1.  Restriction apply for all black-out dates and subject to room availability' +
                    '\n2.  Reservation can be made 90 days in advance and occupancy below 80% upon request of reservation' +
                    '\n3.  In the event of overbooking, hotel reserved the right to source alternate hotel, similar or comparable, to place our guests' +
                    '\n4.  Hotel will contact guests and booker at least 3 days prior to arrival, to inform guest of the alternate hotel' +
                    '\n5.  Hotel will not provide any compensation and complimentary transportation to other hotel'
        }, {
            xtype: 'checkboxfield',
            cls: 'terms-condition-cb',
            boxLabel: 'I Agree to the Terms and Conditions ',
            bind: {
                value: '{reservation.agree}',
                readOnly: '{!editable}'
            }
        }, {
            xtype: 'label',
            margin: '30 0 0 2',
            html: '<font style="color:#00a2f9; font-size:11px;">NOTE: THIS FORM MUST BE SUBMITTED AND APPROVED MINIMUM 2 DAYS PRIOR TO THE REQUESTED DATE.</font>'
        }, {
            xtype:'fieldset',
            title: 'For reservation use only',
            layout: 'anchor',
            bind: {
                hidden: '{!onlyReservation}'
            },
            defaults: { anchor: '100%', labelWidth: 150},
            items: [{
                xtype: 'container',
                layout: 'hbox',  
                defaults: me.defaultsFieldSet,
                items: [{
                    xtype: 'datefield',
                    fieldLabel: 'Received Date<span class="req" style="color:red">*</span>',
                    listeners: {
                        expand: me.minimumDate
                    },
                    bind: {
                        value: '{reservation.receiveDate}',
                        readOnly: '{!reservationReadOnly}'
                    }
                }, {
                    xtype: 'textfield',
                    //minValue: 1,
                    fieldLabel: 'Confirmation Number<span class="req" style="color:red">*</span>',
                    bind: {
                        value: '{reservation.confirmationNumber}',
                        readOnly: '{!reservationReadOnly}'
                    }
                }]
            }]
        }];


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
