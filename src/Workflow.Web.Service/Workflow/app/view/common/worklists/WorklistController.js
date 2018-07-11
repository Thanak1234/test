Ext.define('Workflow.view.common.worklist.WorklistViewController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-worklist',
    control: {
        //'#': {
        //    onItemClick: 'onItemClick'
        //},
        'common-worklist': {
            onItemClick: 'onItemClick'
        }
    },
    requestRoutes: {
        WORKLIST_LIST: Workflow.global.Config.baseUrl + 'api/worklists',
        WORKLIST_RELEASE: Workflow.global.Config.baseUrl + 'api/worklists/release',
        WORKLIST_SHARED_LIST: Workflow.global.Config.baseUrl + 'api/worklists/sharedusers',
        WORKLIST_REDIRECT: Workflow.global.Config.baseUrl + 'api/worklists/redirect',
        WORKLIST_EXECUTE_ACTION: Workflow.global.Config.baseUrl + 'api/worklists/executeaction',
        WORKLIST_FORCE_RELEASE: Workflow.global.Config.baseUrl + 'api/worklists/release'
    },

    worklistOptions: {
        WORKLIST_REFRESH_INTERVAL: 16000
    },
    worklistActions: {
        SHARE: 'Share',
        REDIRECT: 'Redirect',
        SLEEP: 'Sleep',
        RELEASE: 'Release',
        WAKE: 'Wake',
        OPEN_FORM: 'Open Form',
        VIEW_FLOW: 'View Flow'
    },
    searchFields: [
        'requestorId',
        'requestor',
        'startDate',
        'workflowName',
        'folio',
        'activityName',
        'openedBy'
    ],
    intervalId: null,
    init: function() {
        var me = this;
        var v = me.getView();
        var vm = me.getViewModel();

        var store = vm.getStore('worklists');
        v.setStore(store);
        v.view.refresh();
        //me.refreshWorklist();
        //me.autoRefresh(me.worklistOptions.WORKLIST_REFRESH_INTERVAL);
    },
    // Fixed grid sort break cell style
    onSortChange: function (ct, column) {
        var grid = this.getView();
        grid.getView().refresh();
    },
    onHeaderClick: function (ct, column) {
        if (column.locked) {
            switch (column.sortState) {
                case 'DESC':
                    column.sortState = 'ASC';
                    break;

                case 'ASC':
                    column.sortState = 'DESC';
                    break;

                default:
                    column.sortState = 'ASC'
                    break;
            }

            var grid = this.getView();
            var store = grid.getStore();

            store.sort([{
                property: column.dataIndex,
                direction: column.sortState
            }]);
            
            grid.getView().refresh();
        }
    },
    viewItemAction: function (grid) {
        var rec = grid.getSelection()[0];
        window.location.href = rec.get('viewFormUrl');
    },
    onItemclick: function (grid, record, item, index, e, eOpts) {
        var me = this;
        var v = me.getView();
        var vm = v.getViewModel();
        var r = me.getReferences();
        var record = vm.get('selectedRecord');
        if (record) {
            var data = record.getData();
            if (data.status === "Open") {
                vm.set('disableReleaseBtn', false);
            } else {
                vm.set('disableReleaseBtn', true);
            }
        }        
    },
    autoRefresh: function (duration) {
        var me = this;

        forever();

        function forever() {
            clearInterval(me.intervalId);
            me.intervalId = setInterval(function () {
                                me.refreshWorklist();
                                forever();
                            }, duration);
        }
    },
    
    onOutOfficeClickHandler: function (btn, e, eOpts) {       
        var me = this;
        var OOF = Workflow.model.common.worklists.OOF;
        
        OOF.load(0, {
            success: function (record, operation) {
                me.OutOfOfficeData = record;
                var rawData = record.getData(true);
                var window = Ext.create('Workflow.view.common.worklist.OutOfficeWindow', {
                    mainView: me,
                    viewModel: {
                        formulas: {
                            status: {
                                get: function (get) {
                                    return {status: rawData.Status};
                                },
                                set: function (value) {
                                    record.set('Status', value.status);
                                }
                            }
                        },
                        stores: {
                            destinations: {
                                model: 'Workflow.model.common.worklists.Destination',
                                data: rawData.WorkType.Destinations
                            },
                            exceptionRules: {
                                model: 'Workflow.model.common.worklists.WorkTypeException',
                                data: rawData.WorkType.WorkTypeExceptions
                            }
                        }
                    }                    
                });
                if (window) {
                    window.show(me.getReferences().outOfficeBtn);
                }
            },
            failure: function (record, operation) {
                var responseText = operation.error.response.responseText;
                var error = Ext.JSON.decode(responseText);
                Workflow.global.ErrorMessageBox.show(error);
            }
        });
    },
    onSearchClick: function (field, e) {
        var me = this;
        var v = me.getView();
        var vm = me.getViewModel();
        var val = field.value;

        var status = vm.get('status');
        v.searchRegex = new RegExp('(' + val + ')', "gi");

        if (val) {
            var me = this;
            field.getTrigger('clear').show();
            me.filterStoreBy(me.searchFields, val, status);
        } else {
            field.getTrigger('clear').hide();
            me.filterStoreBy(me.searchFields, val, status);
        }

    },
    onSearchClearClick: function (field, e) {
        var me = this;
        field.setValue();
        me.onSearchClick(field, e);
    },
    onSearchKeypress: function (field, e) {
        var me = this;
        if (e.keyCode == '13') {
            me.onSearchClick(field, e);
        }
    },
    onStatusChangeHandler: function (combo, record, eOpts) {
        var me = this;

        var vm = me.getViewModel();
        var v = me.getView();

        var status = vm.get('searchText');

        me.filterStoreBy(me.searchFields, v.searchText, combo.value);
    },
    onRefreshClickHandler: function (btn, e, eOpts) {
        var me = this;      
        me.refreshWorklist();
    },
    onItemClick: function (item) {
        var me = this;

        var morePrefix = 'MORE';
        var actionPrefix = 'ACTION';
        var generalPrefix = 'GENERAL';
        
   
        switch (item.prefix) {

            case generalPrefix:
                {
                    switch (item.text) {
                        case me.worklistActions.OPEN_FORM:
                            me.openForm(item);
                            break;
                        case me.worklistActions.VIEW_FLOW:
                            me.viewWorklistItemFlow(item);
                            break;
                    }
                    break;
                }
            case morePrefix:
                {
                    switch (item.text) {
                        case me.worklistActions.SHARE:
                            me.showSharedUsersWindow(item);
                            break;
                        case me.worklistActions.REDIRECT:
                            me.showRedirectWindow(item);
                            break;
                        case me.worklistActions.RELEASE:
                            me.releaseWorklistItem(item);
                            break;
                        case me.worklistActions.SLEEP:
                            me.showSleepWindow(item);
                            break;
                        case me.worklistActions.WAKE:
                            me.releaseWorklistItem(item);
                            break;
                       
                    }
                    break;
                }
            case actionPrefix:
                {
                    me.executeAction(item);
                    break;
                }
            default:
                {
                    console.log('worklist action default');
                }
        }

    
    },
    openForm: function(item){
        var data = item.wItem;
        var me = this;

        if(data.status == 'Open'){
            Ext.MessageBox.show({
                title: Ext.String.format('Open - {0}', data.folio),
                msg: Ext.String.format('This form is opening by {1}, please choose an action!', data.folio, data.openedBy),
                buttons: Ext.MessageBox.YESNOCANCEL,
                buttonText:{ 
                    yes: "Release", 
                    no: "Release & Open",
                    cancel: "Cancel"
                },
                fn: function(btn) {
                    if(btn == 'yes' ||  btn == 'no'){
                        me.releaseWorklistItem(data, function(){
                            if(btn == 'no'){
                                window.location.href = item.routeUrl;
                            }
                        });
                    }
                }
            });
        }else{
            window.location.href = item.routeUrl;
        }
    },
    onForceReleaseClick: function (btn, e, eOpts) {
        var me = this;
        var v = me.getView();
        var vm = v.getViewModel();

        var record = vm.get('selectedRecord');
        var data = record.getData();
        
        Ext.MessageBox.show({
            title: Ext.String.format('Action Confirm - {0}', data.folio),
            msg: Ext.String.format('Are you sure release this form?', data.folio, data.openedBy),
            buttons: Ext.MessageBox.YESNO,
            fn: function(btn) {
                if(btn == 'yes'){
                    me.releaseWorklistItem(data);
                }
            }
        });
    },
    executeAction: function (item) {
        var me = this;

        Ext.Msg.show({
            title: 'Action Confirm',
            message: 'Are you sure?',
            buttons: Ext.Msg.YESNO,
            icon: Ext.Msg.QUESTION,
            fn: function (btn) {
                if (btn === 'yes') {
                    var params = {
                        serialNumber: item.serial,
                        actionName: item.text,
                        sharedUser: item.sharedUser,
                        managedUser: item.managedUser
                    };

                    Ext.Ajax.request({
                        url: me.requestRoutes.WORKLIST_EXECUTE_ACTION,
                        method: 'GET',
                        headers: { 'Content-Type': 'application/json' },
                        params: params,
                        success: onRequestSuccess,
                        failure: onRequestFailed
                    });
                }
            }
        });

        function onRequestSuccess(response) {
            me.refreshWorklist();
          
        }

        function onRequestFailed(response) {
            var error = Ext.JSON.decode(response.responseText);
            Workflow.global.ErrorMessageBox.show(error);          
        }
    },
    viewWorklistItemFlow: function (item) {
        window.location.href = item.viewFlow;
    },
    releaseWorklistItem: function (item, fn) {
        var me = this;
        
        Ext.Ajax.request({
            url: me.requestRoutes.WORKLIST_RELEASE,
            method: 'GET',
            params: {
                serialNumber: item.serial
            },
            success: function(response) {
                if(fn){
                    fn();
                }
                me.refreshWorklist();
            },
            failure: function(response) {
                var error = Ext.JSON.decode(response.responseText);
                Workflow.global.ErrorMessageBox.show(error);
            }
        });
    },
    refreshWorklist: function () {


        var me = this;
        var vm = me.getViewModel();
        var v = me.getView();
        
        var store = v.getStore();

        store.reload({
            callback: callback
        });

        function callback(records, operation, success) {            
            if (success) {
                var searchText = vm.get('searchText');
                var status = vm.get('status');
                me.filterStoreBy(me.searchFields, searchText, status);
            } else {
                var responseText = operation.error.response.responseText;
                var error = Ext.JSON.decode(responseText);
                Workflow.global.ErrorMessageBox.show(error);
            }
        }
    },
    showSharedUsersWindow: function (item) {
        var me = this;
        var params = {
            serialNumber: item.serial,
            sharedUser: item.sharedUser,
            managedUser: item.managedUser
        };

        me.getSharedUsers(params, onGetSharedUsersCompleted, onGetSharedUsersError);

        function onGetSharedUsersCompleted(response) {
            var window = Ext.create('Workflow.view.common.worklists.ShareWindow', {
                serialNumber: item.serial,
                payload: item.payload,
                mainView: me,
                viewModel: {
                    stores: {
                        destinations: {
                            type: 'store',
                            model: 'Workflow.model.common.worklists.Destination',
                            data: Ext.util.JSON.decode(response.responseText)
                        }
                    }
                }
            });
            if (window) {
                window.show();
            }
        }

        function onGetSharedUsersError(response) {
            var error = Ext.JSON.decode(response.responseText);
            Workflow.global.ErrorMessageBox.show(error);
        }

    },
    showSleepWindow: function (item) {
        
        var me = this;

        var window = Ext.create('Workflow.view.common.worklists.SleepWindow', {
            mainView: me,
            serialNumber: item.serial,
            sharedUser: item.sharedUser,
            managedUser: item.managedUser
        });

        if (window) {
            window.show();
        }
    },

    getSharedUsers: function (params, onRequestSuccess, onRequestFailed) {
        var me = this;

        Ext.Ajax.request({
            url: me.requestRoutes.WORKLIST_SHARED_LIST,
            method: 'GET',
            params: params,
            success: onRequestSuccess,
            failure: onRequestFailed
        });
    },
    
    showRedirectWindow: function (item) {
        var me = this;
        var window = Ext.create('Workflow.view.common.worklists.RedirectWindow', {
            mainView: me,
            payload: item.payload,
            requestHeaderId: item.payload.RequestHeaderId
        });
        if (window) {
            window.show();
        }
    },

    addDestination: function (record, payload, comment) {
        var me = this;
        console.log('payload', payload);
        if (me.validate(record)) {
            Ext.Ajax.request({
                url: me.requestRoutes.WORKLIST_REDIRECT,
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                params: { 
                    serialNumber: payload.serial,
                    comment: comment,
                    requestHeaderId: payload.requestHeaderId
                },
                jsonData: record.getData(),
                success: function (response) {
                    me.refreshWorklist();
                    return true;
                },
                failure: function (response) {
                    var error = Ext.JSON.decode(response.responseText);
                    Workflow.global.ErrorMessageBox.show(error);
                }
            });

            return true;
        }        

        return false;
    },
    validate: function (data) {
        var isValid = true;

        var message = '';

        if (!data) {
            message += 'Employee can\'t blank. Please choose a employee.';
            isValid = false;
        }

        if (!isValid) {
            var error = '<b color="red">' + message + '</b>';
            Ext.MessageBox.alert('Blank Error', error);
        }

        return isValid;
    },
    filterStoreBy: function (fields, value, status) {
        var me = this;
        var v = me.getView();
        v.setLoading(true);
        setTimeout(function () {
            var store = v.getStore();
            store.clearFilter();

            store.each(function (rec, idx) {
                contains = false;
                wlstatus = rec.data['status'];
                for (field in rec.data) {

                    var recordValue = rec.data[field] == null ? '' : rec.data[field].toString();
                    var searchText = value == null ? '' : value.toString();

                    if (Ext.Array.contains(me.searchFields, field)) {
                        if ((searchText == '' || recordValue.indexOf(searchText) > -1)
                            && (status == wlstatus || (status == '' && !Ext.Array.contains(['Sleep'], wlstatus)))) {

                            contains = true;
                        }
                    }
                }
                if (!contains) {
                    rec.filterMeOut = false;
                } else {
                    rec.filterMeOut = true;
                }
            });
            store.filterBy(function (rec, id) {
                if (rec.filterMeOut) {
                    return true;
                }
                else {
                    return false;
                }
            });
            v.setLoading(false);
            v.view.refresh();
        }, 0);   
    }
});