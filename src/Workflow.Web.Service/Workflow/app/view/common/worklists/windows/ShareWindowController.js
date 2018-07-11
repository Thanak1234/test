Ext.define('Workflow.view.common.worklists.ShareWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.common-worklist-sharewindow',
    onOkClickHandler: function (btn, e, eOpts) {
        var me = this, view = me.getView(),
            vm = view.getViewModel(),
            comment = vm.get('comment');
        
        if(!comment || comment == ''){
            Ext.MessageBox.alert('Require', 'Please input comment before you redirect.');
        }else{
            me.setSharedUsers();
            me.closeWindow();         
        }
    },
    addDestination: function (record) {
        var me = this;
        var grid = me.getView().getReferences().destinationGridPanel;
        var store = grid.getStore();

        if (me.existingDestinationUser(store, record)) {
            Ext.MessageBox.alert('Duplicate!', 'The user has already in User List.', function () {

            });

            return false;
        }

        store.add(record);

        return true;
    },
    onCancelClickHandler: function (btn, e, eOpts) {
        var me = this;
        me.closeWindow();
    },
    
    onAddUserClickHandler: function (btn, e, eOpts) {
        var me = this;
        var window = Ext.create('Workflow.view.common.worklists.EmployeeWindow', {
            mainView: me
        });
        if (window) {
            window.show(me.getReferences().btnAddUser);
        }
    },
    onRemoveUserClickHandler: function (btn, e, eOpts) {
        var me = this;
        var selectedRow = me.getView().getViewModel().get('destinationGridPanel').selection;

        if (!selectedRow.dirty) {
            Ext.MessageBox.alert('Remove Error', 'Worklist item shared to this user already so you can\'t remove.', function () {

            });
            return;
        }

        var grid = me.getView().getReferences().destinationGridPanel;
        var record = grid.getSelectionModel().getSelection()[0];
        if (record)
            grid.getStore().remove(record);
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
    setSharedUsers: function () {
        var me = this, view = me.getView(),
            vm = view.getViewModel();
        var payload = view.payload;
        var serialNumber = me.getView().serialNumber;
        var grid = me.getView().getReferences().destinationGridPanel;
        var store = grid.getStore();
        var mainView = me.getView().mainView;

        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/worklists/setsharedusers',
            method: 'POST',
            params: { 
                serialNumber: serialNumber, 
                comment: vm.get('comment'), 
                requestHeaderId: payload.requestHeaderId },
            headers: { 'Content-Type': 'application/json' },
            jsonData: me.getRawDataFromStore(store),
            success: function (response) {
                mainView.refreshWorklist();
            },
            failure: function (response) {
                var error = Ext.JSON.decode(response.responseText);
                Workflow.global.ErrorMessageBox.show(error);
            }
        });
    },
    getRawDataFromStore: function (store) {
        var datar = new Array();
        var records = store.getRange();
        for (var i = 0; i < records.length; i++) {
            datar.push(records[i].data);
        }
        return datar;
    }
});
