Ext.define('Workflow.view.roles.RoleManagerController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.roles-rolemanager',
    onRemoveClick: function (btn, e, opts) {
        var me = this;
        var r = me.getReferences();
        Ext.Msg.show({
            title: 'Remove Confirm',
            message: 'Are you sure to remove these users?',
            buttons: Ext.Msg.YESNO,
            icon: Ext.Msg.QUESTION,
            fn: function (btn) {
                if (btn === 'yes') {
                    var navigation = r.navigation;
                    var treeitem = navigation.getSelection()[0];

                    if (treeitem) {

                        var userPanel = r.users;
                        var employees = userPanel.getSelection();

                        var isDbRole = treeitem.get('isDbRole');
                        var roleName = treeitem.get('value');
                        var description = treeitem.get('text');

                        var data = {
                            isDbRole: isDbRole,
                            roleName: roleName,
                            description: description,
                            users: me.getArray(employees)
                        }

                        Ext.Ajax.request({
                            url: Workflow.global.Config.baseUrl + 'api/roles/remove',
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
    getEmpIds: function(employees) {
        var ids = [];
        var count = employees.length;

        if (count > 0) {
            for (var i = 0; i < count; i++) {
                var empId = employees[i].get('empId');
                ids.push(empId);
            }
        }

        return ids;
    },
    getUsers: function (employees) {
        var users = [];
        var count = employees.length;

        if (count > 0) {
            for (var i = 0; i < count; i++) {
                var loginName = employees[i].get('loginName');
                users.push(loginName);
            }
        }

        return users;
    },
    onUserSelectionChange: function (grid, selected, eOpts) {
        var me = this;
        var count = selected.length;

        if (count == 1) {
            me.setDisabledButtons(false, false);
        } else {
            me.setDisabledButtons(true, true);
        }
    },
    onRefreshClick: function (btn, e, opts) {
        var me = this;        
        me.reloadUserStore();
    },
    onTreeRefreshClick: function(btn, e, opts) {
        var me = this;
        var r = me.getReferences();

        me.filterStore('');

        var field = r.searchTextField;

        field.getTrigger('clear').hide();
        field.setValue();

        var navigation = r.navigation;
        var store = navigation.getStore();

        store.reload();
    },
    onAddClickHandler: function (btn, e, opts) {
        var me = this;
        var r = me.getReferences();
        var navigation = r.navigation;

        var treeitem = navigation.getSelection()[0];
        var roleName = treeitem.get('value');
        var isDbRole = treeitem.get('isDbRole');

        var window = Ext.create('Workflow.view.roles.UserWindow', {
            mainController: me,
            action: 'add',
            buttonText: 'Add',
            viewModel: {
                data: {
                    employeeInfo: null,
                    include: true,
                    roleName: roleName,
                    isDbRole: isDbRole
                }
            }
        });

        window.show(btn);
    },
    onViewClickHandler: function(btn, e, opts) {
        var me = this;
        var r = me.getReferences();
        var userPanel = r.users;
        var selection = userPanel.getSelection()[0];
        var loginName = selection.get('loginName');
        var empId = selection.get('empId');
        var fullName = selection.get('fullName');

        var window = Ext.create('Workflow.view.roles.RoleWindow', {
            mainController: me,
            title: Ext.String.format('User ( {0} )', fullName),
            viewModel: {
                data: {
                    empId: empId,
                    loginName: loginName
                }
            },
            extraParam: {
                loginName: loginName,
                empId: empId
            }
        });

        window.show(btn);
    },
    onEditClickHandler: function (btn, e, opts) {
        var me = this;
        me.showEditUserWindow();
    },
    onUserRowDoubleClick: function(grid, record, tr, rowIndex, e, eOpts) {
        var me = this;
        me.showEditUserWindow();
    },
    showEditUserWindow: function(action) {
        var me = this;
        var r = me.getReferences();

        var btn = r.editButton;
        var userPanel = r.users;
        var record = userPanel.getSelection()[0];
        var selection = record;
        var navigation = r.navigation;

        var treeitem = navigation.getSelection()[0];
        var roleName = treeitem.get('value');
        var isDbRole = treeitem.get('isDbRole');

        var window = Ext.create('Workflow.view.roles.UserWindow', {
            mainController: me,
            action: 'edit',
            buttonText: 'Update',
            viewModel: {
                data: {
                    employeeInfo: selection,
                    include: selection.get('include'),
                    roleName: roleName,
                    isDbRole: isDbRole,
                    empId: selection.get('empId')
                }
            }
        });

        window.show(btn);
    },
    setDisabledButtons: function(edit, view) {
        var me = this;
        var r = me.getReferences();

        var editButton = r.editButton;
        var viewButton = r.viewButton;

        editButton.setDisabled(edit);
        viewButton.setDisabled(view);
    },
    // Tree Navigation panel events
    onNavigationFilterSearchClick: function (field, e) {
        var me = this;
        me.onNavigationFilterChangeHandler(field, field.getValue());
    },

    onNavigationFilterClearClick: function (btn, e) {
        var me = this;
        e.field.setValue();
    },

    onSelectionChangeHandler: function (tree, selected, eOpts) {
        var me = this;
        var vm = me.getViewModel();
        var r = me.getReferences();
        var userPanel = r.users;
        var navigation = r.navigation;

        var record = navigation.getSelection()[0];

        var search = r.search;

        search.setValue();
        search.getTrigger('clear').hide();

        if (record) {
            var item = record.getData();
            
            //depth
            
            var type = record.get('type');

            if (type.toLowerCase() == 'role') {
                userPanel.setDisabled(false);
                var value = record.get('value');
                me.setUserListTitle(record.get('text'));
                me.loadUserStore(record);
            } else {
                me.setUserListTitle('');
                userPanel.setDisabled(true);
                userPanel.getStore().removeAll();
            }
        }        
    },
    onNavigationFilterChangeHandler: function (field, value) {
        var me = this,
            tree = me.getReferences().navigation;
        
        if (value) {
            me.rendererRegExp = new RegExp('(' + value + ')', "gi");
            field.getTrigger('clear').show();
            me.filterStore(value);
        } else {
            me.rendererRegExp = null;
            tree.store.clearFilter();
            field.getTrigger('clear').hide();
        }
    },
    reloadUserStore: function() {
        var me = this;
        var r = me.getReferences();
        var userPanel = r.users;
        var store = userPanel.store;
        var search = r.search;

        search.setValue();
        search.getTrigger('clear').hide();

        store.clearFilter();
        store.reload();
    },
    setUserListTitle: function (roleName) {
        var me = this;
        var title = Ext.String.format('{0} Users', roleName);
        
        var r = me.getReferences();
        var userPanel = r.users;

        userPanel.setTitle(title);
    },
    loadUserStore: function(record) {
        var me = this;
        var r = me.getReferences();

        var roleName = record.get('value');
        var isDbRole = record.get('isDbRole');

        var userPanel = r.users;
        var store = Ext.create('Workflow.store.roles.Users');

        Ext.apply(store.getProxy().extraParams, {
            roleName: roleName,
            isDbRole: isDbRole
        });

        store.load();
        userPanel.setStore(store);
    },
    treeNodeRenderer: function (value) {
        return this.rendererRegExp ? value.replace(this.rendererRegExp, '<span style="color:red;font-weight:bold">$1</span>') : value;
    },

    filterStore: function (value) {
        var nodes = [];
        var me = this,
            tree = me.getReferences().navigation,
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
