/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.av.AvJobBriefFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.av-avjobbriefform',
    
    actionUrl : Workflow.global.Config.baseUrl + 'api/AvbRequest',
    
     renderSubForm : function(data){
        var me              = this,
            view            = me.getView(),
            jobHistoryView = view.getReferences().avJobDetailView,
            requestItemView = view.getReferences().avRequestItemView,
            avRequestItemVisualView = view.getReferences().avRequestItemVisualView,
            avRequestItemLightsView = view.getReferences().avRequestItemLightsView,
            viewSetting     = view.getWorkflowFormConfig(); 

        
        if (data && data.jobDetail) {
            view.getViewModel().set('jobDetail.other', data.jobDetail.other);
        }
        
         jobHistoryView.fireEvent('loadData', { data: data && data.jobDetail ?data.jobDetail: null, viewSetting : viewSetting  });
         requestItemView.fireEvent('loadData', { data: data && data.items ? data.items : null, viewSetting: viewSetting });
         avRequestItemVisualView.fireEvent('loadData', { data: data && data.items ? data.items : null, viewSetting: viewSetting });
         avRequestItemLightsView.fireEvent('loadData', { data: data && data.items ? data.items : null, viewSetting: viewSetting });
     },
      //Astract method implementation
     clearData: function () {
         var view = this.getView();
         view.getReferences().avJobDetailView.fireEvent('onDataClear');
         view.getReferences().avRequestItemView.fireEvent('onDataClear');
         view.getReferences().avRequestItemVisualView.fireEvent('onDataClear');
         view.getReferences().avRequestItemLightsView.fireEvent('onDataClear');
         view.getReferences().avJobDetailOther.value = null;
    },
     
     validateForm: function (){
          var me              = this,
            view            = me.getView(),
            jobHistoryView  = view.getReferences().avJobDetailView,
            jobHistoryModel = jobHistoryView.getViewModel(),
            requestItemView = view.getReferences().avRequestItemView,
            avRequestItemVisualView = view.getReferences().avRequestItemVisualView,
            avRequestItemLightsView = view.getReferences().avRequestItemLightsView;

          var setupDate = jobHistoryModel.get('jobDetail.setupDate'),
              actualDate = jobHistoryModel.get('jobDetail.actualDate'),
              setupTime = jobHistoryModel.get('jobDetail.setupTime'),
              actualTime = jobHistoryModel.get('jobDetail.actualTime');


          setupDate.setHours(setupTime.getHours())
          setupDate.setMinutes(setupTime.getMinutes());
          actualDate.setHours(actualTime.getHours())
          actualDate.setMinutes(actualTime.getMinutes());


          if (actualDate < setupDate) {
              return "ActualDate time must be after setup date time .";
          }
       
       if(!jobHistoryView.form.isValid()){
           return "Some field(s) of job detail is required. Please input the required field(s) before you click the Submit button.";
       } 
             
       if( requestItemView.getStore().getCount() == 0 || 
           avRequestItemVisualView.getStore().getCount() == 0 ||
           avRequestItemLightsView.getStore().getCount() == 0) {
           return "Scope Needed of Sound, Visual and Lights are required in this request. Please add at least one item before you click the Submit button.";
        }       
     },
     
     getRequestItem : function(){
          var me                = this,
            view               = me.getView(),
            jobHistoryModel     = view.getReferences().avJobDetailView.getViewModel(),
            requestItemStore = view.getReferences().avRequestItemView.getStore(),
            requestItemVisualStore = view.getReferences().avRequestItemVisualView.getStore(),
            requestItemLightsStore = view.getReferences().avRequestItemLightsView.getStore();
            
         
            var otherField = view.getReferences().avJobDetailOther;

            var setupDate   = jobHistoryModel.get('jobDetail.setupDate'),
                actualDate  = jobHistoryModel.get('jobDetail.actualDate'),
                setupTime   = jobHistoryModel.get('jobDetail.setupTime'),
                actualTime  = jobHistoryModel.get('jobDetail.actualTime');
                
                
                setupDate.setHours(setupTime.getHours())
                setupDate.setMinutes(setupTime.getMinutes());
                actualDate.setHours(actualTime.getHours())
                actualDate.setMinutes(actualTime.getMinutes());
                   
         var data = {
             jobDetail  : { 
                    id             : jobHistoryModel.get('jobDetail.id'),
                    projectName    : jobHistoryModel.get('jobDetail.projectName'),
                    location       : jobHistoryModel.get('jobDetail.location'),
                    setupDate      : setupDate,
                    actualDate      : actualDate,
                    projectBrief    : jobHistoryModel.get('jobDetail.projectBrief'),
                    other       : otherField.value
                },
             addItems: me.buildItemCollection(
                 me.getOriginDataFromCollection(requestItemStore.getNewRecords()),
                 me.getOriginDataFromCollection(requestItemVisualStore.getNewRecords()),
                 me.getOriginDataFromCollection(requestItemLightsStore.getNewRecords())
             ),
             editItems: me.buildItemCollection(
                 me.getOriginDataFromCollection(requestItemStore.getUpdatedRecords()),
                 me.getOriginDataFromCollection(requestItemVisualStore.getUpdatedRecords()),
                 me.getOriginDataFromCollection(requestItemLightsStore.getUpdatedRecords())
             ),
             delItems: me.buildItemCollection(
                 me.getOriginDataFromCollection(requestItemStore.getRemovedRecords()),
                 me.getOriginDataFromCollection(requestItemVisualStore.getRemovedRecords()),
                 me.getOriginDataFromCollection(requestItemLightsStore.getRemovedRecords())
             )
         };
      
   
         return data;
     },
    /*  Combine three difference items sector together */
     buildItemCollection: function (soundCollection, visualCollection, lightsCollection) {
         var items = new Array();

         soundCollection.forEach(function (element) {
             items.push(element);
         });

         visualCollection.forEach(function (element) {
             items.push(element);
         });

         lightsCollection.forEach(function (element) {
             items.push(element);
         });

         return items;
     }
});
