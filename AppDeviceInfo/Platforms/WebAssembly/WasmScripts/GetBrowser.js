
require([`${config.uno_app_base}/es5.js`], c => Bowser = c);


function p42_UNO_GetBrowserInfo() {
    const browser = Bowser.getParser(window.navigator.userAgent);
    return JSON.stringify(browser.parse());
}
