using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CHS.Web.Services
{
    public interface IService
    {
        Task<HttpResponseMessage> SendRequest(string httpMethod, string url, string serializedData = null);
        Task<HttpStatusCode> Post(HttpResponseMessage response);
        Task<HttpStatusCode> Put(HttpResponseMessage response);
        Task<HttpStatusCode> Delete(HttpResponseMessage response);
        Task<ICollection<T>> GetAll<T>(HttpResponseMessage response) where T : class;
        Task<T> Get<T>(HttpResponseMessage response);
    }
}