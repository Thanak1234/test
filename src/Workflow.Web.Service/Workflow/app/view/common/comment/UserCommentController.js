/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.common.comment.UserCommentController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-comment-usercomment',
    
    control: {
        '*': {
            loadData: 'loadData',
            onDataClear : 'clearData'
        }      
    },
    loadData :  function(data){
        this.getView().getViewModel().set('userComment', data );
    },
    clearData : function (){
        this.getView().getViewModel().set('userComment', null );
    }
    
});
