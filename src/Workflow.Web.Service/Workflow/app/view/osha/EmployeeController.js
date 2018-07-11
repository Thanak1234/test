Ext.define('Workflow.view.osha.EmployeeController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.osha-employee',
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear : 'clearData'
            }      
        }
    },
    loadData: function (data) {
        this.getView().getViewModel().set('config', data.config);
        var dataStore = this.getView().getViewModel().getStore('userStore');
        if(data){
            dataStore.setData(data.data);
        }
        this.getView().setStore(dataStore);
    },
    
    clearData: function () {
        console.log('clear grid');
        this.getView().getStore().removeAll();
    },
    
    addUserAction : function(el, e, eOpts){
        var me = this;
        var view = me.getView();
        
        me.showWindowDialog(el, { action: 'ADD',item:  null }, 'Add User', function(record){
            if(me.getView().getStore().query( 'empNo', record.get('empNo')).items.length>0){
               Ext.Msg.alert({title: 'Information', msg: 'The user is already added.'});
                return false;
            } 
            record.set('empType', view.empType);
            me.getView().getStore().add(record);
            return true;
        });
    },
    
    editUserAction: function(el, e, eOpts){
        var me = this,
            view = me.getView(),
            item = view.getViewModel().get('selectedItem');            
        if (item) {
            id = item.get('id');
            me.showWindowDialog(el, {
                action: 'Edit',
                item: me.getemployeeFromRequestUser(item)
            }, 'Edit User',
             function (record) {
                 if (record.get('empNo') != item.get('empNo') && me.getView().getStore().query('empNo', record.get('empNo')).items.length > 0) {
                     Ext.Msg.alert({ title: 'Information', msg: 'The user is already added.' });
                     return false;
                 }
                 record.set('empType', view.empType);
                 item.set(record.getData());
                 item.set('id', id);
                 return true;
             });
        }
    },
    
    viewUserAction : function (el){
        var me = this,
            viewmodel = me.getView().getViewModel();
        
        var item = viewmodel.get('selectedItem'),
            editable = viewmodel.get('editable');
        if (editable) {
            me.editUserAction(el);
        } else {
            me.showWindowDialog(el, {
                action: 'VIEW',
                item: me.getemployeeFromRequestUser(item)
            }, 'View User', function (record) {
            });
        }
    },
    
    getemployeeFromRequestUser : function (item){
      var record = Ext.create('Workflow.model.common.EmployeeInfo', {
                     id         : item.get('empId'),
                     employeeNo : item.get('empNo'),
                     fullName   : item.get('empName'),
                     subDeptId  : item.get('teamId'),
                     subDept    : item.get('teamName'),
                     deptName   : item.get('deptName'),
                     position   : item.get('position'),
                     email      : item.get('email'),
                     hiredDate  : item.get('hiredDate'),
                     reportTo   : item.get('manager'),
                     phone      : item.get('phone'),
                     department : {id: item.get('teamId'), fullName:item.get('teamName') }
                 }); 
        return record;
    },
    removeRecord: function (grid, rowIndex, colIndex){
        var me      = this,
            store   = grid.getStore(), 
            rec     = store.getAt(rowIndex);
            
        Ext.MessageBox.show({
            title: 'Alert',
            msg: 'Are you sure to delete this record?',
            buttons: Ext.MessageBox.YESNO,
            scope: this,
            fn: function(bt){
                if(bt==='yes') {
                   store.remove(rec);
                   me.showToast(Ext.String.format('Request item {0} has been removed', rec.get('itemName')));
                }
            } 
        });
    },
    // //TODO: consider abstract method or mixin
    showWindowDialog: function(lauchFromel, model, title, cb ){
        
        var me = this;
        var window = Ext.create('Workflow.view.osha.EmployeeWin', 
         {
            title : title, 
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
