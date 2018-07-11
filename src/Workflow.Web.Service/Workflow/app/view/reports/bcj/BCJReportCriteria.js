Ext.define("Workflow.view.reports.bcj.BCJReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-bcjCriteria',
    title: 'BCJ - Criteria',
    reqCode: 'BCJ_REQ',
    viewModel: {
        type: "report-bcj"
    },
    buildFields: function (fields) {
        var me = this;

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Detail',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [{
                xtype: 'textfield',
                maxWidth: 150,
                margin: '0 5 0 0',
                fieldLabel: 'Project Name',
                emptyText: 'PROJECT NAME',
                enableKeyEvents: true,
                scope: me,
                bind: {
                    value: '{criteria.projectName}'
                }
            },
                    {
                        xtype: 'combo',
                        fieldLabel: 'Co Name Branch:',
                        displayField: 'name',
                        changeOnly: false,
                        editable: false,
                        valueField: 'coporationBranch',
                        emptyText: 'BRANCH OF CO NAME',
                        maxWidth: 190,
                        margin: '0 5 0 0',
                        store: Ext.create('Ext.data.Store', {
                            fields: ['coporationBranch', 'name'],
                            data: [
                                { "coporationBranch": "Non Gaming", "name": "N1 - Non Gaming" },
                                { "coporationBranch": "Gaming", "name": "N1 - Gaming" },
                                { "coporationBranch": "N2 - Non Gaming", "name": "N2 - Non Gaming" },
                                { "coporationBranch": "N2 - Gaming", "name": "N2 - Gaming" },
                                { "coporationBranch": "Others", "name": "Shared" }
                            ]
                        }),
                        triggers: {
                            clear: {
                                weight: 1,
                                cls: Ext.baseCSSPrefix + 'form-clear-trigger',
                                hidden: true,
                                handler: 'onClearClick'
                            }
                        },
                        listeners: {
                            change: 'onSelectChanged'
                        },
                        bind: {
                            value: '{criteria.coBranchName}'
                        }
                    },
                    {
                        xtype: 'combo',
                        fieldLabel: 'CAPEX CATEGORY:',
                        displayField: 'name',
                        valueField: 'id',
                        emptyText: 'CAPEX CATEGORY',
                        editable: false,
                        margin: '0 5 0 0',
                        listeners: {
                            change: 'onSelectChanged'
                        },
                        triggers: {
                            clear: {
                                weight: 1,
                                cls: Ext.baseCSSPrefix + 'form-clear-trigger',
                                hidden: true,
                                handler: 'onClearClick'
                            }
                        },
                        bind: {
                            store: '{capexCategories}',
                            value: '{criteria.capexCateId}'
                        }
                    }
                ]
        });

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Estinmate Capex',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [
                    {
                        xtype: 'combo',
                        fieldLabel: 'Operator:',
                        emptyText: 'OPERATOR',
                        displayField: 'name',
                        valueField: 'id',
                        editable: false,
                        maxWidth: 150,
                        margin: '0 5 0 0',
                        store: Ext.create('Ext.data.Store', {
                            fields: ['coporationBranch', 'name'],
                            data: [
                                { "id": 0, "name": "=" },
                                { "id": 2, "name": ">" },
                                { "id": 1, "name": "<" }
                            ]
                        }),
                        bind: {
                            value: '{criteria.operator}'
                        }
                    },
                    {
                        xtype: 'numberfield',
                        fieldLabel: 'Amount',
                        minValue: 0,
                        reference: 'amount',
                        format: '$0,000.00',
                        emptyText: 'AMOUNT',
                        margin: '0 5 0 0',
                        maxWidth: 190,
                        enableKeyEvents: true,
                        bind: {
                            value: '{criteria.estinmateCapex}'
                        }

                    }, {
                        xtype: 'textfield',
                        fieldLabel: 'PO No',
                        emptyText: 'PO NO.',
                        margin: '0 0 0 0',
                        enableKeyEvents: true,
                        maxWidth: 170,
                        bind: {
                            value: '{criteria.poNumber}'
                        }

                    }
            ]
        });

        return fields;
    }
});