/*
https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-node
https://learn.microsoft.com/en-us/azure/azure-functions/functions-reference-node?tabs=v2-v3-v4-export%2Cv2-v3-v4-done%2Cv2%2Cv2-log-custom-telemetry%2Cv2-accessing-request-and-response%2Cwindows-setting-the-node-version
https://learn.microsoft.com/en-us/azure/developer/javascript/how-to/develop-serverless-apps
https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-cli-node?tabs=azure-cli%2Cbrowser
https://www.youtube.com/watch?v=pvyVqczWDIk&t=342s
https://www.youtube.com/watch?v=jy08dnjOV1k&t=594s
https://www.youtube.com/watch?v=F1vTrFAaYck
https://medium.com/bb-tutorials-and-thoughts/how-to-write-serverless-nodejs-azure-functions-with-postgresql-database-typescript-version-83dec2a65fd3
https://www.twilio.com/blog/2017/08/http-requests-in-node-js.html
https://blog.logrocket.com/5-ways-to-make-http-requests-in-node-js/
*/
const fetch = require('node-fetch'); 
module.exports = async function (context, myTimer) {
    var timeStamp = new Date().toISOString();
    
    if (myTimer.isPastDue)
    {
        context.log('JavaScript is running late!');
    }
    try{
        let apiResponse = await fetch('https://api.github.com/');
        if(apiResponse.status === 200 || apiResponse.status === 201){
            let json = await apiResponse.json();
            context.log('Logged result');
            context.log(json);
        }
        else{
            context.log(`Repsonse was not success. Status Code was : ${apiResponse.status}`)
        }
    }
    catch (error){
        context.log.error("An Error Occurred",error);
    }
    context.log('JavaScript timer trigger function ran!', timeStamp);   
};