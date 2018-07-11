
Ext.define("Workflow.view.events.avdr.AvdrForm", {
    extend: "Workflow.view.events.common.FormBase",
    controller: "event-avdr",
    viewModel: {
        type: "event-avdr"
    },
    formType: 'AVDR_REQ',
    title: 'AV Equipment Damage',
    infoTitle: 'Damage/Lost - Reported By',
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
                    xtype: 'panel',
                    title: '<h3 style="padding:0; margin:0">AV Equipment Damage/Lost Report Form</h3>',
                    titleAlign: 'center',
                    border: true,
                    bodyPadding: '5 10 5 10',
                    items:
                    [
                        {
                            xtype: 'label',
                            html: '<h3 style="padding:0; margin:0">Nagaworld AV employees are required to report any damage or lost of Nagaworld owned and operated equipment as soon as possible and submit a completed copy of this form within 24 hours of the incident.</h3>'
                        }
                    ]
                }
            ]            
        };
    },
    buildInfoItems: function () {
        var items = [
            {
                xtype: 'datefield',
                fieldLabel: 'Incident Date',
                allowBlank: false,
                bind: {
                    value: '{formRequestData.incidentDate}',
                    readOnly: '{readOnly}'
                }
            },
            {
                xtype:'combo',
                fieldLabel: 'Status Damage/Lost',
                allowBlank: false,
                editable: false,
                queryMode:'local',
                store: ['Damage', 'Lost'],
                bind: {
                    value: '{formRequestData.sdl}',
                    readOnly: '{readOnly}'
                }
            },
            {
                fieldLabel: 'Equipment Location',
                allowBlank: false,
                bind: {
                    value: '{formRequestData.elocation}',
                    readOnly: '{readOnly}'
                }
            }
        ];

        return items;
    },

    buildBodyItems: function () {
        
        var items = [
            {
                reference: 'bodyinfo',
                iconCls: 'fa fa-file-text-o',
                title: 'Equipment Information',
                layout: {
                    type: 'vbox',
                    pack: 'start',
                    align: 'left'
                },
                defaults: {
                    xtype: 'textarea',
                    labelWidth: 250,
                    width: '100%',
                    padding: 5,
                    labelAlign: 'right'
                },
                items: [
                    {
                        xtype: 'label',
                        text: 'Description of Damage/Lost to Equipment:'
                    },
                    {
                        bind: {
                            value: '{formRequestData.dle}',
                            readOnly: '{readOnly}'
                        }
                    },
                    {
                        xtype: 'label',
                        text: 'Equipment Identification Number:'
                    },
                    {
                        bind: {
                            value: '{formRequestData.ein}',
                            readOnly: '{readOnly}'
                        }
                    },
                    {
                        xtype: 'label',
                        text: 'How was the Equipment Damaged/Lost?:'
                    },
                    {
                        bind: {
                            value: '{formRequestData.hedl}',
                            readOnly: '{readOnly}'
                        }
                    },
                    {
                        xtype: 'label',
                        text: 'Action Taken:'
                    },
                    {
                        bind: {
                            value: '{formRequestData.at}',
                            readOnly: '{readOnly}'
                        }
                    },
                    {
                        xtype: 'label',
                        text: 'Estinmated Cost of Repair/Replacement:'
                    },
                    {
                        bind: {
                            value: '{formRequestData.ecrr}',
                            readOnly: '{readOnly}'
                        }
                    },
                    {
                        xtype: 'label',
                        text: 'Date Completed & Item Returned To Service:'
                    },
                    {
                        bind: {
                            value: '{formRequestData.dcirs}',
                            readOnly: '{readOnly}'
                        }
                    }
                ]
            }
        ];

        return items;
    }
});
