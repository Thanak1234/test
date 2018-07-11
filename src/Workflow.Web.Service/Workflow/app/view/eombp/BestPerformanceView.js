Ext.define("Workflow.view.eombp.BestPerformanceView", {
    extend: "Workflow.view.FormComponent",
    xtype: 'eombp-bestperformance-view',
    modelName: 'bestPerformance',
    loadData: function (data, viewSetting) {
        var me = this,
             viewmodel = me.getViewModel();

        if (data) {
            viewmodel.set('bestPerformance', data.bestPerformance);
        }
    },

    buildComponent: function () {
        var me = this;
            viewmodel = me.getViewModel(),
            mainVM = me.mainView.getViewModel();
        
        if (this.viewSection === 'EMP_INFO') {
            me.title = 'Employee Information';
            return [{
                xtype: 'monthfield',
                reference: 'month',
                maxWidth: 350,
                format: 'F, Y',
                editable: false,
                fieldLabel: 'Employee of the month',
                allowBlank: false,
                bind: {
                    value: '{bestPerformance.employeeOfMonth}'
                }
            }];
        } else {
            me.title = 'T & D Review';
            return [{
                xtype: 'currencyfield',
                minValue: 0.001,
                fieldLabel: 'EOM Eward',
                //allowBlank: false,
                bind: { value: '{bestPerformance.eomAward}' }
            }, {
                xtype: 'currencyfield',
                minValue: 0.001,
                fieldLabel: 'Best Performance Eward',
                //allowBlank: false,
                bind: { value: '{bestPerformance.bestPerformanceAward}' }
            }];
        }
        
    }
});