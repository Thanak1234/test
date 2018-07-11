Ext.define('Workflow.view.approvalmgr.EmployeeDirectory', {
    extend: 'Ext.panel.Panel',
    xtype: 'application-accessibility',
    ngconfig: { layout: 'fullScreen' },
    layout: {
        type: 'hbox',
        pack: 'start',
        align: 'stretch'
    },
    controller: true,
    viewModel: true,
    scrollable: true,
    getIdsFromRecords: function (records, isArray) {
        var ids = [0];
        Ext.each(records, function (record) {
            ids.push(record.get('id'));
        });
        if (isArray) {
            return ids;
        } else {
            return ids.toString();
        }
    },
    setVisibleView: function (view, status) {
        if (status) {
            view.show();
        } else {
            view.hide();
        }
    },
    dataInterface: function (payloadData) {
        if (payloadData) {
            return payloadData;
        } else {
            return {
                appIds: null,
                actIds: null,
                roleIds: null,
                empIds: null,
                approverIds: null,
                type: null
            };
        }
    },
    payloadData: {},
    initComponent: function () {
        var me = this;
        var refs = me.getReferences();

        me.items = [{
            xtype: 'application-list',
            region: 'west',
            reference: 'applicationList',
            width: 300,
            split: true,
            listeners: {
                selectionchange: function (grid, records) {
                    me.loadApproverList("APPLICATION", records);
                }
            }
        }, {
            xtype: 'activity-list',
            region: 'west',
            reference: 'activityList',
            width: 300,
            hidden: true,
            split: true,
            hidden: false,
            listeners: {
                selectionchange: function (grid, records) {
                    me.loadApproverList("ACTIVITY", records);
                }
            }
        }, {
            xtype: 'role-list',
            region: 'west',
            reference: 'roleList',
            split: true,
            hidden: false,
            listeners: {
                selectionchange: function (grid, records) {
                    me.loadApproverList("ROLE", records);
                }
            },
            switchView: function (grid) {
                me.getReferences().employeeList.show();
            }
        }, {
            xtype: 'employee-list',
            region: 'west',
            reference: 'employeeList',
            split: true,
            hidden: true,
            listeners: {
                selectionchange: function (grid, records) {
                    me.loadApproverList("EMP", records);
                }
            },
            switchView: function (grid) {
                me.getReferences().roleList.show();
            }
        }, {
            xtype: 'approver-list',
            region: 'west',
            reference: 'approverList',
            split: true,
            minWidth: 350,
            //flex: 1,
            listeners: {
                selectionchange: function (grid, records) {
                    me.loadApproverList("APPROVER", records);
                }
            },
            saveApprovers: function (grid, records) {
                me.saveApprovers(records);
            }
        }, {
            split: true
        }];

        this.callParent(arguments);
    },
    loadApproverList: function (state, records) {
        var me = this,
            refs = me.getReferences(),
            roleList = refs['roleList']
            employeeList = refs['employeeList'],
            applicationList = refs['applicationList'],
            activityList = refs['activityList'],
            approverList = refs['approverList'];

        var applications = applicationList.getSelection(),
            roles = roleList.getSelection(),
            employees = employeeList.getSelection(),
            activities = activityList.getSelection(),
            approvers = approverList.getSelection();

        var roleIds = me.getIdsFromRecords(roles),
            empIds = me.getIdsFromRecords(employees),
            appIds = me.getIdsFromRecords(applications),
            actIds = me.getIdsFromRecords(activities),
            approverIds = me.getIdsFromRecords(approvers),
            approverIdArr = me.getIdsFromRecords(approvers, true);

        
        if (state == "APPLICATION") {
            activityList.getStore().load({
                params: {
                    appIds: me.getIdsFromRecords(records)
                }
            });


        }

        if (state == "ACTIVITY") {
            roleList.getStore().load({
                params: {
                    actIds: me.getIdsFromRecords(records)
                }
            });
            if (employeeList.isVisible()) {
                employeeList.setSelection(null);
            }
        }

        if (state == "EMP" || state == "ROLE") {
            // reset payload data
            me.payloadData = me.dataInterface(null);
            approverList.loadData({
                type: state,
                ids: ('EMP' == state ? empIds : roleIds),
                actIds: actIds
            }, function (records) {
                // binding payload data
                me.payloadData = me.dataInterface({
                    appIds: appIds,
                    actIds: actIds,
                    roleIds: roleIds,
                    empIds: empIds,
                    approverIds: approverIds,
                    type: state
                });
            });
        }
    },
    saveApprovers: function (records) {
        var me = this;
        if (me.payloadData) {
            me.payloadData.approverIds = me.getIdsFromRecords(records);
            console.log('me.payloadData', me.payloadData);

            Ext.Ajax.request({
                url: '/api/organization/save-approver',
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                jsonData: me.payloadData,
                success: function (response, operation) {
                    var data = Ext.decode(response.responseText);
                    console.log('result', data);
                },
                failure: function (data, operation) {
                    console.log('save approver has been failured');
                }
            });
        }
    }
});