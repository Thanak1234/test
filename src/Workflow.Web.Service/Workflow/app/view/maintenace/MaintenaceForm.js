Ext.define("Workflow.view.maintenace.MaintenaceForm", {
    extend: "Workflow.view.common.GenericForm",
    title: 'Maintenace Work Order',
    viewModel: {
        type: 'maintenace'
    },
    formType: 'MWO_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/mworequest',
    buildComponents: function () {
        var me = this;
        return [
            {
                xtype: 'maintenace-information',
                reference: 'information',
                parent: me,
                margin: 5,
                bind: {
                    hidden: '{config.information.hidden}',
                    disabled: '{config.information.hidden}'
                }
            },
            {
                xtype: 'maintenace-department',
                reference: 'department',
                margin: 5,
                bind: {
                    hidden: '{config.department.hidden}',
                    disabled: '{config.department.hidden}'
                }
            },
            {
                xtype: 'maintenace-technician',
                reference: 'technician',
                margin: 5,
                bind: {
                    hidden: '{config.technician.hidden}',
                    disabled: '{config.technician.hidden}'
                }
            }
        ];
    },
    excludeProps: ['picker', 'subLocation', 'chargable', 'cmboLocationType'],
    loadData: function (viewmodel) {
        var me = this;
        var refs = me.getReferences();
        var information = viewmodel.get('information');
        var picker = refs.picker;
        if (information) {
            if (information.jaTechnician) {
                //var store = picker.getStore();
                //Ext.apply(store.getProxy().extraParams, {
                //    EmpId: information.jaTechnician
                //});
                //store.load({
                //    callback: function (records, operation, success) {
                //        if (success) {
                //            picker.getTrigger('clear').hide();
                //            picker.getTrigger('edit').hide();
                //        }
                //    }
                //});
            } else {
                viewmodel.set('information.jaTechnician', null);
            }            
        }
    },
    transformData: function (viewmodel) {
        var me = this;
        var refs = this.getReferences();
    },
    validate: function (data) {
        var actName = data.activity;
        var refs = this.getReferences();
        if (!refs.container.form.isValid()) {
            return 'Some field(s) of information is required. Please input the required field(s) before you click the Submit button.';
        }       
    },
    buildConfigs: function (curAct, lastAct, config) {
        var container = config.container;
        if (curAct === "Form View" && (lastAct === 'HoD Approval' || lastAct === 'Requestor Rework' || lastAct === 'Submission')) {
            container.information.readOnly = true;
            container.information.hidden = false;

            container.department.readOnly = true;
            container.department.hidden = true;

            container.technician.readOnly = true;
            container.technician.hidden = true;
        } else if (curAct === "Form View" && lastAct === 'ADM Approval') {
            container.information.readOnly = true;
            container.information.hidden = false;

            container.department.readOnly = true;
            container.department.hidden = false;

            container.technician.readOnly = true;
            container.technician.hidden = true;
        } else if(curAct === "Form View") {
            container.information.readOnly = true;
            container.information.hidden = false;

            container.department.readOnly = true;
            container.department.hidden = false;

            container.technician.readOnly = true;
            container.technician.hidden = false;
        }
        return config;
    },
    clearData: function () {
        var refs = this.getReferences();
        var viewmodel = this.getViewModel();
        refs.container.reset();
        viewmodel.set('information.jaTechnician', null);
    }
});
