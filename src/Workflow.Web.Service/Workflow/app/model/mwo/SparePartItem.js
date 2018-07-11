Ext.define('Workflow.model.mwo.SparePartItem', {
    extend: 'Workflow.model.Base',
    idProperty: 'id',
    fields: [
        'id',
        'itemId',
        'itemCode',
        'requestHeaderId',
        'partType',
        'itemDescription',
        'unit',
        'unitPrice',
        'qtyRequested',
        'qtyIssued',
        'qtyReturn',
        'amount',
        'modifiedBy',
        'modifiedDate',
        'createdBy',
        'createdDate'
    ]
});
