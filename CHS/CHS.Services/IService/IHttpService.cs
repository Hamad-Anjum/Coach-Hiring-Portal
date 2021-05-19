using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CHS.Services.IService
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendRequest(string httpMethod, string url, bool isSite = false, string serializedData = null);
        Task<HttpStatusCode> Post(HttpResponseMessage response);
        Task<HttpStatusCode> Put(HttpResponseMessage response);
        Task<HttpStatusCode> Delete(HttpResponseMessage response);
        Task<ICollection<T>> GetAll<T>(HttpResponseMessage response) where T : class;
        Task<T> Get<T>(HttpResponseMessage response);
    }
}