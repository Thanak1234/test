Ext.define('Workflow.model.icdForm.incident', {
    extend: 'Workflow.model.Base',
	proxy: {
	    type: 'rest',
	    url: Workflow.global.Config.baseUrl + 'api/icdrequest'
	}
});
