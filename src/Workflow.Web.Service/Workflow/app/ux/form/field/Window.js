Ext.define('Ext.ux.Window', {
    extend: 'Ext.window.Window',
    alias: 'widget.animatedwindow',
    initComponent: function () {
        this.callParent(arguments);
    },
    afterShow: function (animateTarget, cb, scope) {
        var me = this,
            fromBox,
            toBox,
            ghostPanel;

        // Default to configured animate target if none passed
        animateTarget = me.getAnimateTarget(animateTarget);

        // Need to be able to ghost the Component
        if (!me.ghost) {
            animateTarget = null;
        }
        // If we're animating, kick of an animation of the ghost from the target to the *Element* current box
        if (animateTarget) {
            toBox = me.el.getBox();
            fromBox = animateTarget.getBox();
            me.el.addCls(me.offsetsCls);
            ghostPanel = me.ghost();
            ghostPanel.el.stopAnimation();

            // Shunting it offscreen immediately, *before* the Animation class grabs it ensure no flicker.
            ghostPanel.el.setX(-10000);

            me.ghostBox = toBox;
            ghostPanel.el.animate({
                from: fromBox,
                to: toBox,
                duration: 250,
                listeners: {
                    afteranimate: function () {
                        delete ghostPanel.componentLayout.lastComponentSize;
                        me.unghost();
                        delete me.ghostBox;
                        me.el.removeCls(me.offsetsCls);
                        me.onShowComplete(cb, scope);
                    }
                }
            });
        }
        else {
            me.onShowComplete(cb, scope);
        }
    },
    onHide: function (animateTarget, cb, scope) {
        var me = this,
            ghostPanel,
            toBox,
            activeEl = Ext.Element.getActiveElement();

        // If hiding a Component which is focused, or contains focus: blur the focused el. 
        if (activeEl === me.el || me.el.contains(activeEl)) {
            activeEl.blur();
        }

        // Default to configured animate target if none passed
        animateTarget = me.getAnimateTarget(animateTarget);

        // Need to be able to ghost the Component
        if (!me.ghost) {
            animateTarget = null;
        }
        // If we're animating, kick off an animation of the ghost down to the target
        if (animateTarget) {
            ghostPanel = me.ghost();
            ghostPanel.el.stopAnimation();
            toBox = animateTarget.getBox();
            ghostPanel.el.animate({
                to: toBox,
                duration: 250,
                listeners: {
                    afteranimate: function () {
                        delete ghostPanel.componentLayout.lastComponentSize;
                        ghostPanel.el.hide();
                        me.afterHide(cb, scope);
                    }
                }
            });
        }
        me.el.hide();
        if (!animateTarget) {
            me.afterHide(cb, scope);
        }
    }
});