/**
 *Author : Phanny 
 *
 */

Ext.define("Workflow.view.pbf.FormView", {
    extend: "Ext.form.Panel",
    xtype: 'pbf-form-view',
    requires: [
        "Workflow.view.pbf.FormViewController",
        "Workflow.view.pbf.FormViewModel"
    ],

    controller: "pbf-form",
    viewModel: {
        type: "pbf-form"
    },

    title: 'Project Brief',
    iconCls: 'fa fa-user',
    formReadonly : false,
    minHeight: 100,
    autoWidth: true,
    frame: false,
    bodyPadding: '10',
    method: 'POST',
    defaults: {
        flex: 1,
        anchor: '100%',
        defaultType: 'labelfield',
        labelAlign: 'right',
        labelWidth: 150,
        layout: 'form',
        margin: '5 0 5 0'
    },
    layout: 'anchor',
    initComponent: function() {
        var me = this;
        me.items = [
            {
                xtype: 'textfield',
                fieldLabel: 'Project Name',
                regex: /.*\S.*/,
                allowBlank: false,
                bind: {
                    value: '{projectBrief.projectName}',
                    readOnly: '{!userRequest}'
                }
            }, {
                xtype: 'textfield',
                fieldLabel: 'Business Unit',
                bind: {
                    value: '{projectBrief.businessUnit}',
                    readOnly: '{!userRequest}'
                }
            }, {
                xtype: 'textfield',
                fieldLabel: 'Project Lead',
                bind: {
                    value: '{projectBrief.projectLead}',
                    readOnly: '{!userRequest}'
                }
            }, {
                xtype: 'textfield',
                fieldLabel: 'Account Manager',
                bind: {
                    value: '{projectBrief.accountManager}',
                    readOnly: '{!userRequest}'
                }
            }, {
                xtype: 'currencyfield',
                fieldLabel: 'Budget',
                minValue: 0,
                bind: {
                    value: '{projectBrief.budget}',
                    readOnly: '{!userRequest}'
                }
            }, {
                xtype: 'textfield',
                fieldLabel: 'Billing Info',
                bind: {
                    value: '{projectBrief.billingInfo}',
                    readOnly: '{!userRequest}'
                }
            }, {
                xtype: 'textfield',
                fieldLabel: 'Project Nr',
                readOnly: true,
                bind: {
                    value: '{projectBrief.projectReference}',
                    hidden: '{!visibleNr}'
                }
            }, {
                xtype: 'container',
                layout: 'hbox',
                defaults: me.defaultsFieldSet,
                items: [{
                    xtype: 'datesvrfield',
                    fieldLabel: 'Submission Date',
                    name: 'submissionDate',
                    submissionDate: true,
                    listeners: { change: me.validateDate },
                    bind: {
                        value: '{projectBrief.submissionDate}',
                        readOnly: '{!userRequest}'
                    }
                }, {
                    xtype: 'datesvrfield',
                    fieldLabel: 'Required Date',
                    submissionDate: true,
                    listeners: { change: me.validateDate },
                    bind: {
                        value: '{projectBrief.requiredDate}',
                        readOnly: '{!userRequest}'
                    }
                }]
            }, {
                xtype: 'pbf-specification-view',
                reference: 'specificationView',
                mainView: this,
                border: true
            }, { html : 'Project Introduction: What is this project for?'}, {
                xtype: 'textareafield',
                margin: '5 0 5 8',
                bind: {
                    value: '{projectBrief.introduction}',
                    readOnly: '{!userRequest}'
                }
            }, { html: 'Who is the target Market?' }, {
                xtype: 'textareafield',
                margin: '5 0 5 8',
                bind: {
                    value: '{projectBrief.targetMarket}',
                    readOnly: '{!userRequest}'
                }
            }, { html: 'Where will we use it?' }, {
                xtype: 'textareafield',
                margin: '5 0 5 8',
                bind: {
                    value: '{projectBrief.usage}',
                    readOnly: '{!userRequest}'
                }
            }, {
                xtype: 'panel',
                border: 1,
                title: 'COMMS AND CREATIVE',
                bodyPadding: '10 20 10 10',
                bind: {
                    hidden: '{!showTechnician}'
                },
                items: me.buildTechnicianSec()
            }];


        me.callParent(arguments);
    },
    buildTechnicianSec: function () {
        var me = this;
        return [/*{
            xtype: 'label',
            html: 'Technical Briefing',
            bind: {
                hidden: '{!showTechnician}'
            }
        }, */{
            xtype: 'container',
            layout: 'hbox',
            defaults: me.techFieldSet,
            bind: {
                hidden: '{!showTechnician}'
            },
            items: [{
                xtype: 'textareafield',
                fieldLabel: 'Technical Briefing',
                bind: {
                    value: '{projectBrief.briefing}',
                    readOnly: '{!technicianReadOnly}',
                    hidden: '{!showTechnician}'
                }
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.techFieldSet,
            bind: {
                hidden: '{!showTechnician}'
            },
            items: [{
                xtype: 'textfield',
                fieldLabel: 'Design Duration',
                bind: {
                    value: '{projectBrief.designDuration}',
                    readOnly: '{!technicianReadOnly}'
                }
            }, {
                xtype: 'textfield',
                fieldLabel: 'Project Duration',
                bind: {
                    value: '{projectBrief.productDuration}',
                    readOnly: '{!technicianReadOnly}'
                }
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.techFieldSet,
            bind: {
                hidden: '{!showTechnician}'
            },
            items: [{
                xtype: 'datesvrfield',
                fieldLabel: 'Brain Storm',
                name: 'brainStorm',
                submissionDate: true,
                listeners: { change: me.validateDate },
                bind: {
                    value: '{projectBrief.brainStorm}',
                    readOnly: '{!technicianReadOnly}'
                }
            }, {
                xtype: 'datesvrfield',
                fieldLabel: 'Project Start',
                name: 'projectStart',
                submissionDate: true,
                listeners: { change: me.validateDate },
                bind: {
                    value: '{projectBrief.projectStart}',
                    readOnly: '{!technicianReadOnly}'
                }
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.techFieldSet,
            bind: {
                hidden: '{!showTechnician}'
            },
            items: [{
                xtype: 'datesvrfield',
                fieldLabel: '1st Revision',
                name: 'firstRevision',
                submissionDate: true,
                listeners: { change: me.validateDate },
                bind: {
                    value: '{projectBrief.firstRevision}',
                    readOnly: '{!technicianReadOnly}'
                }
            }, {
                xtype: 'datesvrfield',
                fieldLabel: '2nd Revision',
                name: 'secondRevision',
                submissionDate: true,
                listeners: { select: function (p, v) { me.validateDate(me, p, v); } },
                bind: {
                    value: '{projectBrief.secondRevision}',
                    readOnly: '{!technicianReadOnly}'
                }
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.techFieldSet,
            bind: {
                hidden: '{!showTechnician}'
            },
            items: [{
                xtype: 'datesvrfield',
                fieldLabel: 'Final Approval',
                name: 'finalApproval',
                submissionDate: true,
                listeners: { change: me.validateDate },
                bind: {
                    value: '{projectBrief.finalApproval}',
                    readOnly: '{!technicianReadOnly}'
                }
            }, {
                xtype: 'datesvrfield',
                fieldLabel: 'Completion',
                name: 'completion',
                submissionDate: true,
                listeners: { change: me.validateDate },
                bind: {
                    value: '{projectBrief.completion}',
                    readOnly: '{!technicianReadOnly}'
                }
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.techFieldSet,
            bind: {
                hidden: '{!showTechnician}'
            },
            items: [{
                xtype: 'datesvrfield',
                fieldLabel: 'Deadline',
                submissionDate: true,
                name: 'dateline',
                listeners: { change: me.validateDate },
                bind: {
                    value: '{projectBrief.dateline}',
                    readOnly: '{!technicianReadOnly}'
                }
            }, {
                xtype: 'label'
            }]
        }];
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
    techFieldSet: function () {
        return {
            flex: 1,
            anchor: '80%',
            defaultType: 'textfield',
            labelAlign: 'right',
            labelWidth: 120,
            layout: 'form',
            margin: '5 0 5 0'
        };
    },
    validateDate: function (picker) {
        var frm = picker.up('form') ? picker.up('form') : picker.down('form')
        if (frm) {
            var form = frm.getForm(),
            fields = form.getFields(),
            submissionDateField = form.findField('submissionDate'),
            submissionDate = submissionDateField.getValue();
            var valid = true;

            fields.each(function (field) {
                if (field.xtype == 'datesvrfield' && field.name != 'submissionDate' && submissionDate) {
                    field.setMinValue(submissionDate);
                    field.validate();
                    field.dateRangeMin = submissionDate;
                }
            });
        }
    }
});
