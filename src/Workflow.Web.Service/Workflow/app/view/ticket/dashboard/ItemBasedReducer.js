/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.dashboard.ItemBaseReducer', {

    constructor : function(config){
        return this;
    },

    reducer : function(){
        return this.reducerBuilder;
    },
    
    reducerBuilder : function(data, action) {

        var me = this;

        var dataFilter=function (data, filters){
            
            if(!filters || filters.length ==0 ){
                return data;
            }

            return data.filter(function(item){
                filters.find(function(item){ 
                    return item === item.keyPath.substring(0, item.length); 
                });
            });
        };
       
        var filters = action.filters;
        switch(action.type) {   //Action={type: 'CATE_FILTER', filters:[...]}
            case 'ITEM_FILTER':
                return dataFilter(filters);
            default:
                return data;
        }
    }
});