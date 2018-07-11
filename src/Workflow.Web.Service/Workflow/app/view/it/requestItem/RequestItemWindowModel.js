Ext.define('Workflow.view.it.requestItem.RequestItemWindowModel', {
    extend: 'Workflow.view.common.AbstractWindowModel',
    alias: 'viewmodel.it-requestitem-requestitemwindow',
    data: {
        sessionId   : 0,
        session     : null,
        reqItem     : null,
        itemType    : null,
        itemRole    : null,
        qty         : 1,
        comment     : ''
    },
    
    stores : {
        itemStore: {
            model: 'Workflow.model.itRequestForm.Item',
            pageSize: 20,
            autoLoad: false,
            isEmptyStore: true,
            proxy: {
                type: 'rest',        
                url: Workflow.global.Config.baseUrl + 'api/itrequestitem/items',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        itemTypeStore: {
            model: 'Workflow.model.itRequestForm.ItemType',
            isEmptyStore: true,
            pageSize: 20,
        
            autoLoad: false,
        
            proxy: {
                type: 'rest',        
                url: Workflow.global.Config.baseUrl + 'api/itrequestitem/itemtypes',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        itemRoleStore : {
            model: 'Workflow.model.itRequestForm.ItemRole',
            pageSize: 20,
            autoLoad: false,
            isEmptyStore: true,
            proxy: {
                type: 'rest',        
                url: Workflow.global.Config.baseUrl + 'api/itrequestitem/itemroles',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }   
        }
    },
    formulas: {
        readOnlyField : function(get){
            return  get('action')==='VIEW';
        }
    }

});
