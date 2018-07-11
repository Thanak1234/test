Ext.define('Workflow.global.Config', {
    statics: {
        //server: '10.60.0.111',
        server: 'localhost',
        resource: UI_RESOURCE_PATH + 'resources/',
        //baseUrl: 'http://forms.nagaworld.dev/services/' /*Server web service url*/
        baseUrl: '/', /*Deveplopment web service url*/
        //renewUrl: 'http://qa-k2web.nagaworld.local/security/renew', /*Deveplopment web service url*/
        renewUrl: 'https://forms.nagaworld.com/security/renew'/*Deveplopment production*/
    }
});
