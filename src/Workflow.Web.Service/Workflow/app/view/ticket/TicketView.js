/**
*@author : Phanny
*/
Ext.define("Workflow.view.ticket.TicketView",{
    extend: "Ext.panel.Panel",
    requires: [
        "Workflow.view.ticket.TicketViewController",
        "Workflow.view.ticket.TicketViewModel"
    ],
    xtype: 'ticket-ticketview',
    controller: "ticket-ticketview",
    viewModel: {
        type: "ticket-ticketview"
    },
    ngconfig: {
        layout: 'fullScreen'
    },
    bodyPadding: 0,
    layout: 'border',
    defaults: {
        bodyStyle: 'padding:0px'
    },
    width: '100%',
    margin: 0,
    initComponent: function () {
      var me = this;
      me.items = [];
      me.items.push(me.getTicketListGrid());
      me.dockedItems = me.buildDockedItems();
      me.callParent(arguments);
    },

    getTicketListGrid : function(){
      var me = this;
      return {xtype: 'grid',
              border: true,
              reference: 'ticketListing',
              region: 'center',
              viewConfig: {
                getRowClass: function(rec, idx, rowPrms, ds) {
                  return rec.get('assignee') ? '' : 'ticket-rec-bold';
                }
              },

              bind: {
                  store: '{ticketListingStore}'
              },
              listeners: {
                  // render : me.renderTooltip,                  
                  columnsort: function (elm, column, direction, eOpts) {
                      console.log('elm ', elm, ' column ', column, ' direction ', direction, ' eOpts ', eOpts);
                  },
                  refresh: function (elm, eOpts) {
                      console.log('elm ', elm, ' column ', column, ' direction ', direction, ' eOpts ', eOpts);
                  },
                  itemdblclick: 'onItenDoubleClickHandler',
                  itemcontextmenu: function (view, rec, node, index, e) {
                      e.stopEvent();                      
                      return false;
                  }
              },
              bbar : {
                  xtype : 'pagingtoolbar',
                  bind: {
                      store: '{ticketListingStore}'
                  },
                  dock : 'bottom',
                  displayInfo : true
              },

              columns: [
                  {
                      xtype: 'rownumberer',
                      hidden: true
                  }, {
                      xtype: 'ticketActionCol',
                      dataIndex: 'id',
                      locked: true, 
                      cls: 'tasks-icon-column-header tasks-reminder-column-header',
                      width: 30,
                      tooltip: 'Quick Actions',
                      menuPosition: 'tr-br',
                      menuDisabled: true,
                      sortable: false,
                      emptyCellText: '',
                      scope: 'controller',
                      listeners: {
                          select: 'onTicketActionHandler'
                      }
                  },
                  // {
                  //   width: 30,
                  //   renderer : me.statusMarkRenderer,
                  //   sortable: false,
                  //   dataIndex: 'status'
                  // },
                  // {
                  //   xtype: 'actioncolumn',
                  //   sortable: false,
                  //   menuDisabled: true,
                  //   align: 'center',
                  //   dataIndex: 'status',
                  //   items: [{
                  //       getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                  //           return v == true ? 'fa fa-circle action-column-blue-color' : 'action-column-blue-color';
                  //       }
                  //   },{
                  //       getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                  //           var priority = 0;
                  //           return priority == 0 ? 'fa fa-exclamation action-column-red-color' : 'action-column-red-color';
                  //       }
                  //   }]  
                  // },
                  {
                    menuDisabled: true,
                    sortable: false,
                    xtype: 'actioncolumn',
                    width: 95,
                    items: [
                      {
                        //iconCls: 'fa fa-external-link',
                        //style:'background:#115fa6;'
                        getClass: function (v, metadata, record, rowIndex, colIndex, store) {
                            
                            var statusFlag = record.get('statusFlag');
                            var toolTipText =  statusFlag;
                            var cls = 'action-column-blue-color';

                            if( statusFlag ==='ACTIVE' ){
                              cls = 'action-column-blue-color';
                              toolTipText ='Active';
                            // }

                            // else if(statusFlag==='OnHold' ){
                            //   oolTipText ='On Hold';
                            //   cls = 'action-column-darkred-color';

                            // }else if( statusFlag ==='OverDue' ){
                            //   toolTipText ='Overdue';
                            //   cls = 'action-column-orage-color';
                            }else if(statusFlag ==='REMOVED'){
                                cls = 'action-column-black-color';
                                toolTipText ='Removed';
                            }else if(statusFlag ==='INACTIVE'){
                                toolTipText ='Inactive';
                                cls = 'action-column-green-color';
                            }

                            var moreInfo = null;  
                            if(record.get('fsViolence') ==1){
                              moreInfo = 'First Response';
                            }

                            if(record.get('odViolence') ==1){

                              if(moreInfo){
                                moreInfo += ' and Overdue';
                              }else{
                                moreInfo = 'Overdue';
                              }
                            }  

                            if(moreInfo){
                              moreInfo =  '(' + moreInfo + ' Violence' + ')';
                            }

                            metadata.tdAttr = Ext.String.format('data-qtip="{0} {1}"',toolTipText,  moreInfo);
                            return 'fa fa-circle ' + cls;
                        }
                    },{
                        getClass: function (v, metadata, record, rowIndex, colIndex, store) {

                          var isMain = record.get('isMain');
                          var isSub = record.get('isSub');

                          if(isMain){
                            return 'fa fa-arrow-up action-column-green-color';
                          }

                          if(isSub){
                            return 'fa fa-arrow-down action-column-green-color';
                          }

                        }
                    },{
                      getClass: function (v, metadata, record, rowIndex, colIndex, store) {
                           // var statusFlag = record.get('statusFlag');
                           // if('OverFirstResponse' === statusFlag || 'OverDue' === statusFlag){
                        var statusFlag = record.get('fsViolence');
                        if(statusFlag ==1 ) { 
                          return 'fa fa-exclamation action-column-red-color';  
                        }
                      }
                    },{
                      getClass: function (v, metadata, record, rowIndex, colIndex, store) {
                           // var statusFlag = record.get('statusFlag');
                           // if('OverFirstResponse' === statusFlag || 'OverDue' === statusFlag){
                        var odViolence = record.get('odViolence');
                        if(odViolence ==1 ) { 
                          return 'fa fa-exclamation-circle action-column-red-color';  
                        }
                      }

                    }, {
                        getClass: function (v, metadata, record, rowIndex, colIndex, store) {
                            // render source as icon
                            var source = record.get('source').toLowerCase();
                            if (source == 'phone call') {
                                return 'fa fa-phone-square action-column-blue-color';
                            } else if (source == 'web form'){
                                return 'fa fa-globe action-column-blue-color';
                            } else if (source == 'k2 integration'){
                                return 'fa fa-credit-card action-column-blue-color';
                            } else if (source == 'email') {
                                return 'fa fa-envelope action-column-blue-color';
                            }
                        }

                    }
                    //    , {
                    //    getClass: function (v, metadata, record, rowIndex, colIndex, store) {
                                
                    //        var ticketType = record.get('ticketType').toLowerCase();
                    //        if ('incident' == ticketType){
                    //            return 'combo-incident';
                    //        } else if ('request for information' == ticketType) {
                    //            return 'combo-change-info';
                    //        } else if ('change request' == ticketType) {
                    //            return 'combo-change-status';
                    //        } else if ('task' == ticketType) {
                    //            return 'combo-task';
                    //        }
                    //    }
                    //}
                    ]
                  },
                  {
                      text: 'Ticket #',
                      width: 83,
                      sortable: true,
                      locked: true,
                      dataIndex: 'ticketNo',
                      renderer: function (value, metadata, record) {
                          //return me.showToolTip(value, metadata);
                          var icon = record.data.ticketTypeIcon == null || record.data.ticketTypeIcon == '' || record.data.ticketTypeIcon == undefined ? 'resources/images/unidentify_type.png' : record.data.ticketTypeIcon;
                          return '<span style="margin:0 5px 0 0!important;padding:0!important;">' + value + '</span><img style="width: 16px; height: 16px;border: 0;margin: 0; padding: 0;" src="' + icon + '" />';
                      }
                  }, {
                      text: 'Subject',
                      flex: 1,
                      sortable: true,
                      minWidth: 200,
                      dataIndex: 'subject',
                      renderer: function (value, metadata, record) {
                          return me.showToolTip(value, metadata);                      
                      }
                  }, {
                      text: 'Created Date',
                      width: 130,
                      sortable: true,
                      dataIndex: 'createdDate',
                      renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                      bind: {
                          width: '{columnDateWidth}'
                      }
                  }, {
                      text: 'Requestor',
                      width: 150,
                      sortable: true,
                      dataIndex: 'requestor',
                      renderer: function (value, metadata, record) {
                          return me.showToolTip(value, metadata);
                      }
                  },{
                      text: 'Assigned To',
                      width: 150,
                      sortable: true,
                      dataIndex: 'assignee',                      
                      renderer: function (value, metadata, record) {
                          var valueTxt = (value ? value : 'UnAssigned') + '/' + record.get('teamName');
                          return me.showToolTip(valueTxt, metadata);
                      }                 
                  },{
                      text: 'Status',                      
                      sortable: true,
                      dataIndex: 'status',
                      bind: {
                          width: '{columnStatusWidth}'
                      },
                      renderer: function (value, metadata, record) {
                          return me.showToolTip(value, metadata);
                      }
                  }, {
                      text: 'Type',
                      width: 75,
                      sortable: true,
                      dataIndex: 'ticketType',
                      renderer: function (value, metadata, record) {
                          return me.showToolTip(value, metadata);
                      }
                  },{
                      text: 'Priority',
                      width: 75,
                      sortable: true,
                      dataIndex: 'priority',
                      renderer: function (value, metadata, record) {
                          return me.showToolTip(value, metadata);
                      }
                  }, {
                      text: 'SLA',
                      width: 75,
                      sortable: true,
                      dataIndex: 'sla',
                      renderer: function (value, metadata, record) {
                          return me.showToolTip(value, metadata);
                      }
                  },{
                      text: 'Wait Actioned By',
                      width: 120,
                      sortable: true,
                      dataIndex: 'waitActionedBy',
                      renderer: function (value, metadata, record) {
                          return me.showToolTip(value, metadata);
                      }
                  }]
          };
    },
    
    /****Renderer****/
   //  statusMarkRenderer : function (value, metadata, record) {
   //    var statusFlag = record.get('statusFlag');
   //    var toolTipText =  statusFlag;
     
   //    var color = 'background:#115fa6;';

   //    if( statusFlag ==='UnAssigned' ){
   //      color = 'background:#115fa6;';
   //      toolTipText ='Unassigned';
   //    }else if(statusFlag==='OnHold' ){
   //      oolTipText ='On Hold';
   //      color = 'background:#a61120;';
   //    }else if( statusFlag ==='OverDue' ){
   //      toolTipText ='Overdue';
   //      color = 'background:#ff8809;;';
   //    }else if(statusFlag ==='Removed'){
   //        color = 'background:#111;';
   //    }else if(statusFlag ==='Closed'){
   //      color = 'background:green;';
   //    }
    
   //    var isMain = record.get('isMain');
   //    var isSub = record.get('isSub');

   //    var subTiketMarked = 'a';
   //    var subTiketTitle = '';
   //    if(isMain) {
   //      subTiketMarked = '<i class="fa fa-long-arrow-up"" aria-hidden="true" style="font-size:15px;color:#18D1E9"></i>';
   //      subTiketTitle = '( Main Ticket)';
   //    }else if(isSub){
   //      subTiketMarked = '<i class="fa fa-long-arrow-down" aria-hidden="true" style="font-size:15px;color:#18D1E9"></i>'
   //      subTiketTitle = '( Subticket)';
   //    }
   // // metadata.tdAttr = Ext.String.format('data-qtip="{0} {1} "',toolTipText, subTiketTitle);

   //    return Ext.String.format('<div height = "100%"><span class="x-legend-item-marker" style="{0}"></span>{1}</div>',color, subTiketMarked);
   //  },

    assignToRenderer: function (value, metadata, record) {
        var me = this,
            valueTxt = (value ? value : 'UnAssigned') + '/' + record.get('teamName')
            ;
        
        return me.showToolTip(valueTxt, metadata);      
    },
    showToolTip: function (value, metadata) {        
        value ? metadata.tdAttr = 'data-qtip="' + value + '"' : '';
        return value;
    },
    buildDockedItems: function () {
        var allowAutoRefresh = false;
        var currentUser = Workflow.global.UserAccount.identity;
        if (currentUser && currentUser.roles) {
            allowAutoRefresh = Ext.Array.contains(currentUser.roles.split(','), "[APPLICATION].[DASHBOARD].[TICKET]");
        }

        var me = this;
        var viewmodel = me.getViewModel();
        var ticketListingStore = viewmodel.getStore('ticketListingStore');
        return [{
            xtype: 'toolbar',
            //margin  : '5 0 5 0',
            items: [{
                type: 'button',
                text: 'New Ticket',
                iconCls: 'icon fa-plus-circle ',
                handler: 'onNewTicketByUserActionListener',
                scope: 'controller',
                bind: {
                    hidden: '{isAgent}'
                }

            }, {
                type: 'button',
                text: 'New Ticket',
                iconCls: 'fa fa-plus-circle',
                bind: {
                    hidden: '{!isAgent}'
                },
                menu: [{
                    text: 'As Agent',
                    itemId: 'agent',
                    iconCls: 'fa fa-user-md',
                    handler: 'onNewTicketActionListener',
                    scope: 'controller'
                }, {
                    text: 'As User',
                    itemId: 'user',
                    iconCls: 'fa fa-user',
                    handler: 'onNewTicketActionListener',
                    scope: 'controller'
                }]
                }, '->', {
                    xtype: 'tickettypecombofield',
                    gridStore: ticketListingStore,
                    grid: me,
                    multiSelect: true
                },{
                text: 'All Active Tickets',

                reference: 'ticketFilterButton',
                showText: true,
                width: 200,
                textAlign: 'left',

                menu: []
            },
            {
                xtype: 'label',
                text: 'Or'
            },
            {
                xtype: 'textfield',
                width: 300,
                bind: '{keyword}',
                emptyText: 'Search Any Tickets (ticket number, subject)',
                triggers: {
                    clear: {
                        cls: 'x-form-clear-trigger',
                        handler: 'onNavFilterClearTriggerClick',
                        hidden: true,
                        scope: 'controller'
                    },
                    search: {
                        cls: 'x-form-search-trigger',
                        weight: 1,
                        handler: 'onFilterHandler',
                        scope: 'controller'
                    }
                },

                listeners: {
                    specialkey: 'onEnterFilterHandler',
                    change: 'onNavFilterChanged'
                }
            },
            {
                xtype: 'button',
                iconCls: 'fa fa-refresh',
                handler: 'onFilterHandler',
                tooltip: 'Refresh Now'
            },
            {
                xtype: 'button',
                //iconCls : 'fa fa-play',
                cls: 'notification-icon',
                hidden: !allowAutoRefresh,
                bind: {
                    iconCls: '{autoRefreshIcon}'
                },
                handler: 'onRefreshHandler',
                tooltip: 'Auto Refresh'
            }]
        }];
    }
});
