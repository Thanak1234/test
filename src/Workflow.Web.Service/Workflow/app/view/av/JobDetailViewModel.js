/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.av.JobDetailViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.av-jobdetailview',
    data: {
        jobDetail   : {
                    id          : 0,
                    projectName : null,  
                    location    : null,
                    setupDate   : new Date(),
                    actualDate  : new Date(),
                    setupTime   : new Date(),
                    actualTime: new Date(),
                    projectBrief: null,
                    other: null
                },
        viewSetting : null
    },
    
    formulas:{
         editable : function(get) {
            if(get('viewSetting')  &&  get('viewSetting').requestItemBlock.addEdit ){
                return true;
            }else{
                return false;
            }
            
        }
    }
    

});
