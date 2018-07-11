/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.activity.ActivityModel', {
    extend: 'Workflow.view.common.AbstractWindowModel',
    alias: 'viewmodel.ticket-activity-activity',
    data: {
        requiredComment : true
    },
    
    formulas:{
    	commentLabel: function (get) {

    		if(get('requiredComment')){
    			return 'Comment <span style="color:red;">*</span>';
    		}else{
    			return 'Comment';
    		}
    	},
        submitBtText : function(get) { 
            return "Submit";
        },
        btnSubmitIconCls: function (get) {
            return "fa fa-play-circle-o";

        }
    }

});
