using System;

using CHS.Services.IService;

namespace CHS.Services.Service
{
    public class RefreshService : IRefreshService
    {
        public event Action RefreshRequested;

        public event Action<string> ImageRequest;

        public void CallImageRequest(string e)
        {
            ImageRequest?.Invoke(e);
        }

        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
