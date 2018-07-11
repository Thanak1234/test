/**
 *
 *Author : Phanny
 */
Ext.define('Workflow.view.common.requestor.RequestorController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.common-requestor-requestor',
    requires: ['Workflow.view.common.employee.form.EmployeeForm'],
    onEmployeeChanged : function( el, newValue, oldValue, eOpts){


        if(newValue){
            el.getTrigger('clear').show();
        }else{
            el.getTrigger('clear').hide();
        }

    },

    config: {
        control: {
            '*': {
                loadData: 'loadData',
                clear : 'clearRequestor'
            }
        }
    },

    loadData: function (data) {

        var model = this.getView().getViewModel();
        if (data) {
            var requestor = Ext.create(Workflow.model.common.EmployeeInfo,data.requestor);

            this.setViewData(requestor);
            model.set('viewSetting', data.viewSetting);
            model.set('priorityVal', data.priority);

            this.getView().setTitle("Requestor - Last activity: " + data.lastActivity + ", Status: " + data.status);

        } else {
            this.setDefautRequestor();
        }
    },

    clearRequestor: function(){
        var view = this.getView();
        if(view && view.itemSelectionCB){
            var view = this.getView();
            view.itemSelectionCB(null);
        }

    },
    selectedRequestor: function(picker){
        var view = this.getView(),
            mainView = view.mainView;

        if(mainView){
            var mainViewModel = mainView.getViewModel();
            mainViewModel.set('requestorId', picker.getValue());
        }

        if (view.itemSelectionCB) {
            view.itemSelectionCB(picker.getSelection());
        }

    },
    onRequestorCleared: function(el){
        el.clearValue();
    },

    setDefautRequestor : function(){
        var identity = Ext.create(Workflow.model.common.EmployeeInfo, Workflow.global.UserAccount.identity);
        this.setViewData(identity);
    },

    setViewData: function (emp) {
        var view = this.getView(),
            mainView = view.mainView;


        //this.getView().getViewModel().set('employeeInfo', emp);
        var disableEditBtn = this.isManualEmployee(emp) ? false : true;
        view.getViewModel().set({ 'employeeInfo': emp, 'disableEditButton': disableEditBtn });

        if (mainView) {
            var mainViewModel = mainView.getViewModel();

            //Can be doublicated with selectedRequestor() Method

            mainViewModel.set('requestorId', emp.id);
        }



    },
    showAddWindow: function(){
        //var me=this,
        //    window = Ext.create('Workflow.view.common.requestor.AddRequestor',{mainView: this,lauchFrom: me.getReferences().addNewBt });

        //window.show(me.getReferences().addNewBt);
        var me = this,

        //get id
        view = me.getView(),

         window = Ext.create('Workflow.view.common.employee.form.EmployeeForm',
            {
                iconCls: 'fa fa-plus',
                viewModel: {
                    data: {
                        action: 'ADD',
                        formTitle: 'Add Requestor',
                        mainView: me,
                        employee: Ext.create('Workflow.model.common.Employee')
                    }
                }
            }
        );

        window.show();
    },

    isManualEmployee: function(emp){
    	return emp.data.empType == 'MANUAL' ? true : false;
    },
    showEditWindow: function (button, e, options) {
        var me = this,

        //get id
        view = me.getView(),
        empInfo = view.getViewModel().get('employeeInfo'),
        empStore = Workflow.model.common.Employee;
        empStore.load(empInfo.id, {
            //params: { id: empInfo.id },
            success: function (records, options, success) {
                var reportToId = empInfo.get('reportTo');
                Workflow.model.common.Employee.load(
                    reportToId ? parseInt(reportToId): 0, // ?id={id}
                    {
                    callback: function (manager, operation, success) {
                        var reportTo = null;
                        if (manager.id > 0) {
                            reportTo = Ext.create(Workflow.model.common.EmployeeInfo, {
                                id: reportToId,
                                fullName: manager.data.DisplayName
                            });
                        }

                        var window = Ext.create('Workflow.view.common.employee.form.EmployeeForm', {
                            iconCls: 'fa fa-pencil',
                            session: true,
                            viewModel: {
                                data: {
                                    action: 'EDIT',
                                    formTitle: 'Edit Requestor',
                                    employeeInfo: empInfo,
                                    mainView: me,
                                    employee: records,
                                    reportTo: reportTo
                                },
                                stores: {
                                    departments: Ext.create('Workflow.store.department.Department').load({ params: { query: '' } })
                                }
                            }
                        }
                        );
                        window.show();
                    }
                });

            }
        });

    }


});
