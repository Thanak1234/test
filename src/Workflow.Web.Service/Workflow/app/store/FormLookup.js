Ext.define('Workflow.store.FormLookup', {
	extend: 'Ext.data.Store',
	alias: 'store.form-lookup',
	fields: [
		{ name: 'id', type: 'int' },
        { name: 'parentId', type: 'int' },
        { name: 'name', type: 'string' },
        { name: 'hasChild', type: 'bool' },
        { name: 'value', type: 'string' }
	]
});
