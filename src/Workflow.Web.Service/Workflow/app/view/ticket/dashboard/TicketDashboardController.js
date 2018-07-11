/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.dashboard.TicketDashboardController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-dashboard-ticketdashboard',

    init: function () {
        this.loadStore(1);
    },
    onBarTipRender: function (tooltip, record, item) {
        var fieldIndex = Ext.Array.indexOf(item.series.getYField(), item.field),
            browser = item.series.getTitle()[fieldIndex];

            tooltip.setHtml(browser + ' on [' +
            record.get('itemName') + ']: ' +
            record.get(item.field) + 'ticket(s)');
    },
    onAxisLabelRender: function (axis, label, layoutContext) {
        //return label.toFixed(label);
        var value = layoutContext.renderer(label);
        return value !== '0' ? (value / 1000 + ',000') : value;
    },

    onStackGroupToggle: function (segmentedButton, button, pressed) {
        var chart = this.lookupReference('itemChart'),
            series = chart.getSeries()[0],
            value = segmentedButton.getValue();

        series.setStacked(value === 0);
        chart.redraw();
    },

    onItemToggle : function(segmentedButton, button, pressed) {
        var vm = this.getRef().vm;
        var type = button.getText();
        vm.set('itemType',type );

        this.loadStore( segmentedButton.getValue() + 1);
    },

    loadStore : function(type){
        var vm = this.getRef().vm;
        var store = vm.getStore('ticketItemStore');
        Ext.apply(store.getProxy().extraParams, {
            type: type
        });
        store.load();
    },

    onItemClickHandler: function(series , item , event){
        console.log('dd');
    }

});
