Ext.define('Workflow.view.ticket.setting.sla.TicketSlaFormController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-sla-ticketslaform',

    init: function () {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var refs = me.getView().getReferences();
        me.generateDayResolution();
        me.generateHourResolution();
        me.generateMinuteResolution();
        me.generateDayResponse();
        me.generateHourResponse();
        me.generateMinuteResponse();
    },
    renderStringZeroPad: function(num, pad){
        var str = ''+num, pad = ''+pad;
        return pad.substr(0, pad.length - str.length) + str
    },
    generateDayResolution: function(){
        var me = this,
        vm = me.getView().getViewModel(),
        dayStore = vm.getStore('dayStoreResolution')
        ;
        for(var i = vm.get('minNumDayResolution'); i<=vm.get('maxNumDayResolution'); i++){
            var m = Ext.create('Ext.data.Model', {id: i, num: me.renderStringZeroPad(i, '00')});
            dayStore.add(m);
        }
    },
    generateHourResolution: function(){
        var me = this, vm = me.getView().getViewModel()
        , hourStore = vm.getStore('hourStoreResolution')
        ;
        for(var i = vm.get('minNumHourResolution'); i<=vm.get('maxNumHourResolution'); i++){
            var m = Ext.create('Ext.data.Model', {id: i, num: me.renderStringZeroPad(i, '00')});
            hourStore.add(m);
        }

    },
    generateMinuteResolution: function(){
        var me = this, vm = me.getView().getViewModel()
        , minuteStore = vm.getStore('minuteStoreResolution')
        ;
        for(var i = vm.get('minNumMinuteResolution'); i<=vm.get('maxNumMinuteResolution'); i++){
            var m = Ext.create('Ext.data.Model', {id: i, num: me.renderStringZeroPad(i, '00')});
            minuteStore.add(m);
        }

    },
    generateDayResponse: function(){
        var me = this,
        vm = me.getView().getViewModel(),
        dayStore = vm.getStore('dayStoreResponse')
        ;
        for(var i = vm.get('minNumDayResponse'); i<=vm.get('maxNumDayResponse'); i++){
            var m = Ext.create('Ext.data.Model', {id: i, num: me.renderStringZeroPad(i, '00')});
            dayStore.add(m);
        }
    },
    generateHourResponse: function(){
        var me = this, vm = me.getView().getViewModel()
        , hourStore = vm.getStore('hourStoreResponse')
        ;
        for(var i = vm.get('minNumHourResponse'); i<=vm.get('maxNumHourResponse'); i++){
            var m = Ext.create('Ext.data.Model', {id: i, num: me.renderStringZeroPad(i, '00')});
            hourStore.add(m);
        }

    },
    generateMinuteResponse: function(){
        var me = this, vm = me.getView().getViewModel()
        , minuteStore = vm.getStore('minuteStoreResponse')
        ;
        for(var i = vm.get('minNumMinuteResponse'); i<=vm.get('maxNumMinuteResponse'); i++){
            var m = Ext.create('Ext.data.Model', {id: i, num: me.renderStringZeroPad(i, '00')});
            minuteStore.add(m);
        }

    },
    onWindowClosedHandler: function () {
        var me = this,
            w = me.getView().up();
        w.close();

    },
    getItemId: function (vm, prop) {
        if (!vm.get(prop)) {
            return null;
        } else {
            return vm.get(prop);
        }
    },
    calculateSec: function(obj){
        return (obj.day*24*3600)+(obj.hour*3600)+(obj.minute*60);
    },
    onFormSubmit: function () {
        var me = this;
        var ref = me.getRef();
        var form = ref.refs.formRef;

        if(form.isValid()){
            var id = me.getItemId(ref.vm, 'form.id');
            var objResolution = {
                                    day: me.getItemId(ref.vm, 'form.numDayResolution').getId(),
                                    hour: me.getItemId(ref.vm, 'form.numHourResolution').getId(),
                                    minute: me.getItemId(ref.vm, 'form.numMinuteResolution').getId()
                                };
            var objResponse = {
                                    day: me.getItemId(ref.vm, 'form.numDayResponse').getId(),
                                    hour: me.getItemId(ref.vm, 'form.numHourResponse').getId(),
                                    minute: me.getItemId(ref.vm, 'form.numMinuteResponse').getId()
                                };
            var data = {
                'id':  id,
                'slaName': me.getItemId(ref.vm, 'form.slaName'),
                'description': me.getItemId(ref.vm, 'form.description'),
                'gracePeriod': me.calculateSec(objResolution),
                'firstResponsePeriod': me.calculateSec(objResponse),
                'status':'ACTIVE'
            };
            var view = me.getView();
            Ext.MessageBox.show({
                title: 'Confirmation',
                msg: "Are sure to Save SLA?",
                buttons: Ext.MessageBox.YESNO,
                scope: this,
                icon: Ext.MessageBox.QUESTION,
                fn: function (bt) {
                    if (bt == 'yes') {
                        me.doSave(data, function (record) {
                            if (view.cbFn) {
                                view.cbFn();
                            }
                            me.onWindowClosedHandler();
                        });
                    }
                }
            });
        }
    },
    doSave: function (data, cb) {
        var me = this;
        var view = me.getView();

        var model = Ext.create('Workflow.model.ticket.TicketSla', data);
        model.save({
            params: data,
            failure: function (record, operation) {
                // me.showToast(Ext.String.format('Failed to save SLA: {0}...', model.data.slaName));
                // cb(record);
                model.getProxy().on('exception', function (proxy, response, operation) {
                    var errors = Ext.JSON.decode(response.responseText).msg;
                    Ext.MessageBox.alert('Validation', errors);
                }, this);
            },
            success: function (record, operation) {
                me.showToast(Ext.String.format('Save successfuly SLA: {0}...', model.data.slaName));
                cb(record);
            }

        });
    }
});
