using System;

namespace CHS.Web.Services
{
    public interface IRefreshService
    {
        event Action RefreshRequested;
        void CallRequestRefresh();

        event Action<string> ImageRequest;
        void CallImageRequest(string e);
    }
}
