Ext.define("Workflow.view.common.flowviewer.flowview", {
	extend: "Ext.panel.Panel",
	xtype: 'common-flowview',
	layout: 'fit',
	margin: '3 0 0 0',
	bodyBorder: true,
	processId: 0,
	viewModel: {
		type: 'common-flowview'
	},
	header: {
		hidden: true
	},
	scrollable: true,
	colors: {
		blue: '#4FC3F7',
		green: '#8BC34A',
		white: '#FFF',
		black: '#000',
		red: '#F44336',
		bluegray: '#BDBDBD',
		yellow: '#FFC107'
	},
	initComponent: function () {
		var me = this;
		me.buildItems();
		me.buildToolbars();
		me.callParent(arguments);
		me.loadVersionButton();
	},
	graphContainer: null,
	graph: null,
	buildItems: function () {
		var me = this;
		me.items = [
			{
				style: "overflow: scroll; border: 1 solid red",
				listeners: {
					afterrender: function (el, opts) {
						me.graphContainer = el;
						me.buildGraph(el);
					}
				}
			}
		];
	},
	buildGraph: function (el) {
		var me = this;
		var viewmodel = me.getViewModel();
		if (!Ext.isEmpty(me.processId) && me.processId != 0) {
			var paper = null;

			if (!el) {
				el = me.graphContainer;
			}

			if (!me.graph) {
				paper = new Raphael(el.getEl().dom, 1500, 2000);
				me.graph = paper;
				viewmodel.set('graph', paper);
			}
			else {
				paper = me.graph;
				paper.clear();
			}

			me.getEl().mask("Loading data...");
			Ext.Ajax.request({
				method: 'GET',
				url: Workflow.global.Config.baseUrl + 'api/worklists/flowjson?procInstId=' + me.processId,
				success: function (response, operation) {
					//paper.setStart();
					var data = Ext.JSON.decode(response.responseText);
					if (data) {
						me.buildLines(paper, data.lines);
						me.buildActivities(paper, data.activities);
					}
					//var st = paper.setFinish();
					//var box = st.getBBox(true);
					//paper.setSize(box.width, box.height);
					me.getEl().unmask();
				},
				failure: function (data, operation) {
					me.getEl().unmask();
					var response = Ext.decode(data.responseText);
					Ext.MessageBox.show({
						title: 'Error',
						msg: response.Message,
						buttons: Ext.MessageBox.OK,
						icon: Ext.MessageBox.ERROR
					});
				}

			});
		}
	},
    executeAction: function(actionName){
        var me = this;
        Ext.MessageBox.prompt(actionName + ':: By User', 'Please enter impassionate user:', function(btn, val){
            if(btn == "ok"){
                Ext.Ajax.request({
		            method: 'GET',
		            url: Workflow.global.Config.baseUrl + 'api/worklists/execute',
                    params: {
                        user: val,
                        procInstId: me.processId,
                        actionName: actionName
                    },
		            success: function (response, operation) {
			            var data = Ext.JSON.decode(response.responseText);
					    console.log('data', data);
		            }
	            });
            }
        }, this);
        
    },
	buildToolbars: function () {
		var me = this;

		var store = Ext.create('Ext.data.ArrayStore', {
			autoLoad: true,
			data: [
				['125%', .75],
				['100%', 1],
				['75%', 1.25],
				['50%', 2]
			],
			fields: [
				'name',
				'val'
			]
		});

		me.tbar = [
			{
				xtype: 'combo',
				fieldLabel: 'Zoom',
				displayField: 'name',
				valueField: 'val',
				store: store,
				disabled: false,
				editable: false,
				labelWidth: 80,
				bind: {
					value: '{percentag}',
					disabled: '{!graph}'
				},
				listeners: {
					change: function (el, val) {
						if (val && val != 0) {
						    var paper = me.graph;
						    if (paper && paper.width && paper.height) {
						        var width = paper.width * val;
						        var height = paper.height * val;
						        paper.setViewBox(0, 0, width, height);
						    }
						}
					}
				}
			},
			'|', {
				xtype: 'button',
				text: 'Refresh',
				margin: '0 18 0 0',
				iconCls: 'fa fa-refresh',
				disabled: false,
				bind: {
					disabled: '{!graph}'
				},
				handler: function () {
					me.buildGraph();
				}
			}, {
				xtype: 'button',
				text: 'Action',
				margin: '0 18 0 0',
				hidden: true,
				iconCls: 'fa fa-tasks',
                menu: [{
                    text:'Approved',
                    iconCls: 'fa fa-tasks',
                    handler: function(){
                        me.executeAction('Approved');
                    }
                },{
                    text:'Reviewed',
                    iconCls: 'fa fa-tasks',
                    handler: function(){
                        me.executeAction('Reviewed');
                    }
                },{
                    text:'Rejected',
                    iconCls: 'fa fa-tasks',
                    handler: function(){
                        me.executeAction('Rejected');
                    }
                },{
                    text:'Cancelled',
                    iconCls: 'fa fa-tasks',
                    handler: function(){
                        me.executeAction('Cancelled');
                    }
                }]
				
			}, {
			    xtype: 'splitbutton',
			    text: 'Resolve',
			    itemId: 'btnRepair',
			    iconCls: 'fa fa-wrench',
			    menu: new Ext.menu.Menu(),
			    procId: 0,
                //hidden: true,
			    handler: function (button) {
			        me.repairProcessError(button.procId);
			    }
			}
			//{
			//	xtype: 'button',
			//	text: 'Export PDF',
			//	iconCls: 'fa fa-download',
			//	margin: '0 18 0 0',
			//	disabled: false,
			//	bind: {
			//		disabled: '{!graph}'
			//	},
			//	handler: function () {
			//		var paper = me.graph;
			//		var canvas = document.createElement('canvas');
			//		canvg(canvas, paper.toSVG());
			//		var imgData = canvas.toDataURL('image/png');
			//		var pdf = new jsPDF('portrait', 'pt', "letter");
			//		pdf.addImage(imgData, 'PNG', 0, 0, 800, 1000);
			//		pdf.save(me.generateGuid() + '.pdf');
			//	}
			//}
		];
	},
	buildActivities: function (graph, activities) {
		var me = this;
		Ext.each(activities, function (act, index) {
			me.drawActivity(graph, act);
		});
	},
	buildLines: function (graph, lines) {
		var me = this;
		Ext.each(lines, function (line, index) {
			var condidate = me.getCordinate(line.Points);
			graph.path(condidate).attr({ fill: "none", "arrow-end": "block-wide-long", "stroke-width": 2, stroke: line.Color });
			var text = graph.text(line.Label.Rect.X, line.Label.Rect.Y, line.Label.Name).attr({ stroke: "none", opacity: 1, "font-size": 11, 'text-anchor': 'start', "font-weight": "bold" });
			var box = text.getBBox();
			var rect = graph.rect(box.x, box.y, box.width, box.height).attr({ 'fill': '#FFF', stroke: "none" })
			text.toFront();
		});
	},
	getCordinate: function (points) {
		var first = true;
		var cordinate = '';
		for (var i = 0; i < points.length; i++) {
			if (first) {
				cordinate = Ext.String.format('M{0},{1} ', points[i].X, points[i].Y);
				first = false;
			} else {
				cordinate += Ext.String.format('L{0},{1} ', points[i].X, points[i].Y);
			}
		}
		return cordinate;
	},
	onActivityClick: function (bg, act) {
		var me = this;
		Ext.getBody().mask("Loading data...");
		Ext.Ajax.request({
			url: Workflow.global.Config.baseUrl + 'api/worklists/participants?procInstId=' + me.processId + '&actInstId=' + act.ActInstID,
			success: function (response, operation) {
				var content = Ext.JSON.decode(response.responseText);
				var store = Ext.create('Ext.data.Store', {
					fields: ['User', 'StartDate', 'FinishDate', 'Action', 'Active'],
					data: content.data,
					proxy: {
						type: 'memory',
						reader: {
							type: 'json',
							rootProperty: 'data',
							totalProperty: 'total'
						}
					}
				});
				var win = Ext.create('Workflow.view.common.flowviewer.ActivityWindow', {
				    title: Ext.String.format('Activity Name - {0}', act.Name),
				    procInstId: me.processId,
					store: store,
					animateTarget: bg.node.id
				});

				win.show();
				Ext.getBody().unmask();
			},
			failure: function (data, operation) {
				Ext.getBody().unmask();
				var response = Ext.decode(data.responseText);
				Ext.MessageBox.show({
					title: 'Error',
					msg: response.Message,
					buttons: Ext.MessageBox.OK,
					icon: Ext.MessageBox.ERROR
				});
			}
		});
	},
	getActColor: function (status) {
		var colors = this.colors;
		var color = colors.bluegray;
		switch (status) {
			case 1:
				color = colors.bluegray;
				break;
			case 2:
				color = colors.blue;
				break;
			case 3:
				color = colors.yellow;
				break;
			case 4:
				color = colors.green;
				break;

		}

		return color;
	},
	getEventColor: function (status) {
	    var me = this;
		var colors = this.colors;
		var color = colors.bluegray;
		if (status == 3 || status == 2) {
			color = colors.green;
		}
		else if (status == 1) {
		    color = colors.blue;
		} else if (status == 4) {
		    var btnRepair = me.query('button[itemId=btnRepair]')[0];
		    //btnRepair.setVisible(true);
		    color = colors.red;
		}
		return color;
	},
	drawActivity: function (graph, act) {
		var me = this;
		var cursor = 'normal';
		var fontSize = 12;
		var opacity = 0.8;
		var group = graph.set();
		var title = null;
		var body = null;

		var color = me.getActColor(act.Status);

		if (act.ActInstID != 0 && act.Name != "Start") {
			cursor = 'pointer';
		}

		var attr = { cursor: cursor, "stroke-width": 1, fill: color, stroke: color, opacity: opacity };
		attr.opacity = 0.4;

		var background = graph.rect(act.X, act.Y, act.Width, act.Height).attr(attr);

		attr.opacity = opacity;

		var titleBg = graph.rect(act.X, act.Y, act.Width, act.Height).attr(attr);

		if (act.Name == "Start") {
			me.drawIcon(graph, '\uf04b', act.X + 10, act.Y + 20, 25, '#ECEFF1', 1);
			body = graph.rect(act.X + 5, act.Y + 40, act.Width - 10, act.Height - 40)
				.attr({ cursor: cursor, "stroke-width": 5, fill: 'white', opacity: opacity, stroke: 'none' });

			title = graph.text(act.X + act.Width / 2, act.Y + 20, act.Name)
				.attr({ opacity: opacity, fill: me.colors.black, "font-size": fontSize, "font-weight": "bold" });

		} else {
		    me.drawIcon(graph, '\uf085', act.X + 5, act.Y + 15, 18, '#263238', 0.5);
			body = graph.rect(act.X + 5, act.Y + 30, act.Width - 10, act.Height - 35)
				.attr({ cursor: cursor, "stroke-width": 5, fill: 'white', opacity: opacity, stroke: 'none' });

			title = graph.text(act.X + act.Width / 2, act.Y + 15, act.Name)
				.attr({ cursor: cursor, opacity: opacity, fill: me.colors.black, "font-size": fontSize, "font-weight": "bold" });

			var y = act.Y + 43;
			var x = act.X + 30;
			Ext.each(act.Events, function (evt, index) {
				var innerColor = me.getEventColor(evt.Status);
				var event = me.drawEvent(graph, evt.Name, x, y, innerColor, opacity, cursor);
				group.push(event);
				y += 20;
				if (innerColor == me.colors.red) {
				    var colorAttr = { cursor: cursor, "stroke-width": 1, fill: innerColor, stroke: innerColor, opacity: opacity };
				    background.attr(colorAttr);
				    titleBg.attr(colorAttr);
				}
			});
		}
		group.push(
			background,
			titleBg,
			title,
			body
		);

		titleBg.node.id = me.generateGuid();

		if (act.ActInstID != 0 && act.Name != "Start") {
			group.click(function () {
				me.onActivityClick(titleBg, act);
			});

			group.mouseover(function () {
				background.stop().animate({ transform: "s1.03 1.03" }, 5, "elastic");
			});

			group.mouseout(function () {
				background.stop().animate({ transform: "" }, 5, "elastic");
			});
		}
	},
	generateGuid: function () {
		var length = 9;
		return Math.random().toString(36).replace(/[^a-z]+/g, '').substr(0, length);
	},
	drawIcon: function (graph, name, x, y, size, color, opacity) {
		var text = graph.text(x, y, name)
		text.attr('font-size', size);
		text.attr({ fill: color, stroke: color, opacity: opacity, 'text-anchor': 'start' })
		text.attr('font-family', 'FontAwesome');
		return text;
	},
	drawEvent: function (graph, name, x, y, color, opacity, cursor) {
		var me = this;
		var group = graph.set();
		var eventIcon = me.drawIcon(graph, '\uf1b2', x - 20, y, 14, color, 1);
		var eventText = graph.text(x, y, name).attr({ cursor: cursor, stroke: "none", opacity: opacity, "font-size": 11, 'text-anchor': 'start', "font-weight": "bold" });
		group.push(eventIcon, eventText);
		return group;
	},
	loadVersionButton: function () {
	    var me = this;
	    Ext.Ajax.request({
	        url: Workflow.global.Config.baseUrl + 'api/worklists/procVersions',
	        method: 'GET',
	        params: { procInstId: me.processId },
	        success: function (response) {
	            var record = Ext.decode(response.responseText);
	            var btnRepair = me.query('button[itemId=btnRepair]')[0];

	            btnRepair.getMenu().removeAll();
	            btnRepair.procId = record[0].id;
	            Ext.each(record, function (data) {
	                btnRepair.getMenu().add({
	                    text: data.version,
	                    iconCls: 'fa fa-history',
	                    value: data.id,
	                    handler: function (btn) {
	                        me.repairProcessError(btn.getValue());
	                    }
	                });
	            });
	        }
	    });
	},
	repairProcessError: function (procId) {
	    var me = this;
	    var me = this;

	    Ext.MessageBox.show({
	        msg: 'Repairing your business process, please wait...',
	        progressText: 'Repairing...',
	        width: 300,
	        wait: {
	            interval: 120
	        }
	    });

	    Ext.Ajax.request({
	        url: Workflow.global.Config.baseUrl + 'api/worklists/retryError',
	        params: { procInstId: me.processId, procId: procId },
	        method: 'GET',
	        headers: { 'Content-Type': 'text/html' },
	        success: function (response) {
	            var data = Ext.decode(response.responseText);
	            
	            me.timer = Ext.defer(function () {
	                me.timer = null;
	                Ext.MessageBox.hide();
	                //Ext.toast({
	                //    title: 'Repaired Result',
	                //    html: data.success ? 'The issue has been solved.' : 'The process still got the issue.',
	                //    closable: true,
	                //    align: 'br',
	                //    slideInDuration: 400,
	                //    minWidth: 400
	                //});
	                me.buildGraph();
	            }, 1000);
	        }
	    });
	}
});