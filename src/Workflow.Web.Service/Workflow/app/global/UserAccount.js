Ext.define('Workflow.global.UserAccount', {
    statics: {
        identity: null,
        initIdentity: function () {
            var me = this;
            if (me.identity == null || me.identity == undefined || me.identity == '') {
                Ext.Ajax.request({
                    url: Workflow.global.Config.baseUrl + 'api/employee/currentuser',
                    method: 'GET',
                    success: function (response) {
                        me.identity = Ext.JSON.decode(response.responseText);
                    },
                    failure: function (response) {
                        console.log('error');
                    }
                });
            }
        }
    }
});