/**
 *Author : Phanny 
 *
 */

Ext.define("Workflow.view.complimentary.crr.FormView", {
    extend: "Ext.form.Panel",
    xtype: 'crr-form-view',
    requires: [
        "Workflow.view.reservation.crr.FormViewController",
        "Workflow.view.reservation.crr.FormViewModel"
    ],

    controller: "crr-form",
    viewModel: {
        type: "crr-form"
    },

    title: 'Request Information',
    iconCls: 'x-fa fa-user',
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
        labelWidth: 150,
        layout: 'form',
        margin: '5 0 5 0'
    },
    layout: 'anchor',
    initComponent: function() {
        var me = this;
        me.items = [ {
            xtype: 'fieldset',
            title: 'Arrival Information',
            layout: 'anchor',
            collapsible: false,
            defaults: { anchor: '100%', labelWidth: 150 },
            items: [{
                xtype: 'container',
                layout: 'hbox',
                defaults: me.defaultsFieldSet,
                items: [{
                    xtype: 'datefield',
                    allowBlank: false,
                    labelWidth: 130,
                    fieldLabel: 'Arrival Date',
                    listeners: {
                        expand: me.minimumDate
                    },                    
                    bind: {
                        value: '{complimentary.arrivalDate}',
                        readOnly: '{!editable}'
                    }
                }, { // Transfer
                    xtype: 'radiogroup',                    
                    layout: { autoFlex: false },
                    fieldLabel: 'Airport Transfer',
                    value: 0,
                    reference: 'arrivalTransfer',
                    defaults: {
                        name: 'arrivalTransfer',
                        margin: '0 15 0 0'
                    },
                    bind: {
                        value: { arrivalTransfer: '{complimentary.arrivalTransfer}'},
                        readOnly: '{!editable}'
                    },
                    listeners: {
                        change: function (elm, newValue, oldValue, eOpts) {
                            
                            me.getReferences().arrivalVehicleTypeId.setValue(null);
                            me.getReferences().arrivalVehicleTypeId.setDisabled(!(newValue.arrivalTransfer));                            
                        }
                    },
                    items: [{
                        xtype: 'radiofield',                        
                        inputValue: true,
                        boxLabel: 'Yes',
                        checked: true
                    }, {
                        xtype: 'radiofield',                        
                        inputValue: false,
                        boxLabel: 'No'
                    }]
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaults: me.defaultsFieldSet,
                items: [{
                    xtype: 'textfield',
                    fieldLabel: 'Flight Detail',                    
                    labelWidth: 130,
                    bind: {
                        value: '{complimentary.arrivalFlightDetail}',
                        readOnly: '{!editable}'
                    }
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'Type of Vehicle',
                    reference: 'arrivalVehicleTypeId',
                    allowBlank: true,
                    xtype: 'lookupfield',                    
                    namespace: '[RESERVATION].[COMPLIMENTARY_ROOM].[VEHICLE_TYPE]',
                    bind: {
                        value: '{complimentary.arrivalVehicleTypeId}',
                        readOnly: '{!editable}',
                        disabled: '{disableArrivalVehicle}'
                        //filters: {
                        //    property: 'id',
                        //    value: 1
                        //}
                    }
                }]
            }]
        }, {
            xtype:'fieldset',
            title: 'Departure Information',
            layout: 'anchor',
            collapsible: false,
            defaults: { anchor: '100%', labelWidth: 150 },
            items: [{
                xtype: 'container',
                layout: 'hbox',
                defaults: me.defaultsFieldSet,
                items: [{
                    xtype: 'datefield',
                    fieldLabel: 'Departure Date',
                    allowBlank: false,
                    labelWidth: 130,
                    listeners: {
                        expand: me.minimumDate
                    },
                    bind: {
                        value: '{complimentary.departureDate}',
                        readOnly: '{!editable}'
                    }
                }, { // Transfer
                    xtype: 'radiogroup',                    
                    layout: { autoFlex: false },
                    fieldLabel: 'Airport Transfer',
                    reference: 'departureTransfer',
                    defaults: {
                        name: 'departureTransfer',
                        margin: '0 15 0 0'
                    },
                    bind: {
                        value: { departureTransfer: '{complimentary.departureTransfer}' },
                        readOnly: '{!editable}'
                    },
                    listeners: {
                        change: function (elm, newValue, oldValue, eOpts) {                            
                            me.getReferences().departureVehicleTypeId.setValue(null);
                            me.getReferences().departureVehicleTypeId.setDisabled(!(newValue.departureTransfer));                            
                        }
                    },
                    items: [{
                        xtype: 'radiofield',                        
                        inputValue: true,
                        boxLabel: 'Yes',
                        checked: true
                    }, {
                        xtype: 'radiofield',                        
                        inputValue: false,
                        boxLabel: 'No'
                    }]
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaults: me.defaultsFieldSet,
                items: [{
                    xtype: 'textfield',
                    labelWidth: 130,
                    fieldLabel: 'Flight Detail',
                    bind: {
                        value: '{complimentary.departureFlightDetail}',
                        readOnly: '{!editable}'
                    }
                },{
                    xtype: 'textfield',
                    fieldLabel: 'Type of Vehicle',
                    reference: 'departureVehicleTypeId',
                    allowBlank: true,
                    xtype: 'lookupfield',
                    
                    namespace: '[RESERVATION].[COMPLIMENTARY_ROOM].[VEHICLE_TYPE]',
                    bind: {
                        value: '{complimentary.departureVehicleTypeId}',
                        readOnly: '{!editable}',
                        disabled: '{disableDepartureVehicle}'
                        //filters: {
                        //    property: 'id',
                        //    value: 1
                        //}
                    }
                }]
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                xtype: 'radiogroup',                
                layout: { autoFlex: false },
                fieldLabel: 'Only Room Charge Complimentary',
                labelWidth: 200,
                reference: 'roomCharge',
                defaults: {
                    name: 'roomCharge',
                    margin: '0 15 0 0'
                },
                bind: {
                    value: { roomCharge: '{complimentary.roomCharge}' },
                    readOnly: '{!editable}'
                },
                items: [{
                    xtype: 'radiofield',                    
                    inputValue: true,
                    boxLabel: 'Yes',
                    checked: true
                }, {
                    xtype: 'radiofield',                    
                    inputValue: false,
                    boxLabel: 'No'
                }]
            }, {
                xtype: 'numberfield',
                fieldLabel: 'No. of Room',
                allowBlank: false,
                minValue: 0,
                bind: {
                    value: '{complimentary.room}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaults: me.defaultsFieldSet,
                items: [{
                    xtype: 'textfield',

                    fieldLabel: 'Confirmation Number<span class="req" style="color:red">*</span>',
                    minValue: 1,
                    bind: {
                        value: '{complimentary.confirmationNumber}',
                        readOnly: '{!complimentaryEditConfirmationNumber}',
                        hidden: '{!complimentaryShowConfirmationNumber}'
                    }

                }]
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                xtype: 'numberfield',
                fieldLabel: 'No. of Guest',
                allowBlank: false,
                minValue: 0,
                bind: {
                    value: '{complimentary.adult}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'numberfield',
                fieldLabel: 'Children',
                minValue: 0,
                bind: {
                    value: '{complimentary.childrent}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'radiogroup',                
                layout: { autoFlex: false },
                fieldLabel: 'Extra Bed',
                reference: 'extraBed',
                defaults: {
                    name: 'extraBed',
                    margin: '0 15 0 0'
                },
                bind: {
                    value: { extraBed: '{complimentary.extraBed}' },
                    readOnly: '{!editable}'
                },
                items: [{
                    xtype: 'radiofield',                    
                    inputValue: true,
                    boxLabel: 'Yes',
                    checked: true
                }, {
                    xtype: 'radiofield',                    
                    inputValue: false,
                    boxLabel: 'No'
                }]
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                xtype: 'lookupfield',
                fieldLabel: 'Room Category',
                reference: 'roomCategory',
                maxWidth: 360,
                publishes: 'value',
                listeners: {
                    select: function (combo, record, e) {
                        var data = record.getData(),
							viewmodel = me.getViewModel();
                        viewmodel.set('disableSubCategory', !data.hasChild);
                        if (!data.hasChild) {
                            viewmodel.set('subRoomCategory', 0);
                        }
						viewmodel.set('roomSubCategoryFilter', {
							property: 'parentId',
							value: combo.value
						});
                    }
                },
                namespace: '[RESERVATION].[COMPLIMENTARY_ROOM].[ROOM_CATEGORY]',
                bind: {
                    value: '{complimentary.roomCategoryId}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'lookupfield',
                hiddenLabel: true,
                reference: 'roomSubCategory',
                labelWidth: 15,
                maxWidth: 255,
                fieldLabel: ' ',
                isChild: true,
                disabled: true,
                emptyText: 'Room Subcategory',
                namespace: '[RESERVATION].[COMPLIMENTARY_ROOM].[ROOM_CATEGORY]',
                bind: {
                    value: '{complimentary.RoomSubCategoryId}',
                    readOnly: '{!editable}',
					disabled: '{disableSubCategory}',
                    filters: '{roomSubCategoryFilter}'
                },
				listeners: {
                    change: function (combo, value, e) {
                        var viewmodel = me.getViewModel();
                        viewmodel.set('disableSubCategory', (value == 0));
                    }
                }
            }, {
                xtype: 'lookupfield',
                fieldLabel: 'VIP Status',
                allowBlank: true,
                editable: true,                
                //emptyText: 'Select a VIP Status...',                
                //maxWidth: 260,
                namespace: '[RESERVATION].[COMPLIMENTARY_ROOM].[VIP_STATUS]',
                bind: {
                    value: '{complimentary.vipStatusId}',
                    readOnly: '{!editable}'
                }                
            }]
        }, {
            xtype: 'textfield',
            fieldLabel: 'Room Charges: If Yes, please indicate department to be charged',
            labelWidth: 360,
            bind: {
                value: '{complimentary.departmentIncharge}',
                readOnly: '{!editable}'
            }
        },

        /* new Fields*/
        {
            xtype: 'fieldset',
            title: 'Will the following expenses be borne by the Company/Department <span style="color:red;">*</span>',
            layout: 'anchor',
            allowBlank: false,
            collapsible: false,
            defaults: { anchor: '100%', labelWidth: 200 },
            items: [{
                xtype: 'container',
                layout: 'hbox',
                defaults: me.defaultsFieldSet,
                items: [{ // Meals excluding alcohol
                    xtype: 'radiogroup',
                    layout: { autoFlex: false },
                    fieldLabel: 'Meals excluding alcohol',
                    allowBlank: false,
                    labelWidth: 160,
                    reference: 'mealExcludingAlcohol',
                    defaults: {
                        name: 'mealExcludingAlcohol',
                        margin: '0 15 0 0'
                    },
                    bind: {
                        value: { mealExcludingAlcohol: '{checkExpense.mealExcludingAlcohol}' },
                        readOnly: '{!editable}'
                    },
                    listeners: {
                        change: function (elm, newValue, oldValue, eOpts) {
                            var viewmodel = me.getViewModel();
                            this.mealExcludingAlcohol = newValue;
                        }
                    },
                    items: [{
                        xtype: 'radiofield',
                        inputValue: true,
                        boxLabel: 'Yes'
                    }, {
                        xtype: 'radiofield',
                        inputValue: false,
                        boxLabel: 'No'
                        //checked: true
                    }]
                }, { // Souvenir Shop
                    xtype: 'radiogroup',
                    layout: { autoFlex: false },
                    fieldLabel: 'Souvenir Shop',
                   
                    allowBlank: false,
                    labelWidth: 160,
                    reference: 'souvenirShop',
                    defaults: {
                        name: 'souvenirShop',
                        margin: '0 15 0 0'
                    },
                    bind: {
                        value: { souvenirShop: '{checkExpense.souvenirShop}' },
                        readOnly: '{!editable}'
                    },
                    listeners: {
                        change: function (elm, newValue, oldValue, eOpts) {
                            var viewmodel = me.getViewModel();
                            this.souvenirShop = newValue;
                        }
                    },
                    items: [{
                        xtype: 'radiofield',
                        inputValue: true,
                        boxLabel: 'Yes'
                    }, {
                        xtype: 'radiofield',
                        inputValue: false,
                        boxLabel: 'No'
                        //checked: true
                    }]
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaults: me.defaultsFieldSet,
                items: [{ // Alcohol
                    xtype: 'radiogroup',
                    layout: { autoFlex: false },
                    fieldLabel: 'Alcohol',
                  
                    allowBlank: false,
                    labelWidth: 160,
                    reference: 'alcohol',
                    defaults: {
                        name: 'alcohol',
                        margin: '0 15 0 0'
                    },
                    bind: {
                        value: { alcohol: '{checkExpense.alcohol}' },
                        readOnly: '{!editable}'
                    },
                    listeners: {
                        change: function (elm, newValue, oldValue, eOpts) {
                            var viewmodel = me.getViewModel();
                            this.alcohol = newValue;                            
                        }
                    },
                    items: [{
                        xtype: 'radiofield',
                        inputValue: true,
                        boxLabel: 'Yes'
                        
                    }, {
                        xtype: 'radiofield',
                        inputValue: false,
                        boxLabel: 'No'
                        //checked: true
                    }]
                }, { // Airport Transfer
                    xtype: 'radiogroup',
                    layout: { autoFlex: false },
                    fieldLabel: 'Airport Transfer',
                   
                    allowBlank: false,
                    labelWidth: 160,
                    reference: 'airportTransfers',
                    defaults: {
                        name: 'airportTransfers',
                        margin: '0 15 0 0'
                    },
                    bind: {
                        value: { airportTransfers: '{checkExpense.airportTransfers}' },
                        readOnly: '{!editable}'
                    },
                    listeners: {
                        change: function (elm, newValue, oldValue, eOpts) {
                            var viewmodel = me.getViewModel();
                            this.airportTransfers = newValue;
                        }
                    },
                    items: [{
                        xtype: 'radiofield',
                        inputValue: true,
                        boxLabel: 'Yes'
                    }, {
                        xtype: 'radiofield',
                        inputValue: false,
                        boxLabel: 'No'
                        //checked: true
                    }]
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaults: me.defaultsFieldSet,
                items: [{ // Tobacco
                    xtype: 'radiogroup',
                    layout: { autoFlex: false },
                    fieldLabel: 'Tobacco',
                   
                    allowBlank: false,
                    labelWidth: 160,
                    reference: 'tobacco',
                    defaults: {
                        name: 'tobacco',
                        margin: '0 15 0 0'
                    },
                    bind: {
                        value: { tobacco: '{checkExpense.tobacco}' },
                        readOnly: '{!editable}'
                    },
                    listeners: {
                        change: function (elm, newValue, oldValue, eOpts) {
                            var viewmodel = me.getViewModel();
                            this.tobacco = oldValue;
                        }
                    },
                    items: [{
                        xtype: 'radiofield',
                        inputValue: true,
                        boxLabel: 'Yes'
                    }, {
                        xtype: 'radiofield',
                        inputValue: false,
                        boxLabel: 'No'
                        //checked: true
                    }]
                }, { // Other Transport Within City
                    xtype: 'radiogroup',
                    layout: { autoFlex: false },
                    fieldLabel: 'Other Transport Within City',
                  
                    allowBlank: false,
                    labelWidth:160,
                    reference: 'otherTransportwithinCity',
                    defaults: {
                        name: 'otherTransportwithinCity',
                        margin: '0 15 0 0'
                    },
                    bind: {
                        value: { otherTransportwithinCity: '{checkExpense.otherTransportwithinCity}' },
                        readOnly: '{!editable}'
                    },
                    listeners: {
                        change: function (elm, newValue, oldValue, eOpts) {
                            var viewmodel = me.getViewModel();
                            this.otherTransportWithinCity = oldValue;
                        }
                    },
                    items: [{
                        xtype: 'radiofield',
                        inputValue: true,
                        boxLabel: 'Yes'
                    }, {
                        xtype: 'radiofield',
                        inputValue: false,
                        boxLabel: 'No'
                        //checked: true
                    }]
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaults: me.defaultsFieldSet,
                items: [{ // Spa
                    xtype: 'radiogroup',
                    layout: { autoFlex: false },
                    fieldLabel: 'Spa',
                    labelWidth: 160,
                    
                    allowBlank: false,
                    reference: 'spa',
                    defaults: {
                        name: 'spa',
                        margin: '0 15 0 0'
                    },
                    bind: {
                        value: { spa: '{checkExpense.spa}' },
                        readOnly: '{!editable}'
                    },
                    listeners: {
                        change: function (elm, newValue, oldValue, eOpts) {
                            var viewmodel = me.getViewModel();
                            this.spa = oldValue;
                        }
                    },
                    items: [{
                        xtype: 'radiofield',
                        inputValue: true,
                        boxLabel: 'Yes'
                    }, {
                        xtype: 'radiofield',
                        inputValue: false,
                        boxLabel: 'No'
                        //checked: true
                    }]
                }, { // Extra Bed
                    xtype: 'radiogroup',
                    layout: { autoFlex: false },
                    fieldLabel: 'Extra bed',
                    
                    allowBlank: false,
                    labelWidth: 160,
                    reference: 'extraBeds',
                    defaults: {
                        name: 'extraBeds',
                        margin: '0 15 0 0'
                    },
                    bind: {
                        value: { extraBeds: '{checkExpense.extraBeds}' },
                        readOnly: '{!editable}'
                    },
                    listeners: {
                        change: function (elm, newValue, oldValue, eOpts) {
                            var viewmodel = me.getViewModel();
                            this.extraBeds = oldValue;
                        }
                    },
                    items: [{
                        xtype: 'radiofield',
                        inputValue: true,
                        boxLabel: 'Yes'
                    }, {
                        xtype: 'radiofield',
                        inputValue: false,
                        boxLabel: 'No'
                        //checked: true
                    }]
                }]
            }]
        }, 
        /*end new Fields*/
        {
            xtype: 'label',
            margin: '-10 0 10 10',
            html: '<font style="font-size:11px;">For others (e.g. Restaurants, Laundry, Airport Transfers) please indicate clearly</font>'
        }, {
            xtype: 'textfield',
            fieldLabel: 'Special Request',
            bind: {
                value: '{complimentary.specialRequest}',
                readOnly: '{!editable}'
            }
        }, {
            xtype: 'lookupfield',
            fieldLabel: 'Purpose',
            namespace: '[RESERVATION].[COMPLIMENTARY_ROOM].[PURPOSE]',
            bind: {
                value: '{complimentary.purposeId}',
                readOnly: '{!editable}'
                //filters: {
                //    property: 'id',
                //    value: 1
                //}
            }
        }, {
            xtype: 'textarea',
            margin: '5 0 20 0',
            fieldLabel: 'Remarks',
            bind: {
                value: '{complimentary.remark}',
                readOnly: '{!editable}'
            }
        }, {
            xtype: 'textarea',
            fieldLabel: 'Terms and Conditions',
            readOnly: true,
            cls: 'terms-condition-text',
            height: 130,
            annotation: false,
            scrollable: true,
            value: 'Complimentary room request must provide the reason of stay.' +
                    '\nAll airport transfer request must provide flight details upon reservation.' +
                    '\nOne booking request is for same arrival and departure date.' +
                    '\nSame guest name is not allowed to register more than one room.' +
                    '\nAll complimentary room request are subject to room availability.'+
                    '\nFinal approval from Hotel General Manager.'
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
