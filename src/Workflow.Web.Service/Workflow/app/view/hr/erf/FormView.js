/**
 *Author : Phanny 
 *
 */

Ext.define("Workflow.view.hr.erf.FormView", {
    extend: "Ext.form.Panel",
    xtype: 'erf-form-view',
    requires: [
        "Workflow.view.hr.erf.FormViewController",
        "Workflow.view.hr.erf.FormViewModel"
    ],

    controller: "erf-form",
    viewModel: {
        type: "erf-form"
    },

    title: 'Position Information &amp; Requirement',
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
        //defaultType: 'textfield',
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
                fieldLabel: 'Job Title',
                regex: /.*\S.*/,
                allowBlank: false,
                bind: {
                    value: '{requisition.position}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'checkbox',
                fieldLabel: 'Private &amp; Confidential',
                labelWidth: 220,
                bind: {
                    value: '{requisition.private}',
                    readOnly: '{!editable}'
                }
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                xtype: 'textfield',
                fieldLabel: 'Reporting To',
                bind: {
                    value: '{requisition.reportingTo}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'textfield',
                fieldLabel: 'Propose Salary Range (per month)',
                labelWidth:220,
                bind: {
                    value: '{requisition.salaryRange}',
                    readOnly: '{!editable}'
                }
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                xtype: 'lookupfield',
                fieldLabel: 'Request Type',
                reference: 'requestType',
                publishes: 'value',
                namespace: '[HR].[EMPLOYEE_REQUISITION].[REQUEST_TYPE]',
                bind: {
                    value: '{requisition.requestTypeId}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'numberfield',
                fieldLabel: 'Number',
                labelWidth: 220,
                bind: {
                    value: '{requisition.requisitionNumber}',
                    readOnly: '{!editable}'
                }
            }]
        }, {
            xtype: 'container',
            layout: 'hbox',
            defaults: me.defaultsFieldSet,
            items: [{
                xtype: 'lookupfield',
                fieldLabel: 'Position Type',
                reference: 'shiftType',
                publishes: 'value',
                maxWidth: 290,
                namespace: '[HR].[EMPLOYEE_REQUISITION].[SHIFT_TYPE]',
                bind: {
                    value: '{requisition.shiftTypeId}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'lookupfield',
                reference: 'localtionType',
                publishes: 'value',
                fieldLabel: '&nbsp;',
                labelWidth: 5,
                maxWidth:180,
                allowBlank: true,
                namespace: '[HR].[EMPLOYEE_REQUISITION].[LOCATION_TYPE]',
                bind: {
                    value: '{requisition.locationTypeId}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'label'
            }]
        }, {
            xtype: 'fieldset',
            title: 'Justification For Requirement',
            defaults: { anchor: '100%', labelWidth: 0, msgTarget: 'side' },
            layout: 'anchor',
            items: [{
                xtype: 'label',
                html: '<p style="color:#404040;">(If replacement, please advice name and employee ID of departed staff)</p>'
            },{
                xtype: 'textarea',
                bind: {
                    value: '{requisition.justification}',
                    readOnly: '{!editable}'
                }
            }]
        }, {
            xtype: 'fieldset',
            title: 'For Group Human Resource Department',
            defaults: { anchor: '100%', labelWidth: 150, msgTarget: 'side' },
            layout: 'anchor',
            bind: {
                visible: '{showRefNo}'
            },
            items: [{
                xtype: 'textfield',
                fieldLabel: 'Reference Number',
                readOnly: true,
                bind: {
                    value: '{requisition.referenceNo}'
                }
            }]
        }];


        me.callParent(arguments);
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
