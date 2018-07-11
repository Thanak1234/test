Ext.define("Workflow.view.ticket.report.ReportView", {
    extend: "Ext.form.Panel",
    xtype: 'report-ticket',
    bodyPadding: 0,
    layout: 'border',
    defaults: {
        collapsible: true,
        bodyStyle: 'padding:0px'
    },
    width: '100%',
    margin: 0,
    loadMask: true,
    ngconfig: {
        layout: 'fullScreen'
    },
    controller: "ticket-report-reportview",
    viewModel: {
        type: "ticket-report-reportview"
    },
    initComponent: function () {
        var me = this;
        me.items = me.buildItems();

        me.callParent(arguments);
    },
    buildButtons: function () {
        return [
        {
            xtype: 'button',
            iconCls: 'fa fa-search',
            text: 'Search',
            handler: 'onSearchHanlder'
        }, {
            xtype: 'button',
            text: 'Export',
            iconCls: 'fa fa-download',
            menu: [{
                iconCls: 'fa fa-file-pdf-o',
                text: 'Export PDF',
                handler: 'onExportPdf'
            }, {
                iconCls: 'fa fa-file-excel-o',
                text: 'Export Excel',
                handler: 'onExportExcel'
            }]
        },
        {
            xtype: 'button',
            iconCls: 'fa fa-eraser',
            text: 'Clear',
            handler: 'onClearHanlder'
        }];
    },
    buildItems: function () {
        var me = this;

        var store = Ext.create('Workflow.store.ticket.Report');

        return [
              {
                  xtype: 'form',
                  region: 'north',
                  title: 'Report - Criteria',
                  layout: 'vbox',
                  hideHeaders: true,
                  buttons: me.buildButtons(),
                  items: [
                      {
                          xtype: 'fieldset',
                          layout: 'anchor',
                          padding: 0,
                          margin: 0,
                          hideHeaders: true,
                          width: '100%',
                          defaults: { anchor: '100%', labelWidth: 120, msgTarget: 'side', labelAlign: 'right' },
                          items: [
                              {
                                  xtype: 'fieldcontainer',
                                  fieldLabel: 'Common',
                                  layout: 'vbox',
                                  margin: 5,
                                  defaults: {
                                      flex: 1,
                                      hideLabel: true,
                                      margin: '0 5 0 0'
                                  },
                                  items: [
                                      {
                                          xtype: 'fieldcontainer',
                                          layout: 'hbox',
                                          margin: '5 0 5 0',
                                          defaults: {
                                              flex: 1,
                                              hideLabel: true,
                                              width: 250,
                                              margin: '0 5 0 0',
                                              triggers: {
                                                  clear: {
                                                      type: 'clear',
                                                      hideWhenEmpty: true,
                                                      clearOnEscape: true,
                                                      weight: -1
                                                  }
                                              }
                                          },
                                          items: [
                                              {
                                                  xtype: 'combo',
                                                  emptyText: 'TYPE',
                                                  editable: false,
                                                  forceSelection: true,
                                                  queryMode: 'local',
                                                  displayField: 'typeName',
                                                  valueField: 'id',
                                                  bind: {
                                                      store: '{ticketTypeStore}',
                                                      value: '{form.typeId}'
                                                  },
                                                  multiSelect: true,
                                                  listeners: {
                                                      change: 'onTicketTypeChanged'
                                                  },
                                                  listConfig: {
                                                      minWidth: 200,
                                                      //resizable: true,
                                                      //loadingText: 'Searching...',
                                                      //itemSelector: '.search-item',
                                                      itemTpl: [
                                                          '<div><img src="{icon}"/><span style="margin-left: 10px!important;">{typeName}</span></div>'
                                                      ]
                                                  }
                                              },
                                              {
                                                  xtype: 'textfield',
                                                  emptyText: 'TICKET NO',
                                                  bind: {
                                                      value: '{form.ticketNo}'
                                                  }
                                              }, {
                                                  xtype: 'textfield',
                                                  emptyText: 'SUBJECT',
                                                  bind: {
                                                      value: '{form.subject}'
                                                  }
                                              }
                                          ]
                                      }, {
                                          xtype: 'fieldcontainer',
                                          layout: 'hbox',
                                          margin: 0,
                                          defaults: {
                                              flex: 1,
                                              hideLabel: true,
                                              width: 250,
                                              margin: '0 5 5 0',
                                              triggers: {
                                                  clear: {
                                                      type: 'clear',
                                                      hideWhenEmpty: true,
                                                      clearOnEscape: true,
                                                      weight: -1
                                                  }
                                              }
                                          },
                                          items: [
                                              {
                                                  xtype: 'combo',
                                                  emptyText: 'PRIORITY',
                                                  forceSelection: true,
                                                  editable: false,
                                                  queryMode: 'local',
                                                  displayField: 'priorityName',
                                                  valueField: 'id',
                                                  bind: {
                                                      store: '{ticketPriorityStore}',
                                                      value: '{form.priority}'
                                                  },
                                                  multiSelect: true,
                                                  listeners: {
                                                      change: 'onPriorityChanged'
                                                  }
                                              }, {
                                                  xtype: 'combo',
                                                  emptyText: 'SLA',
                                                  forceSelection: true,
                                                  editable: false,
                                                  queryMode: 'local',
                                                  displayField: 'slaName',
                                                  valueField: 'id',
                                                  bind: {
                                                      selection: '{view.sla}',
                                                      store: '{ticketSlaStore}',
                                                      value: '{form.slaId}'
                                                  },
                                                  multiSelect: true
                                              }, {
                                                  xtype: 'textfield',
                                                  emptyText: 'Comment',
                                                  bind: {
                                                      value: '{form.comment}'
                                                  }
                                              }
                                          ]
                                      },
                                      {
                                          xtype: 'fieldcontainer',
                                          layout: 'hbox',
                                          margin: 0,
                                          defaults: {
                                              flex: 1,
                                              hideLabel: true,
                                              width: 250,
                                              margin: '0 5 0 0',
                                              triggers: {
                                                  clear: {
                                                      type: 'clear',
                                                      hideWhenEmpty: true,
                                                      clearOnEscape: true,
                                                      weight: -1
                                                  }
                                              }
                                          },
                                          items: [
                                              {
                                                  xtype: 'combo',
                                                  emptyText: 'STATUS',
                                                  reference: 'ticketStatusCombo',
                                                  forceSelection: true,
                                                  editable: true,
                                                  queryMode: 'local',
                                                  displayField: 'status',
                                                  valueField: 'id',
                                                  bind: {
                                                      store: '{ticketStatusStore}',
                                                      value: '{form.statusId}'
                                                  },
                                                  multiSelect: true,
                                                  maxLength: 9000
                                              }, {
                                                  xtype: 'checkbox',
                                                  boxLabel: 'Include Ticket Deleted',
                                                  inputValue: 1,
                                                  bind: {
                                                      value: '{form.includeRemoved}'
                                                  },
                                                  listeners: {
                                                      change: 'onIncludeRemovedChecked'
                                                  }
                                              }, {
                                                  xtype: 'combo',
                                                  emptyText: 'SOURCE',
                                                  forceSelection: true,
                                                  editable: false,
                                                  queryMode: 'local',
                                                  displayField: 'source',
                                                  valueField: 'id',
                                                  bind: {
                                                      selection: '{view.source}',
                                                      store: '{ticketSourceStore}',
                                                      value: '{form.sourceId}'
                                                  }
                                              }
                                          ]
                                      }
                                  ]
                              },
                              {
                                  xtype: 'fieldcontainer',
                                  fieldLabel: 'Requestor',
                                  layout: 'hbox',
                                  margin: 5,
                                  defaults: {
                                      hideLabel: true,
                                      width: 250,
                                      margin: '0 5 0 0'
                                  },
                                  items: [
                                  {
                                      xtype: 'combo',
                                      editable: false,
                                      margin: '0 5 0 0',
                                      store: ['DEPARTMENT (S)', 'EMPLOYEE'],
                                      maxWidth: 250,
                                      value: 'EMPLOYEE',
                                      maxLength: 99999,
                                      listeners: {
                                          beforequery: function (record) {
                                              record.query = new RegExp(record.query, 'i');
                                              record.forceAll = true;
                                          },
                                          select: function (combo) {
                                              var viewmodel = me.getViewModel();
                                              var ctlr = me.getController();
                                              if (combo.value == 'EMPLOYEE') {
                                                  viewmodel.set('deptDisplay', null);
                                                  viewmodel.set('showDept', false);
                                                  viewmodel.set('showEmp', true);
                                                  ctlr.deptStore = null;
                                                  viewmodel.set('form.depts', null);
                                              } else if (combo.value == 'DEPARTMENT (S)') {
                                                  viewmodel.set('form.requestorId', null);
                                                  viewmodel.set('showDept', true);
                                                  viewmodel.set('showEmp', false);
                                                  var employeePicker = me.lookupReference('employeePicker');
                                                  employeePicker.onClearClick();
                                              }
                                          }
                                      }
                                  }, {
                                      xtype: 'textfield',
                                      reference: 'department',
                                      editable: false,
                                      emptyText: 'DEPARTMENT <ALL>',
                                      hidden: true,
                                      width: 505,
                                      bind: {
                                          value: '{deptDisplay}',
                                          hidden: '{!showDept}'
                                      },
                                      listeners: {
                                          change: function (el, val) {
                                              if (!val || val == '') {
                                                  el.getTrigger('clear').hide();
                                              } else {
                                                  el.getTrigger('clear').show();
                                              }
                                          }
                                      },
                                      triggers: {
                                          clear: {
                                              weight: 1,
                                              cls: Ext.baseCSSPrefix + 'form-clear-trigger',
                                              hidden: true,
                                              handler: 'onClearClick'
                                          },
                                          add: {
                                              weight: 3,
                                              cls: Ext.baseCSSPrefix + 'form-edit-trigger',
                                              handler: 'onEditClick'
                                          }
                                      }
                                  },
                                  {
                                      fieldLabel: 'Requestor',
                                      hidden: false,
                                      emptyText: 'EMPLOYEE <ALL>',
                                      xtype: 'employeePickup',
                                      reference: 'employeePicker',
                                      width: 505,
                                      includeInactive: true,
                                      includeGenericAcct: true,
                                      loadCurrentUser: true,
                                      afterClear: function (combo) {
                                          var viewmodel = me.getViewModel();
                                          viewmodel.set('form.requestorId', null);
                                      },
                                      listConfig: {
                                          minWidth: 250,
                                          resizable: true,
                                          loadingText: 'Searching...',
                                          emptyText: 'No matching posts found.',
                                          itemSelector: '.search-item',

                                          // Custom rendering template for each item
                                          itemTpl: ['<span>{employeeNo} - {fullName}</span>']
                                      },
                                      bind: {
                                          hidden: '{!showEmp}',
                                          value: '{form.requestorId}'
                                      }
                                  }]
                              },
                              {
                                  xtype: 'fieldcontainer',
                                  fieldLabel: 'Assignee',
                                  layout: 'hbox',
                                  margin: 5,
                                  defaults: {
                                      flex: 1,
                                      hideLabel: true,
                                      maxWidth: 250,
                                      margin: '0 5 0 0',
                                      triggers: {
                                          clear: {
                                              type: 'clear',
                                              hideWhenEmpty: true,
                                              clearOnEscape: true,
                                              weight: -1
                                          }
                                      }
                                  },
                                  items: [
                                      {
                                          xtype: 'combo',
                                          emptyText: 'TEAM',
                                          editable: true,
                                          forceSelection: true,
                                          queryMode: 'local',
                                          displayField: 'teamName',
                                          valueField: 'id',
                                          listeners: {
                                              change: 'onTeamChanged',
                                              beforequery: function (record) {
                                                  me.quickSearch(record);
                                              }
                                          },
                                          bind: {
                                              store: '{ticketTeamStore}',
                                              value: '{form.teamId}'
                                          },
                                          multiSelect: true,
                                          maxLength: 9000
                                      }, {
                                          xtype: 'combo',
                                          reference: 'assignee',
                                          emptyText: 'ASSIGNEE NAME',
                                          bind: {
                                              store: '{ticketAgentStore}',
                                              value: '{form.assignedId}',
                                              disabled: '{disable.assignee}'
                                          },
                                          displayField: 'display',
                                          valueField: 'id',
                                          queryMode: 'local',
                                          forceSelection: true,
                                          minChars: 2,
                                          listConfig: {
                                              minWidth: 500,
                                              resizable: true,
                                              loadingText: 'Searching...',
                                              itemSelector: '.search-item',
                                              itemTpl: [
                                                  '<a >',
                                                      '<h3><span>{display}</span>({display1})</h3>',
                                                      '{description} | {display2}',
                                                  '</a>'
                                              ]
                                          },
                                          listeners: {
                                              beforequery: function (record) {
                                                  me.quickSearch(record);
                                              }
                                          },
                                          multiSelect: true,
                                          maxLength: 9000
                                      }, {
                                          xtype: 'textfield',
                                          emptyText: 'Description',
                                          bind: {
                                              value: '{form.description}'
                                          }
                                      }
                                  ]
                              },
                              {
                                  xtype: 'fieldcontainer',
                                  fieldLabel: 'Completed Date',
                                  layout: 'hbox',
                                  margin: 5,
                                  defaults: {
                                      flex: 1,
                                      hideLabel: true,
                                      maxWidth: 250
                                  },
                                  items: [{
                                      xtype: 'datefield',
                                      emptyText: 'FROM',
                                      margin: '0 5 0 0',
                                      bind: {
                                          value: '{form.completedDateFrom}'
                                      }
                                  }, {
                                      xtype: 'datefield',
                                      emptyText: 'TO',
                                      bind: {
                                          value: '{form.completedDateTo}'
                                      }
                                  }]
                              },
                              {
                                  xtype: 'fieldcontainer',
                                  fieldLabel: 'Due Date',
                                  layout: 'hbox',
                                  margin: 5,
                                  defaults: {
                                      flex: 1,
                                      hideLabel: true,
                                      maxWidth: 250
                                  },
                                  items: [{
                                      xtype: 'datefield',
                                      emptyText: 'FROM',
                                      margin: '0 5 0 0',
                                      bind: {
                                          value: '{form.dueDateFrom}'
                                      }
                                  }, {
                                      xtype: 'datefield',
                                      emptyText: 'TO',
                                      bind: {
                                          value: '{form.dueDateTo}'
                                      }
                                  }]
                              }, {
                                  xtype: 'fieldcontainer',
                                  fieldLabel: 'Submitted Date',
                                  layout: 'hbox',
                                  margin: 5,
                                  defaults: {
                                      flex: 1,
                                      hideLabel: true,
                                      maxWidth: 250
                                  },
                                  items: [{
                                      xtype: 'datefield',
                                      emptyText: 'FROM',
                                      margin: '0 5 0 0',
                                      bind: {
                                          value: '{form.submittedDateFrom}'
                                      }
                                  }, {
                                      xtype: 'datefield',
                                      emptyText: 'TO',
                                      bind: {
                                          value: '{form.submittedDateTo}'
                                      }
                                  }]
                              },
                              {
                                  xtype: 'fieldcontainer',
                                  fieldLabel: 'Item',
                                  layout: 'hbox',
                                  margin: 5,
                                  defaults: {
                                      flex: 1,
                                      hideLabel: true,
                                      maxWidth: 250,
                                      margin: '0 5 0 0',
                                      triggers: {
                                          clear: {
                                              type: 'clear',
                                              hideWhenEmpty: true,
                                              clearOnEscape: true,
                                              weight: -1
                                          }
                                      }
                                  },
                                  items: [
                                      {
                                          xtype: 'combo',
                                          emptyText: 'CATEGORY',
                                          editable: true,
                                          forceSelection: true,
                                          queryMode: 'local',
                                          displayField: 'cateName',
                                          valueField: 'id',
                                          bind: {
                                              store: '{ticketCateStore}',
                                              value: '{form.cateId}'

                                          },
                                          listeners: {
                                              change: 'onCateChanged',
                                              beforequery: function (record) {
                                                  me.quickSearch(record);
                                              }
                                          },
                                          multiSelect: true,
                                          maxLength: 9000
                                      }, {
                                          xtype: 'combo',
                                          reference: 'subcate',
                                          emptyText: 'SUB CATEGORY',
                                          editable: true,
                                          displayField: 'display',
                                          valueField: 'id',
                                          queryMode: 'local',
                                          forceSelection: true,
                                          listeners: {
                                              change: 'onSubCateChanged',
                                              beforequery: function (record) {
                                                  me.quickSearch(record);
                                              }
                                          },
                                          bind: {
                                              store: '{ticketSubCateStore}',
                                              value: '{form.subCateId}',
                                              disabled: '{disable.subCate}'
                                          },
                                          multiSelect: true,
                                          //maxWidth: 350,
                                          maxLength: 9000,
                                          listConfig: {
                                              minWidth: 500,
                                              resizable: true,
                                              loadingText: 'Searching...',
                                              itemSelector: '.search-item',
                                              itemTpl: [
                                                  '<a >',
                                                  '<h3><span>{display}</span></h3>',
                                                  '{display1}',
                                                  '</a>'
                                              ]
                                          }
                                      }, {
                                          xtype: 'combo',
                                          reference: 'item',
                                          emptyText: 'ITEM NAME',
                                          displayField: 'display',
                                          valueField: 'id',
                                          queryMode: 'local',
                                          forceSelection: true,
                                          editable: true,
                                          bind: {
                                              store: '{ticketItemStore}',
                                              value: '{form.itemId}',
                                              disabled: '{disable.item}'
                                          },
                                          multiSelect: true,
                                          //maxWidth: 500,
                                          maxLength: 9000,
                                          listeners: {
                                              beforequery: function (record) {
                                                  me.quickSearch(record);
                                              }
                                          },
                                          listConfig: {
                                              minWidth: 500,
                                              resizable: true,
                                              loadingText: 'Searching...',
                                              itemSelector: '.search-item',
                                              itemTpl: [
                                                  '<a >',
                                                  '<h3><span>{display}</span></h3>',
                                                  '{display1}',
                                                  '</a>'
                                              ]
                                          }
                                      }
                                  ]
                              }, {
                                  xtype: 'fieldcontainer',
                                  fieldLabel: 'Root Cause',
                                  layout: 'hbox',
                                  margin: 5,
                                  defaults: {
                                      flex: 1,
                                      hideLabel: true,
                                      maxWidth: 760,
                                      margin: '0 5 0 0',
                                      triggers: {
                                          clear: {
                                              type: 'clear',
                                              hideWhenEmpty: true,
                                              clearOnEscape: true,
                                              weight: -1
                                          }
                                      }
                                  },
                                  items: [{
                                          xtype: 'ngLookup',
                                          editable: false,
                                          fieldLabel: 'Root Cause',
                                          emptyText: 'ROOT CAUSE',
                                          multiSelect: true,
                                          forceSelection: false,
                                          allowBlank: true,
                                          displayField: 'cause',
                                          valueField: 'causeId',
                                          url: 'api/ticket/lookup/root-causes',
                                          bind: {
                                              value: '{form.RootCauseId}'
                                          },
                                          maxLength: 9000
                                      }]
                              }
                          ]
                      }
                  ]
              },
              {
                  xtype: 'grid',
                  region: 'center',
                  reference: 'gd',
                  collapsible: false,
                  headerBorders: true,
                  columnLines: true,
                  border: true,
                  autoHeight: true,
                  flex: 1,

                  plugins: [{
                      ptype: 'rowexpander',
                      rowBodyTpl: new Ext.XTemplate('<p><b>Description:</b> {Description}</p>')
                  }],

                  viewConfig: {
                      stripeRows: true
                  },
                  store: store,
                  columns: me.buildGridColumns(),
                  bbar: Ext.create('Ext.PagingToolbar', {
                      displayInfo: true,
                      store: store,
                      displayMsg: 'Displaying records {0} - {1} of {2}',
                      emptyMsg: "No records to display"
                  })
              }
        ];
    },
    buildGridColumns: function () {
        return [
            {
                text: 'TICKET NO',
                locked: true,
                dataIndex: 'TicketNo',
                width: 125,
                //renderer: function (value, metaData, record) {                                    
                //    return '<a href="#ticket/' + record.data.TicketId + '">' + value + '</a>';
                //}
                //,
                renderer: function (value, metadata, record) {
                    //return me.showToolTip(value, metadata);
                    //return '<div><span style="margin-right: 5px!important;"><a href="#ticket/' + record.data.TicketId + '">' + value + '</a></span><img src="' + record.data.TypeIcon + '"/></div>';
                    //var icon = record.data.TypeIcon == null || record.data.TypeIcon == '' || record.data.TypeIcon == undefined ? 'resources/images/unidentify_type.png' : record.data.TypeIcon;
                    return '<span style="margin:0 5px 0 0!important;padding:0!important;"><a href="#ticket/' + record.data.TicketId + '">' + value + '</a></span><img style="width: 16px; height: 16px;border: 0;margin: 0; padding: 0;" src="' + record.data.TypeIcon + '" />';
                }
            },
           {
               text: 'REQUESTOR ID',
               dataIndex: 'RequestorId',
               width: 175
           },
           {
               text: 'REQUESTOR',
               dataIndex: 'Requestor',
               width: 175
           },
           {
               text: 'DEPARTMENT',
               dataIndex: 'Department',
               width: 175
           },
           {
               text: 'SUBMITTED BY',
               dataIndex: 'SubmittedBy',
               width: 175
           }, {
               text: 'SUBMITTED DATE',
               dataIndex: 'SubmittedDate',
               renderer: function (value) {
                   var date = Ext.util.Format.date(value, 'Y-m-d H:i:s');
                   return date;
               },
               width: 175
           }, {
               text: 'LAST ACTION BY',
               dataIndex: 'LastActionBy',
               width: 175
           }, {
               text: 'LAST ACTION DATE',
               dataIndex: 'LastActionDate',
               renderer: function (value) {
                   var date = Ext.util.Format.date(value, 'Y-m-d H:i:s');
                   return date;
               },
               width: 175
           }, {
               text: 'LAST ACTION',
               dataIndex: 'LastAction',
               width: 175
           }, {
               text: 'SUBJECT',
               dataIndex: 'Subject',
               width: 175
           }, {
               text: 'DESCRIPTION',
               hidden: true,
               dataIndex: 'Description',
               width: 175
           }, {
               text: 'TYPE',
               dataIndex: 'Type',
               width: 175
           }, {
               text: 'PRIORITY',
               dataIndex: 'Priority',
               width: 175
           }, {
               text: 'SLA',
               dataIndex: 'Sla',
               width: 175
           }, {
               text: 'FIRST RESPONSE MINUTES',
               dataIndex: 'FirstResponseMinutes',
               width: 175
           }, {
               text: 'FIRST RESPONSE WITHIN SLA',
               dataIndex: 'TargetFirstResponseMinutes',
               width: 175
           }, {
               text: 'ACTUAL MINUTES',
               dataIndex: 'ActualMinutes',
               width: 175
           }, {
               text: 'COMPLETED MINUTES',
               dataIndex: 'CompleteMinutes',
               width: 175
           }, {
               text: 'TARGET RESOLUTION WITHIN SLA',
               dataIndex: 'TargetCompleteMinutes',
               width: 175
           }, {
               text: 'STATUS',
               dataIndex: 'Status',
               width: 175
           }, {
               text: 'SOURCE',
               dataIndex: 'Mode',
               width: 175
           }, {
               text: 'IMPACT',
               dataIndex: 'Impact',
               width: 175,
               hidden: true
           }, {
               text: 'URGENCY',
               dataIndex: 'Urgency',
               width: 175,
               hidden: true
           }, {
               text: 'SITE',
               dataIndex: 'Site',
               width: 175
           }, {
               text: 'TEAM',
               dataIndex: 'Group',
               width: 175
           }, {
               text: 'LAST ASSIGNEE',
               dataIndex: 'LastAssignee',
               width: 175
           }, {
               text: 'CATEGORY',
               dataIndex: 'Category',
               width: 175
           }, {
               text: 'SUB CATEGORY',
               dataIndex: 'SubCategory',
               width: 175
           }, {
               text: 'ITEM',
               dataIndex: 'Item',
               width: 175
           }, {
               text: 'ESTIMATED HOURS',
               dataIndex: 'EstimatedHours',
               width: 175
           }, {
               text: 'COMPLETED DATE',
               dataIndex: 'CompletedDate',
               width: 175,
               renderer: function (value) {
                   var date = Ext.util.Format.date(value, 'Y-m-d H:i:s');
                   return date;
               }
           },
            {
                text: 'CLOSE COMMENT',
                dataIndex: 'CloseComment',
                width: 175
            }, {
                text: 'ROOT CAUSE',
                dataIndex: 'RootCause',
                width: 175
            }

        ];
    },
    quickSearch: function (record) {
        if (record.query.indexOf('+') !== -1) {
            record.query = record.query.replace('+', '\\+');
        }
        record.query = new RegExp(record.query, 'i');
        record.forceAll = true;
    }

});
