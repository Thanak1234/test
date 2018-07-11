Ext.define('Workflow.view.ComponentViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.component',
    data: {
        action: 'ADD',

        serial: null,
        activity: 'Submission',
        formData: null , //No Used
        requestHeaderId: 0,
        requestorId: 0
    },
    formulas: {
        submitBtText: function (get) {
            var textLabel = 'Submit', action = get('action');
            if (action.toUpperCase() === 'ADD') {
                textLabel = 'Add';
            } else if (action.toUpperCase() === 'EDIT') {
                textLabel = 'Update';
            }
            return textLabel;
        },
        submitBtVisible: function (get) {
            var action = get('action');
            return action.toUpperCase() !== 'VIEW'
        },
        readOnlyField: function (get) {
            return (get('action') === 'VIEW' || (get('item') && get('item').get('id') > 0));
        }

    }
});