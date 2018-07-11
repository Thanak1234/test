/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.eombp.BestPerformanceListView", {
    extend: "Workflow.view.GridComponent",
    xtype: 'eombp-bestperformance-detail-view', 
    title: 'Best Performance - Employee List',
    //header: true,
    modelName: 'bestPerformanceDetail',
    collectionName: 'bestPerformanceDetails',
    initComponent: function () {
        var me = this, viewmodel = me.getViewModel();

        this.actionListeners = {
            load: function (grid) {
                var count = grid.getStore().count();
                viewmodel.set('bestPerformanceDetail.total', count);
            },
            add: function (grid, store, record) {
                var employee = viewmodel.get('bestPerformanceDetail.employee');

                if (record) {
                    record.employeeNo = employee.employeeNo;
                    record.employeeName = employee.employeeName;
                    record.department = employee.department;
                    record.position = employee.position;
                    var newRecord = store.createModel(record);
                    store.add(newRecord);
                    viewmodel.set('bestPerformanceDetail.total', store.count());
                }
            },
            edit: function (grid, store, record) {
                var recToUpdate = store.getById(record.id);
                
                if (recToUpdate) {
                    var employee = viewmodel.get('bestPerformanceDetail.employee');
                    if (employee) {
                        recToUpdate.employeeNo = employee.employeeNo;
                        recToUpdate.employeeName = employee.employeeName;
                        recToUpdate.department = employee.department;
                        recToUpdate.position = employee.position;
                        recToUpdate.set(record);
                    }
                }
                viewmodel.set('bestPerformanceDetail.total', store.count());
            },
            remove: function (grid, store, record) {
                store.remove(record);
                viewmodel.set('bestPerformanceDetail.total', store.count());
            }
        };

        this.callParent(arguments);
    },
    buildGridComponent: function (component) {
        var me = this;
        me.bbar = ['->', {
            fieldLabel: 'Total',
            labelWidth: 100,
            labelAlign: 'right',
            maxWidth: 250,
            xtype: 'numberfield',
            margin: '0 5 0 0',
            bind: {
                value: '{bestPerformanceDetail.total}',
                readOnly: true
            }
        }];
        return [{
            header: 'Staff ID',
            width: 80,
            sortable: true,
            dataIndex: 'employeeNo'
        }, {
            header: 'Name',
            width: 180,
            dataIndex: 'employeeName'
        }, {
            header: 'Gender',
            width: 80,
            dataIndex: 'gender'
        }, {
            header: 'Division',
            width: 80,
            dataIndex: 'division'
        }, {
            header: 'Department',
            flex: 1,
            dataIndex: 'department'
        }, {
            header: 'Position',
            width: 150,
            dataIndex: 'position'
        }, {
            header: 'Contact No',
            width: 120,
            dataIndex: 'contactNo'
        }];
    },
    buildWindowComponent: function (component) {
        var me = this;
        component.width = 520;
        component.height = 350;
        component.labelWidth = 130;

        return [{
            xtype: 'combo',
            fieldLabel: 'Employee',
            mainView: me,
            store: {
                autoLoad: true,
                proxy: {
                    type: 'rest',
                    url: Workflow.global.Config.baseUrl +
                        'api/lookup/employees',
                    reader: {
                        type: 'json'
                    }
                }
            },
            queryMode: 'local',
            minChars: 0,
            forceSelection: true,
            editable: true,
            displayField: 'value',
            valueField: 'id',
            allowBlank: false,
            mainView: me,
            bind: {
                value: '{bestPerformanceDetail.employeeId}'
            },
            listeners: {
                //change: function (combo) {
                //    combo.store.load();
                //},
                select: function (combo, record) {
                    var viewmodel = me.getViewModel(),
                    componentModel = component.getViewModel();
                    var gridStore = me.getStore();
                    

                    if (record) {
                        var employee = record.getData();
                        var existedRec = gridStore.find('employeeId', employee.id);
                        if (existedRec > -1) {
                            Ext.MessageBox.show({
                                title: 'Employee',
                                msg: 'Employee already exist!',
                                buttons: Ext.MessageBox.OK,
                                fn: function () { combo.clearValue(); },
                                icon: Ext.MessageBox['WARNING']
                            });
                        } else {
                            viewmodel.set('bestPerformanceDetail.employee', employee);
                            componentModel.set('bestPerformanceDetail.employeeNo', employee.employeeNo);
                            componentModel.set('bestPerformanceDetail.employeeName', employee.employeeName);
                            componentModel.set('bestPerformanceDetail.department', employee.department);
                            componentModel.set('bestPerformanceDetail.position', employee.position);
                        }
                    }
                },
                beforequery: function (record) {
                    record.query = new RegExp(record.query, 'i');
                    record.forceAll = true;
                }
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Gender',
            bind: { value: '{bestPerformanceDetail.gender}' }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Division',
            bind: { value: '{bestPerformanceDetail.division}' }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Department',
            bind: {
                value: '{bestPerformanceDetail.department}',
                readOnly: true
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Position',
            bind: {
                value: '{bestPerformanceDetail.position}',
                readOnly: true
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Contact No',
            bind: {
                value: '{bestPerformanceDetail.contactNo}',
                readOnly: false
            }
        }];
    },
    afterDialogRender: function (component) {
        
    }
});