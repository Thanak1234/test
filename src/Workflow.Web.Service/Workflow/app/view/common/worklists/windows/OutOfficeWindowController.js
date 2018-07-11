Ext.define('Workflow.view.worklist.OutOfiiceWindowController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-worklist-outofficewindow',
    outOfOfficeRequest: {
        setOutOfOffice: Workflow.global.Config.baseUrl + 'api/worklists/setoutofoffice'
    },
    init: function() {

    },
    onStatusChange: function (field, newValue, oldValue) {
        var me = this;
        var destinationGridPanel = me.getDestinationGridPanel();
        var getExceptionRuleGridPanel = me.getExceptionRuleGridPanel();
        if (newValue.status == false) {
            destinationGridPanel.setDisabled(false);
            getExceptionRuleGridPanel.setDisabled(false);
        } else {
            destinationGridPanel.setDisabled(true);
            getExceptionRuleGridPanel.setDisabled(true);
        }
    },
    onExceptionRuleAddClickHandler: function (btn, e, eOpts) {
        var me = this;        
        var window = Ext.create('Workflow.view.common.worklist.ExceptionRuleWindow', {
            mainView: me,
            viewModel: {
                data: {
                    action: 'Add',
                    exceptionTemp: null
                },
                stores: {
                    destinations: {
                        model: 'Workflow.model.common.worklists.Destination',
                        data: []
                    }
                }                
            }            
        });
        if(window) {        
            window.show();
        }
    },
    onExceptionRuleEditClickHandler: function (btn, e, eOpts) {
        var me = this;
        var selectedException = me.getView().getViewModel().get('selectedException');
        if (selectedException) {

            var window = Ext.create('Workflow.view.common.worklist.ExceptionRuleWindow', {
                mainView: me,                
                viewModel:{
                    data: {
                        action: 'Edit',
                        selectedException: selectedException,
                        exceptionTemp: {
                            name: selectedException.get('Name'),
                            processPath: selectedException.get('ProcessPath'),
                            activity: selectedException.get('Activity')
                        }
                    },
                    stores: {
                        destinations: {
                            model: 'Workflow.model.common.worklists.Destination',
                            data: selectedException.get('Destinations')
                        }
                    }
                }                
            });
            if (window) {
                window.show();
            }
        }        
    },
    onExceptionRuleRemoveClickHandler: function (btn, e, eOpts) {
        var me = this;
        var store = me.getExceptionRuleStore();
        var record = me.getView().getViewModel().get('selectedException');
        if (record) {
            store.remove(record);
        }
    },
    onRemoveUserClickHandler: function (btn, e, eOpts) {
        var me = this;
        var grid = me.getView().getReferences().destinationGridPanel;
        var record = grid.getSelectionModel().getSelection()[0];
        if (record)
            grid.getStore().remove(record);
    },
    onOkClickHandler: function (btn, e, eOpts) {
        var me = this;
        if (me.setOutOfOffice()) {
            me.closeWindow();
        }        
    },
    onCancelClickHandler: function (btn, e, eOpts) {
        var me = this;
        var w = me.getView();
        w.close();
    },
    onExceptionCancelClickHandler: function (btn, e, eOpts) {
        var me = this;
        var w = me.getView();
        w.close();
    },
    onAddUserClickHandler: function (btn, e, eOpts) {
        var me = this;        
        var window = Ext.create('Workflow.view.common.worklists.EmployeeWindow', {
            mainView: me
        });
        if(window) {        
            window.show(me.getReferences().btnAddUser);
        }
    },
    onAddUserEmployeeClickHandler: function(data) {
        
    },
    addDestination: function (newRecord) {
        var me = this;

        if (me.validate(newRecord)) {
            var store = me.getDestinationStore();

            if (me.existingDestinationUser(store, newRecord)) {
                Ext.MessageBox.alert('Duplicate User', '<div style="color:red">User already extist in User List.</div>');
                return false;
            }

            store.add(newRecord);

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
            var error = '<div style="color:red">' + message + '</div>';
            Ext.MessageBox.alert('Blank Error', error);
        }

        return isValid;
    },
    addExceptionRule: function (newRecord) {
        var me = this;
        var store = me.getExceptionRuleStore();

        if (me.existingExceptionRule(store, newRecord)) {
            Ext.MessageBox.alert('Duplicate User', '<div style="color:red">Exception Rule already extist in Exception Rule List.</div>');
            return false;
        }

        store.add(newRecord);

        return true;
    },
    updateExceptionRule: function(selectedRecord, newRecord) {
        var me = this;
        var store = me.getExceptionRuleStore();

        if (me.existingExceptionRule(store, newRecord, selectedRecord)) {
            Ext.MessageBox.alert('Duplicate!', 'Existing Exception Rule.');
            return false;
        }

        selectedRecord.set('Name', newRecord.Name);
        selectedRecord.set('Process', newRecord.Process);
        selectedRecord.set('ProcessPath', newRecord.ProcessPath);
        selectedRecord.set('Activity', newRecord.Activity);
        selectedRecord.set('ActDisplayName', newRecord.ActDisplayName);
        selectedRecord.set('Destinations', newRecord.Destinations);

        return true;
    },
    existingDestinationUser: function (store, newRecord) {
        var recordIndex = store.findBy(
            function (record, id) {
                if (record.get('LoginName') == newRecord.get('LoginName')) {
                    return true;
                }
                return false;
            }
        );

        return (recordIndex != -1);
    },
    existingExceptionRule: function (store, newRecord, selectedRecord) {
        
        var recordIndex = store.findBy(
            function (record, id) {
                if (record.get('Name') == newRecord.Name && newRecord.Name != selectedRecord.get('Name')) {
                    return true;
                }
                return false;
            }
        );

        return (recordIndex != -1);
    },
    getDestinationGridPanel: function () {
        return this.getView().getReferences().destinationGridPanel;
    },
    getExceptionRuleGridPanel: function() {
        return this.getView().getReferences().exceptionRuleGridPanel;
    },
    getViewModelData: function (key) {
        return this.getView().getViewModel().get(key);
    },
    getDestinationStore: function () {
        return this.getDestinationGridPanel().getStore('destinations');
    },
    getExceptionRuleStore: function () {
        return this.getExceptionRuleGridPanel().getStore('exceptionRules');
    },
    getRawOutOfOfficeData: function () {
        var me = this;
        return me.getView().mainView.OutOfOfficeData;
    },
    getRawDataOfStore: function (store) {
        var datar = new Array();
        var records = store.getRange();
        for (var i = 0; i < records.length; i++) {
            datar.push(records[i].data);
        }
        return datar;
    },
    setOutOfOffice: function () {
        var me = this;
        var v = me.getView();

        var record = me.getRawOutOfOfficeData();
        if (record) {
            var data = record.getData(true);
            data.WorkType.Destinations = me.getRawDataOfStore(me.getDestinationStore());
            data.WorkType.WorkTypeExceptions = me.getRawDataOfStore(me.getExceptionRuleStore());
            var radio = v.getReferences().radio;
            if (data.WorkType.Destinations.length == 0 && radio.getValue().status == false) {
                Ext.MessageBox.alert('Duplicate User', '<div style="color:red">Please select at least one destination user to forward your work items to.</div>');
                return false;
            }

            Ext.Ajax.request({
                url: me.outOfOfficeRequest.setOutOfOffice,
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                jsonData: data,
                success: function (response) {
                    v.mainView.refreshWorklist();
                },
                failure: function (response) {
                    var error = Ext.JSON.decode(response.responseText);
                    Workflow.global.ErrorMessageBox.show(error);
                }
            });

            return true;
        }
    },
    closeWindow: function () {
        var me = this;
        var w = me.getView();
        w.close();
    }
});