/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.view.TicketDisplayController', {
    //extend: 'Workflow.view.AbstractBaseController',
    extend: 'Workflow.view.ticket.TicketActivityController',
    alias: 'controller.ticket-view-ticketdisplay',

    init: function () {
        var me = this;
        var view = me.getView();
        var refs = view.getReferences();
        var mv = view.getViewModel();
        var ticketData = mv.get("ticket");
        var activities = ticketData.get('activities');


        me.refreshActivity(activities);
    },

    /*******************Event handlers**************************/

    onAttachmentClickHandler: function (view, cell, cellIndex, record, row, rowIndex, e) {
      
        if(e.target.tagName == 'A'){
               return; 
        }
        e.preventDefault();
        var linkClicked = (e.target.tagName == 'B') || (e.target.tagName == 'I');
        var clickedDataIndex = view.panel.headerCt.getHeaderAtIndex(cellIndex).dataIndex;

        var files = record.get('fileUpload');

        if (linkClicked && clickedDataIndex == 'createdDate') {
            this.showAttachmentWindow(cell, files,false);
        }
    },

    onActivityClickHandler: function (bt, e) {
        var me = this;
        var item = bt.getItemId();
        var vm = this.getView().getViewModel();
        var ticket = vm.get('ticket');


        me.openActivityWindow(bt, item, ticket, function () {
            me.refreshData(ticket.getId());
        });
    
    },

    onActivityFilter : function(){
        var vm = this.getRef().vm;
        if(!vm.get('acitvityType')){
            return;
        }
        var selectedType = vm.get('acitvityType').getId();

        var activityStore = vm.getStore('activityStore');
        if(selectedType !=='0'){
            //activityStore.setFilters ([{property: 'activityType', value : selectedType }] ) ;  
            activityStore.filter('activityType', selectedType);

        }else{
            activityStore.clearFilter();
        
        }
    },
    /***********Private methods**********/

    

    getActivityStore: function () {
        return this.getView().getViewModel().getStore('activityStore');
    },
   

    refreshData:function(ticketId) {
        var me = this;
        var view = me.getView();
        var vm = view.getViewModel();

        Ext.getBody().mask("Loading data...");

        Workflow.model.ticket.Ticket.load(ticketId, {
            scope: this,
            failure: function (opt, operation) {
                Ext.getBody().unmask();

                var lastView = mainLayout.getActiveItem()
                if (lastView && lastView.routeId) {
                    me.redirectTo(lastView.routeId);
                }
                var response = Ext.decode(operation.error.response.responseText);
                Ext.MessageBox.show({
                    title: 'Error',
                    msg: response.Message,
                    buttons: Ext.MessageBox.OK,
                    icon: Ext.MessageBox.ERROR
                });
            },
            success: function (record, operation) {
                vm.set('ticket', record)
                var activities = record.get('activities');
                me.refreshActivity(activities);
                Ext.getBody().unmask();
                me.refreshActions(vm.get('ticket.actions'));
            }
        });
    },




    refreshActivityType : function(){
        var ref = this.getRef();
        var activityTypes = ref.vm.get('ticket.activityTypes');
        var allActs =[{id:'0', display: 'All'}];
        allActs = allActs.concat(activityTypes)
        var store = ref.vm.getStore('activityTypeStore');
        store.setData(allActs);
    },

    refreshActivity:function(activities) {
        var activityStore = this.getActivityStore();
        activityStore.setData(activities);
        var ref = this.getRef() , refs = ref.refs;
        refs.activityList.setStore(activityStore);

        this.refreshActivityType();
        this.refreshActions(ref.vm.get('ticket.actions'));
    },

    openMsOutlook: function () {
        var me = this;
        var viewmodel = me.getViewModel();
        var ticket = viewmodel.get('ticket');
        var to = viewmodel.get('ticket.emailItem.to');
        var cc = viewmodel.get('ticket.emailItem.cc');
        var subject = ticket.get('subject');
        console.log(to, cc, subject)
        window.location.href = 'mailto:'+ to + '?subject='+ subject + '&cc=' + cc;
    },
    refreshActions: function (actions) {
        var me = this;
        var ref = me.getRef();
        var menus = [];
        var groupName;
        var viewmodel = me.getViewModel();
        var ticket = viewmodel.get('ticket');
        var source = ticket.get('source');

        if (source && source == 'Email') {
            menus.push({
                text: 'Reply All via Email',
                iconCls: 'fa fa-reply',
                handler: 'openMsOutlook'
            });
        }
        actions.forEach(function (action) {

            if (groupName && groupName !== action.groupName) {
                menus.push('-');
            }

            menus.push({
                text: action.name,
                iconCls : me.getActionIcon(action.activityCode),
                itemId: action.activityCode,
                handler: 'onActivityClickHandler'
            });
            groupName = action.groupName;
        });
        
        ref.refs.actionBt.getMenu().removeAll();
        ref.refs.actionBt.getMenu().add(menus);
    },

    //TODO: Remove doublicated code
    getActionIcon: function(code){

        if('OPEN' === code){
            return 'fa fa-external-link';
        }
        else if('POST_REPLY' === code ){
            return 'fa fa-reply';
        }
        else if('TICKET_ASSIGNED' === code){
            return 'fa fa-users';
        }
        else if('CHANGE_STATUS' === code) {
            return 'fa fa-arrows-h';
        }
        else if( 'EDIT_TICKET_INFO' === code) {
            return 'fa fa-pencil-square-o';
        }
        else if('MERGE_TICKET' === code) { 
            return 'fa fa-code-fork';
        }
        else if('DELETE_TICKET' === code) {
            return 'fa fa-times';
        }
        else if('POST_PUBLIC_NOTE' === code ){
            return 'fa fa-comments-o';
        }
        else if('POST_INTERNAL_NOTE' === code){
            return 'fa fa-comment-o';
        }
        else if('VIEW_ATTACHMENT' === code){
            return 'fa fa-paperclip';
        }
        else if('SUB_TICKET_POSTING' === code) {
            return 'fa fa-plus-circle';
        }
        else{
            return 'fa fa-dot-circle-o';
        }
    }

});
