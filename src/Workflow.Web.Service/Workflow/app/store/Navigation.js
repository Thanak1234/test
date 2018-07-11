/**
 * 
 *Author : Phanny 
 */

Ext.define('Workflow.store.Navigation', {
    extend: 'Ext.data.TreeStore',
    alias: 'store.navigation',
    
    autoLoad        : false, 
    model           : 'Workflow.model.Navigation',
    storeId         : 'NavigationTree',
    //defaultRootId   : 'menu',
     proxy  : {
        type    : 'ajax',
        url     : '/api/lookup/menu'
    }
    
    /*
    root: {
        expanded: true,
        children: [
            {
                text:   'Dashboard',
                view:   'Workflow.view.dashboard.Dashboard',
                leaf:   true,
                iconCls: 'right-icon new-icon fa fa-desktop',
                routeId: 'dashboard',
                closableTab: false
            },
            {
                text: 'IT',
                expanded: false,
                selectable: false,
                iconCls: 'fa fa-leanpub',
                routeId : 'it-parent',
                id:       'it-parent',
                children: [
                    {
                        text:   'IT Request',
                        view:   'Workflow.view.it.ItRequestForm',
                        iconCls: 'right-icon hot-icon fa fa-send',
                        leaf:   true,
                        routeId: 'it-request-form'
        
                    }
                ]
            },{
               text: 'Finance',
                expanded: false,
                selectable: false,
                iconCls: 'fa fa-leanpub',
                routeId : 'bcj-parent',
                id:       'bcj-parent',
                children: [
                    {
                        text:   'Business Cass Justification',
                        view:   'Workflow.view.bcj.BcjRequestForm',
                        iconCls: 'right-icon hot-icon fa fa-send ',
                        leaf:   true,
                        routeId: 'bcj-request-form'
        
                    }
                ] 
            },{
               text: 'HR',
                expanded: false,
                selectable: false,
                iconCls: 'fa fa-leanpub',
                routeId : 'mdc-parent',
                id:       'mdc-parent',
                children: [
                    {
                        text:   'Medical Treatment Form',
                        view:   'Workflow.view.hr.MdcRequestForm',
                        iconCls: 'right-icon hot-icon fa fa-send ',
                        leaf:   true,
                        routeId: 'mdc-request-form'
        
                    }
                ] 
            }
            
         
        ]
    }*/
});
