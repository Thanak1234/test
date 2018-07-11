
Ext.define("Workflow.view.events.avir.AvirForm", {
    extend: "Workflow.view.events.common.FormBase",
    controller: "event-avir",
    viewModel: {
        type: "event-avir"
    },
    formType: 'AVIR_REQ',
    title: 'AV Incident Report',
    infoTitle: 'Incident - Received By',
    buildFormHeader: function () {

        return {
            xtype: 'container',
            layout: 'vbox',
            defaults: {
                width: '100%',
                align: 'center'                
            },
            items: [
                {
                    xtype:'panel',
                    title: '<h3 style="padding:0; margin:0">AV Incident Report Form</h3>',
                    titleAlign : 'center',
                    border: true,
                    bodyPadding: '5 10 5 10',
                    items:
                    [
                        {
                            xtype: 'label',
                            html: '<h3 style="padding:0; margin:0">Nagaworld AV employees are required to report any incident, which occurs in Nagaworld Outlet as soon as possible, and submit a completed copy of this form within 24 hours of the incident. Any incident such as sound loud/soft, visual distracting customer need to be clearly stated.</h3>'
                        }
                    ]
                }
            ]
        };
    },
    buildInfoItems: function () {
        var items = [
            {
                xtype: 'datetimefield',
                reference: 'incidentDate',
                fieldLabel: 'Incident Date',
                allowBlank: false,
                columnWidth: 0.5,
                bind: {
                    readOnly: '{readOnly}'
                }
            },
            {
                fieldLabel: 'Location',
                allowBlank: false,
                columnWidth: 1,
                bind: {
                    value: '{formRequestData.location}',
                    readOnly: '{readOnly}'
                }
            }
        ];

        return items;
    },

    buildBodyItems: function () {

        var items = [
            {
                xtype: 'form',
                title: 'Complaint - Reported By',
                reference: 'report',
                layout: 'column',                
                defaults: {
                    xtype: 'textfield',
                    columnWidth: 0.5,
                    padding: 10,
                    labelWidth: 130
                },
                iconCls: 'fa fa-file-text-o',
                items: [
                    {
                        fieldLabel: 'Employee',
                        reference: 'reportBy',
                        xtype: 'employeePickup',
                        employeeEditable: true,
                        allowBlank: false,
                        changeOnly: true,
                        triggers: {
                            add: {
                                weight: 3,
                                cls: Ext.baseCSSPrefix + 'form-add-trigger',
                                scope: 'controller',
                                reference: 'addNewBt',
                                handler: 'showAddWindow',
                                hidden: true
                            },
                            edit: {
                                weight: 3,
                                cls: Ext.baseCSSPrefix + 'form-edit-trigger',
                                scope: 'controller',
                                reference: 'editBt',
                                handler: 'showEditWindow',
                                hidden: true
                            }
                        },
                        bind: {
                            selection: '{reporter}',
                            value: '{formRequestData.reporterId}',
                            readOnly: '{readOnly}'
                        }
                    },
                    {
                        fieldLabel: 'Employee Number',
                        readOnly: true,
                        bind: {
                            value: '{reporter.employeeNo}'
                        }
                    },
                    {
                        fieldLabel: 'Position',
                        readOnly: true,
                        bind: {
                            value: '{reporter.position}'
                        }
                    },
                    {
                        allowBlank: false,
                        fieldLabel: 'Complaint Regarding',
                        bind: {
                            value: '{formRequestData.complaintRegarding}',
                            readOnly: '{readOnly}'
                        }
                    }
                ]
            },
            {
                xtype: 'form',
                iconCls: 'fa fa-file-text-o',
                title: 'Complaint',
                reference: 'complaint',
                layout: 'vbox',
                defaults: {
                    xtype: 'textarea',
                    labelWidth: 250,
                    width: '100%',
                    margin: '20 5 20 5'
                },
                flex: 1,
                items: [
                    {
                        allowBlank: false,
                        hiddenLabel: true,
                        flex: 1,
                        bind: {
                            value: '{formRequestData.complaint}',
                            readOnly: '{readOnly}'
                        }
                    }
                ]
            }
        ];

        return items;
    }
});
