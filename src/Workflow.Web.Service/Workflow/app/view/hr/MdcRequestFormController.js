Ext.define('Workflow.view.hr.MdcRequestFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.hr-mdcrequestform',
    
    formSubmission : function(){
        console.log('submitted');
    },
    formAcations : function(el, e, eOpts){
        console.log(el.text);
    } 
    
    
});
