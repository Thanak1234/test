Ext.define('Workflow.view.vaf.VAFModel', {
    extend: 'Workflow.view.common.GenericModel',
    alias: 'viewmodel.vaf',
    data: {
        Information: {
            AdjType: '',
            Remark: ''
        },
        AllOutlines: [],
        NewOutlines: [],
        ModifiedOutlines: [],
        DeletedOutlines: [],
        viewSetting: null
    },
    stores: {
        outlineStore: {
            baseParams: {
                async: false
            },
            model: 'Workflow.model.vaf.Outline'
        }
    },
    formulas: {
        readOnly: function (get) {
            if (get('viewSetting') && get('viewSetting').container.readOnly) {
                return true;
            } else {
                return false;
            }
        }
    }
});
