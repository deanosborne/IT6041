using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace App5.Views.Folder
{
public class Users
    {
        public string Id { get; set; }
        public string name  { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string usergroup { get; set; }

        HttpClient client;

        public Users()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"https://meihana.azurewebsites.net")
            };
        }

        public static MobileServiceClient client1 = new MobileServiceClient("https://meihana.azurewebsites.net");

        public async Task<bool> SaveUser()
        {
            try
            {
                await client1.GetTable<Users>().InsertAsync(this);
                return true;
            }
            catch(MobileServiceInvalidOperationException msioe)
            {
                var response = await msioe.Response.Content.ReadAsStringAsync();
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<List<Users>> ReadUser()
        {
            return await client1.GetTable<Users>().ToListAsync();
        }
        public override string ToString()
        {
            return $"{name} - {email} - {usergroup}";

        }

        public async Task<bool> ValidateUser(string email, string password)
        {
            var result = await client.GetStringAsync($"api/validate?email={email}&password={password}");
            if (result == "true")
            {
                this.email = email;
                return true;
            }
            return false;
        }

        public async Task<bool> ValidateUserGroup(string email, string password, string usergroup)
        {
            var result = await client.GetStringAsync($"api/validate?email={email}&password={password}&usergroup={usergroup}");
            if (result == "true")
            {
                this.email = email;
                this.usergroup = "none";
                return true;
            }
            return false;
        }
    }
}
