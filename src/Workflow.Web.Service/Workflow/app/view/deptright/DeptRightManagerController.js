Ext.define('Workflow.view.deptright.DeptRightManagerController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.deptrightmanager',
   
    onRoleManagementSync: function () {
        var me = this;
        //mask
        Ext.getBody().mask("Loading...");

        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/deptrights/rolemngtsync',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },            
            success: function (response) {
                me.onLoadGridWithParam();
                var obj = Ext.decode(response.responseText);
                me.showToast(Ext.String.format(obj.message));
                //unmask
                Ext.getBody().unmask();
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
            //viewButton.setDisabled(false);
        } else {
            editButton.setDisabled(true);
            //viewButton.setDisabled(true);
        }
    },
    onAddClick: function(btn, e, eOpts) {
        var me = this;
        var r = me.getReferences();
        var vm = me.getViewModel();
        var cmbForm = r.cmbForm;
        var cmbDept = r.cmbDept;

        var window = Ext.create('Workflow.view.deptright.UserWindow', {
            mainController: me,
            action: 'add',
            buttonText: 'Add',
            viewModel: {
                data: {
                    employeeInfo: null,
                    formid: cmbForm.getValue(),
                    deptid: cmbDept.getValue(),
                    active: false
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
        var cmbForm = r.cmbForm;
        var cmbDept = r.cmbDept;


        var user = vm.get('user');        
        //alert(user.getData().reqapp);
        //console.log(user);
        var window = Ext.create('Workflow.view.deptright.UserWindow', {
            mainController: me,
            action: 'edit',
            buttonText: 'Update',
            viewModel: {
                data: {
                    employeeInfo: user,                    
                    formid: cmbForm.getValue(),
                    deptid: cmbDept.getValue(),
                    active: (user.getData().status == 'ACTIVE' ? true : false)                
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

        var cmbDept = r.cmbDept;
        //var cmbRole = r.cmbRole;

        //cmbDept.reset();
        //cmbRole.reset();
        me.onLoadGridWithParam();
        //cmbRole.setDisabled(true);
        //me.disableGrid();
    },
    setDisabledButton: function () {
        var me = this;
        var r = me.getReferences();
        r.editButton.setDisabled(true);
        r.viewButton.setDisabled(true);
        r.syncButton.setDisabled(true);
    },
    onDepartmentFilterClearClick: function (btn, e) {
        var me = this;
        var r = me.getReferences();
        var syncButton = r.syncButton;
        syncButton.setDisabled(true);
        e.field.setValue();
    },
    onDepartmentSelect: function (combo, record, eOpts) {
        var me = this;
        var v = me.getView();
        var r = me.getReferences();

        //var cmbForm = r.cmbForm;

        var syncButton = r.syncButton;

        var id = record.get('id');

        //alert("dept id : " + id);
        //alert("form id: " + cmbForm.getValue());

        syncButton.setDisabled(false);
        me.onLoadGridWithParam();

        //alert(id);

        //cmbRole.reset();
        //cmbRole.setDisabled(true);

        //me.disableGrid();

        

        //cmbRole.setStore(store);

        //store.load({
        //    callback: function (records, operation, success) {
        //        cmbRole.setDisabled(false);
        //    }
        //});
    },
    disableGrid: function() {
        var me = this;
        var r = me.getReferences();

        var deptacessright = r.deptacessright;
        deptacessright.setDisabled(true);
        deptacessright.getStore().removeAll();
    },
    onRoleSelectChange: function (combo, record, eOpts) {
        var me = this;
        var v = me.getView();
        var r = me.getReferences();
        var deptacessright = r.deptacessright;
        var paging = r.paging;

        var id = record.get('id');

        var store = Ext.create('Workflow.store.rights.UserRight');

        Ext.apply(store.getProxy().extraParams, {
            darId: id
        });
        deptacessright.setStore(store);

        store.reload({
            callback: function (records, operation, success) {
                deptacessright.setDisabled(false);
                paging.setStore(store);
            }
        });


    },
    onLoadGridWithParam : function(){
        var me = this;
        var v = me.getView();
        var r = me.getReferences();
        var deptacessright = r.deptacessright;
        var paging = r.paging;

        var cmbForm = r.cmbForm;
        var cmbDept = r.cmbDept;

        if (cmbForm.getValue() == null || cmbDept.getValue() == null)
            return;

        var store = Ext.create('Workflow.store.deptrights.DeptAccessStore');

        Ext.apply(store.getProxy().extraParams, {
            formid: cmbForm.getValue(),
            deptid: cmbDept.getValue()
        });

        deptacessright.setStore(store);

        store.reload({
            callback: function (records, operation, success) {
                deptacessright.setDisabled(false);
                paging.setStore(store);
            }
        });

    },
    onClearClick: function (btn, e, eOpts) {
        var me = this;
        var r = me.getReferences();
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
        var deptacessright = r.deptacessright;
        var store = deptacessright.store;

        deptacessright.searchRegex = new RegExp('(' + query + ')', "gi");

        Ext.apply(store.getProxy().extraParams, {           
            query: query
        });

        deptacessright.selection = null;

        store.loadPage(1);
        
    },
    onRefreshClick: function (btn, e) {
        var me = this;
        var r = me.getReferences();

        var search = r.search;
        search.setValue();
        me.onLoadGridWithParam();
    }
});