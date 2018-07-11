/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.reservation.crr.FormViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.crr-form',
    
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear : 'clearData'
            }      
        }
    },
    loadData: function (data) {
        var me = this,
            view = me.getView(),
            model = me.getView().getViewModel();

        console.log('ForViewLog',view);

        me.view.getForm().reset(); // clean form before bind model.
        console.log('load data here', data);
        console.log('checkExpenseItem', data.checkExpenseItem);
        
        if (data.complimentary) {

            if(data.complimentary.arrivalDate)
                data.complimentary.arrivalDate = new Date(data.complimentary.arrivalDate);

            if (data.complimentary.departureDate)
                data.complimentary.departureDate = new Date(data.complimentary.departureDate);

            model.set('complimentary', data.complimentary);
            
        }

        if (data.checkExpense) {
            model.set('checkExpense', data.checkExpense);
            console.log('checkExpense', data.checkExpense);
        }

        model.set('viewSetting', data.viewSetting);
    },
    clearData : function(){
        this.getView().reset();
    }
});
