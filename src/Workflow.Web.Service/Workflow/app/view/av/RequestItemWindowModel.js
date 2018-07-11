/**
 *Author : Phanny 
 *
 */

Ext.define('Workflow.view.av.RequestItemWindowModel', {
    extend: 'Workflow.view.common.AbstractWindowModel',
    alias: 'viewmodel.av-requestitemwindow',
    data: {
        itemId        : null,
        comment: null,
        quantity: null,
        scopeType       : null
    },
    
    stores : {
        itemStore : {
            model: 'Workflow.model.avForm.AcbItem',
            pageSize: 20,
            autoLoad: false,
        
            proxy: {
                type: 'rest',        
                url: Workflow.global.Config.baseUrl + 'api/avitem/items-by-name',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        itemTypeStore: {
            model: 'Workflow.model.avForm.AcbItemType',
            pageSize: 20,
            autoLoad: false,
        
            proxy: {
                type: 'rest',        
                url: Workflow.global.Config.baseUrl + 'api/avitem/itemtypes',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
        
    },
    formulas:{
         readOnlyField : function(get){
            return get('action') === 'VIEW';
         }    
    }

});
