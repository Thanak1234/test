Ext.define("Workflow.view.admcpp.DetailView", {
    extend: "Ext.form.Panel",
    xtype: 'admcppdetailview',
    controller: "admcppdetailview",
    viewModel: {
        type: "admcppdetailview"
    },
    border: false,
    minHeight: 100,
    layout: 'column',
    autoWidth: true,
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'form',
                layout: 'column',
                reference: 'verhicle',
                collapsible: true,
                border: true,
                iconCls: 'fa fa-file-text-o',
                title: 'Vehicle',
                columnWidth: 1,
                defaultType: 'textfield',
                defaults: {
                    columnWidth: .5,
                    margin: 10
                },
                items: [
                    {
                        fieldLabel: 'Model',
                        allowBlank: false,
                        bind: {
                            value: '{admcpp.model}',
                            readOnly: '{!editable}'
                        }
                    },
                    {
                        fieldLabel: 'Plate No.',
                        labelWidth: 125,
                        allowBlank: false,
                        bind: {
                            value: '{admcpp.plateNo}',
                            readOnly: '{!editable}'
                        }
                    },
                    {
                        xtype: 'combo',
                        fieldLabel: 'Color',
                        store: [
                            'White',
                            'Black',
                            'Silver',
                            'Red',
                            'Gray',
                            'Blue',
                            'Brown',
                            'Green',
                            'Gold'
                        ],
                        allowBlank: false,
                        bind: {
                            value: '{admcpp.color}',
                            readOnly: '{!editable}'
                        }
                    },
                    {
                        xtype: 'combo',
                        fieldLabel: 'Year Of Production',
                        store: me.buildYears(),
                        maskRe: /[\d,\.]/,
                        stripCharsRe: /[^\d,\.]/g,
                        labelWidth: 125,
                        allowBlank: false,
                        bind: {
                            value: '{admcpp.yop}',
                            readOnly: '{!editable}'
                        }
                    },
                    {
                        xtype: 'textarea',
                        fieldLabel: 'Remark',
                        columnWidth: 1,
                        bind: {
                            value: '{admcpp.remark}',
                            readOnly: '{!editable}'
                        }
                    },
                    {
                        xtype: 'label',
                        columnWidth: 1,
                        html: 'Please attach the requirement document (<span style="color: red">*</span>): <br/>&nbsp;&nbsp;&nbsp;&nbsp;- Company ID Card</br>&nbsp;&nbsp;&nbsp;&nbsp;- Driving License<br/>&nbsp;&nbsp;&nbsp;&nbsp;- Car Registration',
                        style: 'font-size: 1opt; font-weight: bold; border: 1px solid #d0d0d0; padding: 10px'
                    }
                ]
            },
            {
                xtype: 'form',
                reference: 'adminissue',
                iconCls: 'fa fa-file-text-o',
                title: 'Car Park Permit Information',
                margin: '10 0 0 0',
                border: true,
                collapsible: true,
                layout: 'column',
                columnWidth: 1,
                defaults: {
                    columnWidth: .5,
                    margin: 10
                },
                bind: {
                    hidden: '{!visibleIssue}'
                },
                items: [
                    {
                        xtype: 'textfield',
                        fieldLabel: 'Car Park S/N',
                        allowBlank: false,
                        bind: {
                            value: '{admcpp.cpsn}',
                            readOnly: '{!editIssue}'
                        }
                    },
                    {
                        xtype: 'datefield',
                        fieldLabel: 'Issue Date',
                        allowBlank: false,
                        bind: {
                            value: '{admcpp.issueDate}',
                            readOnly: '{!editIssue}'
                        }
                    }
                ]
            }            
        ];

        me.callParent(arguments);
    },
    buildYears: function () {
        var years = [];
        var initYear = 1980;
        for (var i = 0; i <= 45; i++) {
            years.push((initYear+i).toString());
        }

        return years;
    }
});
