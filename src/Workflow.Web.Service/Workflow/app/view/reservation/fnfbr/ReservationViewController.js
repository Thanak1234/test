/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.reservation.fnfbr.ReservationViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.fnfbr-reservation',
    
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear : 'clearData'
            }      
        }
    },
    calculateRoomTaken: function (ctr, value) {
        var me = this,
            model = me.getView().getViewModel(),
            mainView = me.getView().mainView,
            mainViewModel = mainView.getViewModel(),
            reservation = model.get('reservation');
        if (model.get('editable')) {
            if (ctr.name == 'checkInDate') {
                reservation.checkInDate = value;
            } else if (ctr.name == 'checkOutDate') {
                reservation.checkOutDate = value;
            } else if (ctr.name == 'numberOfRoom') {
                reservation.numberOfRoom = value;
            }

            if (reservation.checkInDate && reservation.checkOutDate && reservation.numberOfRoom > 0) {
                Ext.Ajax.request({
                    url: Workflow.global.Config.baseUrl + 'api/fnfrequest/TotalRoomNightTaken',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: {
                        checkinDate: new Date(reservation.checkInDate).toISOString(),
                        checkoutDate: new Date(reservation.checkOutDate).toISOString(),
                        numberOfRoom: reservation.numberOfRoom,
                        requestorId: mainViewModel.get('requestorId')
                    },
                    success: function (conn, response, options, eOpts) {
                        var result = Ext.JSON.decode(conn.responseText);
                        me.setTotalRoomTaken(model, result.value);
                    }
                });
            }
        } else {
            Ext.Ajax.request({
                url: Workflow.global.Config.baseUrl + 'api/fnfrequest/TotalRoomNightTakenByHeader',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: {
                    requestHeaderId: mainViewModel.get('requestHeaderId')
                },
                success: function (conn, response, options, eOpts) {
                    var result = Ext.JSON.decode(conn.responseText);
                    me.setTotalRoomTaken(model, result.value);
                }
            });
        }
    },
    setTotalRoomTaken: function(model, value){
        model.set('reservation.totalRoomCount', (value < 0)?((-1)* value):value);
        model.set('reservation.totalRoomTaken', value);
    },
    loadData: function (data) {
        var me = this,
            view = me.getView(),
            model = me.getView().getViewModel();

        me.view.getForm().reset(); // clean form before bind model.

        if (data.reservation) {

            data.reservation.checkInDate = data.reservation.checkInDate ? new Date(data.reservation.checkInDate) : null;
            data.reservation.checkOutDate = data.reservation.checkOutDate ? new Date(data.reservation.checkOutDate) : null;
            data.reservation.receiveDate = data.reservation.receiveDate ? new Date(data.reservation.receiveDate) : null;
            data.reservation.extraBed = { value: data.reservation.extraBed };
            model.set('reservation', data.reservation);
        }
        model.set('viewSetting', data.viewSetting);
    },
    clearData : function(){
        this.getView().reset();
    }
    
});
