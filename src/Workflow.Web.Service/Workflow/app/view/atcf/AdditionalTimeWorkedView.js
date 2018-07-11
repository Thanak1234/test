/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.atcf.AdditionalTimeWorkedView", {
    extend: "Workflow.view.GridComponent",
    xtype: 'atcf-additional-time-worked-view',
    title: 'Additional Time Worked',
    header: false,
    modelName: 'additionalTimeWorked',
    collectionName: 'additionalTimeWorkeds',
    actionListeners: {
        beforeAdd: function (grid, datamodel) {
            
        },
        add: function (grid, store, record) {
            var newRecord = store.createModel(record);
            store.add(newRecord);
        }
    },
    initComponent: function () {
        var me = this,
            viewmodel = me.getViewModel(),
            parent = me.mainView,
            refs = parent.getReferences();

        this.actionListeners = {
            load: function (grid) {
                // TO DO SOMTHING
            },
            add: function (grid, store, record) {
                var employee = viewmodel.get('additionalTimeWorked.employee');
                
                if (record) {
                    record.employeeNo = employee.employeeNo;
                    record.employeeName = employee.employeeName;
                    var newRecord = store.createModel(record);
                    var exists = false;
                    store.each(function (rec) {
                        var data = rec.getData();
						
                        console.log(data.workingDate.getDate(), record.workingDate.getDate());
                        if (data.employeeId == record.employeeId &&
                            Ext.Date.format(data.workingDate, 'Ymd') === Ext.Date.format(record.workingDate, 'Ymd') &&
                            !exists) {
                            exists = true;
                        }
                    });

                    
                    me.checkValidation(record, exists, function () {
                        store.add(newRecord);
                    });
                    
                }
            },
            edit: function (grid, store, record) {
                var recToUpdate = store.getById(record.id);
                if (recToUpdate) {
                    var employee = viewmodel.get('additionalTimeWorked.employee');
                    if (employee) {
                        recToUpdate.employeeNo = employee.employeeNo;
                        recToUpdate.employeeName = employee.employeeName;
                    }
                    var exists = false;
                    store.each(function (rec) {
                        var data = rec.getData();
                        if (data.employeeId == record.employeeId &&
                            Ext.Date.format(data.workingDate, 'Ymd') === Ext.Date.format(record.workingDate, 'Ymd') &&
                            !exists) {
                            exists = true;
                        }
                    });

                    if (Ext.Date.format(recToUpdate.get('workingDate'), 'Ymd') == Ext.Date.format(record.workingDate, 'Ymd')) {
                        recToUpdate.set(record);
                    } else {
                        me.checkValidation(record, exists, function () {
                            recToUpdate.set(record);
                        });    
                    }
                }
            }
        };

        this.callParent(arguments);
    },
    checkValidation: function (data, exists, callback) {
        Ext.Ajax.request({
            url: '/api/atcfrequest/working-date-list?empId=' + data.employeeId,
            method: 'GET',
            success: function (response) {
                var records = Ext.JSON.decode(response.responseText);
                var folio = '';
                Ext.each(records, function (record) {
                    if (record && record.workingDate) {
                        var d1 = data.workingDate.getDate()+
                        '-' + data.workingDate.getMonth() +
                        '-' + data.workingDate.getYear();

                        var d2 = (new Date(record.workingDate)).getDate() + 
                        '-' + (new Date(record.workingDate)).getMonth() + 
                        '-' + (new Date(record.workingDate)).getYear();
						
                        if (d1 === d2 && !exists) {
                            exists = true;
                            folio = record.folio;
                        }
                    }
                });

                if (exists) {
                    console.log('data', data);
                    Ext.MessageBox.show({
                        title: 'Employee',
                        msg: Ext.String.format('Request already existing for employee ID {0} in form {1}.', data.employeeNo, folio),
                        buttons: Ext.MessageBox.OK,
                        fn: function () {
                            // TODO: after click OK button.
                        },
                        icon: Ext.MessageBox['WARNING']
                    });
                } else {
                    callback();
                }
            }
        });
    },
    buildGridComponent: function (component) {
        var me = this;
       
        return [{
            header: 'ID No',
            flex: 1,
            sortable: true,
            dataIndex: 'employeeNo'
        }, {
            header: 'Name',
            width: 120,
            dataIndex: 'employeeName'
        }, {
            xtype: 'datecolumn',
            header: 'Date',
            width: 150,
            format: 'm/d/Y',
            dataIndex: 'workingDate'
        }, {
            header: 'Work On',
            width: 120,
            dataIndex: 'workOn',
            renderer: function (value) {
                if (value == 'RD'){
                    return 'Reqular Day';
                } else if (value == 'OD') {
                    return 'OFF Day';
                } else if (value == 'PH') {
                    return 'Public Holiday';
                }
                return value;
            }
        }, {
            header: 'No. of Hour(s)',
            width: 120,
            dataIndex: 'numberOfHour'
        }, {
            header: 'Remark',
            width: 250,
            dataIndex: 'comment'
        }];
    },
    buildWindowComponent: function (component) {
        var me = this;
        component.width = 520;
        component.height = 400;
        component.labelWidth = 130;
        var descrReadOnly = false;

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
                value: '{additionalTimeWorked.employeeId}'
            },
            listeners: {
                change: function (combo) {
                    combo.store.load();
                },
                select: function (combo, record) {
                    var viewmodel = me.getViewModel(),
                        componentModel = component.getViewModel(),
                        store = combo.getStore();
                    viewmodel.set('additionalTimeWorked.employee', record.getData());
                },
                beforequery: function (record) {
                    record.query = new RegExp(record.query, 'i');
                    record.forceAll = true;
                }
            }
        }, {
            xtype: 'datefield',
            fieldLabel: 'Date',
            allowBlank: false,
            bind: { value: '{additionalTimeWorked.workingDate}' },
            listeners: {
                change: function (datepick) {
                    //console.log('datepick', datepick);
                },
                select: function (datepick, record) {
                    //console.log(datepick, record);
                }
            }
        }, {
            xtype: 'combo',
            fieldLabel: 'Work On',
            allowBlank: false,
            editable: false,
            store: Ext.create('Ext.data.Store', {
                fields: ['name', 'label'],
                data: [
                    { "name": "RD", "label": "Reqular Day" },
                    { "name": "OD", "label": "OFF Day" },
                    { "name": "PH", "label": "Public Holiday" }
                ]
            }),
            displayField: 'label',
            valueField: 'name',
            bind: { value: '{additionalTimeWorked.workOn}' }
        }, {
            xtype: 'numberfield',
            fieldLabel: 'No. of Hour',
            allowBlank: false,
            maxValue: 24,
            minValue: 0.01,
            bind: { value: '{additionalTimeWorked.numberOfHour}' }
        }, {
            xtype: 'textarea',
            fieldLabel: 'Remark',
            allowBlank: false,
            bind: {
                value: '{additionalTimeWorked.comment}'
            }
        }, {
            xtype: 'label',
            padding: '0 0 0 140',
            text: '* If characters more than 2000, please attach your file for detail.'
        }];
    }
});