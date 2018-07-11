Ext.define("Workflow.view.reports.procInst.ProcessInstanceCriteria", {
    extend: "Ext.form.Panel",
    xtype: 'report-processinstancecriteria',
    //title: 'Process Instance - Criteria',
    requires: [
        'Ext.grid.*',
        'Ext.dd.*',
        'Ext.layout.container.HBox'
    ],
    viewModel: {
        type: "report-processinstancereport"
    },
    split: true,
    layout: 'hbox',
    hideHeaders:true,
    bodyPadding: 0,
    defaults: {
        layout: 'form',
        xtype: 'container',
        defaultType: 'textfield',
        anchor: '100%',
        margin: '0 0 0 0',
        flex: 1
    },
    //header: {
    //    titlePosition: 1,
    //    items: [{
    //        type: 'refresh',
    //        tooltip: 'Reset Criteria',
    //        handler: 'resetCriteriaForm'
    //    }]
    //},
    listeners: {
        afterRender: 'resetCriteriaForm'
    },
    initComponent: function () {
        var me = this;

        me.items = [{
            xtype: 'fieldset',
            layout: 'anchor',
            defaults: { anchor: '100%', labelWidth: 100, msgTarget: 'side', labelAlign: 'right' },
            items: me.buildCriteriaItem()
        }];

        me.callParent(arguments);
    },
    getDataStatus: function () {
        var identity = Workflow.global.UserAccount.identity;
        var status = [
            { "status": 0, "displayName": "--- All ---" },
            { "status": 2, "displayName": "In Progress" },
            { "status": 3, "displayName": "Completed" },
            { "status": 4, "displayName": "Rejected" },
            { "status": 5, "displayName": "Cancelled" }
        ];
        if(Workflow.global.UserAccount.identity.isAdmin){
            status.push({ "status": 6, "displayName": "Failured" });
        }
        return status;
    },
    getDepartmentStore: function(level){
        return Ext.create('Ext.data.Store', {
            autoLoad: true,
            proxy: {
                type: 'ajax',
                url: Workflow.global.Config.baseUrl +
                    'api/employee/department-level?level=' + level,
                reader: {
                    type: 'json'
                }
            },
            
        });
    },
    buildCriteriaItem: function () {
        
        var me = this, currentEmp, preDept;
        var identity = Workflow.global.UserAccount.identity;
        var deptStore = me.getDepartmentStore('DEPT');
        var teamStore = me.getDepartmentStore('TEAM');
        me.teamStore = me.getDepartmentStore('TEAM');

        var fields = [{
            xtype: 'fieldcontainer',
            fieldLabel: 'Form',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [{
                xtype: 'ngLookup',
                fieldLabel: 'Workflow',
                emptyText: 'NAME <ALL>',
                editable: false,
                displayField: 'name',
                valueField: 'value',
                url: 'api/lookup/report/process',
                margin: '0 5 0 0',
                value: null,
                maxWidth: 300,
                listeners: {
                    select: 'reloadCriteria'
                },
                bind: {
                    value: '{criteria.AppName}',
                    hidden: '{!showField.AppName}'
                }
            }, {
                xtype: 'combo',
                fieldLabel: 'Status',
                editable: false,
                margin: '0 5 0 0',
                emptyText: 'STATUS <ALL>',
                displayField: 'displayName',
                valueField: 'status',
                store: Ext.create('Ext.data.Store', {
                    fields: ['status', 'displayName'],
                    data: me.getDataStatus()
                }),
                bind: {
                    value: '{criteria.Status}'
                }
            }, {
                xtype: 'textfield',
                maxWidth: 300,
                fieldLabel: 'Request Code',
                emptyText: 'FORM NO <ALL>',
                enableKeyEvents: true,
                scope: me,
                bind: {
                    value: '{criteria.Folio}'
                }
            }]
        }, {
            xtype: 'fieldcontainer',
            fieldLabel: 'Participation',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true
            },
            items: [{
                xtype: 'combo',
                fieldLabel: 'Type',
                editable: false,
                emptyText: 'TYPE <ALL>',
                margin: '0 5 0 0',
                maxWidth: 300,
                displayField: 'displayName',
                valueField: 'participation',
                store: Ext.create('Ext.data.Store', {
                    fields: ['participation', 'displayName'],
                    data: [
                        { "participation": null, "displayName": "--- ALL ---" },
                        { "participation": "SUBMITTER", "displayName": "SUBMIT BY" }, //I've start (Submitter)
                        { "participation": "CONTRIBUTOR", "displayName": "DECISION BY" }, //I've participated (Approver/Those who take action)
                        { "participation": "REQUESTOR", "displayName": "REQUEST BY" } //I've requested (Requestor)
                    ]
                }),
                bind: {
                    value: '{criteria.ParticipationType}'
                }
            }, {
                xtype: 'combo',
                fieldLabel: 'Department',
                editable: false,
                margin: '0 5 0 0',
                displayField: 'displayName',
                valueField: 'type',
                value: 'EMP',
                maxWidth: 150,
                listeners: {
                    beforequery: function (record) {
                        // query any text match in text.
                        record.query = new RegExp(record.query, 'i');
                        record.forceAll = true;
                    },
                    select: function (combo) {
                        var viewmodel = me.getViewModel();
                        var isAdmin = viewmodel.get('isAdmin');

                        if (combo.value == 'EMP') {
                            preDept = viewmodel.get('criteria.departments');
                            viewmodel.set('criteria.deptList', null);
                            viewmodel.set('criteria.EmployeeId', preEmp);
                            viewmodel.set('cboDept', preDept);

                            viewmodel.set('showDept', false);
                            viewmodel.set('showEmp', true);
                        } else if (combo.value == 'DEPT') {
                            preEmp = viewmodel.get('criteria.EmployeeId');
                            viewmodel.set('criteria.EmployeeId', null);
                            //if (isAdmin) {
                            //    viewmodel.set('criteria.EmployeeId', null);
                            //}
                            viewmodel.set('criteria.deptList', preDept);
                            viewmodel.set('cboDept', preDept);

                            viewmodel.set('showDept', true);
                            viewmodel.set('showEmp', false);
                        }
                    }
                },
                bind: {
                    value: '{empDept}',
                    store: '{empDeptStore}'
                }
            }, {
                fieldLabel: 'Requestor',
                hidden: false,
                emptyText: 'EMPLOYEE <ALL>',
                xtype: 'employeePickup',
                maxWidth: 400,
                includeInactive: true,
                includeGenericAcct: true,
                loadCurrentUser: false,
                listeners: {
                    select: 'reloadCriteria',
                    change: function (combo, value) {
                        var selection = combo.getSelection();
                        var viewmodel = me.getViewModel();
                        if(selection){
                            viewmodel.set('isAdmin', selection.get('isAdmin'));
                        }
                    }
                },
                afterClear: function(combo){
                    var viewmodel = me.getViewModel();
                    viewmodel.set('criteria.EmployeeId', null);
                },
                listConfig: {
                    minWidth: 250,
                    resizable: true,
                    loadingText: 'Searching...',
                    emptyText: 'No matching posts found.',
                    itemSelector: '.search-item',

                    // Custom rendering template for each item
                    itemTpl: ['<span>{employeeNo} - {fullName}</span>']
                },
                bind: {
                    value: '{criteria.EmployeeId}',
                    selection: '{employee}',
                    hidden: '{!showEmp}'
                }
            }]
        }, {
            xtype: 'fieldcontainer',
            fieldLabel: 'Department',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                margin: '0 5 0 0',
                triggers: {
                    clear: {
                        type: 'clear',
                        hideWhenEmpty: true,
                        clearOnEscape: true,
                        weight: -1
                    }
                }
            },
            bind: {
                hidden: '{!showDept}'
            },
            items: [
                {
                    xtype: 'combo',
                    reference: 'deptGroup',
                    emptyText: 'DEPT.',
                    displayField: 'groupName',
                    valueField: 'groupName',
                    queryMode: 'local',
                    editable: false,
                    forceSelection: true,
                    multiSelect: true,
                    maxLength: 9000,
                    maxWidth: 120,
                    store: me.getDepartmentStore('GROUP'),
                    bind: {
                        value: '{criteria.groupNames}'
                    },
                    listeners: {
                        change: function (combo, values) {
                            var viewmodel = me.getViewModel();
                            
                            if (values.length > 0) {
                                deptStore.clearFilter();
                                deptStore.filterBy(function (record, id) {
                                    return (Ext.Array.indexOf(values, record.get("groupName")) !== -1);
                                }, this);

                                teamStore.clearFilter();
                                teamStore.filterBy(function (record, id) {
                                    return (Ext.Array.indexOf(values, record.get("groupName")) !== -1);
                                }, this);

                            } else { // clear
                                deptStore.clearFilter();
                                teamStore.clearFilter();
                            }
                            viewmodel.set('criteria.deptNames', null);
                            viewmodel.set('criteria.groupNames', null);
                        }
                    }
                }, {
                    xtype: 'combo',
                    reference: 'deptLine',
                    emptyText: 'LINE',
                    displayField: 'deptName',
                    valueField: 'deptName',
                    queryMode: 'local',
                    editable: true,
                    forceSelection: true,
                    multiSelect: true,
                    maxWidth: 290,
                    maxLength: 9000,
                    store: deptStore,
                    listeners: {
                        change: function (combo, values) {
                            var viewmodel = me.getViewModel();
                            var groupNames = viewmodel.get('criteria.groupNames');

                            viewmodel.set('criteria.teamIds', null);
                            teamStore.clearFilter();
                            if (values.length > 0) {
                                teamStore.filterBy(function (record, id) {
                                    return (Ext.Array.indexOf(values, record.get("deptName")) !== -1)
                                    && (Ext.Array.indexOf(groupNames, record.get("groupName")) !== -1);
                                }, this);
                            }
                        },
                        beforequery: function (record) {
                            record.query = new RegExp(record.query, 'i');
                            record.forceAll = true;
                        }
                    },
                    bind: {
                        value: '{criteria.deptNames}'
                    }
                }, {
                    xtype: 'combo',
                    reference: 'deptTeam',
                    emptyText: 'TEAM',
                    displayField: 'fullName',
                    valueField: 'id',
                    queryMode: 'local',
                    forceSelection: true,
                    editable: true,
                    store: teamStore,
                    bind: {
                        value: '{criteria.teamIds}'
                    },
                    multiSelect: true,
                    maxWidth: 390,
                    maxLength: 9000,
                    listeners: {
                        change: function (combo, values) {
							console.log('clear-store', values.length);
                            var viewmodel = me.getViewModel();

                            if (values.length > 0) {
                             
                            } else {
                                //teamStore.clearFilter();
                            }
                        },
                        beforequery: function (record) {
                            record.query = new RegExp(record.query, 'i');
                            record.forceAll = true;
                        }
                    }
                }
            ]
        }, {
            xtype: 'fieldcontainer',
            fieldLabel: 'Submitted Date',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [{
                xtype: 'datefield',
                name: 'startDate',
                maxWidth: 300,
                fieldLabel: 'Start',
                emptyText: 'START DATE',
                margin: '0 5 0 0',
                bind: {
                    value: '{criteria.SubmittedDateStarted}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'datefield',
                name: 'endDate',
                fieldLabel: 'End',
                emptyText: 'END DATE',
                bind: {
                    value: '{criteria.SubmittedDateEnded}',
                    readOnly: '{!editable}'
                }
            }]
        }, {
            xtype: 'fieldcontainer',
            fieldLabel: 'Decision Date',
            layout: 'hbox',
			hidden: true,
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [{
                xtype: 'datefield',
                name: 'startDate',
                fieldLabel: 'Start',
                emptyText: 'START DATE',
                margin: '0 5 0 0',
                bind: {
                    value: '{criteria.ParticipatedDateStarted}',
                    readOnly: '{!editable}'
                }
            }, {
                xtype: 'datefield',
                name: 'endDate',
                fieldLabel: 'End',
                emptyText: 'END DATE',
                bind: {
                    value: '{criteria.ParticipatedDateEnded}',
                    readOnly: '{!editable}'
                }
            }]
        }, {
            xtype: 'fieldcontainer',
            fieldLabel: 'Activity',
            layout: 'hbox',
            hidden: (me.reqCode ? false : true),
            defaults: {
                //flex: 1,
                hideLabel: true
            },
            items: [{
                xtype: 'tagfield',
                fieldLabel: 'Select a state',
                emptyText: 'CURRENT ACTIVITY <ALL>',
                minWidth: 250,
                store: Ext.create('Ext.data.Store', {
                    autoLoad: true,
                    proxy: {
                        type: 'rest',
                        url: Workflow.global.Config.baseUrl + 'api/processinstant/activities?reqCode=' + (me.reqCode ? me.reqCode : ''),
                        reader: {
                            type: 'json'
                        }
                    }
                }),
                value: null,
                reference: 'activity',
                displayField: 'label',
                valueField: 'name',
                filterPickList: true,
                queryMode: 'local',
                publishes: 'value',
                bind: {
                    value: '{criteria.Activities}',
                    readOnly: '{!editable}'
                }
            }]
        }];

        return this.buildFields(fields);
    },
    // overwrite function
    buildFields: function (fields) {
        return fields;
    },
    buttons: [
        {
            xtype: 'button',
            iconCls: 'fa fa-search',
            text: 'Search',
            handler: 'onSearch'
        }, {
            xtype: 'button',
            iconCls: 'fa fa-refresh',
            text: 'Reset',
            handler: 'resetCriteriaForm'
        }, '-', {
            xtype: 'button',
            text: 'Export',
            iconCls: 'fa fa-download',
            menu: [{
                iconCls: 'fa fa-file-pdf-o',
                text: 'Export PDF',
                handler: 'onExportPdf'
            }, {
                iconCls: 'fa fa-file-excel-o',
                text: 'Export Excel',
                handler: 'onExportExcel'
            }]
        }
    ]
});