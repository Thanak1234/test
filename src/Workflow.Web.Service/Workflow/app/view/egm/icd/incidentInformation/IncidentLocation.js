Ext.define("Workflow.view.icd.incidentInformation.IncidentLocation", {
    extend: "Ext.form.Panel",
    xtype: 'icd-incident-location',

    requires: [
        'Workflow.view.icd.incidentInformation.IncidentLocationController',
        'Workflow.view.icd.incidentInformation.IncidentLocationModel'
    ],

    controller: 'icd-incident-location',
    viewModel: {
        type: 'icd-incident-location'
    },
    title: 'Information',
    iconCls: 'fa fa-file-text-o',
    collapsible: true,
    formReadonly: false,
    //frame: true,
    minHeight: 100,
    layout: 'column',
    autoWidth: true,
    //bodyPadding: 10,
    defaults: {
        layout: 'form',
        xtype: 'container',
        defaultType: 'textfield',
        flex: 1
    }
    ,
    initComponent: function () {
        this.items = [{
            items: [
                {
                    fieldLabel: 'MCID',
                    labelAlign: 'left',
                    xtype: 'numberfield',
                    minValue: 1,
                    allowExponential: false,
                    // Remove spinner buttons, and arrow key and mouse wheel listeners
                    hideTrigger: true,
                    keyNavEnabled: false,
                    mouseWheelEnabled: false,
                    allowBlank: false,
                    bind: { value: '{Incident.mcid}', readOnly: '{!editable}' }
                },
                {
                    fieldLabel: 'Game Name',
                    labelAlign: 'left',
                    maxLengthText:100,
                    allowBlank: true,
                    bind: { value: '{Incident.gamename}', readOnly: '{!editable}' }
                }
                ]
        },
        {
            items: [
                {
                    fieldLabel: 'Zone',
                    labelAlign: 'left',
                    maxLengthText: 100,
                    allowBlank: true,
                    bind: { value: '{Incident.zone}', readOnly: '{!editable}' }
                },
                {
                    fieldLabel: 'Customer Name',
                    labelAlign: 'left',
                    maxLengthText: 256,
                    allowBlank: true,
                    bind: { value: '{Incident.customername}', readOnly: '{!editable}' }
                }
            ]
        },
        {
            items: [               
                {
                    fieldLabel: 'CCTV',
                    labelAlign: 'left',
                    maxLengthText: 100,
                    allowBlank: true,
                    bind: { value: '{Incident.cctv}', readOnly: '{!editable}' }
                }
            ]
        }

        ];

        this.callParent(arguments);
    }

});