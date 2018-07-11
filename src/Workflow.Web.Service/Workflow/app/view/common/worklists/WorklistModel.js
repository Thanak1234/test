Ext.define('Workflow.view.common.worklist.WorklistViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-worklist',
    data: {
        searchText: '',
        status: '',
        selectedRecord: null,
        disableReleaseBtn: true
    },
    stores:{
        worklists:{ 
            model: 'Workflow.model.common.worklists.Worklist',
            autoLoad: true 
        },
        statusStore: {
            autoLoad: true,
            fields: ['display', 'value'],
            data: [
                {
                    display: 'Any (except sleep)',
                    value: ''
                },
                {
                    display: 'Open',
                    value: 'Open'
                },
                {
                    display: 'Available',
                    value: 'Available'
                },
                {
                    display: 'Allocated',
                    value: 'Allocated'
                },
                {
                    display: 'Sleep',
                    value: 'Sleep'
                }
            ]
        }
    }
});