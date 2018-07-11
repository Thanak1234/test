
Ext.define('Workflow.view.it.ItRequestFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.it-itrequestform',
    
    actionUrl: Workflow.global.Config.baseUrl + 'api/itrequest',
    //Astract method implementation
    renderSubForm : function(data){
        var me              = this,
            view            = me.getView(),
            requestUserView = view.getReferences().requestUser,
            requestItemView = view.getReferences().requestItem,
            viewSetting     = view.getWorkflowFormConfig(); 
        
    
        
        requestUserView.fireEvent('loadData', { data: data && data.requestUsers ?data.requestUsers: null, viewSetting : viewSetting  });
        requestItemView.fireEvent('loadData' , {data:  data  && data.requestItems ?data.requestItems: null, viewSetting : viewSetting });    
        
    },
    
    //Astract method implementation
    getRequestItem :function(){
        var me = this, references = me.getView().getReferences() ;
        
        var data =  {
            delRequestUsers : me.getOriginDataFromCollection(references.requestUser.getStore().getRemovedRecords()),
            editRequestUsers: me.getOriginDataFromCollection(references.requestUser.getStore().getUpdatedRecords()),
            addRequestUsers : me.getOriginDataFromCollection(references.requestUser.getStore().getNewRecords()),
            delRequestItems : me.getOriginDataFromCollection(references.requestItem.getStore().getRemovedRecords()),
            editRequestItems : me.getOriginDataFromCollection(references.requestItem.getStore().getUpdatedRecords()),
            addRequestItems : me.getOriginDataFromCollection(references.requestItem.getStore().getNewRecords())
        };
        
        return data;
    },
    
    //Astract method implementation
    clearData : function(){
        this.getView().getReferences().requestUser.fireEvent('onDataClear');
        this.getView().getReferences().requestItem.fireEvent('onDataClear');
    },
    
    //Astract method implementation
    validateForm: function (){
        var me = this, references = me.getView().getReferences() ;
        var requestUsers = references.requestUser.getStore();
        
        if( requestUsers.getCount() ==0 ) {
            return "There is no request user item in this request. Please add at least one item before you click the button submit.";
        } 
      
        var requestItems = references.requestItem.getStore();
        if(requestItems.getCount() == 0) {
            return 'There is no request user item in this request. Please add at least one item before you click the button submit.'
        }
      
        return null;
    }
 });
