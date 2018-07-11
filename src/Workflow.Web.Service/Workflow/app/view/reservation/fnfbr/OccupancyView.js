/**
 *Author : Phanny 
 *
 */
Ext.define("Workflow.view.fnfbr.OccupancyView", {
    extend: "Ext.grid.Panel",
    xtype: "fnf-occupancyview",
    
    requires: [
        "Workflow.view.fnfbr.OccupancyViewController",
        "Workflow.view.fnfbr.OccupancyViewModel"
    ],
    title: 'Occupancy',
    iconCls: 'fa fa-binoculars',
    controller: "fnf-occupancyview",
    viewModel: {
        type: "fnf-occupancyview"
    },
    bind: {
        selection: '{selectedItem}',
        hidden: '{!reservationOnly}'
    },
    viewConfig: {
        enableTextSelection: true
    },
    stateful: true,
    collapsible: true,
    headerBorders: false,
    plugins: {
        ptype: 'rowediting',
        clicksToEdit: 2,
        listeners: {
            beforeedit: function (e, context) {
                var actioncolumnEdit = context.grid.getReferences().actioncolumnEdit;
                if (actioncolumnEdit.hidden) {
                    return false;
                }
                return true;
            }
        }
    },
    initComponent: function () {
        var me = this;

        me.columns = [{
            xtype: 'rownumberer',
            width: 60,
            align: 'center',
            header: 'NO.'
        }, {
            text: 'DATE',
            flex: 1,
            align: 'right',
            renderer: Ext.util.Format.dateRenderer('d/m/Y'),
            sortable: true,
            dataIndex: 'checkDate'
        }, {
            xtype: 'numbercolumn',
            text: 'OCCUPANCY (%)',
            width: 150,
            format: '00%',
            sortable: true,
            dataIndex: 'occupancy',
            editor: {
                xtype: 'numberfield',
                allowBlank: true,
                minValue: 0,
                maxValue: 100,
                listeners: {
                    blur: function () {
                        var editor = me.plugins[0];
                        editor.completeEdit();
                    }
                }
            }
        }, {
            xtype: 'actioncolumn',
            header: 'ACTION',
            align: 'center',
            width: 80,
            bind: {
                hidden: '{!editable}'
            },
            reference: 'actioncolumnEdit',
            items: [{
                iconCls: 'fa fa-plus-circlefa fa-pencil-square-o',
                tooltip: 'Edit',
                handler: function (grid, rowIndex, colIndex) {
                    var rec = grid.getStore().getAt(rowIndex);
                    var editor = grid.up('grid').plugins[0];
                    editor.startEdit(rec, 2); 
                }
            }]
        }];

        me.callParent(arguments);
    }
});
