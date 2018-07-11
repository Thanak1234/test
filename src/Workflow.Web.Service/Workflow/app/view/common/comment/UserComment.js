/**
 *Author : Phanny 
 *
 */
Ext.define("Workflow.view.common.comment.UserComment",{
   extend: "Ext.form.Panel",
    xtype: 'user-comment',

    requires: [
        "Workflow.view.common.comment.UserCommentController",
        "Workflow.view.common.comment.UserCommentModel"
    ],
    defaults: {
        anchor: '100%',
        padding: '20 10 10 10'
    },
    controller: "common-comment-usercomment",
    viewModel: {
        type: "common-comment-usercomment"
    },
    iconCls: 'fa fa-comments-o',
    title: 'Comment',
    items: [
        {
            //fieldLabel  : 'Comment',
            bind   : {
                value : '{userComment}'  
            },
            emptyText: 'Comment',
            xtype       : 'textarea'    
        }
    ]
});
