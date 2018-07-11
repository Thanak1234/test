Ext.define('Workflow.view.ticket.setting.team.TicketTeamFormController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-team-ticketteamform',

    init: function () {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var refs = me.getView().getReferences();
        var status = model.getStore('statusStore');
        status.proxy.extraParams = {lookupKey:'TEAM_STATUS'};
        status.load();

        var teamId = model.get('form.id') | 0;
        var ticketSettingTeamAgentsStore = model.getStore('ticketSettingTeamAgentsStore');
        ticketSettingTeamAgentsStore.proxy.extraParams = {teamId: teamId};
        ticketSettingTeamAgentsStore.load();

        //hide buttons
        me.toggleButton('isAgentSelected', false);
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
    getAgentIdList: function(){
        var me = this, model = me.getViewModel(), arrId=[],
        ticketSettingTeamAgentsStore = model.getStore('ticketSettingTeamAgentsStore');
        var agentList = [];
        ticketSettingTeamAgentsStore.data.items.forEach(function(item){
            agentList.push({id:item.id, immediateAssign : item.data.immediateAssign});
        });
        return agentList;
    },
    isValidAgentList : function(cb){
        var me = this, model = me.getViewModel(), isValid = false,
        ticketSettingTeamAgentsStore = model.getStore('ticketSettingTeamAgentsStore');

        if(ticketSettingTeamAgentsStore.getCount() > 0){
            cb(ticketSettingTeamAgentsStore.findExact('immediateAssign', true));
        }else{
            cb(1);
        }

    },
    onFormSubmit: function () {
        var me = this;

        var ref = me.getRef();
        var model = me.getViewModel();
        var form = ref.refs.formRef;

        if(form.isValid()){

            me.isValidAgentList(function(isValid){
                // if(isValid < 0){
                //     Ext.Msg.alert('Validation', 'There is no Agent has Immediate Assign');
                //     return false;
                // }

                var id = me.getItemId(ref.vm, 'form.id');
                var status = me.getItemId(ref.vm, 'form.status');
                var agentIdList = me.getAgentIdList();

                var data = {
                    'id':  id,
                    'teamName': me.getItemId(ref.vm, 'form.teamName'),
                    'alertAllMembers': me.getItemId(ref.vm, 'form.alertAllMembers'),
                    'alertAssignedAgent': me.getItemId(ref.vm, 'form.alertAssignedAgent'),
                    'directoryListing': me.getItemId(ref.vm, 'form.directoryListing'),
                    'description': me.getItemId(ref.vm, 'form.description'),
                    'status': status.getData().display1,
                    'registeredAgents' : agentIdList
                };
                var view = me.getView();

                Ext.MessageBox.show({
                    title: 'Confirmation',
                    msg: "Are sure to Save team?",
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
            });
        }

    },
    doSave: function (data, cb) {
        var me = this;
        var view = me.getView();

        var model = Ext.create('Workflow.model.ticket.TicketTeam', data);
        if(model.id > 0){
            me.getView().mask("Data processing...");
            Ext.Ajax.request({
                url: 'api/ticket/setting/team/update',
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                jsonData: data,
                success: function (response, operation) {
                    me.getView().unmask();
                    me.showToast("Team was saved successfully.");
                    var record = Ext.decode(response.responseText);
                    cb(record);
                    me.getView().close();
                },
                failure: function (data, operation) {
                    me.getView().unmask();
                    me.showToast("Failed to save team.");
                    var response = Ext.decode(data.responseText);
                    Ext.MessageBox.show({
                        title: 'Error',
                        msg: response.msg,
                        buttons: Ext.MessageBox.OK,
                        icon: Ext.MessageBox.ERROR
                    });
                }
            });
        }else{
            model.save({
                params: data,
                failure: function (record, operation) {
                    // me.showToast(Ext.String.format('Failed to save team: {0}...', model.data.teamName));
                    // cb(record);
                    model.getProxy().on('exception', function (proxy, response, operation) {
                        var errors = Ext.JSON.decode(response.responseText).msg;
                        Ext.MessageBox.alert('Validation', errors);
                    }, this);
                },
                success: function (record, operation) {
                    me.showToast(Ext.String.format('Team was saved successfully: {0}...', model.data.teamName));
                    cb(record);
                }

            });
         }
    },
    onRegisterAgentHandler : function(){
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var store = model.getStore('ticketSettingTeamAgentsStore');

        Ext.create('Workflow.view.ticket.setting.team.RegisteredAgentWindow',{
         form : null,
         cbFn: function(formData, form){
            var immediateAssigned = false;
            var existed = false;

            store.each(function(record){
                if(record.data.immediateAssign){
                    immediateAssigned = true;

                }
                if(formData.id == record.id){
                   existed = true;

                }
            });

            if(existed){
                Ext.MessageBox.show({
                    title: 'Validation',
                    msg: formData.fullName+' was existing in Agent List...',
                    buttons: Ext.MessageBox.OK,
                    icon: Ext.MessageBox.ERROR
                });
                return false;
            }

            if(immediateAssigned && formData.immediateAssign){
                Ext.MessageBox.show({
                    title: 'Validation',
                    msg: 'There is an agent already has Immediate Assign in Agent List...',
                    buttons: Ext.MessageBox.OK,
                    icon: Ext.MessageBox.ERROR
                });
                return false;
            }
            store.add(formData);
            form.reset();
         }
        }).show();
    },
    onDeleteHandler : function(grid, rowIndex, columnIndex, opt){
        var me = this, model = me.getView().getViewModel();
        Ext.Msg.confirm ('Confirmation', 'Are you sure to remove?' , function(btn, text){
             if (btn == 'yes') {
                var store = model.getStore('ticketSettingTeamAgentsStore');
                var record = store.getAt(rowIndex);
                store.removeAt(rowIndex);

                //flag btnasign
                var selectedRow = model.get('selectedAgentRow');
                if(selectedRow && selectedRow.id == record.id){
                    me.toggleButton('isAgentSelected', false);
                }
             }
        });

    },
    clearForm: function () {
        this.getView().getReferences().requestor.clearData();

        var uploadView = this.getView().getReferences().fileUpload;
        uploadView.fireEvent('onDataClear');

        if (this.clearData) {
            this.clearData();
        }
    },
    closeWindow: function () {
        var me = this,
        w = me.getView().up();
        w.close();
    },
    onRowSelectedHanler: function(elm , record , index , eOpts){
        var me = this, vm = me.getView().getViewModel();
        me.toggleButton('isAgentSelected', true);
        vm.set('selectedAgentRow', record);
        me.onSetBtnAssignTitle(record);
    },
    onSetBtnAssignTitle: function(selectedRow){
        var me = this, vm = me.getView().getViewModel();
        vm.set('btnAssignImmediateName', selectedRow.data.immediateAssign ? 'Remove Assign Immediate': 'Assign Immediate');
    },
    toggleButton: function(name, show){
        var me = this, vm = me.getView().getViewModel();
        vm.set(name, show);
    },
    onAssignImmediateHandler: function(){
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var store = model.getStore('ticketSettingTeamAgentsStore');
        var selectedRow = model.get('selectedAgentRow');
        var existedAssign = me.isExistedAssign();


        if(!selectedRow.data.immediateAssign && existedAssign){
            Ext.Msg.confirm('Confirmation', 'Another Agent has been set to Immediate Assigned, do you want to continue?', function(btn, text){
                if (btn == 'yes') {
                    var records = [];
                    store.each(function(record){
                        record.data.immediateAssign = false;
                        if(record.id == selectedRow.id){
                            record.data.immediateAssign = true;
                        }
                        records.push(record);
                    });
                    store.setRecords(records);
                    me.onSetBtnAssignTitle(selectedRow);
                    me.showToast(selectedRow.data.fullName+" has been immediate assigned successfully...");
                }

            });
        }else{
            if(selectedRow.data.immediateAssign){
                Ext.Msg.confirm('Confirmation', 'Are you sure to remove immediate assign of '+selectedRow.data.fullName+'?', function(btn, text){
                    if (btn == 'yes') {
                        var records = [];
                        store.each(function(record){
                            if(record.id == selectedRow.id){
                                record.data.immediateAssign = false;
                            }
                            records.push(record);
                        });
                        store.setRecords(records);
                        me.onSetBtnAssignTitle(selectedRow);
                        me.showToast(selectedRow.data.fullName+" has been removed immediate assign successfully...");
                    }

                });
            }else{
                Ext.Msg.confirm('Confirmation', 'Are you sure to immediate assign to '+selectedRow.data.fullName+'?', function(btn, text){
                    if (btn == 'yes') {
                        var records = [];
                        store.each(function(record){
                            if(record.id == selectedRow.id){
                                record.data.immediateAssign = true;
                            }
                            records.push(record);
                        });
                        store.setRecords(records);
                        me.onSetBtnAssignTitle(selectedRow);
                        me.showToast(selectedRow.data.fullName+" has been immediate assigned successfully...");
                    }

                });
            }
        }


    },
    isExistedAssign: function(){
        var existedAssign = false;
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var store = model.getStore('ticketSettingTeamAgentsStore');
        store.each(function(record){
            if(record.data.immediateAssign){
               existedAssign = true;
               return false;
            }
        });
        return existedAssign;
    }
});
