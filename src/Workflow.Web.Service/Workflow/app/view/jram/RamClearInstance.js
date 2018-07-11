/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.jram.RamClearInstance", {
    extend: "Workflow.view.FormComponent",
    xtype: 'jram-ramclear-instance',
    modelName: 'ramClear',
    layout: 'anchor',
    defaults: {
        anchor: '100%'
    },
    loadData: function (data, viewSetting) {
        var me = this,
            viewmodel = me.getViewModel(),
            reference = me.getReferences();

        if (data) {
            viewmodel.set('ramClear', data.ramClear);
        }
    },
    afterRender: function(){
        var me = this;
        vm = me.getViewModel();

        vm.set('ramClear.disabledDescr', vm.get('ramClear.disabledDescr')?vm.get('ramClear.disabledDescr'): true);
        me.callParent(arguments);
    },
    buildComponent: function () {
        var me = this;

        return [{
            xtype: 'checkboxgroup',
            cls: 'x-check-group-alt',
            columns: [450, 450],
            vertical: true,
            bind: {
                value: '{ramClear.instances}'
            },
            listeners: {
                change: function (field, v, oldValue, eOpts) {
                    var vm = me.getViewModel();
                    var disabledDescr = !(v.instances == 7 || (v.instances && v.instances.length > 0 && v.instances.indexOf(7) > -1));
                    vm.set('ramClear.disabledDescr', disabledDescr);
                }
            },
            items: [{
                boxLabel: 'Failure of Self Audit Check',
                name: 'instances',
                inputValue: 1
            }, {
                boxLabel: 'Any EGM/Terminal (in Freeze mode)',
                name: 'instances',
                inputValue: 2
                //checked: true
            }, {
                boxLabel: 'Test new software',
                name: 'instances',
                inputValue: 3
            }, {
                boxLabel: 'Game conversion',
                name: 'instances',
                inputValue: 4
            }, {
                boxLabel: 'Denomination change',
                name: 'instances',
                inputValue: 5
            }, {
                boxLabel: 'Data corruption',
                name: 'instances',
                inputValue: 6
            }, {
                boxLabel: 'Others (description below)',
                name: 'instances',
                inputValue: 7
            }, {
                boxLabel: 'Processing VOID game',
                name: 'instances',
                inputValue: 8
            }, {
                boxLabel: 'Incorrect settings of Max bet',
                name: 'instances',
                inputValue: 9
            }, {
                boxLabel: 'Recoverable Error',
                name: 'instances',
                inputValue: 10
            }, {
                boxLabel: 'Reset credit Error',
                name: 'instances',
                inputValue: 11
            }, {
                boxLabel: 'Non critical Ram corrupted',
                name: 'instances',
                inputValue: 12
            }, {
                boxLabel: 'Change PC main board',
                name: 'instances',
                inputValue: 13
            }]
        }, {
            xtype: 'textarea',
            margin: '20 0 10',
            bind: {
                disabled: '{ramClear.disabledDescr}',
                value: '{ramClear.descr}'
            }
        }];
    }
});