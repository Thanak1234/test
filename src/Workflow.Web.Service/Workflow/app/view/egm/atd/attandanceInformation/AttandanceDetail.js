Ext.define("Workflow.view.atd.attandanceInformation.AttandanceDetail", {
    extend: "Ext.form.Panel",
    xtype: 'atd-attandance-detail',

    requires: [
        'Workflow.view.atd.attandanceInformation.AttandanceDetailController',
        'Workflow.view.atd.attandanceInformation.AttandanceDetailModel'
    ],

    controller: 'atd-attandance-detail',
    viewModel: {
        type: 'atd-attandance-detail'
    },
    title: 'Particular',
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
                    xtype: 'combo',
                    fieldLabel: 'Detail',
                    editable: false,
                    columnWidth: 0.3,
                    displayField: 'type',
                    allowBlank: false,
                    bind: {
                        store: '{types}',
                        value: '{Attandance.detail}',
                        readOnly: '{readOnly}'
                    }
                }
                ,
                //{
                //    fieldLabel: 'Detail',
                //    labelAlign: 'left',
                //    maxLengthText: 256,
                //    allowBlank: false,
                //    bind: { value: '{Attandance.detail}', readOnly: '{!editable}' }
                //},                
                {
                    fieldLabel: 'Remarks',
                    labelAlign: 'left',
                    xtype: 'textareafield',
                    allowBlank: false,
                    bind: { value: '{Attandance.remarks}', readOnly: '{!editable}' }
                }
                ]
            }];

        this.callParent(arguments);
    }

});