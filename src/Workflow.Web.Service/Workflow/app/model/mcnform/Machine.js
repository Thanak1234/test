Ext.define('Workflow.model.mcnForm.Machine', {
    extend: 'Workflow.model.Base',
	proxy: {
	    type: 'rest',
	    url: Workflow.global.Config.baseUrl + 'api/mcnrequest'
	}
});
