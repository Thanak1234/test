Ext.define("Workflow.view.mcn.machineInformation.MachineLocation", {
    extend: "Ext.form.Panel",
    xtype: 'mcn-machine-location',

    requires: [
        'Workflow.view.mcn.machineInformation.MachineLocationController',
        'Workflow.view.mcn.machineInformation.MachineLocationModel'
    ],

    controller: 'mcn-machine-location',
    viewModel: {
        type: 'mcn-machine-location'
    },
    title: 'Machine Information',
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
            width: 940,
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
                    bind: { value: '{Machine.mcid}', readOnly: '{!editable}' }
                },
                {
                    fieldLabel: 'Game Name',
                    labelAlign: 'left',
                    maxLengthText: 100,
                    allowBlank: true,
                    bind: { value: '{Machine.gamename}', readOnly: '{!editable}' }
                },
                {
                    fieldLabel: 'Zone',
                    labelAlign: 'left',
                    maxLengthText: 100,
                    allowBlank: true,
                    bind: { value: '{Machine.zone}', readOnly: '{!editable}' }
                },                
                {
                    xtype: 'combo',
                    fieldLabel: 'Type',
                    editable: false,
                    columnWidth: 0.3,
                    displayField: 'type',
                    allowBlank: false,
                    bind: {
                        store: '{types}',
                        value: '{Machine.type}',
                        readOnly: '{readOnly}'
                    }
                }
                ,
                {
                    fieldLabel: 'Remarks',
                    labelAlign: 'left',
                    xtype: 'textareafield',
                    allowBlank: true,
                    bind: { value: '{Machine.remarks}', readOnly: '{!editable}' }
                }
                ]
            }];

        this.callParent(arguments);
    }

});