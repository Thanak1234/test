/**
 * This class is the controller for the main view for the application. It is specified as
 * the "controller" of the Main view class.
 *
 * author: Phanny
 */
Ext.define('Workflow.view.main.MainController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.main',
    listen: {
        controller: {
            '#': {
                unmatchedroute: 'onRouteChange'
            }
        }
    },
    config: {
        control: {
            '*': {
                //refreshNotification: 'refreshNotification'
            }
        }
    },
    routes: {
        ':node': {
            action: 'onRouteChange',
            before: function (id, action) {
                var me = this;
                me.loadMenu(function () {
                    me.beforRouteChange(id, action);
                });

            } 
        },
        ':formName/clone=:cmpId': {
            action: 'onRouteChange',
            before: function (formName, clone, action) {
                var me = this;
                me.loadMenu(function () {
                    me.beforRouteChange(formName, action);
                });

            }
        },
        'k2/flow/:procInst/:folio': {
            action: function (procInst, folio, action) {
                var me = this;
                me.loadMenu(function () {
                    me.addTab(
                        Ext.create('Workflow.view.common.flowviewer.flowview', {
                            processId: procInst
                        }),
                        'view-process-instance-' + procInst,
                        ('VIEW FLOW (' + folio + ')'),
                        'fa fa-sitemap',
                        true);
                });

            }
        },
        //':formName/SN=:serial/:sharedUser': {
        //    before: function (formName, serial, sharedUser, action) {
        //        Ext.getBody().mask("Opening...");
        //        var me = this;
        //        serial += '/' + sharedUser;
        //        me.loadMenu(function () {
        //            me.beforeFormRouteChange(formName, serial, action);
        //            Ext.getBody().unmask();
        //        });
        //    }
        //},
        ':formName/SN=:serial': {
            // action:  'onFormRouteChange',
            before: function (formName, serial, action) {
                Ext.getBody().mask("Opening...");
                var me = this;
                me.loadMenu(function () {
                    me.beforeFormRouteChange(formName, serial, action);
                    Ext.getBody().unmask();
                });

            }
        },
        //Ticket used only (@TICKET)
        'ticket/:id' : {
            before: function (id) {
                var me = this;
                me.loadMenu(function () {
                    var route = me.getRouteMapping('ticketView');
                    me.loadTicket(route, id)
                });
            }
        },
        // top bar action
        onProfileView: 'onProfileView',
        onLogout: 'onLogout'
    },
    init: function () {
        var me = this;
        var vm = me.getView().getViewModel();
        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/employee/currentuser',
            headers: { 'Content-Type': 'application/json' },
            success: function (response) {
                var identity = Ext.JSON.decode(response.responseText);
                Workflow.global.UserAccount.identity = identity;
                vm.set('identity', identity);
            }

        });

        //me.refreshNotification();

        //var task = {
        //    run :  me.refreshNotification.bind(me),
        //    interval: 5000
        //};

        //Ext.TaskManager.start(task);
    },
	startTimer: function(duration, display) {
		var timer = duration, minutes, seconds;
		setInterval(function () {
			minutes = parseInt(timer / 60, 10);
			seconds = parseInt(timer % 60, 10);

			minutes = minutes < 10 ? "0" + minutes : minutes;
			seconds = seconds < 10 ? "0" + seconds : seconds;

			display.textContent = minutes + ":" + seconds;
			
			if (--timer < 0) {
				timer = duration;
				window.location = "/account/logoff";
			}
		}, 1000);
	},
    refreshNotification: function() {
        var me = this;
        var vm = me.getView().getViewModel();
        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/notification/count',
            method: 'GET',
            success: function(response) {
                var count = Ext.JSON.decode(response.responseText);
                vm.set('notifiedCount', count);
            },
            failure: function(response) {
				
                console.log('error', response.status);
            }
        });
    },

    loadTicket: function (route, id) {
         var me              = this,
            refs            = me.getReferences(),
            contentPanel    = refs.contentPanel,
            mainLayout = contentPanel.getLayout(),
            viewId = 'viewId_' +id;

         var exist = contentPanel.child('component[routeId=' + viewId + ']');

         if (exist) {
             mainLayout.setActiveItem(exist);
             return;
         }

        Ext.getBody().mask("Loading ticket...");

        route.model.load(id, {
            scope: this,
            failure: function (opt, operation) {
                
                Ext.getBody().unmask();

                var lastView = mainLayout.getActiveItem()
                if (lastView && lastView.routeId) {
                    me.redirectTo(lastView.routeId);
                }
                var response = Ext.decode(operation.error.response.responseText);
                Ext.MessageBox.show({
                    title: 'Error',
                    msg: response.Message,
                    buttons: Ext.MessageBox.OK,
                    icon: Ext.MessageBox.ERROR
                });
            },
            success: function (record, operation) {
                Ext.getBody().unmask();
                var content = Ext.create(route.viewCls, { viewModel: { data: {ticket: record }} });
                me.addTab(function () { return content; }, viewId, '#' +record.get('ticketNo'), 'fa fa-ticket', true);
            }
        });
    },

    openK2Viewer: function (prefixKey, url, requestNo) {
        var regex = /(ProcessID=)([\d]+)/i;
		if (!regex.test(url)) {
            this.addTab(
                Ext.create('Ext.ux.IFrame', { src: url }), 
                (prefixKey + '_' + requestNo, prefixKey + ": " + requestNo), 
                null, 
                true
            );
        }        
    },

    loadMenu: function (fn) {
        var treeView = this.getReferences().navigationTreeList;
        if (!treeView.getStore().isLoaded()) {
            treeView.getStore().load({
                scope: this,
                callback: function (records, operation, success) {
                    if (fn) {
                        fn();
                    }
                }
            });
        } else {
            if (fn) {
                fn();
            }
        }
    },

    onProfileView: function (record) {
        var me = this,
           window = Ext.create('Workflow.view.common.profile.EmployeeProfileWindow', {
               mainView : me,
               viewModel: {
                   data : {
                       action: 'Edit',
                       employeeInfo: Ext.create(Workflow.model.common.EmployeeInfo, Workflow.global.UserAccount.identity)                   
                   }
               },
               lauchFrom: me.getReferences().onProfile,
               cbFn: function (rec) {
                    //TODO: SOMETHING
               }
           });

        window.show(me.getReferences().onProfile);
    },
    onLogout: function () {
        Ext.Msg.show({
            title: 'Logout confirm',
            message: 'Do you want to logout?',
            buttons: Ext.Msg.YESNO,
            icon: Ext.Msg.QUESTION,
            fn: function (btn) {
                if (btn === 'yes') {
                    window.location = "/account/logoff";
                }
            }
        });        
    },

    onNavigationTreeSelectionChange: function (tree, records) {
        var record = records[0];

        if (record && record.get('view')) {
            this.redirectTo(record.get("routeId"));
        }
    },
    beforRouteChange: function (id, action) {
        var me = this,
           workflowFormOpen = me.getWorkflowFormOpenFlag(),
           leaveForm = false;

        if (workflowFormOpen) {
            me.leaveFormConfirmation(function (bt) {
                if (bt == 'yes') {
                    workflowFormOpen.close();
                    action.resume();
                } else {
                    action.stop;
                }
            });
        } else {
            action.resume();
        }

        return leaveForm;
    },

    // beforeFormRouteChange: function (formName, serial, activity, action){
    beforeFormRouteChange: function (formName, serial, action) {
        var me = this, 
            route = this.getRouteMapping(formName),
            workflowFormOpen = me.getWorkflowFormOpenFlag();

        action.stop();
        var openWindow = function () {
            me.openWorkListForm(route, serial);
        };
        if (workflowFormOpen) {
            me.leaveFormConfirmation(function (bt) {
                if (bt == 'yes') {
                    workflowFormOpen.close();
                    openWindow();
                } else {
                  //  window.history.back();
                }
            });
        } else {
            openWindow();
        }
    },
    leaveFormConfirmation: function (fn) {
        Ext.MessageBox.show({
            title: 'Alert',
            msg: 'Are you sure to leave this request form?',
            buttons: Ext.MessageBox.YESNO,
            scope: this,
            fn: fn
        });
    },
    openWorkListForm: function (route, serial) {
        var height = this.getView().getHeight();
        this.openRequestForm(route, { 
            performAction: true, 
            serial: serial, 
            height: height 
        });
    },
    openRequestForm: function (route, opts) {
        var me              = this,
            refs            = me.getReferences(),
            navigationList  = refs.navigationTreeList,
            contentPanel    = refs.contentPanel,
            store           = navigationList.getStore(),
            mainLayout      = contentPanel.getLayout(),
            view            = me.getRouteMapping(route.id).viewCls;

        Ext.getBody().mask("Loading form data...");
        route.model.load(opts.serial, {
            scope: this,
            failure: function (opt, operation) {

                Ext.getBody().unmask();

                var lastView = mainLayout.getActiveItem()
                if (lastView && lastView.routeId) {
                    me.redirectTo(lastView.routeId);
                }
                var response = Ext.decode(operation.error.response.responseText);
                Ext.MessageBox.show({
                    title: 'Error',
                    msg: response.Message,
                    buttons: Ext.MessageBox.OK,
                    icon: Ext.MessageBox.ERROR
                });
            },
            success: function (record, operation) {
                Ext.getBody().unmask();

                //TODO: open dialog from suitable component
                opts.activity = record.get('activity');
                opts.record = record;
                opts.requestNo = record.get('requestNo');
                opts.serial = record.get('serial');
                opts.routeId = opts.requestNo + "_" + opts.activity;

                //Tab or window dialog
                var content = Ext.create(view, { workFlowConfig: opts });
                var config = content.getWorkflowFormConfig();

                if (config.openIn === 'DIALOG') {
                        me.openWindowDialog(navigationList, content, opts);
                } else {
                    me.addTab(content, opts.routeId, opts.requestNo, null, true);
                }
            }
        });
    },

    onRouteChange: function (id) {
        Ext.getBody().mask("Processing...");
        this.setCurrentView(id, function () {
            Ext.getBody().unmask();
        });
    },

    addTab: function (form, viewId, text, iconCls, closableTable, cb) {
        var me = this,
            refs = me.getReferences(),
            contentPanel = refs.contentPanel,
            mainLayout = contentPanel.getLayout(),
            viewModel = me.getViewModel(),
            vmData = viewModel.getData(),
            lastView = mainLayout.getActiveItem(),
            existingItem = contentPanel.child('component[routeId=' + viewId + ']'),
            newView = null,
            currtab = null;

        var addTabContent = function (cbInner) {
            if (lastView && lastView.isWindow) {
                lastView.destroy();
                lastView = null;
            }

            if (!existingItem) {
                if (form.isXType('uxiframe') || form.isXType('common-flowview')) {
                    form.setWidth('100%');
                    form.setHeight('100%');
                } else {
                    form.setBodyStyle('background-color:#ffffff;padding:0px;');

                    var width = 1010;
                    if ((form.ngconfig && form.ngconfig.layout === 'fullScreen')) {
                        width = '100%';
                    }
                    form.setWidth(width);
                    form.setHeight('100%');
                }

                newView = Ext.create(('Ext.panel.Panel' || 'pages.Error404Window'), {
                    hideMode: 'offsets',
                    routeId: viewId,
                    title: text,
					scrollable: true,
                    hashTag: Ext.util.History.getHash(),
                    iconCls: iconCls ? iconCls : 'fa fa-file-text-o',
                    closable: typeof closableTable === 'undefined' ? true : closableTable,
                    layout: {
                        type: 'vbox',       
                        align: 'center',    
                        fullscreen: true
                    }
                });
                
                newView.add(form);
                newView.ngconfig = form.ngconfig; // custom config for nagaworld project

                if (form.ngconfig && form.ngconfig.layout === 'fullScreen'){
                    newView.setBodyStyle('border-top:3px solid #ffd777 !important;');
                    newView.ngconfig.xtype = form.xtype;
				} else {
                    newView.setBodyStyle('background:#3e4752; border-top:3px solid #ffd777 !important;');
                }
				
				var qParam = Ext.urlDecode(window.location.search.substring(1));
				var singleForm = (qParam.singleForm == 'true');
				if(singleForm){
					contentPanel.getTabBar().setVisible(false);
				}
            }

            // !newView means we have an existing view, but if the newView isWindow
            // we don't add it to the card layout.
            if (existingItem && (!newView || !newView.isWindow)) {
                // We don't have a newView, so activate the existing view.
                if (existingItem !== lastView) {
                    mainLayout.setActiveItem(existingItem);
                }
                newView = existingItem;
            }
            else {
                currtab = contentPanel.add(newView); 
                currtab.ngconfig = newView.ngconfig;
                mainLayout.setActiveItem(currtab);
            }

            if (newView && newView.isFocusable(true)  ) {
                newView.focus();
            }
            vmData.currentView = newView;
            if (cbInner) {
                cbInner();
            }
        };
        
        addTabContent(function () {

            contentPanel.setActiveItem(currtab);
            if(cb) cb();
        });
    },

    setCurrentView: function (viewId, callback) {
        viewId = (viewId || '').toLowerCase();
        var me = this,
            refs = me.getReferences(),
            navigationList = refs.navigationTreeList,
            store = navigationList.getStore(),
            node = store.findNode('routeId', viewId),
            viewCls = node ? node.get('view') : null,
            iconCls = node ? node.get('iconCls') : null,
            text = node ? node.get('text') : 'None',
            closableTable = node ? node.get('closableTab') : true;
  
        me.addTab(
            Ext.create(viewCls), 
            viewId, 
            text, 
            iconCls, 
            closableTable, 
            callback
        );
    },

    oncontentTabSelectionChange: function (tabPanel, newCard, oldCard, eOpts) {
        var refs = this.getReferences(),
            navigationList = refs.navigationTreeList,
            store = navigationList.getStore(),
            hashTag = newCard.routeId || null,
            node = hashTag ? store.findNode('routeId', hashTag) : null;

        if (node) {
            navigationList.setSelection(node);
        }
        this.redirectTo(newCard.hashTag);
    },

    openWindowDialog: function (view, content, opts) {
        var preview     = view,
            me          = this,
            refs = me.getReferences(),
            contentPanel = refs.contentPanel,
            mainLayout = contentPanel.getLayout(),
            activity    = opts.activity ? ' - ' + opts.activity : '',
            moreTitle   = opts.requestNo ? ' (' + opts.requestNo + ')' : '',
            //content     = Ext.create(cls, { workFlowConfig: opts }),
            performAction = opts ? opts.performAction : false,
            w = new Ext.window.Window({
                rtl: false,
                modal: true,
                maximizable: performAction,
                collapsable: true,
                title: content.getTitle() + moreTitle + activity,
                closable: true,
                layout: 'fit',
                height: opts.height ? opts.height : 400,
                items: content,
                doClose: function () {
                    w.hide(preview, function () {
                        me.setWorkflowFormOpenFlag(null);
                        w.destroy();
                        if (opts.performAction) {
                            var lastView = mainLayout.getActiveItem()
                            if (lastView && lastView.hashTag) {
                                me.redirectTo(lastView.hashTag);
                            } else {
                                me.redirectTo('dashboard');
                            }       
                        }
                    });
                },
                onFocusLeave: function () {
                    if (!opts.performAction) {
                        this.close();
                    }
                }
            });

        if (performAction) {
            me.setWorkflowFormOpenFlag(w);
        }

        w.show(preview);
    },

    getRouteMapping: function (requestForm) {
        var routeIdMapping = {
            'mtf-request-form': { id: 'mtf-request-form', model: Workflow.model.mtfForm.MTFRequestForm, viewCls: 'Workflow.view.mtf.RequestForm' },
            'it-request-form': { id: 'it-request-form', model: Workflow.model.common.RequestForm, viewCls: 'Workflow.view.it.ItRequestForm' },
            'av-job-brief': { id: 'av-job-brief', model: Workflow.model.avForm.AvbRequestForm, viewCls: 'Workflow.view.av.AvJobBriefForm' },
            'bcj-request-form': { id: 'bcj-request-form', model: Workflow.model.bcjForm.BcjRequestForm, viewCls: 'Workflow.view.bcj.BcjRequestForm' },
            'fnf-booking-request': { id: 'fnf-booking-request', model: Workflow.model.fnfForm.FnFRequestForm, viewCls: 'Workflow.view.reservation.fnfbr.FnFBookingRequestForm' },
            'mwo-request-form': { id: 'mwo-request-form', model: Workflow.view.maintenace.MaintenaceRequestModel, viewCls: 'Workflow.view.maintenace.MaintenaceForm' },
            'n2mwo-request-form': { id: 'n2mwo-request-form', model: Workflow.view.n2maintenace.MaintenaceRequestModel, viewCls: 'Workflow.view.n2maintenace.MaintenaceForm' },
            'erf-request-form': { id: 'erf-request-form', model: Workflow.model.erfForm.ERFRequestForm, viewCls: 'Workflow.view.hr.erf.RequestForm' },
            'crr-request-form': { id: 'crr-request-form', model: Workflow.model.crrForm.CRRRequestForm, viewCls: 'Workflow.view.reservation.crr.RequestForm' },            
            'pbf-request-form': { id: 'pbf-request-form', model: Workflow.model.pbfForm.PBFRequestForm, viewCls: 'Workflow.view.pbf.RequestForm' },
            'att-request-form': { id: 'att-request-form', model: Workflow.model.attForm.ATTRequestForm, viewCls: 'Workflow.view.hr.att.RequestForm' },
            'event-avdr': { id: 'event-avdr', model: Workflow.model.avdr.AvdrForm, viewCls: 'Workflow.view.events.avdr.AvdrForm' },
            'event-avir': { id: 'event-avir', model: Workflow.model.avir.AvirForm, viewCls: 'Workflow.view.events.avir.AvirForm' },
            'eom-request-form': { id: 'eom-request-form', model: Workflow.model.eom.EOMRequestForm, viewCls: 'Workflow.view.eom.EOMForm' },
            'icd-request-form': { id: 'icd-request-form', model: Workflow.model.icdForm.incident, viewCls: 'Workflow.view.icd.IncidentRequestForm' },
            'mcn-request-form': { id: 'mcn-request-form', model: Workflow.model.mcnForm.Machine, viewCls: 'Workflow.view.mcn.MachineRequestForm' },
            'atd-request-form': { id: 'atd-request-form', model: Workflow.model.atdForm.Attandance, viewCls: 'Workflow.view.atd.AttandanceRequestForm' },
			'admcpp-request-form': { id: 'admcpp-request-form', model: Workflow.model.admcpp.AdmCppRequestForm, viewCls: 'Workflow.view.admcpp.AdmCppForm' },
            'ticket': { id: 'ticket', model:null, viewCls: 'Workflow.view.ticket.TicketView' },
            'ticketView': { id: 'ticketView', model: Workflow.model.ticket.Ticket, viewCls: 'Workflow.view.ticket.view.TicketDisplay' },
            'itapp-request-form': { id: 'itapp-request-form', model: Workflow.model.itapp.ItAppRequestForm, viewCls: 'Workflow.view.itapp.ItAppForm' },
            'va-request-form': { id: 'va-request-form', model: Workflow.model.vaf.VARequestForm, viewCls: 'Workflow.view.vaf.VAForm' },
            'vr-request-form': { id: 'vr-request-form', model: Workflow.view.vr.VRRequestModel, viewCls: 'Workflow.view.vr.VRForm' },
            'ccr-request-form': { id: 'ccr-request-form', model: Workflow.view.ccr.CCRRequestModel, viewCls: 'Workflow.view.ccr.CCRForm' },
            'fat-request-form': { id: 'fat-request-form', model: Workflow.model.fatForm.FATRequestForm, viewCls: 'Workflow.view.fat.RequestForm' },
            'fad-request-form': { id: 'fad-request-form', model: Workflow.model.fadForm.FADRequestForm, viewCls: 'Workflow.view.fad.RequestForm' },
            'rac-request-form': { id: 'rac-request-form', model: Workflow.view.rac.RACRequestModel, viewCls: 'Workflow.view.rac.RACForm' },
            'atcf-request-form': { id: 'atcf-request-form', model: Workflow.model.atcfForm.ATCFRequestForm, viewCls: 'Workflow.view.atcf.RequestForm' },
            'eombp-request-form': { id: 'eombp-request-form', model: Workflow.model.eombpForm.EOMBPRequestForm, viewCls: 'Workflow.view.eombp.RequestForm' },
            'tascr-request-form': { id: 'tascr-request-form', model: Workflow.model.tascrForm.TASCRRequestForm, viewCls: 'Workflow.view.tascr.RequestForm' },
            'osha-request-form': { id: 'osha-request-form', model: Workflow.view.osha.OSHARequestModel, viewCls: 'Workflow.view.osha.OSHAForm' },
            'itad-request-form': { id: 'itad-request-form', model: Workflow.view.itad.ITAdRequestModel, viewCls: 'Workflow.view.itad.ITAdForm' },
            'admsr-request-form': { id: 'admsr-request-form', model: Workflow.view.admsr.AdmsrRequestModel, viewCls: 'Workflow.view.admsr.AdmsrForm' },
            'iteirq-request-form': { id: 'iteirq-request-form', model: Workflow.model.iteirqForm.ITEIRQRequestForm, viewCls: 'Workflow.view.iteirq.RequestForm' },
            'rmd-request-form': { id: 'rmd-request-form', model: Workflow.model.rmdForm.RMDRequestForm, viewCls: 'Workflow.view.rmd.RequestForm' },
            'jram-request-form': { id: 'jram-request-form', model: Workflow.model.jramForm.JRAMRequestForm, viewCls: 'Workflow.view.jram.RequestForm' },
            'itcr-request-form': { id: 'itcr-request-form', model: Workflow.view.itcr.ITCRRequestModel, viewCls: 'Workflow.view.itcr.ITCRForm' },
            'gmu-request-form': { id: 'gmu-request-form', model: Workflow.model.gmuForm.GMURequestForm, viewCls: 'Workflow.view.gmu.RequestForm' },
            'itirf-request-form': { id: 'itirf-request-form', model: Workflow.view.irf.IRFRequestModel, viewCls: 'Workflow.view.irf.IRFForm' },
            'hgvr-request-form': { id: 'hgvr-request-form', model: Workflow.model.hgvrForm.HGVRRequestForm, viewCls: 'Workflow.view.hgvr.RequestForm' }
        };
        return routeIdMapping[requestForm];
    },

    setWorkflowFormOpenFlag: function (w) {
        this.getView().getViewModel().set('workflowFormOpen', w);
    },

    getWorkflowFormOpenFlag: function () {
        return this.getView().getViewModel().get('workflowFormOpen');
    },

    //search menus
    treeNavNodeRenderer: function (value) {
        return this.rendererRegExp ? value.replace(this.rendererRegExp, '<span style="color:red;font-weight:bold">$1</span>') : value;
    },

    onNavFilterFieldChange: function (field, value) {

        var me = this,
            tree = me.getReferences().navigationTreeList;

        if (value) {
            me.preFilterSelection = me.getViewModel().get('selectedView');
            me.rendererRegExp = new RegExp('(' + value + ')', "gi");
            field.getTrigger('clear').show();
            me.filterStore(value);
        } else {
            me.rendererRegExp = null;
            tree.store.clearFilter();
            field.getTrigger('clear').hide();

            // Ensure selection is still selected.
            // It may have been evicted by the filter
            if (me.preFilterSelection) {
                tree.ensureVisible(me.preFilterSelection, {
                    select: true
                });
            }
        }
    },

    onNavFilterClearTriggerClick: function () {
        var me = this;
        /*tree = me.getReferences().navigationTreeList;
        
        // reset expanded = false while click on the (X) search
        tree.store.root.data.expanded = false;*/

        // reset all value 
        me.getReferences().navtreeFilter.setValue();

    },

    onNavFilterSearchTriggerClick: function () {
        var field = this.getReferences().navtreeFilter;

        this.onNavFilterFieldChange(field, field.getValue());
    },

    filterStore: function (value) {
        var nodes = [];
        var me = this,
            tree = me.getReferences().navigationTreeList,
            store = tree.store,
            searchString = value.toLowerCase(),
            filterFn = function (node) {
                // set expended menu  
                if (searchString) {
                    if (node.data.leaf == false && node.data.expanded == false) {
                        nodes.push(node);
                    }
                }
                //
                var children = node.childNodes,
                    len = children && children.length,
                    visible = v.test(node.get('text')),
                    i;

                // If the current node does NOT match the search condition
                // specified by the user...
                if (!visible) {

                    // Check to see if any of the child nodes of this node
                    // match the search condition.  If they do then we will
                    // mark the current node as visible as well.
                    for (i = 0; i < len; i++) {
                        if (children[i].isLeaf()) {
                            visible = children[i].get('visible');
                        }
                        else {
                            visible = filterFn(children[i]);
                        }
                        if (visible) {
                            break;
                        }
                    }

                }

                else { // Current node matches the search condition...

                    // Force all of its child nodes to be visible as well so
                    // that the user is able to select an example to display.
                    for (i = 0; i < len; i++) {
                        children[i].set('visible', true);
                    }
                }

                return visible;
            }, v;

        if (searchString.length < 1) {
            store.clearFilter();
        } else {
            v = new RegExp(searchString, 'i');
            store.getFilters().replaceAll({
                filterFn: filterFn
            });
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].expand();
            }
        }
    }
});