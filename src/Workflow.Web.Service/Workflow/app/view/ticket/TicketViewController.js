/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.TicketViewController', {
    //extend: 'Workflow.view.AbstractBaseController',
    extend: 'Workflow.view.ticket.TicketActivityController',
    alias: 'controller.ticket-ticketview',

    task: null,

    init: function () {
        var me = this;
        var vm = me.getView().getViewModel();
        vm.set('statusVal', 0);
        this.initData(function (data) {
            vm.set('initData', data);
            me.initFormData(data.isAgent);
        });

        me.task= {
                    run: this.filler.bind(this),
                    interval: 20000
                };
        if(vm.get('autoRefresh')){
          Ext.TaskManager.start(me.task);  
        }
    },

    destroy: function(){
        Ext.TaskManager.stop(this.task);
    },
    initFormData: function(isAgent){
      var me = this;
      var vm = me.getView().getViewModel();
      var statusStore = vm.getStore('ticketStatusStore');
      statusStore.load({
          scope: this,
          callback: function(records, operation, success) {
              this.initStatusMenu(records, isAgent);
      }})

      //Default tickets assigned to me

      this.refreshData();
    },

    onRefreshHandler: function(el, e) {

        var ref = this.getRef();
        var vm = ref.vm;

        var autoRefresh = !vm.get('autoRefresh');
        vm.set('autoRefresh', autoRefresh); 

        if(autoRefresh){
            Ext.TaskManager.start(this.task);
        }else{
            Ext.TaskManager.stop(this.task);
        }
        

        //autoRefresh
    },

    /****************************************filter************************************/
    onEnterFilterHandler: function (el, e) {
        //keyword
        if (e.getKey() == e.ENTER) {
            this.filler();
        }
    },
    onFilterHandler: function () {
        this.filler();
    },


    filler: function () {
        var vm = this.getViewModel();
        var keyworkd = vm.get('keyword') || null;
        this.refreshData({ keyword: keyworkd});
    },

    onNavFilterChanged: function (field, value) {
        if (value) {
            field.getTrigger('clear').show();
        } else {
            field.getTrigger('clear').hide();
        }
    },

    onNavFilterClearTriggerClick: function (field, value) {
        field.getTrigger('clear').hide();
        var vm = this.getView().getViewModel();
        vm.set('keyword', null);
        this.filler();
    },

    refreshData: function (params) {
        var me = this;
        var vm = me.getView().getViewModel();
        var refs = me.getView().getReferences();
        var status = vm.get('statusVal') || 0 ;
        if(!params ){
            params = {status:status };
        }else if(!params.status){
            params.status = status;
        }

        var ticketListingStore = vm.getStore('ticketListingStore');
        ticketListingStore.getProxy().extraParams = Ext.Object.merge(ticketListingStore.getProxy().extraParams, params);
        ticketListingStore.load();
    },


    /***********************************Event*****************************************/
    onTicketClassifyClick: function (el, e, eOpts) {
        var ticketFilterButton = this.getView().getReferences().ticketFilterButton;
        var menus = this.getFilterMenuItems();
        menus.items.forEach(function (item) {
            if (item === el) {
                el.setIconCls('fa fa-check')
            } else {
                item.setIconCls('')
            }
        });

        ticketFilterButton.setText(el.text);
        var ref = this.getRef();
        var vm = ref.vm;
        vm.set('keyword', null);
        vm.set('statusVal', el.statusId);
        this.filler();

    },

    onStatusChanged: function ( el , newValue , oldValue , eOpts) {
        this.filler();
    },
    onNewTicketByUserActionListener: function (el, e, eOpts) {
        this.createTicket(el, 'user');
    },
    onNewTicketActionListener: function (el, e, eOpts) {
        this.createTicket(el, el.getItemId());

    },

    onItenDoubleClickHandler: function (record, item, index, e) {
        this.onTicketActionHandler(item, { ticket: item, activityCode: 'OPEN' });
    },
    onTicketActionHandler: function (item, value) {
        var me = this;        
        if (value.activityCode === 'OPEN') {
            window.location.href = "#ticket/" + value.ticket.getId();
        } else {
            me.openActivityWindow(value.el, value.activityCode, value.ticket, function () { me.refreshData() });
        }

    },

    /*************************Private Methods**************************/

    initData: function(fn){
        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/init-data',
            method: 'GET',
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                if (fn) {
                    fn(data);
                }
            },
            failure: function (response) {
                console.log('error');
            }
        });

    },

    initStatusMenu: function(datas, isAgent){

        var me = this;
        var ref = this.getRef();

        var statusMenu = ref.refs.ticketFilterButton;
        var group=null;
        var menuItems=[];
        var quickActionId = isAgent?1003: 1201;

        if(!isAgent){
            statusMenu.setText("Active Tickets I Request");
        }

        datas.forEach(function(menu){

            if(group &&  group != (menu.get('classify') || 'status' ) ){
                menuItems.push('-');
            }
            menuItems.push(me.createMenu(menu, quickActionId));
            group = menu.get('classify') || 'status';

        });

        statusMenu.getMenu().add(menuItems);
    },

    createMenu : function(menu, quickActionId){
        return  {
                    text: menu.get('status'),
                    statusId: menu.getId(),
                    iconCls:  menu.getId()===quickActionId?'fa fa-check':'a',
                    handler: 'onTicketClassifyClick',
                    scope: 'controller'
                };
    },

    getFilterMenuItems: function () {
        var ticketFilterButton = this.getView().getReferences().ticketFilterButton;
        return ticketFilterButton.getMenu().items;
    },
    itemcontextmenu: function () {
        console.log('test');
    },

    createTicket: function (el, by) {
        var me = this;
        me.createTicketDialog( {el : el , by : by } , function(){
            me.refreshData();
        });
    },

    onSubjectRenderer : function(value, metadata, record){
      //return Ext.String.format('<div><i class="fa fa-object-group" aria-hidden="true"></i></div>',value);
      //return '<i class="fa fa-object-group" aria-hidden="true"></i>';
      return value;
      //return Ext.String.format('<div height = "100%"><i class="fa fa-bolt" aria-hidden="true" style="color:#4CAF50"></i>{0}</div>',value);
    }


});
