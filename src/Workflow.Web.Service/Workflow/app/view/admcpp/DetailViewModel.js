Ext.define('Workflow.view.eom.DetailViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.admcppdetailview',
    data: {
        admcpp: {
            model: null,
            platNo: null,
            color: null,
            yop: null,
            remark: null,
            cpsn: null,
            issueDate: null
        },
        viewSetting : null
    },
    formulas: {
        editable : function(get) {
            if (get('viewSetting') && get('viewSetting').detailview.editable) {
                return true;
            } else {
                return false;
            }
        },
        editIssue: function (get) {
            if (get('viewSetting') && get('viewSetting').detailview.editIssue) {
                return true;
            } else {
                return false;
            }
        },
        visibleIssue: function (get) {
            if (get('viewSetting') && get('viewSetting').detailview.visibleIssue) {
                return true;
            } else {
                return false;
            }
        }
    }
});
