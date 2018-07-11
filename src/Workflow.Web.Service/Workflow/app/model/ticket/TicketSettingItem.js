Ext.define('Workflow.model.ticket.TicketSettingItem', {
    extend: 'Ext.data.Model',
    idProperty: 'id',
    fields: [
        'id',
        'subCateId',
        'itemName',
        'teamId',
        'slaId',
        'description',
        'createdDate',
        'modifiedDate',
        'subCateName',
        'subCateDescription',
        'teamName',
        'teamDescription'
    ]
});
