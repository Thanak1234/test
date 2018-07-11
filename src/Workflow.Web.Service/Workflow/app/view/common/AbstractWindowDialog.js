/**
 * 
 *Author : Phanny 
 */
Ext.define("Workflow.view.common.requestor.AbstractWindowDialog",{
	extend: "Ext.window.Window",
	
	
	width: 700,
	height: 400,
	modal: true, 
	mainView: null, 
	lauchFrom: null,
	cbFn: function(){
	  console.warn('Callback function needs to be implemented');  
	},

	//Force to destroy window when it is cosed.
	doClose: function() {
		var me=this;
		me.hide(me.lauchFrom, function() {
			me.destroy();
		});
	},
	
	buttons: [
		{
			xtype: 'button',
			bind : {
				text : '{submitBtText}',
                visible: '{submitBtVisible}',
                iconCls: '{btnSubmitIconCls}'
			},
			handler: 'sumbmitForm'
		},{
			xtype: 'button',
			text: 'Close',
            handler: 'closeWindow',
            bind: {
                iconCls: '{btnCloseIconCls}'
            }
		}
	],
	
	
	initComponent: function() {
		var me=this,config = {}; 
		
		// build config properties
		var items = this.buildItems();
		if(items){
			config.items = items;
		}
		
		// apply config
		Ext.apply(this, Ext.apply(this.initialConfig, config));
		
					   
		me.callParent();
	},
	
	buildItems: function(){
		return undefined;
	}
});
