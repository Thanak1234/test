/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.eombp.EmployeeOfMonthDetailView", {
    extend: "Workflow.view.GridComponent",
    xtype: 'eombp-employeeofmonth-detail-view', 
    title: 'EOM - Employee List',
    //header: true,
    modelName: 'employeeOfMonthDetail',
    collectionName: 'employeeOfMonthDetails',
    initComponent: function () {
        var me = this, viewmodel = me.getViewModel();

        this.actionListeners = {
            load: function (grid) {
                var count = grid.getStore().count();
                viewmodel.set('employeeOfMonthDetail.total', count);
            },
            add: function (grid, store, record) {
                var employee = viewmodel.get('employeeOfMonthDetail.employee');

                if (record) {
                    record.employeeNo = employee.employeeNo;
                    record.employeeName = employee.employeeName;
                    record.department = employee.department;
                    record.position = employee.position;
                    var newRecord = store.createModel(record);
                    store.add(newRecord);
                    viewmodel.set('employeeOfMonthDetail.total', store.count());
                }
            },
            edit: function (grid, store, record) {
                var recToUpdate = store.getById(record.id);

                if (recToUpdate) {
                    var employee = viewmodel.get('employeeOfMonthDetail.employee');
                    recToUpdate.employeeNo = employee.employeeNo;
                    recToUpdate.employeeName = employee.employeeName;
                    recToUpdate.department = employee.department;
                    recToUpdate.position = employee.position;
                    recToUpdate.set(record);
                } viewmodel.set('employeeOfMonthDetail.total', store.count());
            },
            remove: function (grid, store, record) {
                store.remove(record);
                console.log('store', store);
                viewmodel.set('employeeOfMonthDetail.total', store.count());
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
                value: '{employeeOfMonthDetail.total}',
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
                value: '{employeeOfMonthDetail.employeeId}'
            },
            listeners: {
                //change: function (combo) {
                //    combo.store.load();
                //},
                select: function (combo, record) {
                    var viewmodel = me.getViewModel(),
                        componentModel = component.getViewModel(),
                        store = combo.getStore();

                    if (record) {
                        var employee = record.getData();
                        var gridStore = me.getStore();
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
                            viewmodel.set('employeeOfMonthDetail.employee', employee);
                            componentModel.set('employeeOfMonthDetail.employeeNo', employee.employeeNo);
                            componentModel.set('employeeOfMonthDetail.employeeName', employee.employeeName);
                            componentModel.set('employeeOfMonthDetail.department', employee.department);
                            componentModel.set('employeeOfMonthDetail.position', employee.position);
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
            bind: { value: '{employeeOfMonthDetail.gender}' }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Division',
            bind: { value: '{employeeOfMonthDetail.division}' }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Department',
            bind: {
                value: '{employeeOfMonthDetail.department}',
                readOnly: true
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Position',
            bind: {
                value: '{employeeOfMonthDetail.position}',
                readOnly: true
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Contact No',
            bind: {
                value: '{employeeOfMonthDetail.contactNo}',
                readOnly: false
            }
        }];
    },
    afterDialogRender: function (component) {
        
    }
});