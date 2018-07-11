Ext.define('Workflow.model.common.worklists.Worklist', {
    extend: 'Ext.data.Model',
    local:true,
    fields: [
		{ name: 'requestorId',  type: 'int', mapping: 'requestor' },
		{ name: 'requestor',    type: 'string', mapping: 'requestor' },
		{ name: 'serial', type: 'string', mapping: 'serial' },
		{ name: 'originator', type: 'string', mapping: 'originator' },
		{ name: 'allocatedUser', type: 'string', mapping: 'allocatedUser' }, // TODO: fixed mising field
		{ name: 'activityName', type: 'string', mapping: 'activityName' },
		{ name: 'folio', type: 'string', mapping: 'folio' },
        { name: 'workflowName', type: 'string', mapping: 'workflowPath' },
        { name: 'processName' },
        { name: 'priority', type: 'int', mapping: 'priority' },
		{ name: 'activityStartDate', mapping: 'lastActionDate' },
		{ name: 'startDate', mapping: 'lastActionDate' },  // TODO: fixed mising field
		{ name: 'status', type: 'string', mapping: 'status' },
		{ name: 'viewFlow', type: 'string', mapping: 'workflowUrl' },
        { name: 'data', type: 'string', mapping: 'openFormUrl' },
		{ name: 'openedBy', type: 'string', mapping: 'openedBy' },
        { name: 'viewFormUrl', type: 'string', mapping: 'formPath',
            convert: function (value, record) {
                return Ext.String.format('#{0}/SN={1}_99999',value, record.get('procInstId'));
            }
        },
        //{ name: 'noneK2Form', type: 'boolean', mapping: 'NoneK2Form' },
        { name: 'sharedUser', type: 'string', mapping: 'sharedUser' },
        { name: 'managedUser', type: 'string', mapping: 'managedUser' },
        { name: 'isShared', type: 'boolean', mapping: 'delegated' }
    ],
	 proxy:{
        type:'rest',
        url: Workflow.global.Config.baseUrl + 'api/worklists',
        reader:{
            type: 'json',
            rootProperty: 'records'
        }
     } 
});
//requestHeaderId: 29500,
//procInstId: 6158,
//actInstDestId: 44,
//serial: "6158_44",
//folio: "ATCF-000030",
//requestCode: "ATCF_REQ",
//workflowName: "Additional Time Worked Claim Form",
//formPath: "atcf-request-form",
//activityName: "Requestor Rework",
//originator: "K2:NAGAWORLD\yimsamaune",
//requestor: "011541-YIM SAMAUNE ",
//openedBy: "K2:NAGAWORLD\YIMSAMAUNE",
//lastActionDate: "2017-11-16T11:26:19.93",
//status: "Allocated",
//priority: "Medium",
//actions: "Cancelled"

/*
 convert: function (value, record) {
    if (!Ext.isArray(value)) {
        return [];
    }
    else {
        return value;
    }
},
 */