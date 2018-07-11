Ext.define("Workflow.view.n2maintenace.Information", {
    extend: "Ext.form.Panel",
    xtype: 'n2maintenace-information',
    border: true,
    layout: 'column',
    margin: '0 0 10 0',
    defaults: {
        xtype: 'textfield',
        width: '100%',
        margin: '5 10',
        columnWidth: .5,
        labelWidth: 100
    },
    width: '100%',
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'Request information',
    initComponent: function () {
        var me = this;        

        me.items = [
           {
               xtype: 'ngLookup',
               fieldLabel: 'Mode',
               editable: false,
               columnWidth: 0.3,
               displayField: 'name',
               allowBlank: false,
               url: 'api/n2mwoitem/modes',
               bind: {
                   value: '{information.mode}',
                   readOnly: '{config.information.readOnly}'
               }
           },
            {
                xtype: 'ngLookup',
                allowBlank: false,
                fieldLabel: 'Request Type',
                editable: false,
                columnWidth: 0.4,
                displayField: 'name',
                url: 'api/n2mwoitem/requestTypes',
                bind: {
                    value: '{information.requestType}',
                    readOnly: '{config.information.readOnly}'
                }
            },
            {
                xtype: 'textfield',
                fieldLabel: 'Reference Number',
                labelWidth: 120,
                columnWidth: 0.3,
                readOnly: true,
                bind: {
                    value: '{information.referenceNumber}'
                }
            },
            {
                xtype: 'combo',
                fieldLabel: 'Location',
                reference: 'cmboLocationType',
                allowBlank: false,
                editable: false,
                columnWidth: 0.3,
                queryMode: 'local',
                store: [
                    'HOTEL',
                    'CASINO',
                    'SPA',
                    'THEATER'
                ],
                publishes: 'value',
                bind: {
                    value: '{information.location}',
                    readOnly: '{config.information.readOnly}'
                }
            },
            {
                xtype: 'ngLookup',
                allowBlank: false,
                reference: 'subLocation',
                fieldLabel: 'Sub Location',
                columnWidth: 0.7,
                queryMode: 'local',
                editable: true,
                displayField: 'sub',
                valueField: 'sub',
                forceSelection: true,
                minChars: 1,
                url: 'api/n2mwoitem/sublocations',
                bind: {
                    readOnly: '{config.information.readOnly}',
                    value: '{information.subLocation}',
                    filters: {
                        property: 'loc',
                        value: '{cmboLocationType.value}'
                    }
                }
            },
            {
                xtype: 'label',
                hidden: true,
                padding: '0 0 0 110',
                html: '<span style="color: red;">Note: Any request for Hotel Guest Room, Please use Opera application</span>',
                columnWidth: 1
            },
            {
                xtype: 'ngLookup',
                allowBlank: false,
                reference: 'chargable',
                fieldLabel: 'Cost Chargable to Department',
                labelWidth: 180,
                columnWidth: 0.5,
                displayField: 'dept',
                valueField: 'id',
                editable: true,
                url: 'api/n2mwoitem/departmentChargables',
                bind: {
                    value: '{information.ccdId}',
                    readOnly: '{config.information.readOnly}',
                    selection: '{chargable}',
                    filters: {
                        anyMatch: true,
                        property: 'loc',
                        value: '{cmboLocationType.value}'
                    }
                }
            },
            {
                xtype: 'textfield',
                reference: 'txtDepartment',
                columnWidth: 0.2,
                readOnly: true,
                bind: {
                    value: '{chargable.ccd}'
                }
            },
            {
                xtype: 'label',
                text: 'Remark',
                columnWidth: 1
            },
            {
                xtype: 'textarea',
                labelWidth: 180,
                columnWidth: 1,
                bind: {
                    value: '{information.remark}',
                    readOnly: '{config.information.readOnly}'
                }
            },
            {
                xtype: 'label',
                text: 'Work Request/Job Description',
                columnWidth: 1
            },
            {
                xtype: 'textarea',
                labelWidth: 180,
                columnWidth: 1,
                bind: {
                    value: '{information.wrjd}',
                    readOnly: '{config.information.readOnly}'
                }
            }
        ];

        me.callParent(arguments);
    }
});
