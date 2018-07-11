/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.activity.ActivityController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.ticket-activity-activity',


    sumbmitForm: function () {
        
        var me = this;

        var form = me.getView().getReferences().form.getForm();
        if (!form.isValid()) {
            return;
        }
        var data = me.getData();

        if(me.validate){
            if(!me.validate()){
                return;
            }
        }
        if (me.isRequiredComment() && (!data.comment || me.trim(data.comment) === '' )) {
            Ext.MessageBox.show({
                title: 'Warning',
                msg: 'Please leave some comment.',
                icon: Ext.MessageBox.WARNING,
                buttons : Ext.MessageBox.OK
            });

            return;
        }
        Ext.MessageBox.show({
            title: 'Confirmation',
            msg: 'Are sure to take the action?',
            buttons: Ext.MessageBox.YESNO,
            scope: this,
            icon: Ext.MessageBox.QUESTION,
            fn: function (bt) {
                if (bt == 'yes') {
                    me.takeAction(data);
                }
            }
        });
    },

    isRequiredComment : function(){
        var vm = this.getRef().vm;
        var requiredComment = vm.get('requiredComment');
        return requiredComment;
    },
    takeAction: function (data) {
        //User attachment list
        var me = this;
        
        var view = me.getView();

        view.hide();

        Ext.getBody().mask("Save...");

        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/ticket/action',
            headers: { 'Content-Type': 'application/json' },
            method: 'POST',
            jsonData: data,
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText) ;
                Ext.getBody().unmask();
                me.closeWindow();
                //Refresh main form
                if (view.cbFn) {
                    view.cbFn(data.message);
                }

                me.showToast('Your action is succesfully submitted.');

            },
            failure: function (opt, operation) {
                Ext.getBody().unmask();
                view.show();
                var data = Ext.JSON.decode(opt.responseText) || {message : opt.responseText};

                Ext.MessageBox.show({
                    title: 'Error',
                    msg: data.message,
                    icon: Ext.MessageBox.ERROR,
                    buttons: Ext.MessageBox.OK
                });
                Ext.getBody().unmask();
            }
        });
    },

    getData: function (actIdentifier) {
        var me = this;
        var vm = this.getView().getViewModel();
        var refs = me.getView().getReferences();
        var attachFiles = me.getOriginDataFromCollection(refs.attachmentList.getStore().getNewRecords());

        var data = {
            ticketId: vm.get('ticket').getId(),
            activityCode: vm.get('actIdentifier'),
            comment:( vm.get('description') || '').trim(),
            fileUploads: attachFiles
        };

        //Add more params if available from sub class
        if (me.getMoreData) {
            Ext.apply(data, me.getMoreData());
        }

        return data;
    },
    trim: function (o) {
        var n = o.replace(new RegExp('&nbsp;', 'g'), '');
        return Ext.String.trim(n);
    }
});
