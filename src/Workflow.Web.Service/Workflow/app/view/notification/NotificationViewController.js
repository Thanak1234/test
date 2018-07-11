Ext.define('Workflow.view.notification.NotificationViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.notification-notificationview',

    init : function(){

    },

    onShow: function() {
	   
	    this.loadData();
	    var self = this.getView();
	    Ext.getDoc().on('mouseup',function(e) {
	        if (self.isVisible() && !self.owns(e.getTarget())) {
	            self.hide();
	        }
	    }, self);
	},

	loadData : function(){

		var refs = this.getRef().refs;
		var notificationList = refs.notificationList;
		notificationList.getStore().load();
	},

	onViewTicket : function(grid, rowIndex, colIndex) {
		var me =this;
        var rec = grid.getStore().getAt(rowIndex);
        var ticketId = rec.get('metaData').ticketId;
        window.location.href = "#ticket/" + ticketId;

        //Marked as read

        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/notification/read/' + rec.getId(),
            method: 'GET',
            success: function (response) {
                //var data = Ext.JSON.decode(response.responseText);
                rec.set('STATUS', 'READ');
                var fn = me.getView().cbFn;
                if(fn){
                	fn();
                }

                grid.refresh();
            },
            failure: function (response) {
                console.log('error');
            }
        });


    }
    
});
