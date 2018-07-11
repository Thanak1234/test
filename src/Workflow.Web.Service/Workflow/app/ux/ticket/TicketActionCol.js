Ext.define('Workflow.ux.ticket.TicketActionCol', {
    extend: 'Ext.grid.column.Column',
    xtype: 'ticketActionCol',
    
    config: {
        /**
         * @cfg {String} menuPosition
         * Positing to show the menu relative to the reminder icon.
         * Alignment position as used by Ext.Element.getAlignToXY
         * Defaults to 'tl-bl'
         */
        menuPosition: 'tl-bl'
    },

    //tdCls: Ext.baseCSSPrefix + 'grid-cell-remindercolumn',

    /**
     * @event select
     * Fires when a reminder time is selected from the dropdown menu
     * @param {Ext.data.Model} record    The underlying record of the row that was clicked to show the reminder menu
     * @param {String|Number} value      The value that was selected
     */

    /**
     * initializes the dropdown menu
     * @private
     */

    initMenu: function () {
        var me = this;
        me.menu = Ext.create('Ext.menu.Menu', {
            plain: true
        });
    },

    /**
     * Handles a click on a menu item
     * @private
     * @param {Ext.menu.Item} menuItem
     * @param {Ext.EventObject} e
     * @param {Object} options
     * @param {String|Number} value
     */
    handleMenuItemClick: function (menuItem, options, e, value) {             
        if (value.el == undefined) {
            value.el = { iconCls: menuItem.iconCls };
        } else {
            value.el.iconCls = menuItem.iconCls;
        }
        this.fireEvent('select', this.record, value);
    },

    /**
     * Process and refire events routed from the GridView's processEvent method.
     * @private
     */
    processEvent: function (type, view, cell, rowIndex, colIndex, e) {
        var me = this,
            cssPrefix = Ext.baseCSSPrefix,
            target = Ext.get(e.getTarget());
        if (type === 'click') {
            if (!me.menu) {
                me.initMenu();
            } else {
                me.menu.removeAll();
            }
            
            //Add more item
            
            me.record = view.store.getAt(rowIndex);
            me.getAvailableActions(cell, me.record);
            me.menu.showBy(target);
        } else {
            return me.callParent(arguments);
        }
    },

    /**
     * Renderer for the reminder column
     * @private
     * @param {Number} value
     * @param {Object} metaData
     * @param {SimpleTasks.model.Task} task
     * @param {Number} rowIndex
     * @param {Number} colIndex
     * @param {SimpleTasks.store.Tasks} store
     * @param {Ext.grid.View} view
     */
    renderer: function (value, metaData, item, rowIndex, colIndex, store, view) {

        var cssPrefix = Ext.baseCSSPrefix,
            cls = [cssPrefix + 'grid-reminder'];

        //if(task.get('done') || !task.get('due')) {
        //    // if the task is done or has no due date, a reminder cannot be set
        //    return '';
        //}

        if (!value) {
            cls.push(cssPrefix + 'grid-reminder-empty');
        }
        //return '<div class="' + cls.join(' ') + '"></div>';

        return '<i class="fa fa-tasks" aria-hidden="true"></i>'
        
    },


    getAvailableActions: function(cell, ticket){
        var me = this;
        me.menu.add(me.createItem('Open', { ticket: ticket, activityCode: 'OPEN'}));
        me.menu.add({ xtype: 'menuseparator' });
        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/ticket/loadActions/' + ticket.getId() +'/',
            headers: { 'Content-Type': 'application/json' },
            //params: { by: by },
            method: 'GET',
            success: function (response) {
                var datas = Ext.JSON.decode(response.responseText);
                var groupName = null
                if (datas) {
                    datas.map(function (item) {
                        
                        if (groupName && groupName !== item.groupName) {
                            me.menu.add({ xtype: 'menuseparator' });
                        }

                        me.menu.add(me.createItem(item.name, { ticket: ticket, activityCode: item.activityCode, el: cell }));
                        groupName = item.groupName;

                    });
                }
            },
            failure: function (opt, operation) {
                var response = Ext.decode(opt.responseText);
                Ext.MessageBox.show({
                    title: 'Error',
                    msg: response.Message,
                    buttons: Ext.MessageBox.OK,
                    icon: Ext.MessageBox.ERROR
                });
            }
        });

    },

    createItem: function (text, value) {
        var me = this;
        return {
            text: text,
            iconCls:  me.getActionIcon(value.activityCode),
            listeners: {
                click: Ext.bind(me.handleMenuItemClick, me, [value], true)
            }
        }
    },

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
        else if('SUB_TICKET_POSTING' === code) {
            return 'fa fa-plus-circle';
        }
        else{
            return 'fa fa-dot-circle-o';
        }
    }

});