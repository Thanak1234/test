Ext.define('Workflow.view.events.avdr.AvdrFormModel', {
    extend: 'Workflow.view.events.common.FormBaseModel',
    alias: 'viewmodel.event-avdr',
    data: {
        name: 'workflow',
        formRequestData: {
            incidentDate: null,
            sdl: 'Damage'
        }
    }
});
