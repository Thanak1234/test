Ext.define('Workflow.ux.form.field.StatusComboField', {
    extend: 'Ext.form.field.ComboBox',
    alias: ['widget.statuscombofield'],
    config: {

    },
    fieldLabel: 'Status',
    labelWidth: 50,
    labelAlign: 'right',
    emptyText: 'Select Status',
    forceSelection: true,
    queryMode: 'Local',
    gridStore: null,
    value: 'ACTIVE',
    initComponent: function () {
        var me = this;
        me.store = Ext.create('Ext.data.Store', {
          fields: ['id', 'code', 'name'],
          data: [{'id': 0, 'code': 'ALL', 'name': 'All'}, {'id':1, 'code':'ACTIVE', 'name':'Active'}, {'id':2, 'code':'INACTIVE', 'name':'Inactive'}]
        });
        me.listeners= {
            select: function(combo, record, eOpts){
                if(me.gridStore){
                    var param =  me.gridStore.proxy.extraParams;
                    if(param){
                      param.status = record.data.code;
                    }else{
                      param = {status : record.data.code};
                    }
                    me.gridStore.proxy.extraParams = param;
                    me.gridStore.load();
                }
            }
        };
        me.callParent();

    },
    queryMode: 'local',
    displayField: 'name',
    valueField: 'code'
});
