/**
 *Author : Yim Samaune 
 *
 */


Ext.define("Workflow.view.bcj.ProjectDetailView", {
    extend: "Ext.form.Panel",
    xtype : 'bcj-project-detail-view',
    requires: [
        "Workflow.view.bcj.ProjectDetailViewController",
        "Workflow.view.bcj.ProjectDetailViewModel"
    ],

    controller: "bcj-projectdetailview",
    viewModel: {
        type: "bcj-projectdetailview"
    },

    title : 'Business Case Justification',
    formReadonly : false,
    minHeight: 100,
    autoWidth: true,
    frame: false,
    bodyPadding: '10 10 0',
    method: 'POST',
    defaultListenerScope: true,
    defaults: {
        flex: 1,
        anchor: '100%',
        msgTarget: 'side',
        labelWidth: 150,
        defaultType: 'textfield',
        layout: 'form',
        xtype: 'container'
    },
    initComponent: function() {
        var me=this;
        me.items = [{
            xtype:'fieldset',
            title: 'Name of Item or Project',
            defaults: { anchor: '100%', labelWidth: 280, msgTarget: 'side'},
            layout: 'anchor',
            items: [{
                fieldLabel: 'Name of Item or Project ',
                //labelClsExtra: 'x-required',
                allowBlank: false,
                bind: {
                    value: '{projectDetail.projectName}',
                    readOnly: '{!projectItemEditable}'
                }
            }, {
                xtype: 'combo',
                fieldLabel: 'Branch of Co Name:',
                allowBlank: false,
                editable: false,
                displayField: 'name',
                valueField: 'coporationBranch',
                store:  Ext.create('Ext.data.Store', {
                    fields: ['coporationBranch', 'name'],
                    data : [
                        { "coporationBranch": "Non Gaming", "name": "N1 - Non Gaming" },
                        { "coporationBranch": "Gaming", "name": "N1 - Gaming" },
                        { "coporationBranch": "N2 - Non Gaming", "name": "N2 - Non Gaming" },
                        { "coporationBranch": "N2 - Gaming", "name": "N2 - Gaming" },
                        { "coporationBranch": "Others", "name": "Shared" }
                    ]
                }),
                bind: {
                    value: '{projectDetail.coporationBranch}',
                    readOnly: '{!projectItemEditable}'
                }
            }, {
                xtype: 'combo',
                fieldLabel: 'Capex Category:',
                allowBlank: false,
                editable: false,
                displayField: 'name',
                valueField: 'id',
                store: Ext.create('Ext.data.Store', {
                    fields: ['id', 'name'],
                    data: [
                        { "id": 12, "name": "AV/Event Equipment - Hotel" },
                        { "id": 13, "name": "AV/Event Equipment - Gaming" },
                        { "id": 15, "name": "AV/Event Equipment - Corporate" },
                        { "id": 17, "name": "Surveillance Equipment" },
                        { "id": 19, "name": "Fire Safety Equipment" },
                        { "id": 20, "name": "Security Equipment" },
                        { "id": 21, "name": "F&B Equipment" },
                        { "id": 22, "name": "IT hardware/software - Hotel" },
                        { "id": 23, "name": "IT hardware/software - Gaming" },
                        { "id": 24, "name": "IT hardware/software - Corporate" },
                        { "id": 26, "name": "Table Equipment, Furniture" },
                        { "id": 28, "name": "Electronic Gaming Equipment, Furniture" },
                        { "id": 29, "name": "Hotel equipment & service (electronic/electrical item, furniture & etc)" },
                        { "id": 30, "name": "Stationery/Admin equipment" },
                        { "id": 31, "name": "Interior/exterior renovation/refurbishment" },
                        { "id": 32, "name": "Construction/land/property development" },
                        { "id": 33, "name": "Motor/vehicles" },
                        { "id": 34, "name": "Kitchen Equipment" },
                        { "id": 36, "name": "Aircraft" }
                    ]
                }),
                bind: {
                    value: '{projectDetail.capexCategoryId}',
                    readOnly: '{!projectItemEditable}'
                }
            }]
        }, {
            xtype: 'fieldset',
            title: 'Project Description-What do you want to do?',
            defaults: { anchor: '100%', labelWidth: 280, msgTarget: 'side' },
            layout: 'anchor',
            items: [{
                xtype: 'textarea',
                fieldLabel: '1. Provide a brief description',
                allowBlank: false,
                emptyText: 'What do you want to do?',
                bind: {
                    value: '{projectDetail.whatToDo}',
                    readOnly: '{!editable}'
                }
            },{
				xtype: 'label', 
				html: 'Note: Please specify a username for request PC/Laptop.', 
				margin:'0 0 0 285'
			}]
        }, {
            xtype: 'fieldset',
            title: 'Justification for Spend-Why do you want to do this?',
            defaults: { anchor: '100%', labelWidth: 280, msgTarget: 'side' },
            layout: 'anchor',
            items: [{
                xtype: 'textarea',
                fieldLabel: '2. Explain the existing arrangement',
                emptyText: 'Is there an existing asset? How old is it? ...',
                bind: {
                    value: '{projectDetail.arrangement}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'textarea',
                fieldLabel: '3. Outline the need for this expenditure',
                emptyText: 'Why do you need this? ...',
                bind: {
                    value: '{projectDetail.whyToDo}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'label',
                text: 'Consider ROI (increasing profits), WHS, Critical to Operation, etc',
                margin: '0 0 0 285'
            }]
        }, {
            xtype: 'fieldset',
            title: 'Name of Item or Project',
            defaults: { anchor: '100%', labelWidth: 280, msgTarget: 'side' },
            layout: 'anchor',
            items: [{
                xtype: 'label',
                text: '4. Provide a detailed description'
            }, {
                xtype: 'currencyfield',
                fieldLabel: 'Estimated Capital Expenditure',
                readOnly: true,
                margin: '10 0 10 0',
                bind: {
                    value: '{projectDetail.totalAmount}'
                    //readOnly: '{!editable}'
                }
            }, {
                xtype: 'bcj-request-item-view',
                reference: 'requestItemView',
                mainView: this,
                border: true
            }, {
                xtype: 'textarea',
                margin: '10 0 0 0',
                emptyText: 'Explain pricing if needed, quotes etc. If contingency please explain, include all taxes, freight and installation costs.',
                bind: {
                    value: '{projectDetail.capitalRequired}',
                    readOnly: '{!editable}'
                }
            }]
        }, {
            xtype: 'fieldset',
            title: 'Financial Analysis',
            defaults: { anchor: '100%', labelWidth: 280, msgTarget: 'side' },
            layout: 'anchor',
            items: [{
                xtype: 'label',
                text: '5. State the quantifiable benefits'
            }, {
                xtype: 'currencyfield',
                fieldLabel: 'Estimated Capital Expenditure',
                minValue: 1,
                allowBlank: false,
                margin: '10 0 10 0',
                bind: {
                    value: '{projectDetail.estimateCapex}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'currencyfield',
                fieldLabel: 'Incremental Net Contribution',
                readOnly: true,
                bind: {
                    value: '{projectDetail.incrementalNetContribution}'
                    //readOnly: '{!editable}'
                }
            }, {
                fieldLabel: 'Payback in years',
                bind: {
                    value: '{projectDetail.paybackYear}'
                    //readOnly: '{!editable}'
                },
                readOnly: true,
                emptyText: 'N/A'
            }, {
                xtype: 'bcj-analysis-item-view',
                reference: 'analysisItemView',
                mainView: this,
                border: true
            }]
        }, {
            xtype: 'fieldset',
            title: 'Other Benefits-What are the benefits other than cost saving or revenue generation',
            defaults: { anchor: '100%', labelWidth: 280, msgTarget: 'side' },
            layout: 'anchor',
            items: [{
                xtype: 'textarea',
                fieldLabel: '6. Outline the non-quantifiable benefits',
                bind: {
                    value: '{projectDetail.outlineBenefit}',
                    readOnly: '{!editable}'
                }
            }]
        }, {
            xtype: 'fieldset',
            title: 'Timing-When are you going to do it? Start and Finish Dates',
            defaults: { anchor: '100%', labelWidth: 280, msgTarget: 'side' },
            layout: 'anchor',
            items: [{
                xtype: 'label',
                text: '7. Provide anticipated project commencement date and expected completion date'
            }, {
                xtype: 'datefield',
                fieldLabel: 'Commencement',
                margin: '10 0 10 0',
                bind: {
                    value: '{projectDetail.commencement}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'datefield',
                fieldLabel: 'Completion',
                bind: {
                    value: '{projectDetail.completion}',
                    readOnly: '{!editable}'
                }
            }]
        }, {
            xtype: 'fieldset',
            title: 'Alternatives - What else could we do?',
            defaults: { anchor: '100%', labelWidth: 280, msgTarget: 'side' },
            layout: 'anchor',
            items: [{
                xtype: 'textarea',
                fieldLabel: '8. What else did you consider before recommending',
                bind: {
                    value: '{projectDetail.alternative}',
                    readOnly: '{!editable}'
                }
            }]
        }, {
            xtype: 'fieldset',
            title: 'Risk Analysis-What could go wrong?',
            defaults: { anchor: '100%', labelWidth: 280, msgTarget: 'side' },
            layout: 'anchor',
            items: [{
                xtype: 'textarea',
                fieldLabel: '9. Outline all risks',
                bind: {
                    value: '{projectDetail.outlineRisk}',
                    readOnly: '{!editable}'
                }
            }]
        }];
         
        me.callParent(arguments);
     }
});
