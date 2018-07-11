Ext.define('Workflow.view.mcn.employeeList.requestEmployeeController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.mcn-employeelist-requestemployee',

    config: {
        control: {
            '*':{
                loadData: 'loadData',
                onDataClear: 'clearData'
            }
        }
    },

    loadData: function (data) {
        //debugger;
        this.getView().getViewModel().set('viewSetting', data.viewSetting);

        var dataStore = this.getView().getViewModel().getStore('userStore');
        if (data) {
            dataStore.setData(data.data);
            // this.getView().getStore().setData(data.data);    
        }
        this.getView().setStore(dataStore);
    },

    clearData: function(data){
        this.getView().getStore().removeAll();
    }
    ,

    addUserAction: function (el, e, eOpts) {
        var me = this;
        me.showWindowDialog(el, { action: 'ADD', item: null }, 'Add User', function (record) {

            if (me.getView().getStore().query('empNo', record.get('empNo')).items.length > 0) {
                Ext.Msg.alert({ title: 'Information', msg: 'The user is already added.' });
                return false;
            }

            me.getView().getStore().add(record);
            return true;
        });
    },

    editUserAction: function (el, e, eOpts) {
        var me = this,
            item = me.getView().getViewModel().get('selectedItem'),
            id = item.get('id');

        me.showWindowDialog(el, {
            action: 'Edit',
            item: me.getemployeeFromRequestEmployee(item)
        }, 'Edit User',
             function (record) {
                 if (record.get('empNo') != item.get('empNo') && me.getView().getStore().query('empNo', record.get('empNo')).items.length > 0) {
                     Ext.Msg.alert({ title: 'Information', msg: 'The user is already added.' });
                     return false;
                 }
                 item.set(record.getData());
                 item.set('id', id);
                 return true;
             });
    },

    viewUserAction: function (el) {
        var me = this;
        var item = me.getView().getViewModel().get('selectedItem');
        me.showWindowDialog(el, {
            action: 'VIEW',
            item: me.getemployeeFromRequestEmployee(item)
        }, 'View User', function (record) {
        });
    },

    getemployeeFromRequestEmployee: function (item) {
        var record = Ext.create('Workflow.model.common.EmployeeInfo', {
            id: item.get('empId'),
            employeeNo: item.get('empNo'),
            fullName: item.get('empName'),
            subDeptId: item.get('teamId'),
            subDept: item.get('teamName'),
            position: item.get('position'),
            email: item.get('email'),
            hiredDate: item.get('hiredDate'),
            reportTo: item.get('manager'),
            phone: item.get('phone'),
            department: { id: item.get('teamId'), fullName: item.get('teamName') }
        });
        return record;
    },

    removeRecord: function (grid, rowIndex, colIndex) {
        var me = this,
            store = grid.getStore(),
            rec = store.getAt(rowIndex);

        Ext.MessageBox.show({
            title: 'Alert',
            msg: 'Are you sure to delete this record?',
            buttons: Ext.MessageBox.YESNO,
            scope: this,
            fn: function (bt) {
                if (bt === 'yes') {
                    store.remove(rec);
                    me.showToast(Ext.String.format('Request item {0} has been removed', rec.get('itemName')));
                }
            }
        });
    },

    //TODO: consider abstract method or mixin
    showWindowDialog: function (lauchFromel, model, title, cb) {

        var me = this;
        var window = Ext.create('Workflow.view.mcn.employeeList.requestEmployeeWindow',
         {
             title: title,
             mainView: me,
             viewModel: {
                 data: model
             },
             lauchFrom: lauchFromel,
             cbFn: cb
         });

        window.show(lauchFromel);
    }

});