Ext.define("Workflow.view.n2maintenace.Department", {
    extend: "Ext.form.Panel",
    xtype: 'n2maintenace-department',
    border: true,
    layout: 'column',
    margin: '0 0 10 0',
    defaults: {
        xtype: 'textfield',
        width: '100%',
        margin: '5 10',
        columnWidth: 1,
        labelWidth: 100
    },
    width: '100%',
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'Maintenance Department',
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'label',
                html: 'CE/SME/ADM Instruction <span style="color:red">*<span>'

            },
            {
                xtype: 'textarea',
                labelWidth: 175,
                hiddenLabel: true,
                allowBlank: false,
                bind: {
                    value: '{information.instruction}',
                    readOnly: '{config.department.readOnly}'
                }
            },
            {
                xtype: 'fieldset',
                title: 'Job Assign',
                defaults: {
                    padding: 5
                },
                layout: 'column',
                items: [
                    {
                        xtype: 'datetime',
                        fieldLabel: 'Date/Time',
                        allowBlank: false,
                        columnWidth: 0.5,
                        bind: {
                            value: '{information.jaDate}',
                            readOnly: '{config.department.readOnly}'
                        }
                    },
                    {
                        xtype: 'ngLookup',
                        fieldLabel: 'Technician',
                        allowBlank: false,
                        editable: true,
                        columnWidth: 0.5,
                        forceSelection: true,
                        displayField: 'displayMember',
                        valueField: 'valueMember',
                        minChars: 1,
                        queryMode: 'local',
                        url: 'api/mwoitem/technicians',
                        bind: {
                            value: '{information.jaTechnician}',
                            readOnly: '{config.department.readOnly}'
                        },
                        listeners: {
                            beforequery: function (record) {
                                record.query = new RegExp(record.query, 'i');
                                record.forceAll = true;
                            }
                        }
                    },
                    {
                        xtype: 'ngLookup',
                        fieldLabel: 'Work Type',
                        allowBlank: false,
                        editable: false,
                        columnWidth: 1,
                        forceSelection: true,
                        displayField: 'name',
                        valueField: 'name',
                        url: 'api/mwoitem/worktypes',
                        bind: {
                            value: '{information.workType}',
                            readOnly: '{config.department.readOnly}'
                        }
                    }
                ]
            }
        ];

        me.callParent(arguments);
    }
});
