
(function () {

    var isRequestAccepted = false;
    window.blazorPushNotifications = {
        sendPushNotification: async (message) => {

            pushNotification(message);
        }
    };

    window.BlazorRequestPushNotification = {
        requestPushNotification: async () => {

            //Check if user has already accepted permission or not
            if (!Push.Permission.has()) {

                Push.Permission.request(onGranted, onDenied); //send user permission alert
                
            } else {
                isRequestAccepted = true;
            }

            return isRequestAccepted;
        }
    }

    //if user grants permission then isRequestAccepted = true
    function onGranted() {
        isRequestAccepted = true;
    }

    function onDenied() {
        isRequestAccepted = false;
    }

    function pushNotification(message) {
        Push.create("Blazor PWA", {
            body: message,
            icon: 'icon-32.png',
            timeout: 4000,
            vibrate: [100, 50, 100],
            onClick: function () {
                window.focus();
                this.close();
            }
        });


    }


})();
