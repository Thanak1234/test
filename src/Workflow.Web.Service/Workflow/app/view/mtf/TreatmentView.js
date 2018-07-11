/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.mtf.TreatmentView", {
    extend: "Workflow.view.FormComponent",
    xtype: 'mtf-treatment-view',
    title: 'Medical Treatment',
    iconCls: 'fa fa-user',
    modelName: 'treatment',
    loadData: function (data, viewSetting) {
        var me = this, 
            viewmodel = me.getViewModel(),
            mainVM = me.mainView.getViewModel();
        var reference = me.getReferences();

        if (data) {
            var treatment = data.treatment;
            
            viewmodel.set('treatment', treatment);
            if (treatment && reference.dpTimeArrived) {
                reference.dpTimeArrived.setMinValue(treatment.CheckInDateTime);
            }
            
            if (treatment.Hours) {
                viewmodel.set('treatment.Hours', new Date(treatment.Hours));
            }
        }
    },
    
    buildComponent: function (component) {
        var me = this,
            viewmodel = me.getViewModel(),
            mainVM = me.mainView.getViewModel();
        
        var maxDateTime = Ext.Date.add(new Date(), Ext.Date.HOUR, 8);

        return [{
                xtype: 'combo',
                fieldLabel: 'Work Shift',
                maxWidth: 350,
                editable: false,    
                store: Ext.create('Ext.data.Store', {
                    fields: ['name', 'label'],
                    data: [
                        { "name": "MORNING", "label": "Morning" },
                        { "name": "NIGHT", "label": "Night" },
                        { "name": "SPLIT", "label": "Split" },
                        { "name": "OFFICE_HOURS", "label": "Office Hours" },
                        { "name": "AFTERNOON", "label": "Afternoon" },
                        { "name": "PH", "label": "PH" },
                        { "name": "DAY_OFF", "label": "Day Off" }
                    ]
                }),
                queryMode: 'local',
                displayField: 'label',
                valueField: 'name',
                allowBlank: false,
                bind: {
                    value: '{treatment.WorkShift}'
                },
                margin: '5 0 15 0'
        }, {
            xtype: 'textareafield',
            fieldLabel: "Submitter's Remark",
            bind: {
                value: '{treatment.Comment}'
            }
        }, {
            html: "DOCTOR'S CERTIFICATION: This is to certify that I have examined and treated the above named employee.",
            bind: {
                hidden: '{treatmentProperty.Annotation.hidden}'
            }
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                fieldLabel: 'Date/Time Arrived',
                xtype: 'datetime',
                labelWidth: 160,
                allowBlank: true,
                bind: {
                    value: '{treatment.CheckInDateTime}'
                }
            }, {
                fieldLabel: 'Date/Time Departed',
                xtype: 'datetime',
                allowBlank: true,
                maxValue: maxDateTime,
                listeners: { change: me.validateDate },
                bind: {
                    value: '{treatment.TimeDeparted}'
                }
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                fieldLabel: 'Date/Time Treatment From',
                xtype: 'datetime',
                labelWidth: 160,
                name: 'vTimeArrived',
                validateRang: 'vTimeArrived',
                allowBlank: false,
                maxValue: maxDateTime,
                listeners: {
                    change: me.validateDate
                },
                reference: 'dpTimeArrived',
                bind: {
                    value: '{treatment.TimeArrived}'
                }
            }, {
                fieldLabel: 'Date/Time Treatment To',
                xtype: 'datetime',
                validateRang: 'vTimeArrived',
                allowBlank: false,
                maxValue: maxDateTime,
                listeners: {
                    change: me.validateDate,
                    select: function (field, value) {
                        var viewmodel = me.getViewModel();
                        var checkOutDateTime = viewmodel.get('treatment.CheckOutDateTime');
                        
                        //if (!checkOutDateTime) {
                            viewmodel.set('treatment.CheckOutDateTime', value);
                        //}
                    }
                },
                bind: {
                    value: '{treatment.TimeDeparted}'
                }
            }]
        }, {
            xtype: 'textareafield',
            fieldLabel: "Symptom",
            allowBlank: false,
            margin: '15 0 20 0',
            bind: {
                value: '{treatment.Symptom}'
            }
        }, {
            xtype: 'textareafield',
            fieldLabel: "Diagnosis",
            allowBlank: false,
            bind: {
                value: '{treatment.Diagnosis}'
            }
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                xtype     : 'combo',
                fieldLabel: 'Status',
                allowBlank: false,
                bind      : {
                    value : '{treatment.FitToWork}'
                },
                store: Ext.create('Ext.data.Store', {
                    fields: ['name', 'label'],
                    data: [
                        { "name": 0, "label": "Unfit To Work" },
                        { "name": 1, "label": "Fit To Work" }
                    ]
                }),
                editable: false,
                maxWidth: 300,
                queryMode: 'local',
                displayField: 'label',
                valueField: 'name',
                listeners: {
                    afterrender: function(combo) {
                        if(!mainVM.get('unfittoworkProperty.hidden')){
                            mainVM.set('unfittoworkProperty.hidden', true);
                        }

                        if (!viewmodel.get('treatmentProperty.Remark.hidden')) {
                            viewmodel.set('treatmentProperty.Remark.hidden', true);
                        }

                        if(!viewmodel.get('treatmentProperty.Hours.hidden')){ // show
                            viewmodel.set('treatmentProperty.Hours.hidden', true);    
                        }
                    },
                    change: function(combo, value) {
                        if(!combo.isHidden()){
                            mainVM.set('unfittoworkProperty.hidden', value);
                            viewmodel.set('treatmentProperty.Hours.hidden', !value);
                            viewmodel.set('treatmentProperty.Remark.hidden', value);
                        }
                    }
                }
            },  {
                xtype: 'timefield',
                format: 'H:i',
                labelWidth: 80,
                maxWidth: 200,
                minValue: Ext.Date.parse('00:00:00 AM', 'h:i:s A'),
                maxValue: Ext.Date.parse('08:00:00 AM', 'h:i:s A'),
                fieldLabel: 'Rest Hours',
                value: Ext.Date.parse('00:00:00 AM', 'h:i:s A'),
                bind: {
                    value: '{treatment.Hours}'
                }
            }]
        }, {
            xtype: 'textareafield',
            fieldLabel: "Remark",
            allowBlank: false,
            bind: {
                value: '{treatment.Remark}'
            }
        }];

    }
});