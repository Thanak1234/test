/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.view.TicketActivityList',{
    extend: 'Ext.grid.Panel',
    xtype: 'ticket-activity-listing',
    cls: 'feed-grid',
    requires: ['Ext.ux.PreviewPlugin'],
    border: true,
    //bodyPadding: '10 10 0',
    margin: 5,
    viewConfig: {
        plugins: [{
            pluginId: 'preview',
            ptype: 'preview',
            bodyField: 'description',
            previewExpanded: true
        }],
        listeners: {
            cellclick: 'onAttachmentClickHandler'
        },
        stripeRows: true
    },
    initComponent: function () {
        this.columns = [{
            text: 'Activity ',
            dataIndex: 'activityType',
            flex: 1,
            renderer: this.formatTitle,
            sortable: false
        }, {
            text: 'Date',
            dataIndex: 'createdDate',
            renderer: this.formatDate,
            width: 200
        }];

        this.callParent(arguments);
    },

    /**
     * Title renderer
     * @private
     */
    formatTitle: function (value, p, record) {        
        var more = '';
        if (value === 'TICKET_ASSIGNED') {
            var assignee = '<span class="author"><u><b>Assigned To: {0} | {1}{2}<b></u></span>';
            var empNo =  record.get('empNoAssignee');
            if(empNo){
                empNo = '(' + empNo + ')';
            }

            more = Ext.String.format(assignee, record.get('team'), record.get('assignee') || 'unassigned', empNo || '');

            if (record.get('assignedExpired')) {
                more = Ext.String.format('<strike>{0}</strike>', more);
            }
        } else if (value === 'CHANGE_STATUS') {
            more = Ext.String.format('<span class="author"><u><b>From {0}<b></u></span>', record.get('moreData'));
        } else if (value === 'MERGE_TICKET') {
            var data =  record.get('addData');
            more = Ext.String.format('<span class="author"><u><b>{0}<a href="#ticket/{1}"> #{2}</a> </b></u></span>', data['Action'], data['TicketId'], data['TicketNo']);
        } else if(value === 'SUB_TICKET_POSTING' ){
            var data =  record.get('addData');
            if(data){
                more = Ext.String.format('<span class="author"><b><u><a href="#ticket/{1}">{0} #{2}, Status: {3}</a></u> </b></span>', 'Subticket: ', data['id'], data['ticketNo'],data['status']);    
            }
            
        }else if(value == 'TICKET_POSTING'){
            var data =  record.get('addData');
            if(data){
                if(data['FormIntegrated']){                    
                    if (record.data.action == 'Edit Ticket') {
                        more = '';
                    } else {
                        var link = Ext.String.format('#{0}/SN={1}_99999', data['Url'], data['PNo']); 
                        more = Ext.String.format('<span class="author"><b><u>  <a href="{0}"> Form No: {1}</a></u> </b></span>', link, data['FormNo']);
                    }
                    
                }else{
                    more = Ext.String.format('<span class="author"><b><u>  <a href="#ticket/{1}"> {0} #{2}, Status: {3}</a></u> </b></span>', 'Main Ticket: ', data['id'], data['ticketNo'], data['status']);        
                }
            }
        }

        return Ext.String.format('<div class="topic"><b>{0}</b><span class="author">{1} by <i> {2} </i></span>{3}</div>', record.get('activityName'), record.get('action'), record.get('actionBy'), more);

    },

    /**
     * Date renderer
     * @private
     */
    formatDate: function (date, p, record) {
        if (!date) {
            return '';
        }
        var now = new Date(),
            d = Ext.Date.clearTime(now, true),
            notime = Ext.Date.clearTime(date, true).getTime();

        var value = '';
        if (notime === d.getTime()) {
            value = 'Today ' + Ext.Date.format(date, 'g:i a');
        }

        d = Ext.Date.add(d, 'd', -6);
        if (d.getTime() <= notime) {
            value = Ext.Date.format(date, 'D g:i a');
        } else {
            value = Ext.Date.format(date, 'Y/m/d g:i a');
        }
        //return Ext.Date.format(date, 'Y/m/d g:i a');

        var files = record.get('fileUpload');

        var attachDisplay = '';
        if (files && files.length > 0) {
            attachDisplay = Ext.String.format('<a href=""><i class="fa fa-paperclip" aria-hidden="true"><b>{0}</b></i></a>', files.length);
        }
        return Ext.String.format('<div><span class="author">{0}</span><br>{1}</div>', value, attachDisplay);
    }

});
