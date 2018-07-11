/**
 *
 *Author : Phanny
 */
Ext.define("Workflow.view.common.requestor.Requestor",{
    extend: "Ext.form.Panel",
    xtype: 'form-requestor',

    requires: [
        "Workflow.view.common.requestor.RequestorController",
        "Workflow.view.common.requestor.RequestorModel",
        "Workflow.store.common.Priorities"
    ],

    controller: "common-requestor-requestor",
    viewModel: {
        type: "common-requestor-requestor"
    },
    iconCls: 'fa fa-user',
    title: 'Requestor',
    formReadonly : false,
    optaional : false,
    allowAddRequestor : true,
    minHeight: 200,
    layout: 'column',
    autoWidth: true,
    priorityHidden : false,
    defaults: {
        layout: 'form',
        xtype: 'container',
        defaultType: 'textfield',
        flex: 1
    },

    initComponent: function() {
        var me=this;

        var col1 = me.col1();
        var col2 = me.col2();

        if(!me.priorityHidden){
            //col1.push({ fieldLabel: 'Division', bind: '{employeeInfo.devision}', readOnly: true });
            col1.push({ fieldLabel: 'Team', bind: '{employeeInfo.subDept}', readOnly: true });
        }else{
            //col2.push({ fieldLabel: 'Division', bind: '{employeeInfo.devision}', readOnly: true });
            col2.push({ fieldLabel: 'Team', bind: '{employeeInfo.subDept}', readOnly: true });
        }

        me.items= [
            {
            width: 450,
            items: col1
        }, {
            width: 450,
            items: col2
        }];

        me.callParent(arguments);
    },


    col1 : function(){
        var me = this;
        return [
                {
                    fieldLabel: Ext.String.format('Employee Name {0}',  me.optaional? '': '<span style="color:red;">*</span>') ,
                    xtype: 'employeePickup',
                    employeeEditable: me.allowAddRequestor,
                    forceSelection : false,
                    triggers: me.triggersBt(),
                    listeners: {
                        select: 'selectedRequestor'
                    },
                    bind: {
                        selection: '{employeeInfo}',
                        readOnly : '{readOnly}'
                    }
                },
                { fieldLabel: 'Employee No', bind: '{employeeInfo.employeeNo}', readOnly: true },
                { fieldLabel: 'Department', bind: '{employeeInfo.groupName}', readOnly: true },
                { fieldLabel: 'Line', bind: '{employeeInfo.deptName}', readOnly: true }
        ];
    },

    triggersBt: function(){

      if(!this.allowAddRequestor) {
        return {};
      }

      return {
        add: {
            weight: 3,
            cls: Ext.baseCSSPrefix + 'form-add-trigger',
            scope: 'controller',
            reference: 'addNewBt',
            handler: 'showAddWindow',
            hidden: true
        },
        edit: {
            weight: 3,
            cls: Ext.baseCSSPrefix + 'form-edit-trigger',
            scope: 'controller',
            reference: 'editBt',
            handler: 'showEditWindow',
            //hidden: true,
            bind:{
                //disabled: '{toggleButtonEdit}'
            }
        }
      };
    },
    col2: function () {
        var me = this;
        return [
                { fieldLabel: 'Position', bind: '{employeeInfo.position}', readOnly: true },
                { fieldLabel: 'Phone', bind: '{employeeInfo.phone}', readOnly: true },
                { fieldLabel: 'Phone (Ext)', bind: '{employeeInfo.ext}', readOnly: true },
                { fieldLabel: 'Email', bind: '{employeeInfo.email}', readOnly: true },
                { fieldLabel: 'HoD', bind: '{employeeInfo.hod}',hidden: true, readOnly: true },{
                    fieldLabel: 'Priority',
                    xtype: 'combobox',
                    reference: 'priority',
                    publishes: 'description',
                    displayField: 'description',
                    valueField: 'id',
                    anchor: '-15',
                    editable:false,
                    store: 'priorities',
                    //minChars: 0,
                    readOnly: me.formReadonly,
                    hidden : me.priorityHidden,
                    queryMode: 'local',
                    typeAhead: false,
                    bind: '{priorityVal}'
                }
        ];
    },


    getData: function (){
        var requestor = this.getViewModel().get('employeeInfo');
        return requestor? requestor.data:null;
    },
    getPriority: function() {
        return  this.getViewModel().get('priorityVal');
    },
    clearData : function(){
        //this.getViewModel().set('employeeInfo',null);
        this.getViewModel().set('priorityVal',1);
    }

});
