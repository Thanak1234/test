Ext.define('Workflow.view.common.worklists.ExceptionRuleWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.common-worklists-exceptionRuleWindowController',

    init: function () {

    },
    onExceptionOkClickHandler: function (btn, e, eOpts) {
        var me = this;
        var controller = me.getMainController();
        
        var action = me.getViewModelData('action');
        var temp = me.getViewModelData('exceptionTemp');

        var cmbProcess = me.getReferences().cmbProcess;
        var cmdActivity = me.getReferences().cmbActivity;

        if (action == 'Edit') {

            var selectedRecord = me.getViewModelData('selectedException');
            var newRecord = {
                Name: temp != null ? temp.name : null,
                Process: cmbProcess.getRawValue() != null ? cmbProcess.getRawValue() : null,
                ProcessPath: temp != null ? temp.processPath : null,
                Activity: temp != null ? temp.activity : null,
                ActDisplayName: cmdActivity.getRawValue() != null ? cmdActivity.getRawValue() : null,
                Destinations: me.getRawOfStore(me.getDestinationStore())
            };

            if (me.validate(newRecord)) {
                if (controller.updateExceptionRule(selectedRecord, newRecord)) {
                    me.closeWindow();
                }
            }            
        } else {
            var newRecord = {
                Name: temp != null ? temp.name : null,
                Process: cmbProcess.getRawValue() != null ? cmbProcess.getRawValue() : null,
                ProcessPath: temp != null ? temp.processPath : null,
                Activity: temp != null ? temp.activity : null,
                ActDisplayName: cmdActivity.getRawValue() != null ? cmdActivity.getRawValue() : null,
                Destinations: me.getRawOfStore(me.getDestinationStore())
            };

            if (me.validate(newRecord)) {
                if (controller.addExceptionRule(newRecord)) {
                    me.closeWindow();
                }
            }
        }
        
    },
    onAddUserClickHandler: function (btn, e, eOpts) {
        var me = this;
        var window = me.getDestinationWindow({
            mainView: me
        });
        if (window) {
            window.show(me.getReferences().btnAddUser);
        }
    },
    onRemoveUserClickHandler: function (btn, e, eOpts) {
        var me = this;
        var grid = me.getDestinationGridPanel();

        var record = grid.getSelectionModel().getSelection()[0];
        if (record)
            grid.getStore().remove(record);
    },
    validate: function (record) {
        var isValid = true;

        var message = '';

        if (!record.Name) {
            message += '<li><b>Rule name</b> is required. </li>';
            isValid = false;
        }

        if (!record.Process) {
            message += '<li><b>Process name</b> is required. </li>';
            isValid = false;
        }

        if (!record.Activity) {
            message += '<li><b>Activity name</b> is required. </li>';
            isValid = false;
        }

        if (!record.Destinations || record.Destinations.length == 0) {
            message += '<li><b>Users</b> is required. </li>';
            isValid = false;
        }

        if (!isValid) {
            var error = 'Please enter the following fields: <br /><ul style="color: red">' + message + '</u>';
            Ext.MessageBox.alert('Blank Error', error);
        }

        return isValid;
    },
    validateUser: function (data) {
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
    addDestination: function (record) {
        var me = this;

        if (me.validateUser(record)) {
            var store = me.getDestinationStore();

            if (me.existingDestinationUser(store, record)) {
                Ext.MessageBox.alert('Duplicate User', '<div style="color:red">User already extist in User List.</div>');
                return false;
            }

            store.add(record);

            return true;
        }       

        return false;
    },

    getRawOfStore: function(store){
        var datar = new Array();
        var records = store.getRange();
        for (var i = 0; i < records.length; i++) {
            datar.push(records[i].data);
        }
        return datar;
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
    getReferences: function () {
        return this.getView().getReferences();
    },
    getViewModel: function () {
        return this.getView().getViewModel();
    },
    getViewModelData: function (key) {
        var me = this;
        return me.getViewModel().get(key);
    },
    getDestinationWindow: function (extend) {
        var window = Ext.create('Workflow.view.common.worklists.EmployeeWindow', extend);
        return window;
    },
    getDestinationStore: function () {
        return this.getDestinationGridPanel().getStore('destinations');
    },
    getDestinationGridPanel: function () {
        return this.getReferences().destinationGridPanel;
    },
    getMainController: function () {
        return this.getView().mainView;
    }

});

