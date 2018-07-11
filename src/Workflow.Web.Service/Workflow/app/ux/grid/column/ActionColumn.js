Ext.define('Workflow.ux.grid.column.ActionColumn', {
    extend: 'Workflow.ux.grid.LiveSearchGridPanel',
    xtype: 'ng-actioncolumn',
    config: { menuPosition: 'tl-bl' },
    initMenu: function () {
        var me = this;
        me.menu = Ext.create('Ext.menu.Menu', {
            plain: true
        });
    },
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

            me.record = view.store.getAt(rowIndex);
            me.getAvailableActions(cell, me.record);

            console.log('target', target);
            e.stopEvent();
            var xy = e.getXY(), x = xy[0], y = xy[1];
            me.menu.showAt([x, y]);
            //me.menu.showBy(target);
        } else {
            console.log('type', type);
            return me.callParent(arguments);
        }
    },
    renderer: function (value, metaData, item, rowIndex, colIndex, store, view) {
        console.log('renderer');
        var cssPrefix = Ext.baseCSSPrefix,
            cls = [cssPrefix + 'grid-reminder'];

        if (!value) {
            cls.push(cssPrefix + 'grid-reminder-empty');
        }

        return '<i class="fa fa-chevron-right" aria-hidden="true"></i>'

    },
    getAvailableActions: function (cell, record) {
        this.menu = [];
    }
});