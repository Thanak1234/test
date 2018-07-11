/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.TicketFormController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-ticketform',
    init: function () {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var refs = me.getView().getReferences();
        var ticket = model.get('ticket');
        
        if (ticket != null && ticket.id > 0) {
            model.set('isTicketTicketTypeChangeInit', true);
            model.set('isTicketTicketPriorityChangeInit', true);
        }

        me.initFormData();
        me.initForm();        
        
    },


    //*********************Evend handlers*************************///

    onTeamChanged: function () {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var team = model.get('form.team');
        var ticketAgentSore = model.getStore('ticketAgentStore');
        var teamId = team.getId();

        ticketAgentSore.getProxy().extraParams = { teamId: teamId };
        ticketAgentSore.load();

    },


    onCateChanged: function () {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var cate = model.get('form.category');
        var ticketSubCateSore = model.getStore('ticketSubCateStore');

        ticketSubCateSore.getProxy().extraParams = { cateId: cate ? cate.getId() : -1 };
        ticketSubCateSore.load(function (records) {

        });

        me.onSubCateChanged();

    },

    onSubCateChanged: function (el) {

        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var subCate = model.get('form.subCate');
        var ticketItemStore = model.getStore('ticketItemStore');
        ticketItemStore.getProxy().extraParams = { subCateId: subCate ? subCate.getId() : -1 };
        ticketItemStore.load(function (records) {

        });

    },
    onFileAddedHander: function (field, value) {
        var me = this;
        var vm = me.getRef().vm;
        var uploadedFileStore;
        if (field.itemId === 'userUploadedFile') {
            uploadedFileStore = vm.getStore('userUploadFilesStore');
        } else if (field.itemId === 'agentUploadedFile') {
            uploadedFileStore = vm.getStore('agentUploadedFilesStore');
        } else {
            console.error = 'Upload file does not belong to both user and agent block';
            return;
        }

        var window = Ext.create('Workflow.view.common.fileUpload.SimpleFileUploadDialog', {
            mainView: me,
            lauchFrom: me.getReferences().addBt,
            cbFn: function (rec) {
                uploadedFileStore.add(Ext.create('Workflow.model.common.FileUpload', { fileName: rec.fileName, serial: rec.serial, ext: rec.ext }));
            }
        });

        window.show(field);

    },
    oneRemoveFileHandler: function (grid, rowIndex, colIndex) {
        var me = this, store = grid.getStore(), rec = store.getAt(rowIndex);
        Ext.MessageBox.show({
            title: 'Alert',
            msg: 'Are you sure to delete this file?',
            buttons: Ext.MessageBox.YESNO,
            scope: this,
            fn: function (bt) {
                if (bt === 'yes') {
                    store.remove(rec);
                    me.showToast(Ext.String.format('File name {0} has been removed', rec.get('fileName')));
                }
            }
        });
    },

    onFormSubmit: function () {
        this.saveTicket();
    },

    onGetPriorityHandler: function () {
        var ref = this.getRef();
        var vm = ref.vm;

        var impactId = vm.get('form.impact') ? vm.get('form.impact').getId() : 0;
        var urgencyId = vm.get('form.urgency') ? vm.get('form.urgency').getId() : 0;

        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/getPriority',
            method: 'GET',
            params: { impactId: impactId, urgencyId: urgencyId },
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                var priority = Ext.create('Workflow.model.ticket.TicketPriority', data);
                vm.set('form.priority', priority);
            },
            failure: function (response) {
                vm.set('form.priority', null);
            }
        });

    },

    /******************Private Methods***********************/

    isEdit: function () {

        var ref = this.getRef();
        return ref.vm.get('ticket') ? ref.vm.get('ticket').getId() != null : false;
    },

    isSubTicket: function () {
        var ref = this.getRef();
        return ref.vm.get('data.refs') && ref.vm.get('data.refs.mainTicket') ? ref.vm.get('data.refs.mainTicket').getId() != null : false;
    },

    isByAgent: function () {
        var ref = this.getRef();
        return ref.vm.get('createdBy').toUpperCase() == 'AGENT';
    },

    initFormData: function () {
        var ref = this.getRef();
        if (!this.isEdit()) {

            if (ref.vm.get('createdBy').toUpperCase() !== 'AGENT') {
                ref.refs.requestor.fireEvent('loadData', null);
                return;
            }

            return;
        }

        /////////////Check more
        var requestor = ref.vm.get('data.requestor');
        var priorityId = ref.vm.get('data.formData.priorityId');
        var subject = ref.vm.get('data.formData.subject');
        var description = ref.vm.get('data.formData.description');
        ref.refs.requestor.fireEvent('loadData', { requestor: requestor, priority: priorityId });

        ref.vm.set('form.subject', subject);
        ref.vm.set('form.description', description);

        this.setFormData(ref.vm, 'Workflow.model.ticket.TicketType', 'data.formData.typeId', 'form.ticketType');
        this.setFormData(ref.vm, 'Workflow.model.ticket.TicketStatus', 'data.formData.statusId', 'form.status');
        this.setFormData(ref.vm, 'Workflow.model.ticket.TicketSource', 'data.formData.sourceId', 'form.source');
        this.setFormData(ref.vm, 'Workflow.model.ticket.TicketImpact', 'data.formData.impactId', 'form.impact');
        this.setFormData(ref.vm, 'Workflow.model.ticket.TicketUrgency', 'data.formData.urgencyId', 'form.urgency');
        this.setFormData(ref.vm, 'Workflow.model.ticket.TicketPriority', 'data.formData.priorityId', 'form.priority');
        this.setFormData(ref.vm, 'Workflow.model.ticket.TicketTeam', 'data.formData.teamId', 'form.team');
        this.setFormData(ref.vm, 'Workflow.model.common.GeneralLookup', 'data.formData.assigneeId', 'form.assignee');
        this.setFormData(ref.vm, 'Workflow.model.ticket.TicketCate', 'data.formData.categoryId', 'form.category');


        this.setFormData(ref.vm, 'Workflow.model.common.GeneralLookup', 'data.formData.subCateId', 'form.subCate');
        this.setFormData(ref.vm, 'Workflow.model.common.GeneralLookup', 'data.formData.itemId', 'form.item');

        this.setFormData(ref.vm, 'Workflow.model.ticket.TicketSite', 'data.formData.siteId', 'form.site');
        this.setFormData(ref.vm, 'Workflow.model.ticket.TicketSite', 'data.formData.estimatedHours', 'form.estimatedHours', true);
        this.setFormData(ref.vm, 'Workflow.model.ticket.TicketSite', 'data.formData.dueDate', 'form.dueDate', true);
        
    },

    setFormData: function (vm, cls, idName, property, isVal) {

        isVal = isVal || false;
        var val = vm.get(idName);

        if ('data.formData.dueDate' === idName) {
            if (val) {
                val = new Date(val);
            } else {
                return null;
            }

        }

        if (val) {
            if (!isVal) {
                var data = { id: val };
                var item = Ext.create(cls, data);
                vm.set(property, item);
            } else {
                vm.set(property, val);
            }

        }
    },

    initForm: function () {


        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var refs = me.getView().getReferences();

        var ref = this.getRef();

        var lookUpData = model.get('data');

        var ticketTypeStore = model.getStore('ticketTypeStore');
        var ticketStatustore = model.getStore('ticketStatusStore');
        var ticketSourceStore = model.getStore('ticketSourceStore');
        var ticketImpactStore = model.getStore('ticketImpactStore');
        var ticketUrgencyStore = model.getStore('ticketUrgencyStore');
        var ticketPriorityStore = model.getStore('ticketPriorityStore');
        var ticketSiteStore = model.getStore('ticketSiteStore');
        var ticketTeamStore = model.getStore('ticketTeamStore');
        var ticketAgentStore = model.getStore('ticketAgentStore');
        var ticketCateStore = model.getStore('ticketCateStore');
        var ticketSubCateStore = model.getStore('ticketSubCateStore');
        var ticketItemStore = model.getStore('ticketItemStore');
        
        ticketTypeStore.setData(lookUpData.types);
        ticketStatustore.setData(lookUpData.statuses);
        ticketSourceStore.setData(lookUpData.sources);
        ticketUrgencyStore.setData(lookUpData.urgencies);
        ticketImpactStore.setData(lookUpData.impacts);
        ticketPriorityStore.setData(lookUpData.priorities)
        ticketSiteStore.setData(lookUpData.sites);
        ticketTeamStore.setData(lookUpData.teams);
        ticketCateStore.setData(lookUpData.cates);
        
        if (lookUpData.agents) ticketAgentStore.setData(lookUpData.agents);
        // if (lookUpData.subCates) ticketSubCateStore.setData(lookUpData.subCates);
        // if (lookUpData.items) ticketItemStore.setData(lookUpData.items);

        //ref.vm.set('form.sla.slaName', ref.vm.get('data.formData.sla'));
        //ref.vm.set('form.sla.id', ref.vm.get('data.formData.slaId'));     

                
    },

    saveTicket: function () {

        var me = this;
        var ref = me.getRef();

        var ticketId =0

        var activityCode = 'TICKET_POSTING';

        var mainTicket = ref.vm.get('data.refs.mainTicket');

        if(me.isSubTicket() ){
            ticketId = ref.vm.get('data.refs.mainTicket').getId();
            activityCode = 'SUB_TICKET_POSTING';
        }else{
            ticketId = ref.vm.get('ticket')?ref.vm.get('ticket').getId():0;
        }

        var userAttachFilesDel = this.getOriginDataFromCollection(ref.vm.getStore('userUploadFilesStore').getRemovedRecords());
        var userAttachFiles = this.getOriginDataFromCollection(ref.vm.getStore('userUploadFilesStore').getNewRecords());

        var data=  {
            requestorId:me.getItemId(ref.vm,'form.requestor'), //  ref.vm.get('form.requestor').getId(),
            userAttachFiles: userAttachFiles,
            subject: ref.vm.get('form.subject'),
            description: ref.vm.get('form.description')
        };

        var validate = [
          // { propName: 'Requestor', prop: 'requestorId' },
           { propName: 'Subject', prop: 'subject',isRequired: true   },
           { propName: 'Description', prop: 'description', isRequired: true  }
        ];

        if (me.isEdit() || me.isByAgent() ) {
            var moreData = {
                ticketId: ticketId,
                activityCode: activityCode,
                ticketItemId: me.getItemId(ref.vm,'form.item'),
                ticketTypeId: me.getItemId(ref.vm,'form.ticketType'),
                statusId : me.getItemId(ref.vm,'form.status'),
                siteId: me.getItemId(ref.vm, 'form.site'),
                impactId: me.getItemId(ref.vm, 'form.impact'),
                urgencyId: me.getItemId(ref.vm, 'form.urgency'),
                sourceId: me.getItemId(ref.vm, 'form.source'),
                teamId: me.getItemId(ref.vm, 'form.team'),
                assignee: me.getItemId(ref.vm, 'form.assignee'),
                userAttachFilesDel: userAttachFilesDel,
                comment: ref.vm.get('form.comment'),
                estimatedHours :  ref.vm.get('form.estimatedHours'),
                dueDate:  ref.vm.get('form.dueDate'),
                priorityId: me.getItemId(ref.vm, 'form.priority'),
                slaId: me.getItemId(ref.vm, 'form.sla')
            };


            validate.push({ propName: 'Ticket Type', prop: 'ticketTypeId', isRequired: true });
            validate.push({ propName: 'Ticket Source', prop: 'sourceId' ,isRequired: true});
            validate.push({ propName: 'Ticket Status', prop: 'statusId' ,isRequired: true});
            //validate.push({ propName: 'Impact', prop: 'impactId' ,isRequired: true});
            //validate.push({ propName: 'Urgency', prop: 'urgencyId' ,isRequired: true});
            validate.push({ propName: 'Priority', prop: 'priorityId',isRequired: true });
            validate.push({ propName: 'Site', prop: 'siteId' ,isRequired: true});
            validate.push({ propName: 'Group', prop: 'teamId',isRequired: true });
            validate.push({ propName: 'Ticket Item', prop: 'ticketItemId', isRequired: true });
            validate.push({ propName: 'SLA', prop: 'slaId', isRequired: true });

            // if(me.isSubTicket()){
            //     validate.push({ propName: 'Comment', prop: 'comment',isRequired: true });
            // }

            if (me.isEdit()) {
                validate.push({ propName: 'Ticket Id', prop: 'ticketId',isRequired: true });
                // validate.push({ propName: 'Comment', prop: 'comment',isRequired: true });
                validate.push({ propName: 'Estimated Hours', prop: 'estimatedHours',isRequired: false, check:function(val){
                       if(val<0){
                           return "Estimated Hours is always positive.";
                       }else{
                           return null;
                       }
                    }
                });
                validate.push({ propName: 'Due Date', prop: 'dueDate',isRequired: false, check:function(val){
                        var pointInTime = new Date();

                        if(me.isEdit()){
                            pointInTime = new Date(ref.vm.get('data.formData.createdDate'));
                        }

                        if(val < pointInTime){
                            return "Due date must be after now."
                        }else{
                            return null;
                        }

                    }
                });
            }
            Ext.apply(data, moreData);
        }

        try{
            me.validation(data, validate);
        } catch (err) {
            Ext.MessageBox.alert({
                title: 'Date validation',
                msg: err,
                icon:Ext.MessageBox.ERROR,
                buttons: Ext.MessageBox.YES
            });
            return;
        }


        Ext.MessageBox.show({
            title: 'Confirmation',
            msg: "Are sure to create ticket?",
            buttons: Ext.MessageBox.YESNO,
            scope: this,
            icon: Ext.MessageBox.QUESTION,
            fn: function (bt) {
                if (bt == 'yes') {
                    me.doSave(data, !ticketId);
                }
            }
        });


    },

    getItemId: function(vm, prop){
        if (!vm.get(prop)) {
            return null;
        } else {
           return vm.get(prop).getId();
        }
    },

    doSave: function (data, isNew) {
        var me = this;
        var w = me.getView().up();
        w.hide();
        Ext.getBody().mask("Save...");
        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/ticket',
            headers: { 'Content-Type': 'application/json' },
            method: 'POST',
            jsonData: data,
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                Ext.getBody().unmask();
                w.close();
                //Refresh main form
                var v = me.getView();
                var fn = me.getView().cbFn;
                if (fn) {
                    fn();
                }
                var message = isNew?" has been created" : " has been updated"
                Ext.MessageBox.alert({
                    title: 'Ticket',
                    msg: Ext.String.format('Ticket #{0} {1} successfully', data.message.ticketNo, message),
                    buttons: Ext.MessageBox.YES
                });

            },
            failure: function (opt, operation) {
                w.show();
                me.showToast(Ext.String.format('Failed to create ticket, {0}'));
                Ext.getBody().unmask();
                var data = Ext.JSON.decode(opt.responseText);
                Ext.MessageBox.alert({
                    title: 'Error',
                    msg: data.message,
                    icon:Ext.MessageBox.ERROR,
                    buttons: Ext.MessageBox.YES
                });

            }
        });
    },

    onWindowClosedHandler: function () {
        var me = this,
            w = me.getView().up();
        w.close();

    },
    onTicketTypeChange: function (el, v) {
        var ref = this.getRef();
        var vm = ref.vm;
        var ticket = vm.get('ticket');        
        var statusStore = vm.get('ticketStatusStore');
        var data = vm.get('data');
        statusStore.setData(data.statuses);

        if (v && v.get('id') == 1) {
            statusStore.removeAt(4);
            statusStore.removeAt(4);
        }

        //if ((ticket != null && ticket.id > 0) && vm.get('isTicketTicketTypeChangeInit')) {
        //    vm.set('isTicketTicketTypeChangeInit', false);
        //    return false;
        //}
        this.onGetSlaHandler();
    },
    onTicketPriorityChange: function () {
        var ref = this.getRef();
        var vm = ref.vm;
        var ticket = vm.get('ticket');
        
        //if ((ticket != null && ticket.id > 0) && vm.get('isTicketTicketPriorityChangeInit')) {
        //    vm.set('isTicketTicketPriorityChangeInit', false);
        //    return false;
        //}
        this.onGetSlaHandler();
    },
    onGetSlaHandler: function () {
        var ref = this.getRef();
        var vm = ref.vm;
        var ticket = vm.get('ticket');
        
        var typeId = vm.get('form.ticketType') ? vm.get('form.ticketType').getId() : 0;
        var priorityId = vm.get('form.priority') ? vm.get('form.priority').getId() : 0;
        
        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/getSlaMapping',
            method: 'GET',
            params: { typeId: typeId, priorityId: priorityId },
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                var sla = Ext.create('Workflow.model.ticket.TicketSla', data);
                vm.set('form.sla', sla);
            },
            failure: function (response) {
                vm.set('form.sla', null);
            }
        });
        
    }
});
