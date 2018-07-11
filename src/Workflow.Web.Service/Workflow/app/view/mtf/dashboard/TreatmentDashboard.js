Ext.define('Workflow.view.mtf.dashboard.TreatmentDashboard', {
    extend: 'Ext.panel.Panel',
    xtype: 'mtf-treatment-dashboard',
    cls: 'ng-dashboard',
    id: 'ng-patient-dashboard',
    viewModel: Ext.create('Ext.app.ViewModel', {
        data: {
       
        }
    }),
    enableLive: true,
    isLocalEvent: false,
    layout: {
        type: 'vbox',
        pack: 'start',
        align: 'stretch'
    },
    ngconfig: {
        layout: 'fullScreen'
    },
    defaults: {
        bodyPadding: 3
    },
    margin: 0,
    bodyPadding: 0,
    scrollable: true,
    hasAuthorize: function(roleKey){
        var identity = Workflow.global.UserAccount.identity;
        if(identity && identity.roles){
            var roles = identity.roles.split(',');
            var index = Ext.Array.indexOf(roles, roleKey);
            return (index > -1);
        }
        
        return false;
    },
    openUnfitToWorkBoard: function(button){
        var me = this;
        this.window = new Ext.window.Window({
            viewModel: true,
            maximized: true,
            /* hidden window title */
            header: false,
            border: false,
            closable: false,
            draggable: false,
            cls: 'window-patient-board',
            items: [{
                xtype: 'panel',
                cls: 'ng-panel-left',
                viewModel: true,
                layout: {
                    type: 'hbox',
                    pack: 'start',
                    align: 'stretch'
                },
                items: [{
                    xtype: 'mtf-patient-utw-panel',
                    margin: '0 0 0 0',
                    border: true,
                    title: "LIST OF PATIENT - UNFIT TO WORK (LAST 12 HOURS FROM SUBMISSION'S TIME)",
                    iconCls: 'fa fa-sign-in',
                    cls: 'ng-panel-top ng-grid-dashboard',
                    itemId: 'patients-waiting-grid',
                    flex: 1,
                    autoLoad: false,
                    listeners: {
                        load: function () {
                            console.log('load data', this);
                        }
                    },
                    viewModel: Ext.create('Ext.app.ViewModel')
                }]
            }]
        });

        this.window.show(button, function () {
            me.windowBoard(this);
            var that = this;
        });
    },
    openWindowBoard: function (button) {
        var me = this;
        this.window = new Ext.window.Window({
            viewModel: true,
            maximized: true,
            /* hidden window title */
            header: false,
            border: false,
            closable: false,
            draggable: false,
            layout: {
                type: 'vbox',
                pack: 'start',
                align: 'stretch'
            },
            cls: 'window-patient-board',
            items: [{
                xtype: 'panel',
                cls: 'ng-panel-top',
                //border: true,
                //bodyStyle: 'border-color:#fff !important;',
                viewModel: true,
                items: [{
                    xtype: 'panel',
                    title: 'PATIENT NAME',
                    iconCls: 'fa fa-user',
                    cls: 'label-serving-number',
                    height: 120,
                    margin: '0 0 0 0',
                    bind: {
                        title: '{board.doctor}',
                        html: '{board.patient}&nbsp;{board.folio}'
                    }
                }]
            }, {
                xtype: 'panel',
                flex: 1,
                layout: {
                    type: 'hbox',
                    pack: 'start',
                    align: 'stretch'
                },
                items: [{
                    xtype: 'mtf-patient-panel',
                    margin: '0 0 0 0',
                    border: true,
                    title: 'LIST OF PATIENT CHECKED IN',
                    iconCls: 'fa fa-sign-in',
                    cls: 'ng-panel-top ng-panel-right ng-grid-dashboard',
                    itemId: 'patients-waiting-grid',
                    flex: 4,
                    autoLoad: false,
                    listeners: {
                        load: function () {
                            console.log('load data', this);
                        }
                    },
                    viewModel: Ext.create('Ext.app.ViewModel', {
                        data: {
                            params: { state: 'CHECK_IN' },
                            action: {
                                hidden: {
                                    column: true,
                                    open: false,
                                    important: false,
                                    skip: false,
                                    checkin: true
                                }
                            },
                            columnConfig: {
                                folio: { },
                                number: { hidden: false },
                                arrivalTime: { hidden: false }
                            }
                        }
                    })
                }, {
                    xtype: 'panel',
                    flex: 3,
                    layout: {
                        type: 'vbox',
                        pack: 'start',
                        align: 'stretch'
                    },
                    items: [{
                        xtype: 'mtf-patient-panel',
                        border: true,
                        title: 'LIST OF REQUESTOR PENDING BY HOD',
                        iconCls: 'fa fa-list',
                        cls: 'ng-tab-item-ui ng-grid-dashboard',
                        itemId: 'patients-pendingby-hod-grid',
                        flex: 1,
                        viewModel: Ext.create('Ext.app.ViewModel', {
                            data: {
                                params: { state: 'PENDING_HOD' },
                                action: {
                                    hidden: {
                                        column: true,
                                        open: false,
                                        important: false,
                                        skip: false,
                                        checkin: true
                                    }
                                },
                                columnConfig: {
                                    number: { hidden: false },
                                    arrivalTime: { hidden: true },
                                    actionTime: { hidden: false, title: 'SUBMITTED TIME' }
                                }
                            }
                        })
                    }, {
                        xtype: "mtf-patient-panel",
                        itemId: 'patients-pending-grid-window',
                        cls: 'ng-tab-item-ui ng-grid-dashboard',
                        iconCls: 'fa fa-inbox',
                        title: 'LIST AVAILABLE FOR CHECK-IN',
                        flex: 1,
                        viewModel: Ext.create('Ext.app.ViewModel', {
                            data: {
                                params: { state: 'PENDING' },
                                action: {
                                    hidden: {
                                        open: true,
                                        important: true,
                                        skip: true,
                                        checkin: false
                                    }
                                },
                                columnConfig: {
                                    number: { hidden: true },
                                    arrivalTime: { hidden: true },
                                    actionTime: { hidden: false, title: 'APPROVED TIME' }
                                }
                            }
                        })
                    }]
                }]
            }, {
                xtype: 'panel',
                layout: {
                    type: 'hbox',
                    pack: 'center'
                },
                padding: 0,
                margin: 0,
                cls: 'ng-dashboard-footer',
                items: [{
                    flex: 1,
                    padding: 8,
                    bind: {
                        html: '<div>{board.maquee}</div>'
                    }
                }, {
                    width: 120,
                    padding: 8,
                    bind: {
                        html: '<div>{board.time}</div>'
                    }
                }]
            }],
            //Force to destroy window when it is cosed.
            doClose: function() {
                Ext.TaskManager.stop(me.task);
                delete me.task;
                this.destroy();
            }
        });

        this.window.show(button, function () {
            me.windowBoard(this);
            var that = this;
            me.task = Ext.TaskManager.start({
                run : function() {
                    viewmodel = that.getViewModel();
                    viewmodel.set('board.time', Ext.Date.format(new Date(),'h:i:s A'));
                },
                interval : 1000
            });
        });
    },
    showMTFReport: function(){
        var profileWin = Ext.create('Ext.window.Window', {
            title: 'Medical Treatment - Criteria',
            // hideHeaders:true,
            // header: false,
            // preventHeader: true,
            scrollable: true,
            bodyPadding: 0,
            modal: true,
            items: [{
                xtype: 'report-mt',
                width: 1200,
                height: 600,
                layout: 'border'
            }],
            maximizable: false,
            constrain: true,
            closable: true
        });
        profileWin.show();
    },
    initComponent: function () {
        var me = this,
            viewmodel = me.getViewModel();
        
        // initial right
        me.right = {
            isWorkforce: me.hasAuthorize("[APPLICATION].[DASHBOARD].[MTF].[WORKFORCE]")
            //isDoctor:  me.hasAuthorize("[APPLICATION].[DASHBOARD].[MTF].[DOCTOR]"),
        };

        console.log('me.right.isWorkforce', me.right.isWorkforce);
        
        me.tbar = [{
            xtype: 'button',
            text: 'Live',
            enableToggle: true,
            iconCls: 'fa fa-circle',
            cls: 'btn-green',
            pressed: true,
            handler: function (button) {
                me.enableLive = button.pressed;
                if (button.pressed) {
                    button.addCls('btn-green');
                } else {
                    button.removeCls('btn-green');
                }
            }
        }, {
            xtype: 'button',
            iconCls: 'fa fa-television',
            text: 'Open Board (Unfit To Work)',
            hidden: true,
            handler: function (button) {
                me.openUnfitToWorkBoard(button);
            }
        }, {
            xtype: 'button',
            iconCls: 'fa fa-refresh',
            text: 'Refresh',
            handler: function () {
                me.isLocalEvent = true;
                me.refresh();
            }
        }, '->', {
            xtype: 'button',
            iconCls: 'fa fa-television',
            text: 'Open Board',
            hidden: me.right.isWorkforce,
            handler: function (button) {
                me.openWindowBoard(button);
            }
        }, {
            xtype: 'button',
            iconCls: 'fa fa-pie-chart',
            text: 'Report',
            handler: function () {
                me.showMTFReport();
            }
        }];

        me.items = [{
            layout: {
                type: 'hbox',
                pack: 'start',
                align: 'stretch'
            },
            height: 320,
            items: [
            // First Row
            {
                flex: 1,
                border: true,
                items: [{
                    xtype: 'tabpanel',
                    ui: 'ng-tab-panel-ui',
                    cls: 'ng-tab-panel-ui',
                    items: [{
                        title: 'MY PATIENT',
                        xtype: 'mtf-patient-panel',
                        itemId: 'patients-onboard-grid',
                        cls: 'ng-tab-item-ui',
                        iconCls: 'fa fa-user',
                        dashboard: me,
                        height: 280,
                        viewModel: Ext.create('Ext.app.ViewModel', {
                            data: {
                                params: { state: 'CHECK_IN' },
                                action: {
                                    hidden: {
                                        open: me.right.isWorkforce,
                                        important: me.right.isWorkforce,
                                        skip: false,
                                        checkin: true,
                                        history: me.right.isWorkforce
                                    }
                                },
                                columnConfig: {
                                    number: { hidden: true },
                                    arrivalTime: { hidden: false },
                                    actionTime: { hidden: true }
                                }
                            }
                        }),
                        rendererColumn: function (value, metaData, record) {
                            //var priority = record.get('PRIORITY');
                            //if (parseInt(priority) == 0) {
                            //    metaData.style = "color:#cf4c35;";
                            //} else if (parseInt(priority) == -1) {
                            //    metaData.style = "color:#73b51e;";
                            //} else {

                            //}
                            return value;
                        }
                    }]
                }]
            },  {
                width: 600,
                border: true,
                margin: '0 0 0 3',
                items: [{
                    xtype: 'tabpanel',
                    ui: 'ng-tab-panel-ui',
                    cls: 'ng-tab-panel-ui',
                    items: [{
                        title: 'QUEUE STATUS',
                        xtype: 'patient-pie-chart',
                        cls: 'ng-tab-item-ui',
                        border: false,
                        margin: '0 0 0 0'
                    }]
                }]
            }]
            
        },
        // Second Row
        {
            minHeight: 380,
            flex: 1,
            layout: {
                type: 'hbox',
                pack: 'start',
                align: 'stretch'
            },
            items: [{
                flex: 1,
                border: true,
                items: [{ // First Column
                    xtype: 'tabpanel',
                    ui: 'ng-tab-panel-ui',
                    cls: 'ng-tab-panel-ui',
                    items: [{
                        xtype: "mtf-patient-panel",
                        itemId: 'patients-pending-grid',
                        cls: 'ng-tab-item-ui',
                        iconCls: 'fa fa-inbox',
                        title: 'PATIENT PENDING',
                        dashboard: me,
                        viewModel: Ext.create('Ext.app.ViewModel', {
                            data: {
                                params: { state: 'PENDING' },
                                action: {
                                    hidden: {
                                        open: true,
                                        important: true,
                                        skip: true,
                                        checkin: false,
                                        history: me.right.isWorkforce
                                    }
                                },
                                columnConfig: {
                                    number: { hidden: true },
                                    arrivalTime: { hidden: true },
                                    actionTime: { hidden: false, title: 'APPROVED TIME' }
                                }
                            }
                        })
                    }, { // Second Column
                        xtype: "mtf-patient-panel",
                        itemId: 'patients-skiped-grid',
                        cls: 'ng-tab-item-ui',
                        iconCls: 'fa fa-recycle',
                        title: 'PATIENT SKIP',
                        dashboard: me,
                        viewModel: Ext.create('Ext.app.ViewModel', {
                            data: {
                                params: { state: 'SKIP' },
                                action: {
                                    hidden: {
                                        open: true,
                                        important: true,
                                        skip: true,
                                        checkin: false,
                                        history: me.right.isWorkforce
                                    }
                                },
                                columnConfig: {
                                    number: { hidden: true },
                                    arrivalTime: { hidden: true },
                                    actionTime: { hidden: false, title: 'APPROVED TIME' }
                                }
                            }
                        })
                    }]
                }]
            }, {
                width: 600,
                border: true,
                margin: '0 0 0 3',
                items: [{
                    xtype: 'tabpanel',
                    ui: 'ng-tab-panel-ui',
                    cls: 'ng-tab-panel-ui',
                    activeTab: (me.right.isWorkforce?1:0),
                    items: [{
                        title: 'PATIENT LEAVE STATUS',
                        xtype: 'patient-line-chart',
                        cls: 'ng-tab-item-ui',
                        border: false,
                        margin: '0 0 0 0'
                    }, {
                        xtype: 'mtf-patient-panel',
                        border: false,
                        title: 'LIST OF REQUESTOR PENDING BY HOD',
                        iconCls: 'fa fa-list',
                        cls: 'ng-tab-item-ui',
                        margin: '0 0 0 0',
                        id: 'patients-pendingby-hod-grid-id',
                        flex: 1,                        
                        viewModel: Ext.create('Ext.app.ViewModel', {
                            data: {
                                params: { state: 'PENDING_HOD' },
                                action: {
                                    hidden: {
                                        column: false,
                                        open: true,
                                        important: true,
                                        skip: true,
                                        checkin: true,
                                        history: true,
                                        profile: false
                                    }
                                },
                                columnConfig: {
                                    number: { hidden: true },
                                    arrivalTime: { hidden: true },
                                    actionTime: { hidden: false, title: 'SUBMITTED TIME' }
                                }
                            }
                        })
                    }]
                }]
            }]
        }];

        this.startFingerHub();
        this.callParent(arguments);
    },
    startFingerHub: function(){
        window.hubReady = $.connection.hub.start();
        console.log('window.hubReady', window.hubReady);
    },
    refresh: function (state) {
        var me = this,
            viewmodel = me.getViewModel();
        
        var patientsOnBoardGrid = me.getComponentByItemId('patients-onboard-grid');
        var patientsPendingGrid = me.getComponentByItemId('patients-pending-grid');
        var patientsSkipedGrid = me.getComponentByItemId('patients-skiped-grid');
        var graphPatientStatus = me.getComponentByItemId('patient-status-polar');
        var patientsPendingHodGrid = me.getComponentByItemId('patients-pendingby-hod-grid-id');
        
        
        
        if (patientsOnBoardGrid && patientsOnBoardGrid.isLocalEvent) {
            me.isLocalEvent = true;
            patientsOnBoardGrid.isLocalEvent = false;
        }

        if (patientsPendingGrid && patientsPendingGrid.isLocalEvent) {
            me.isLocalEvent = true;
            patientsPendingGrid.isLocalEvent = false;
        }

        if (patientsSkipedGrid && patientsSkipedGrid.isLocalEvent) {
            me.isLocalEvent = true;
            patientsSkipedGrid.isLocalEvent = false;       
        }

        if (patientsPendingHodGrid && patientsPendingHodGrid.isLocalEvent) {
            me.isLocalEvent = true;
            patientsSkipedGrid.isLocalEvent = false; 
        }

        console.log(me.enableLive, me.isLocalEvent);

        if (!me.enableLive && !me.isLocalEvent) {
            me.isLocalEvent = false;
            return;
        }
		
        if (me.window) {
            me.windowBoard(me.window, state);
        }
        if (graphPatientStatus) {
            var store = graphPatientStatus.getStore();
            store.reload();
        }

        if (patientsOnBoardGrid) {
            var store = patientsOnBoardGrid.getStore();
            store.reload();
        }

        if (patientsPendingGrid) {
            var store = patientsPendingGrid.getStore();
            store.reload();
        }

        if (patientsSkipedGrid) {
            var store = patientsSkipedGrid.getStore();
            store.reload();
        }

        if (patientsPendingHodGrid) {
            var store = patientsPendingHodGrid.getStore();
            store.load();
        }
    },
    windowBoard: function (window, state) {
        var me = this,
            patientsOnBoardGrid = window.items ? window.down('#patients-waiting-grid') : null,
            patientsPendingByHoDGrid = window.items ? window.down('#patients-pendingby-hod-grid') : null,
            patientsPendingGridWindow = window.items ? window.down('#patients-pending-grid-window') : null,
            refs = window.getReferences(),
            viewmodel = window.getViewModel();
        
        if (patientsPendingGridWindow) {
            var store = patientsPendingGridWindow.getStore();
            //sorting by multiple fields
            store.sort([{
                property: 'LAST_ACTION_DATE',
                direction: 'DESC'
            }]);
            store.load();
        }

        if (patientsPendingByHoDGrid) {
            var store = patientsPendingByHoDGrid.getStore();
            store.load();
        }

        if (patientsOnBoardGrid) {
            var store = patientsOnBoardGrid.getStore();
            var maxRecord = 1;
            
            store.load({
                callback: function (records, operation, success) {
                    var maquee = '';
                    Ext.each(records, function (record) {
                        maxRecord--;
                        if (maxRecord < 0) {
                            var data = record.getData();
                            maquee += ('[' + (Math.abs(maxRecord) + 1) + '. ' + data.PATIENT + '] ');
                        }
                    });

                    viewmodel.set('board.maquee', '<marquee behavior="scroll" direction="right">' + maquee + '</marquee>');
                    if (records.length > 0) {
                        var data = records[0].getData();
                        viewmodel.set('board.patient', data.PATIENT);
                        viewmodel.set('board.folio', '(' + data.FOLIO + ')');
                        patientsOnBoardGrid.getSelectionModel().select(0);

                        if (data.FOLIO != me.folio) {
                            me.folio = data.FOLIO;                            
                            //var msg = new SpeechSynthesisUtterance();
							//msg.voice = speechSynthesis.getVoices()[3];
                            //msg.voiceURI = 'native';
                            //msg.volume = 1;
                            //// 0 to 1
                            //msg.rate = 0.7;
                            //// 0.1 to 10
                            //msg.pitch = 0;
                            ////0 to 2
                            //msg.text = 'ID ' + (data.REQUESTOR_NO.replace('-', '. '));
                            //msg.lang = 'en-US';

                            //msg.onend = function (e) {

                            //};
                            var audio = new Audio();
                            audio.src = '/api/sounds/english?empId=' + data.REQUESTOR_NO;
                            audio.onended = function () {
                                //speechSynthesis.speak(msg);
                            };
                            audio.play();
                        }

                        if (patientsOnBoardGrid.getWorklistItemByProcId) {
                            patientsOnBoardGrid.getWorklistItemByProcId(data.PROCESS_INSTANCE_ID, function (wItem) {
                                if (wItem) {
                                    viewmodel.set('board.doctor', 'DOCTOR ' + wItem.OpenBy);
                                }
                            });
                        }
                    } else {
                        viewmodel.set('board.patient', '');
                        viewmodel.set('board.folio', '');
                    }
                }
            });
        }
    },
    getComponentByItemId: function (key) {
        return this.down('#' + key);
    },
    showToast: function (title, message) {
        Ext.toast({
            title: title,
            html: message,
            closable: true,
            align: 'br',
            slideInDuration: 400,
            minWidth: 400
        });
    }
});
