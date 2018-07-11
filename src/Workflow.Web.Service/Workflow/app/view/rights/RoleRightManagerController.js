Ext.define('Workflow.view.rights.RoleRightManagerController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.rolerightmanager',
    onViewClick: function (btn, e, opt) {
        var me = this;
        var r = me.getReferences();

        var gridUser = r.gridUser;

        var user = gridUser.getSelection()[0];

        var empId = user.get('id');
        var fullName = user.get('fullName');


        var window = Ext.create('Workflow.view.rights.RoleRightWindow', {
            mainController: me,
            title: Ext.String.format('User ( {0} )', fullName),
            viewModel: {
                data: {
                    empId: empId
                }
            },
            extraParam: {
                empId: empId
            }
        });

        if (window) {
            window.show(btn);
        }        
    },
    onRemoveClick: function(btn, e, opt) {
        var me = this;
        var r = me.getReferences();

        var gridUser = r.gridUser;
        var records = gridUser.getSelection();

        var data = me.getArray(records);

        Ext.Msg.show({
            title: 'Remove Confirm',
            message: 'Are you sure to remove these users?',
            buttons: Ext.Msg.YESNO,
            icon: Ext.Msg.QUESTION,
            fn: function (btn) {
                if (btn === 'yes') {
                    Ext.Ajax.request({
                        url: Workflow.global.Config.baseUrl + 'api/rights/deletes',
                        method: 'post',
                        headers: { 'Content-Type': 'application/json' },
                        jsonData: data,
                        success: function (response) {
                            me.onRefreshClick();
                        },
                        failure: function (response) {
                            var error = Ext.JSON.decode(response.responseText);
                            Ext.Msg.show({
                                title: 'Error!',
                                message: error.Message,
                                buttons: Ext.Msg.OK,
                                icon: Ext.MessageBox.ERROR
                            });
                        }
                    });
                }
            }
        });
    },
    getArray: function (records) {
        var result = [];
        for (var i = 0; i < records.length; i++) {
            var data = records[i].data;
            result.push(data);
        }
        return result;
    },
    onUserDoubleClick: function(grid, record, tr, rowIndex, e, eOpts) {
        var me = this;
        var r = me.getReferences();
        var records = grid.getSelection();
        if (records.length == 1) {
            me.onEditClick(r.editButton);
        }        
    },
    onUserSelectionChange: function (grid, selected, eOpts) {
        var me = this;
        var count = selected.length;
        var r = me.getReferences();

        var editButton = r.editButton;
        var viewButton = r.viewButton;

        if (count == 1) {
            editButton.setDisabled(false);
            viewButton.setDisabled(false);
        } else {
            editButton.setDisabled(true);
            viewButton.setDisabled(true);
        }
    },
    onAddClick: function(btn, e, eOpts) {
        var me = this;
        var r = me.getReferences();
        var vm = me.getViewModel();
        var cmbRole = r.cmbRole;

        var window = Ext.create('Workflow.view.rights.UserWindow', {
            mainController: me,
            action: 'add',
            buttonText: 'Add',
            viewModel: {
                data: {
                    employeeInfo: null,
                    active: true,
                    darId: cmbRole.getValue(),
                    desc: ''
                }
            }
        });

        if (window) {
            window.show(btn);
        }

    },
    onEditClick: function(btn, e, eOpts) {
        var me = this;
        var r = me.getReferences();
        var vm = me.getViewModel();
        var cmbRole = r.cmbRole;
        var user = vm.get('user');
        var desc = user.get('desc');
        var roleRightId = user.get('roleRightId');
        var active = user.get('active');

        var window = Ext.create('Workflow.view.rights.UserWindow', {
            mainController: me,
            action: 'edit',
            buttonText: 'Update',
            viewModel: {
                data: {
                    employeeInfo: user,
                    active: active,
                    darId: cmbRole.getValue(),
                    desc: desc,
                    roleRightId: roleRightId
                }
            }
        });

        if (window) {
            window.show(btn);
        }
    },
    onFormSelect: function (combo, record, eOpts) {
        var me = this;
        var v = me.getView();
        var r = me.getReferences();

        var cmbActivity = r.cmbActivity;
        var cmbRole = r.cmbRole;

        cmbActivity.reset();
        cmbRole.reset();

        cmbRole.setDisabled(true);
        me.disableGrid();
    },
    setDisabledButton: function () {
        var me = this;
        var r = me.getReferences();
        r.editButton.setDisabled(true);
        r.viewButton.setDisabled(true);
    },
    onActivitySelect: function (combo, record, eOpts) {
        var me = this;
        var v = me.getView();
        var r = me.getReferences();

        var cmbRole = r.cmbRole;

        var id = record.get('id');

        cmbRole.reset();
        cmbRole.setDisabled(true);

        me.disableGrid();

        var store = Ext.create('Workflow.store.rights.Role');

        Ext.apply(store.getProxy().extraParams, {
            actId: id
        });

        cmbRole.setStore(store);

        store.load({
            callback: function (records, operation, success) {
                cmbRole.setDisabled(false);
            }
        });
    },
    disableGrid: function() {
        var me = this;
        var r = me.getReferences();

        var gridUser = r.gridUser;
        gridUser.setDisabled(true);
        gridUser.getStore().removeAll();
    },
    onRoleSelectChange: function (combo, record, eOpts) {
        var me = this;
        var v = me.getView();
        var r = me.getReferences();
        var gridUser = r.gridUser;
        var paging = r.paging;

        var id = record.get('id');

        var store = Ext.create('Workflow.store.rights.UserRight');

        Ext.apply(store.getProxy().extraParams, {
            darId: id
        });
        gridUser.setStore(store);

        store.reload({
            callback: function (records, operation, success) {
                gridUser.setDisabled(false);
                paging.setStore(store);
            }
        });
    },
    onClearClick: function (btn, e, eOpts) {
        var me = this;
        btn.setValue();
        me.loadStore(null);
    },
    onSearchClick: function (btn, e, eOpts) {
        var me = this;
        if (e.field.value && e.field.value != '') {
            me.loadStore(e.field.value);
        }
    },
    onSearchChange: function (field, value) {
        var me = this;

        if (value) {
            field.getTrigger('clear').show();
        } else {
            field.getTrigger('clear').hide();            
        }
    },
    onSearchKeypress: function (field, e, eOpts) {
        var me = this;
        if (e.keyCode == '13') {
            me.loadStore(field.value);
        }
    },
    loadStore: function (query) {
        var me = this;
        var r = me.getReferences();
        var gridUser = r.gridUser;
        var store = gridUser.store;

        gridUser.searchRegex = new RegExp('(' + query + ')', "gi");

        Ext.apply(store.getProxy().extraParams, {
            query: query
        });

        gridUser.selection = null;

        store.loadPage(1);
    },
    onRefreshClick: function (btn, e) {
        var me = this;
        var r = me.getReferences();

        var search = r.search;
        search.setValue();
        me.loadStore(null);
    }
});