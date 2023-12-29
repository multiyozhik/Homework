using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientAppWpf
{
    public class ContactsData
    {
        private HttpClient httpClient {get; set;}
        public ContactsData()
        {
            httpClient = new HttpClient();
        }

        public void CreateContact(Contact newContact)
        {

        }

        public void ChangeContact(Contact newContact)
        {

        }

        public void DeleteContact(Contact newContact)
        {

        }

    }
}
