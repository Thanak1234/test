Ext.define('Workflow.model.atdForm.Attandance', {
    extend: 'Workflow.model.Base',
	proxy: {
	    type: 'rest',
	    url: Workflow.global.Config.baseUrl + 'api/atdrequest'
	}
});
