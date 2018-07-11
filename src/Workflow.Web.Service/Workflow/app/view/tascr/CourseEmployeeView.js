/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.tascr.CourseEmployeeView", {
    extend: "Workflow.view.GridComponent",
    xtype: 'tascr-course-employee-view',
    title: 'EOM - Employee List',
    //header: true,
    modelName: 'courseEmployee',
    collectionName: 'courseEmployees',
    initComponent: function () {
        var me = this,
            viewmodel = me.getViewModel();

        this.actionListeners = {
            load: function (grid) {
                // TO DO SOMTHING
            },
            add: function (grid, store, record) {
                var employee = viewmodel.get('courseEmployee.employee');

                if (record) {
                    record.employeeNo = employee.employeeNo;
                    record.employeeName = employee.employeeName;
                    record.department = employee.department;
                    record.position = employee.position;
                    var newRecord = store.createModel(record);
                    store.add(newRecord);
                }
            },
            edit: function (grid, store, record) {
                var recToUpdate = store.getById(record.id);

                if (recToUpdate) {
                    var employee = viewmodel.get('courseEmployee.employee');
                    recToUpdate.employeeNo = employee.employeeNo;
                    recToUpdate.employeeName = employee.employeeName;
                    recToUpdate.department = employee.department;
                    recToUpdate.position = employee.position;
                    recToUpdate.set(record);
                }
            }
        };

        this.callParent(arguments);
    },
    buildGridComponent: function (component) {
        var me = this;
       
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
                value: '{courseEmployee.employeeId}'
            },
            listeners: {
                change: function (combo) {
                    combo.store.load();
                },
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
                            viewmodel.set('courseEmployee.employee', employee);
                            componentModel.set('courseEmployee.employeeNo', employee.employeeNo);
                            componentModel.set('courseEmployee.employeeName', employee.employeeName);
                            componentModel.set('courseEmployee.department', employee.department);
                            componentModel.set('courseEmployee.position', employee.position);
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
            bind: { value: '{courseEmployee.gender}' }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Division',
            bind: { value: '{courseEmployee.division}' }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Department',
            bind: {
                value: '{courseEmployee.department}',
                readOnly: true
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Position',
            bind: {
                value: '{courseEmployee.position}',
                readOnly: true
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Contact No',
            bind: {
                value: '{courseEmployee.contactNo}',
                readOnly: false
            }
        }];
    },
    afterDialogRender: function (component) {
        
    }
});