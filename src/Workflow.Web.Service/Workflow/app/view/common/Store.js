Ext.define('Workflow.view.common.Store', {

    config: {
        data: undefined, //extjs store
        reducerCls: undefined,
        subcribes: {}
    },

    constructor : function(config){
        this.initConfig(config);
        if(this.reducerCls){
            throw "No reducer found";
        }
        var re = Ext.create(config.reducerCls);
        this.reducer = re.reducer();
        return this;
    },
    dispatch : function(action){
        if(this.reducer) {
            var reData =  this.reducer(this.getData(), action);
            this.finSubscribeHandler(action)(reData);
            return reData;
        }
    },

    finSubscribeHandler: function(action){
        if(this.getSubcribes){
            var subcribe =this.getSubcribes()[action.type]; 
            if(subcribe){
                return subcribe;
            }
        }
    }



})