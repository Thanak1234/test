/**
 *Author : Phanny 
 *
 */
Ext.define("Workflow.view.av.JobDetailView",{
    extend: "Ext.form.Panel",
    xtype : 'av-job-detail-view',
    requires: [
        "Workflow.view.av.JobDetailViewController",
        "Workflow.view.av.JobDetailViewModel"
    ],

    controller: "av-jobdetailview",
    viewModel: {
        type: "av-jobdetailview"
    },

    title: 'Job Detail',
    iconCls: 'fa fa-file-text-o',
    collapsible: true,
    formReadonly : false,
    //frame: true,
    minHeight: 100,
    layout: 'column',
    autoWidth: true,
    //bodyPadding: 10,
    defaults: {
        layout: 'form',
        xtype: 'container',
        defaultType: 'textfield',
        flex : 1
    },
     initComponent: function() {
        var me=this;
         me.items= [
            {
            //width: 300,
            items: [
               
                { fieldLabel: 'Project name', labelAlign: 'right', allowBlank: false, bind: { value: '{jobDetail.projectName}', readOnly: '{!editable}' } },
                { fieldLabel: 'Location', labelAlign: 'right', allowBlank: false, bind: { value: '{jobDetail.location}', readOnly: '{!editable}' } }
            ]
        }, {
        //width: 300,
        items: [
                {   fieldLabel      : 'Setup Date', 
                    name            : 'date_from' ,
                    endTimePeriod   :'date_to', //below component
                    validationEvent : 'change', 
                    xtype: 'datefield',
                    labelAlign: 'right',
                    allowBlank      : false, 
                    bind            : {value:'{jobDetail.setupDate}', readOnly: '{!editable}'} 
                },
                // { fieldLabel: '1', xtype: 'timefield' , allowBlank  : false, bind: {value:'{jobDetail.setupTime}', readOnly: '{!editable}'} },
                {   fieldLabel      : 'Actual Event Date', 
                    name            : 'date_to', 
                    startTimeField: 'date_from',
                    labelAlign: 'right',
                    validationEvent : 'change', 
                    xtype           : 'datefield', 
                    allowBlank      : false, 
                    bind            : {value:'{jobDetail.actualDate}', readOnly: '{!editable}'} 
                 }
                // { fieldLabel: '1', xtype: 'timefield', allowBlank  : false, bind: {value:'{jobDetail.actualEventTime}', readOnly: '{!editable}'} }
            ]
        },{
          //width : 200,
          items : [
              {fieldLabel : 'Time', xtype: 'timefield',allowBlank  : false, bind : {value : '{jobDetail.setupTime}' , readOnly: '{!editable}'} },
              {fieldLabel : 'Time', xtype: 'timefield',allowBlank  : false, bind : {value : '{jobDetail.actualTime}' , readOnly: '{!editable}'} }
          ]  
        }, {
            width: 820,
            margin: '5 0 5 12',
            items : [
                {
                    fieldLabel: 'Project Brief',
                    xtype: 'textareafield',
                    bind: {
                        value: '{jobDetail.projectBrief}',
                        readOnly: '{!editable}'
                    }
                }
            ]  
        }];
         
        me.callParent(arguments);
     }
});
