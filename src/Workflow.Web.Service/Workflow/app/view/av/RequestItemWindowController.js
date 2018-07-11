/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.av.RequestItemWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.av-requestitemwindow',
    
    //onItemTypeChanged: function () {
    //    var me          = this,
    //        model       = me.getView().getViewModel(),
    //        references  = me.getView().getReferences(),
    //        itemType    = model.get('itemType'),
    //        itemStore   = references.item.getStore();
            
    //        if(itemType){
    //            itemStore.getProxy().extraParams = { itemTypeName: itemType.get('itemTypeName') };
    //            itemStore.load();    
    //        }
                          
    //},
    
    getFormItem : function (){
        var me = this,
            viewmodel = me.getView().getViewModel();

        var requestItem = Ext.create('Workflow.model.avForm.RequestItem', {
            itemId: viewmodel.get('itemId'),
            itemName: viewmodel.get('itemName'),
            quantity: viewmodel.get('quantity'),
            comment: viewmodel.get('comment'),
            itemTypeName: viewmodel.get('scopeType')
        });
        console.log('requestItem', requestItem);
        return requestItem;
    }
    
});
