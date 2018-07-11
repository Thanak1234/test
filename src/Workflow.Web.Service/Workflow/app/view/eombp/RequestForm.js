Ext.define("Workflow.view.eombp.RequestForm", {
    extend: "Workflow.view.ApplicationComponent",
    xtype: 'eombp-request-form',
    title: 'EOM & Best Performance',
    formType: 'EOMBP_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/eombprequest',
    bindPayloadData: function (reference) {
        var me = this,
            bestPerformance = reference.bestPerformance,
            tndReview = reference.tndReview,
            employeeOfMonthDetail = reference.employeeOfMonthDetail,
            bestPerformanceDetail = reference.bestPerformanceDetail;

        var bestPerformanceData = bestPerformance.getViewModel().getData().bestPerformance,
            bpStore = bestPerformanceDetail.getStore(),
            eomStore = employeeOfMonthDetail.getStore();

        var data = {
            bestPerformance: bestPerformanceData,
            addBestPerformanceDetails: me.getOriginDataFromCollection(bpStore.getNewRecords()),
            editBestPerformanceDetails: me.getOriginDataFromCollection(bpStore.getUpdatedRecords()),
            delBestPerformanceDetails: me.getOriginDataFromCollection(bpStore.getRemovedRecords()),

            addEmployeeOfMonthDetails: me.getOriginDataFromCollection(eomStore.getNewRecords()),
            editEmployeeOfMonthDetails: me.getOriginDataFromCollection(eomStore.getUpdatedRecords()),
            delEmployeeOfMonthDetails: me.getOriginDataFromCollection(eomStore.getRemovedRecords())
        }

        return data;
    },
    loadDataToView: function (reference, data, acl) {
        var me = this,
            viewmodel = me.getViewModel(),
            viewSetting = me.currentActivityProperty;
        
        if (data && data.bestPerformance) {
            data.bestPerformance.employeeOfMonth = new Date(data.bestPerformance.employeeOfMonth);
        }
        
        this.fireEventLoad(reference.tndReview, data);
        this.fireEventLoad(reference.bestPerformance, data);
        this.fireEventLoad(reference.bestPerformanceDetail, data);
        this.fireEventLoad(reference.employeeOfMonthDetail, data);
        
    },
    clearData: function (reference) {
        reference.bestPerformance.getForm().reset();
        reference.bestPerformanceDetail.fireEvent('onDataClear');
        reference.employeeOfMonthDetail.fireEvent('onDataClear');
    },
    validateForm: function(reference, data){
        var me = this,
            bestPerformance = reference.bestPerformance,
            bestPerformanceDetail = reference.bestPerformanceDetail;

        var treatmentData = bestPerformance.getViewModel().getData().bestPerformance,
            bpStore = bestPerformanceDetail.getStore();
        if (data) {
            if (!bestPerformance.isValid()) {
                return "Some fields of form request are required. Please input the required field(s) before you click the take action.";
            }
            if (!bestPerformanceDetail.isHidden() && !bpStore.getAt(0)) {
                return "Please add item to item list before you take action.";
            }
        }
    },
    buildComponent: function () {
        var me = this;
        return [{
            margin: 5,
            title: 'T & D Review',
            xtype: 'eombp-bestperformance-view',
            reference: 'bestPerformance',
            viewSection: 'EMP_INFO',
            mainView: me,
            bind: {
                hidden: '{bestPerformanceProperty.hidden}'
            },
            border: true
        },{
            margin: 5,
            minHeight: 150,
            title: 'EOM - Employee List',
            xtype: 'eombp-employeeofmonth-detail-view',
            reference: 'employeeOfMonthDetail',
            mainView: me,
            border: true,
            bind: {
                hidden: '{employeeOfMonthDetailProperty.hidden}'
            }
        }, {
            margin: 5,
            minHeight: 150,
            title: 'Best Performance - Employee List',
            xtype: 'eombp-bestperformance-detail-view',
            reference: 'bestPerformanceDetail',
            mainView: me,
            border: true,
            bind: {
                hidden: '{bestPerformanceDetailProperty.hidden}'
            }
        }, {
            margin: 5,
            title: 'T & D Review',
            xtype: 'eombp-bestperformance-view',
            reference: 'tndReview',
            viewSection: 'TD_REVIEW',
            mainView: me,
            bind: {
                hidden: '{bestPerformanceProperty.hiddenTND}'
            },
            border: true
        }];
    }
});
