using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace App5.Views.Folder
{
public class Users
    {
        public string Id { get; set; }
        public string name  { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string usergroup { get; set; }

        public static MobileServiceClient client = new MobileServiceClient("https://meihana.azurewebsites.net");

        public async Task<bool> SaveUser()
        {
            try
            {
                await client.GetTable<Users>().InsertAsync(this);
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
            return await client.GetTable<Users>().ToListAsync();
        }
        public override string ToString()
        {
            return $"{name} - {email} - {usergroup}";

        }
    }
}
