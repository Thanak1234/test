Ext.define('Workflow.view.mtf.dashboard.PatientGrid', {
    extend: 'Ext.grid.Panel',
    xtype: 'mtf-patient-panel',
    viewModel: Ext.create('Ext.app.ViewModel', {
        data: {
            columnConfig: {
                number: { hidden: true },
                arrivalTime: { hidden: true },
                actionTime: { hidden: true, title: 'LAST ACTION TIME' }
            }
        }
    }),
    height: 400,
    scrollable: true,
    viewConfig: {
        loadMask: false
    },
    autoLoad: true,
    property: {
        url: {
            _patientlist: Workflow.global.Config.baseUrl + 'api/mtfrequest/patient-list',
            _patientrecord: Workflow.global.Config.baseUrl + 'api/mtfrequest/patient-record',
            _worklistitem: Workflow.global.Config.baseUrl + 'api/worklists/worklist-item',
            _releaseworklistitem: Workflow.global.Config.baseUrl + 'api/worklists/release'
            
        }
    },
    rendererColumn: function (value, metaData, record) {
        return value;
    },
    parserValue: function(key, defaultValue){
        viewmodel = this.getViewModel();
        var value = viewmodel.get(key);
        
        return value == null ? defaultValue : value;
    },
    initComponent: function() {
        var me = this, viewmodel = me.getViewModel();
        var actionColTitle = me.parserValue('columnConfig.actionTime.title', 'LAST ACTION TIME');
        var actionColHidden = me.parserValue('columnConfig.actionTime.hidden', true);

        me.store = new Ext.create('Ext.data.Store', {
            autoLoad: me.autoLoad,
            proxy: {
                type: 'rest',
                extraParams: viewmodel.get('params'),
                url: me.property.url._patientlist,
                reader: {
                    type: 'json'
                }
            },
            pageSize: 100
        });
        
        me.columns = [{
            width: 50,
            xtype: 'widgetcolumn',
            menuDisabled: true,
            hidden: viewmodel.get('action.hidden.column'),
            widget: {
                xtype: 'button',
                textAlign: 'left',
                arrowCls: '',
                iconCls: 'fa fa-chevron-right'
            },
            onWidgetAttach: function (column, widget, record) {
                var data = record.getData();
                var menu = me.buildColumnAction(data);
                widget.setMenu(menu);
            }
        }, {
            text: 'Nº',
            width: 50,
            align: 'center',
            hidden: viewmodel.get('columnConfig.number.hidden'),
            xtype: 'rownumberer'
        }, {
            text: "REQUEST NO",
            menuDisabled: true,
            sortable: false,
            width: 100,
            dataIndex: 'FOLIO',
            renderer: me.rendererColumn
        }, {
            text: "EMPLOYEE",
            menuDisabled: true,
            sortable: false,
            flex: 1,
            minWidth: 200,
            dataIndex: 'PATIENT'
        }, {
            text: "ARRIVAL TIME",
            menuDisabled: true,
            sortable: false,
            hidden: viewmodel.get('columnConfig.arrivalTime.hidden'),
            width: 120,
            renderer: Ext.util.Format.dateRenderer('H:i:s'),
            dataIndex: 'CHECK_IN_DATE'
        }, {
            text: actionColTitle,
            menuDisabled: true,
            sortable: false,
            hidden: actionColHidden,
            width: 120,
            renderer: Ext.util.Format.dateRenderer('H:i:s'),
            dataIndex: 'LAST_ACTION_DATE'
        }];

        if (me.tbar && me.tbar.length > 0) {
            me.tbar.push({
                xtype: 'button',
                iconCls: 'fa fa-refresh',
                text: 'Refresh',
                handler: function () {
                    me.dashboard.refresh();
                }
            });
        }
        
        me.callParent(arguments);
    },
    buildColumnAction: function (data) {
        var me = this;

        return [{
            text: 'Open',
            iconCls: 'fa fa-folder-open',
            hidden: true,
            bind: {
                hidden: '{action.hidden.open}'
            },
            handler: function (button) {
                me.getWorklistItemByProcId(data.PROCESS_INSTANCE_ID, function (wItem) {
                    var currentEmpLogin = Workflow.global.UserAccount.identity;
                    if (wItem && currentEmpLogin) {
                        console.log(wItem.AllocatedUser.toUpperCase(), currentEmpLogin.loginName.toUpperCase());
						window.hubReady.done(function() {
						  window.HUB_FINGER_PRINT.server.processPatient(data.REQUEST_HEADER_ID, 'UPDATE_DOCTOR');
						});
                        if (wItem.AllocatedUser && wItem.AllocatedUser.replace('K2:', '').toUpperCase() != currentEmpLogin.loginName.toUpperCase()) {
                            Ext.MessageBox.confirm('Confirm',
                                "The action can't be completed because the form is open by " + wItem.OpenBy +
                                "<br/>Are you sure you want to release and open the form?",
                                function (answer) {
                                    if (answer == 'yes') {
                                        Ext.Ajax.request({
                                            url: me.property.url._releaseworklistitem + '/' + wItem.SerialNumber,
                                            method: 'POST',
                                            headers: { 'Content-Type': 'text/html' },
                                            success: function (response) {
                                                me.getWorklistItemByProcId(data.PROCESS_INSTANCE_ID, function (newWItem) {
                                                    window.location.href = "#mtf-request-form/SN=" + newWItem.SerialNumber;
                                                });
                                            },
                                            failure: function (response) {
                                                var data = Ext.JSON.decode(response.responseText);
                                                var message = data.ExceptionMessage;
                                                Ext.Msg.show({
                                                    title: 'Permission require!',
                                                    message: message,
                                                    buttons: Ext.Msg.OK,
                                                    icon: Ext.Msg.ERROR
                                                });
                                            }
                                        });
                                    }
                                }, this);
                        } else {
                            window.location.href = "#mtf-request-form/SN=" + wItem.SerialNumber;
                        }
                    } else {
                        Ext.MessageBox.show({
                            title: "You're not a doctor",
                            msg: "You don't have permission to open this form number!",
                            buttons: Ext.MessageBox.OK,
                            animateTarget: button,
                            scope: this,
                            icon: Ext.MessageBox.WARNING
                        });
                    }
                });
            }
        }, {
            text: 'Call',
            iconCls: 'fa fa-bullhorn',
            hidden: true,
            bind: {
                hidden: '{action.hidden.open}'
            },
            handler: function (button) {
                window.hubReady.done(function () {
                    window.HUB_FINGER_PRINT.server.processPatient(data.REQUEST_HEADER_ID, 'CALL');
                });
            }
        },{
            text: 'Important',
            iconCls: 'fa fa-exclamation-triangle',
            hidden: true,
            bind: {
                hidden: '{action.hidden.important}'
            },
            handler: function (button) {
                me.isLocalEvent = true;
				window.hubReady.done(function () {
                    window.HUB_FINGER_PRINT.server.processPatient(data.REQUEST_HEADER_ID, 'PRIORITY');
				});
            }
        }, {
            text: 'Skip',
            iconCls: 'fa fa-arrow-right',
            hidden: true,
            bind: {
                hidden: '{action.hidden.skip}'
            },
            handler: function (button) {
                me.isLocalEvent = true;
				window.hubReady.done(function () {
                    window.HUB_FINGER_PRINT.server.processPatient(data.REQUEST_HEADER_ID, 'SKIP');
				});
            }
        }, {
            text: 'Check In',
            iconCls: 'fa fa-calendar-check-o',
            hidden: true,
            bind: {
                hidden: '{action.hidden.checkin}'
            },
            handler: function (button) {
                me.isLocalEvent = true;
				window.hubReady.done(function () {
                    window.HUB_FINGER_PRINT.server.processPatient(data.REQUEST_HEADER_ID, 'MANUAL_CHECK_IN');
				});
            }
        }, {
            text: 'History Record',
            iconCls: 'fa fa-history',
            hidden: false,
            bind: {
                hidden: '{action.hidden.history}'
            },
            handler: function (button) {
                me.buildPatientRecordView(button, data);
            }
        },{
            text: 'Employee Profile',
            iconCls: 'fa fa-user',
            hidden: false,
            bind: {
                hidden: '{action.hidden.profile}'
            },
            handler: function (button) {
                if(data && data.PATIENT){
                    data.REQUESTOR_NO = data.PATIENT.split(' - ')[0];
                }

                Ext.Ajax.request({
                    method: 'GET',
                    url: 'api/employee/profile',
                    params: { empNo: data.REQUESTOR_NO },
                    success: function (response, opts) {
                        var profile = Ext.decode(response.responseText);
                        me.showWindowEmpProfile(profile);
                    }
                });
                
            }
        }];
    },
    showWindowEmpProfile: function(data){
        var profileWin = Ext.create('Ext.window.Window', {
            title: 'Employee Profile',
            scrollable: true,
            bodyPadding: 10,
            modal: true,
            items: [{
                xtype: 'form',
                bodyPadding: '5 5 0',
                width: 600,
                fieldDefaults: {
                    labelAlign: 'top',
                    msgTarget: 'side'
                },
            
                defaults: {
                    border: false,
                    xtype: 'panel',
                    flex: 1,
                    layout: 'anchor'
                },
            
                layout: 'hbox',
            
                items: [{
                    items: [{
                        xtype: 'textfield',
                        fieldLabel: 'Employee No',
                        anchor: '-5',
                        value: data.employeeNo
                    }, {
                        xtype:'textfield',
                        fieldLabel: 'Department',
                        anchor: '-5',
                        value: data.subDept
                    }]
                }, {
                    items: [{
                        xtype: 'textfield',
                        fieldLabel: 'Employee Name',
                        anchor: '100%',
                        value: data.fullName
                    },{
                        xtype: 'textfield',
                        fieldLabel: 'Position',
                        anchor: '100%',
                        value: data.position
                    }]
                }]
            }],
            constrain: true,
            closable: true
        });
        profileWin.show();
    },
    buildPatientRecordView: function (button, data) {
        var me = this;
        var window = new Ext.window.Window({
            viewModel: true,
            maximized: false,
            closable: true,
            modal: true,
            layout: {
                type: 'vbox',
                pack: 'start',
                align: 'stretch'
            },
            title: 'Patient History',
            items: [{
                xtype: 'grid',
                store: new Ext.create('Ext.data.Store', {
                    autoLoad: me.autoLoad,
                    isModel: false,
                    proxy: {
                        type: 'rest',
                        extraParams: { empId: data.REQUESTOR },
                        url: me.property.url._patientrecord,
                        reader: {
                            type: 'json'
                        }
                    },
                    pageSize: 100
                }),
                plugins: [{
                    ptype: 'rowexpander',
                    rowBodyTpl: new Ext.XTemplate('<p><b>SYMPTOM:</b> {SYMPTOM}</p><p><b>DIAGNOSIS:</b> {DIAGNOSIS}</p>')
                }],
                columns: [
                    { text: 'REQUEST NO', dataIndex: 'FOLIO',renderer: function (value, metaData, record) {
                        
                        return '<a href="#mtf-request-form/SN='  + record.get('ID') + '_99999">' + value + '</a>';
                    } },
                    { text: 'PATIENT', dataIndex: 'PATIENT', flex: 1 },
                    { text: 'TREATMENT BY', dataIndex: 'TREATMENT_BY', flex: 1 },
                    { text: 'SUBMIT DATE', dataIndex: 'SUBMITTED_DATE', renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s'), width: 150 },
                    { text: 'LAST ACTION DATE', dataIndex: 'LAST_ACTION_DATE', renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s'), width: 150 }

                ],
                height: 300,
                width: 900

            }]
        });

        window.show(button);
    },
    getWorklistItemByProcId: function(procId, callback){
        var me = this;
        Ext.Ajax.request({
            method: 'GET',
            url: me.property.url._worklistitem,
            params: { procInstId: procId },
            success: function (response, opts) {
                var worklistitem = Ext.decode(response.responseText);
                callback(worklistitem);
            },
            failure: function (response, opts) {
                console.log('Form cannot be open ' + response);
            }
        });
    }
});
/*
onForceReleaseClick: function (btn, e, eOpts) {
    var me = this;
    var v = me.getView();
    var vm = v.getViewModel();

    var record = vm.get('selectedRecord');
    var data = record.getData();
        
    Ext.Msg.show({
        title: 'Action Confirm',
        message: 'Are you sure release this form?',
        buttons: Ext.Msg.YESNO,
        icon: Ext.Msg.QUESTION,
        fn: function (btn) {
            if (btn === 'yes') {
                Ext.Ajax.request({
                    url: me.requestRoutes.WORKLIST_FORCE_RELEASE + '/' + data.serial,
                    method: 'POST',
                    headers: { 'Content-Type': 'text/html' },
                    success: function (response) {
                        me.refreshWorklist();
                        vm.set('selectedRecord', null);
                    },
                    failure: function (response) {
                        var data = Ext.JSON.decode(response.responseText);
                        var message = data.ExceptionMessage;
                        Ext.Msg.show({
                            title: 'Permission require!',
                            message: message,
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.ERROR
                        });
                    }
                });
            }
        }
    });
}
*/