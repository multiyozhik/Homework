using System;
using System.Collections.Generic;
using System.Net.Http;
using WpfClientApp.Models;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text.Json.Serialization;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace WpfClientApp.Services
{
    public class ContactsApi
    {
        private readonly HttpClient httpClient;        

        public ContactsApi(Uri baseRoute) 
        {
            httpClient = new HttpClient() { BaseAddress = baseRoute };
        }

        public async Task<IEnumerable<Contact>?> GetContacts()
        {
            var uri = new Uri(httpClient.BaseAddress, "/api/contacts");
            var httpResponseMsg = await httpClient.GetAsync(uri);
            return await httpResponseMsg.Content.ReadFromJsonAsync<IEnumerable<Contact>?>();            
        }

        public async Task<Contact?> GetContactsById(Guid id)
        {
            var uri = new Uri(httpClient.BaseAddress, "/api/contacts/id");
            var httpResponseMsg = await httpClient.GetAsync(uri);
            return await httpResponseMsg.Content.ReadFromJsonAsync<Contact?>();
        }

        public async Task AddContact(Contact contact)
        {
            var uri = new Uri(httpClient.BaseAddress, $"/api/add/{contact.Id}");
            var httpResponseMsg = await httpClient.PostAsync(
                requestUri: uri,
                content: new StringContent(JsonSerializer.Serialize(contact)));
            if (!httpResponseMsg.IsSuccessStatusCode)
                await ThrowException(httpResponseMsg);
        }

        public async Task ChangeContact(Contact contact)
        {
            var uri = new Uri(httpClient.BaseAddress, $"/api/change/{contact.Id}");
            var httpResponseMsg = await httpClient.PutAsync(
                requestUri: uri,
                content: new StringContent(JsonSerializer.Serialize(contact)));
            if (!httpResponseMsg.IsSuccessStatusCode)
                await ThrowException(httpResponseMsg);
        }

        public async Task DeleteContact(Guid id)
        {
            var uri = new Uri(httpClient.BaseAddress, $"/api/delete/{id}");
            var httpResponseMsg = await httpClient.DeleteAsync(uri);
            if (!httpResponseMsg.IsSuccessStatusCode)
                await ThrowException(httpResponseMsg);
        }

        private async Task ThrowException(HttpResponseMessage httpResponseMsg)
        {
            string msg = await httpResponseMsg.Content.ReadAsStringAsync();
            Console.WriteLine(msg);
            throw new Exception(msg);
        }
    }
}
