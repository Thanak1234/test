Ext.define('Workflow.view.hr.att.RequestFormModel', {
    extend: 'Workflow.view.RequestFormModel',
    alias: 'viewmodel.att-requestform',    
    formulas: {
        hidden: function (get) {
            
            var lastActivity = get('record') != null? get('record').data.lastActivity: '';

            if ('NAGA Travel'.toLowerCase() === get('activity').toLowerCase()) {                
                return false;
            } else if ('Modification'.toLowerCase() === get('activity').toLowerCase()) {
                return false;
            } else if (
                'Form View'.toLowerCase() === get('activity').toLowerCase()
                && (
                    'NAGA Travel'.toLowerCase() === lastActivity.toLowerCase()
                    || 'Modification'.toLowerCase() === lastActivity.toLowerCase()
                    )
                ) {
                return false;
            }else {
                return true;
            }
        }
    }
});
