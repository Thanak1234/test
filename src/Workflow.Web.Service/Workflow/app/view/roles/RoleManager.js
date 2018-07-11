Ext.define('Workflow.view.roles.RoleManager', {
    extend: 'Ext.panel.Panel',
    xtype: 'roles-rolemanager',
    
    controller: 'roles-rolemanager',
    viewModel: {
        type: 'roles-rolemanager'
    },
    // title: '<span style="font-size: 16px;">User Role Management</span>',
    layout: {
        type: 'vbox',
        pack: 'start',
        align: 'center'
    },
    // margin: 0,
    // defaults: {
    //     bodyStyle: 'padding:0px'
    // },
     ngconfig: {
         layout: 'fullScreen'
     },
    titleAlign: 'center', 
    width: 1100,
    width: '100%',
    flex: 1,
    bodyPadding: 0,
    frame: true,
    initComponent: function () {
        var me = this;
      
        me.items = [
        {
            xtype: 'panel',
            layout: 'border',
            width: '100%',            
            flex: 1,
            items: [
                {
                    region: 'west',
                    xtype: 'roles-navigation',
                    ui: 'navigation',
                    reference: 'navigation'
                },{
                     region: 'center',
                     xtype: 'roles-userrole',
                     reference: 'users',
                     border: false,
                     disabled: true
                }
            ]
        }];

        me.callParent(arguments);
    }
});