Ext.define('Workflow.view.events.common.FormBaseController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.formbase',    
    renderSubForm: function (data) {
        var me = this;
        var v = me.getView();
        var r = v.getReferences();
        var viewSetting = v.getWorkflowFormConfig();
        me.loadData({ formRequestData: data && data.FormRequestData ? data.FormRequestData: null, viewSetting: viewSetting });
    },
    setDefaultReporter: function () {
        var identity = Ext.create(Workflow.model.common.EmployeeInfo, Workflow.global.UserAccount.identity);
        this.setViewData(identity);
    },
    loadReporter: function (data) {
        var me = this;
        var v = me.getView();
        var r = v.getReferences();
        if (data && data.formRequestData) {
            r.reporter.getStore().load({
                id: data.formRequestData.reporterId,
                callback: function (records, operation, success) {
                    if (success) {
                        r.reporter.getTrigger('clear').hide();
                        r.reporter.getTrigger('edit').hide();
                    }
                }
            });
        } else {
            me.setDefaultReporter();
        }        
    },
    setViewData: function (emp) {
        var me = this;
        var v = me.getView();
        var r = v.getReferences();
        var disableEditBtn = this.isManualEmployee(emp) ? false : true;
        v.getViewModel().set({ 'employeeInfo': emp, disableEditBtn: disableEditBtn });
        r.reporter.getStore().load();
    },
    showAddWindow: function () {
        var me = this,
        view = me.getView(),
         window = Ext.create('Workflow.view.common.employee.form.EmployeeForm',
            {
                iconCls: 'fa fa-plus',
                viewModel: {
                    data: {
                        action: 'ADD',
                        formTitle: 'Add',
                        mainView: me,
                        employee: Ext.create('Workflow.model.common.Employee')
                    }
                }
            }
        );

        window.show();
    },
    onRequestorCleared: function (el) {
        el.clearValue();
    },
    isManualEmployee: function (emp) {
        return emp.data.empType == 'MANUAL' ? true : false;
    },
    showEditWindow: function (button, e, options) {
        var me = this,
        view = me.getView(),
        empInfo = view.getViewModel().get('employeeInfo'),
        empStore = Workflow.model.common.Employee;
        empStore.load(empInfo.id, {
            success: function (records, options, success) {
                var reportToId = empInfo.get('reportTo');
                Workflow.model.common.Employee.load(
                    reportToId ? parseInt(reportToId) : 0, // ?id={id}
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
                                        formTitle: 'Edit',
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
