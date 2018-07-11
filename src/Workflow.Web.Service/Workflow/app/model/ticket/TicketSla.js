Ext.define('Workflow.model.ticket.TicketSla', {
    extend: 'Ext.data.Model',
    fields: [
        'id',
        'slaName',
        'description',        
        'gracePeriod',
        'firstResponsePeriod',
        'status',
        {
            name: 'gracePeriodFormated',
            mapping: 'gracePeriod',
            convert: function (secs, model) {
                return model.renderDateStr(secs);
            }
        },
        {
            name: 'firstResponsePeriodFormated',
            mapping: 'firstResponsePeriod',
            convert: function (secs, model) {
                return model.renderDateStr(secs);                
            }
        }
    ],
    proxy: {
        
        type : 'rest',
        api : {
            update: 'api/ticket/setting/sla/update',
            create : 'api/ticket/setting/sla/create', 
            destroy: 'api/ticket/setting/sla/delete'
        },
        reader :{
                type : 'json',
                rootProperty: 'data',
                totalProperty: 'totalCount'
        }
    },
    renderDateStr: function (value) {
        var obj = this.renderToDate(value);
        return obj.day + 'd ' + obj.hour + 'h ' + obj.minute + 'm';
    },
    renderToDate: function (secs) {
        var obj = { day: 0, hour: 0, minute: 0 };
        var sec_num = parseInt(secs, 10);

        obj.day = Math.floor((secs / 86400) % 60);
        var daySec = obj.day * 24 * 3600;
        obj.hour = Math.floor((sec_num - daySec) / 3600);
        obj.minute = Math.floor((sec_num - (daySec + obj.hour * 3600)) / 60);
        return obj;
    }
});
