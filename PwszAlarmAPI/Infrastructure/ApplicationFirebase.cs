using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using PwszAlarmAPI.Controllers;
using PwszAlarmAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PwszAlarmAPI.Infrastructure
{
    public class ApplicationFirebase : BaseApiController
    {
        public class Message
        {
            public string[] registration_ids { get; set; }
            public Notification notification { get; set; }
            public object data { get; set; }
        }
        public class Notification
        {
            public string title { get; set; }
            public string body { get; set; }
        }
        public static async Task SendData(string title, string body)
        {
            ApplicationFirebase applicationFirebase = new ApplicationFirebase();
            await applicationFirebase.SendNotificationsAsync(title, body);
        }
        public async Task SendNotificationsAsync(string _title, string _body)
        {
            var usersContext = new ApplicationDbContext();
            var users = usersContext.Users.Where(u => u.SendNotifications == true).ToList();
            var applicationID = "AAAAPDLq1mw:APA91bEU9YUz6LL-5zrjyHRzxi2a_7CViesVgN0u_0Xyjfeo8Lwqa6q4sEHuYUy-s61XmQsYa1jQ8ab2v7M84_VfHPXejnSP-uzBFTISS08gs9a8N6xgHGfgHRMCvy017oo3jXSgwVjd";
            List<string> tokens = new List<string>();
           
            foreach (var user in users)
            {
                if(user.FirebaseToken != null)tokens.Add(user.FirebaseToken);
            }
            string[] deviceTokens = new string[tokens.Count()];
            deviceTokens = tokens.ToArray();

            var messageInformation = new Message()
            {
                notification = new Notification()
                {
                    title = _title,
                    body = _body

                },
                registration_ids = deviceTokens
            };
            string jsonMessage = JsonConvert.SerializeObject(messageInformation);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
            request.Headers.TryAddWithoutValidation("Authorization", "key=" +applicationID);
            request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                result = await client.SendAsync(request);
            }
            
        }
    }
}