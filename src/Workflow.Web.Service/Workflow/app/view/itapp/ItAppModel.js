Ext.define('Workflow.view.itapp.ItAppModel', {
    extend: 'Workflow.view.common.GenericModel',
    alias: 'viewmodel.itapp',
    data: {
        ProjectInit: {
            application: '',
            proposedChange: '',
            description: '',
            benefitCS: 0,
            benefitIIS: 0,
            benefitRM: 0,
            benefitOther: null,
            priorityConsideration: 0
        },
        ProjectApproval: {
            hc: 0,
            slc: 0,
            scmd: 0,
            rsim: null,
            rawm: null,
            deliveryDate: null,
            goLiveDate: null
        },
        ProjectDev: {
            startDate: null,
            endDate: null,
            remark: null,
            isQA: 0
        },
        ProjectQA: {
            startDate: null,
            endDate: null,
            remark: null,
            isQA: 1
        },        
        viewSetting: null
    },
    formulas: {
        hideProjectApproval: function (get) {
            if (get('viewSetting') && get('viewSetting').container.hideProjectApproval) {
                return true;
            } else {
                return false;
            }
        },
        hideDev: function (get) {
            if (get('viewSetting') && get('viewSetting').container.hideDev) {
                return true;
            } else {
                return false;
            }
        },
        hideQA: function (get) {
            if (get('viewSetting') && get('viewSetting').container.hideQA) {
                return true;
            } else {
                return false;
            }
        },
        hideGoLiveDate: function (get) {
            if (get('viewSetting') && get('viewSetting').container.hideGoLiveDate) {
                return true;
            } else {
                return false;
            }
        },
        readOnlyProjectApproval: function (get) {
            if (get('viewSetting') && get('viewSetting').container.readOnlyProjectApproval) {
                return true;
            } else {
                return false;
            }
        },
        readOnlyDev: function (get) {
            if (get('viewSetting') && get('viewSetting').container.readOnlyDev) {
                return true;
            } else {
                return false;
            }
        },
        readOnlyQA: function (get) {
            if (get('viewSetting') && get('viewSetting').container.readOnlyQA) {
                return true;
            } else {
                return false;
            }
        },
        readOnlyGoLiveDate: function (get) {
            if (get('viewSetting') && get('viewSetting').container.readOnlyGoLiveDate) {
                return true;
            } else {
                return false;
            }
        },
        readOnlyProjectInit: function (get) {
            if (get('viewSetting') && get('viewSetting').container.readOnlyProjectInit) {
                return true;
            } else {
                return false;
            }
        }
    }
});
