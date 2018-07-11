/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.common.activity.activityHistoryWindow.ActivityHistoryWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.activityHistoryWindow',

    /**
     * Called when the view is created
     */
    init: function () {
        console.log(this.getView().getViewModel().get('item'));
    }
});