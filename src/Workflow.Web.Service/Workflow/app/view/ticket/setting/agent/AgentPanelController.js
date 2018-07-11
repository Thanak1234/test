Ext.define('Workflow.view.ticket.setting.agent.AgentPanelController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-agent-agent',
    init: function () {
        // this.loadData();
    },
    loadData: function (params) {
        var me = this;
        var model = me.getView().getViewModel();

        var ticketSettingAgentStore = model.getStore('ticketSettingAgentStore');
        ticketSettingAgentStore.getProxy().extraParams = params;
        ticketSettingAgentStore.load();

    },
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
        var vm = this.getView().getViewModel();
        var query = vm.get('query') || null;
        this.loadData({ query: query});
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
        vm.set('query', null);
        this.filler();
    },
    onDblClickHandler: function( el , record , agent , index , e , eOpts ) {
        this.onEditHandler(el, index);
    },
    onEditHandler: function (el, rowIndex, checked, obj) {
        var me = this, model = me.getView().getViewModel();
        var record = el.getStore().getAt(rowIndex);


        var empModel = Ext.create('Workflow.model.common.EmployeeInfo',{
                'id': record.get('empId'),
                'employeeNo': record.get('employeeNo'),
                'fullName': record.get('fullName'),
                'position': record.get('position'),
                'subDeptId': record.get('subDeptId'),
                'subDept': record.get('subDept'),
                'groupName': record.get('groupName'),
                'devision': record.get('devision'),
                'phone': record.get('phone'),
                'ext': record.get('ext'),
                'email': record.get('email'),
                'hod': record.get('hod'),
                'priority': record.get('priority'),
                'reportTo': record.get('reportTo')
        });

        var data = {
            'id': record.get('id'),
            'description': record.get('description'),
            'createdDate': record.get('createdDate'),
            'modifiedDate': record.get('modifiedDate'),
            dept: Ext.create('Workflow.model.ticket.TicketDepartment',{id: record.get('deptId')}),
            groupPolicy: Ext.create('Workflow.model.ticket.TicketGroupPolicy',{id: record.get('groupPolicyId')}),
            accountType: Ext.create('Workflow.model.ticket.TicketLookup',{id: record.get('accountTypeId')}),
            employee: empModel,
            status: Ext.create('Workflow.model.ticket.TicketLookup',{id: record.get('statusId')})
        };

        me.showWindowDialog(el, 'Workflow.view.ticket.setting.agent.TicketAgentForm', {
            form: data,
            empId: record.get('empId')

        }, function () {
            me.getView().getStore().reload();
        });
    },
    onAddHandler: function (el, e, eOpts) {
        var me = this;
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.agent.TicketAgentForm', {
            form: null
        }, function () {
            me.getView().getStore().reload();
        });
    },
    showWindowDialog: function (lauchFromEl, windowClass, model, cb) {
        var content = Ext.create(windowClass, {
            viewModel: {
                data: model
            },
            cbFn: cb
        });
        var me = this;
        var w = new Ext.window.Window({
            rtl: false,
            modal: true,
            title: 'Ticket Agent Form',
            layout: 'fit',
            closable: true,
            iconCls: 'fa fa-street-view',
            //collapsable: true,
            items: content,
            height: me.getView().getHeight(),
            width: 970,
            doClose: function () {
                w.hide(lauchFromEl, function () {
                    w.destroy();
                });
            }
        });
        w.show(lauchFromEl);
    }
});
