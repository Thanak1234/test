Ext.define('Workflow.view.rac.RACModel', {
    extend: 'Workflow.view.common.GenericModel',
    alias: 'viewmodel.rac',
    data: {
        Information: {
            Items: null,
            Reasons: null,
            Remark: null,
            SerialNo: null,
            IssueDate: null
        },
        viewSetting: null,
        itemSelected: null
    },
    formulas: {
        readOnly: function (get) {
            if (get('viewSetting') && get('viewSetting').container.readOnly) {
                return true;
            } else {
                return false;
            }
        },
        isIssue: function (get) {
            if (get('viewSetting') && get('viewSetting').container.isIssue) {
                return true;
            } else {
                return false;
            }
        },
        editable: function (get) {
            if (get('viewSetting') && get('viewSetting').container.editable) {
                return true;
            } else {
                return false;
            }
        }
    }
});
