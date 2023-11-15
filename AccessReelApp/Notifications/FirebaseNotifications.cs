﻿using CommunityToolkit.Mvvm.Messaging.Messages;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace AccessReelApp.Notifications
{
    public class PushNotificationReceived : ValueChangedMessage<string>
    {
        public PushNotificationReceived(string message) : base(message) { }
    }

    public class PushNotificationRequest
    {
        public List<string> registration_ids { get; set; } = new List<string>();
        public NotificationMessageBody notification { get; set; }
        public object data { get; set; }
    }

    public class NotificationMessageBody
    {
        public string title { get; set; }
        public string body { get; set; }
    }

    // The main class that encapsulates the functionality
    public class NotificationManager
    {
        // Private fields can be declared here
        private string _deviceToken;

        // Constructor can be used to initialize class-level variables
        public NotificationManager()
        {
            if(Preferences.ContainsKey("DeviceToken"))
            {
                _deviceToken = Preferences.Get("DeviceToken", "");
            }
        }

        // Method to read Firebase Admin SDK
        public static async void ReadFireBaseAdminSDK()
        {
            var stream = await FileSystem.OpenAppPackageFileAsync("Platforms\\Android\\admin_sdk.json");
            var reader = new StreamReader(stream);

            var jsonContent = reader.ReadToEnd();

            if (FirebaseMessaging.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(jsonContent)
                });
            }
        }

        // Method to handle button click event
        public async void HandleButtonClick()
        {
            // Setup authorization and create push notification request
            var androidNotificationObject = new Dictionary<string, string>();
            androidNotificationObject.Add("NavigationID", "2");

            var pushNotificationRequest = new PushNotificationRequest
            {
                notification = new NotificationMessageBody
                {
                    title = "Notification Title",
                    body = "Notification body"
                },
                data = androidNotificationObject,
                registration_ids = new List<string> { _deviceToken }
            };

            // Create a list of messages
            var messageList = new List<Message>();

            var obj = new Message
            {
                Token = _deviceToken,
                Notification = new Notification
                {
                    Title = "Title",
                    Body = "message body"
                },
                Data = androidNotificationObject,
                // Include additional configurations if needed
            };

            messageList.Add(obj);

            // Send push notification
            var response = await FirebaseMessaging.DefaultInstance.SendAllAsync(messageList);
        }
    }


    //private string _deviceToken;

    //private void DeviceToken()
    //{
    //    if (Preferences.ContainsKey("DeviceToken"))
    //    {
    //        _deviceToken = Preferences.Get("DeviceToken", "");
    //    }
    //}


    //private async void ReadFireBaseAdminSDK()
    //{
    //    var stream = await FileSystem.OpenAppPackageFileAsync("Platforms\\Android\\admin_sdk.json");
    //    var reader = new StreamReader(stream);

    //    var jsonContent = reader.ReadToEnd();

    //    if (FirebaseMessaging.DefaultInstance == null)
    //    {
    //        FirebaseApp.Create(new AppOptions()
    //        {
    //            Credential = GoogleCredential.FromJson(jsonContent)
    //        });
    //    }
    //}




    //private async void Button_Clicked_1(object sender, EventArgs e)
    //{
    //    //Setup authorisation https://firebase.google.com/docs/cloud-messaging/migrate-v1

    //    var androidNotificationObject = new Dictionary<string, string>();
    //    androidNotificationObject.Add("NavigationID", "2");


    //    var pushNotificationRequest = new PushNotificationRequest
    //    {
    //        notification = new NotificationMessageBody
    //        {
    //            title = "Notification Title",
    //            body = "Notification body"
    //        },
    //        data = androidNotificationObject,
    //        registration_ids = new List<string> { _deviceToken }
    //    };



    //    var messageList = new List<Message>();

    //    var obj = new Message
    //    {
    //        Token = _deviceToken,
    //        Notification = new Notification
    //        {
    //            Title = "Tilte",
    //            Body = "message body"
    //        },
    //        Data = androidNotificationObject,
    //        //Apns = new ApnsConfig()
    //        //{
    //        //    Aps = new Aps
    //        //    {
    //        //        Badge = 15,
    //        //        //CustomData = iosNotificationObject,
    //        //    }
    //        //}
    //    };

    //    messageList.Add(obj);

    //    var response = await FirebaseMessaging.DefaultInstance.SendAllAsync(messageList);
    //}


}
