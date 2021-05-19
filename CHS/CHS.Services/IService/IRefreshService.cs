using System;

namespace CHS.Services.IService
{
    public interface IRefreshService
    {
        event Action RefreshRequested;
        void CallRequestRefresh();

        event Action<string> ImageRequest;
        void CallImageRequest(string e);
    }
}
