Ext.define("Workflow.view.icd.incidentInformation.IncidentOutline", {
    extend: "Ext.form.Panel",
    xtype: "icd-incident-outline",

    requires: [
        'Workflow.view.icd.incidentInformation.IncidentLocationController',
        'Workflow.view.icd.incidentInformation.IncidentLocationModel'
    ],

    controller: 'icd-incident-location',
    viewModel: {
        type: 'icd-incident-location'
    },
    title: 'Outline of Incident',
    iconCls: 'fa fa-file-text-o',
    collapsible: true,
    formReadonly: false,
    //frame: true,
    //minHeight: 100,
    layout: 'column',
    autoWidth: true,
    //bodyPadding: 10,
   

    defaults: {
        layout: 'form',
        xtype: 'container',
        defaultType: 'textfield'

    },
    //,
    initComponent: function () {


        this.items = [{
            width:940,
            items: [
                {
                    fieldLabel: 'Subject',
                    labelAlign: 'left',
                    allowBlank: false,
                    maxLengthText: 256,
                    bind: { value: '{Incident.subject}', readOnly: '{!editable}' }
                }
                 ,
                {
                    fieldLabel: 'Outline',
                    labelAlign: 'left',
                    xtype: 'textareafield',
                    allowBlank: false,
                    bind: { value: '{Incident.outline}', readOnly: '{!editable}' }
                },
                {
                    fieldLabel: 'Remarks',
                    labelAlign: 'left',
                    xtype: 'textareafield',
                    allowBlank: true,
                    bind: { value: '{Incident.remarks}', readOnly: '{!editable}' }
                }
            ]
        }
        

        ];

        this.callParent(arguments);
    }


});