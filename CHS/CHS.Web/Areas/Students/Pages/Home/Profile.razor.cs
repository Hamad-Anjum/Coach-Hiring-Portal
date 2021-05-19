using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;

using Newtonsoft.Json;

namespace CHS.Web.Areas.Students.Pages.Home
{
    [Authorize(Roles = "Student,Gym,Trainer", Policy = "FilledSurveyForm")]
    public partial class Profile : ComponentBase
    {
        [Parameter]
        public Guid? Id { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ProtectedLocalStorage ProtectedLocalStorage { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public IFileUpload FileUpload { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        private PostUploadViewModel _postUpload = new();
        private PostViewModel _editPost;
        private AboutMeViewModel _aboutMe;
        private ProfileViewModel _profile;
        private ICollection<SubscriptionViewModel> _subscriptions;
        private string _class, _role = string.Empty, _temp, _id = string.Empty, _imgSrc = string.Empty, _label = "Say Something...";
        private bool _processing, _isOpen, _isOwner, _isProfile, _isCover;

        protected override async Task OnInitializedAsync()
        {
            var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (Id == Guid.Empty)
            {
                NavigationManager.NavigateTo("Home");
            }
            else if (user.User.IsInRole("Student"))
            {
                if (Id == null || Id == Guid.Empty)
                {
                    NavigationManager.NavigateTo("Home");
                }
                else
                {
                    _role = "Student";
                }
            }
            else if (user.User.IsInRole("Gym"))
            {
                _role = "Gym";
            }
            else if (user.User.IsInRole("Trainer"))
            {
                _role = "Trainer";
            }
            var result = await ProtectedLocalStorage.GetAsync<string>("id");
            if (result.Success)
            {
                _id = result.Value;
                if (Id != Guid.Empty)
                {
                    _isOwner = Id == Guid.Parse(_id) || Id == null;
                    if (!_isOwner)
                    {
                        _class = "parent-subscribe";
                    }
                }
            }
            RefreshService.CallRequestRefresh();

            var response = await Service.SendRequest("Get", $"Profile/GetEverything?id={(_isOwner ? _id : Id)}");
            if (response.IsSuccessStatusCode)
            {
                _profile = await Service.Get<ProfileViewModel>(response);
            }
            else
            {

            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("import", "/JavaScript.js");
            await JSRuntime.InvokeVoidAsync("window.Custom2");
            await JSRuntime.InvokeVoidAsync("window.Custom3");
            await JSRuntime.InvokeVoidAsync("window.DevNav");
            await JSRuntime.InvokeVoidAsync("window.LoadChart");
            await JSRuntime.InvokeVoidAsync("window.Dashboard1");
            await JSRuntime.InvokeVoidAsync("carouselReview");
        }

        private async Task OnChangeFile(InputFileChangeEventArgs e)
        {
            _imgSrc = await FileUpload.UploadAsync(e.File, _role);
            _postUpload.File = Path.GetFileName(_imgSrc);
        }

        private async Task OnChangePostImage(InputFileChangeEventArgs e)
        {
            var dirExist = true;
            _isProfile = false;
            _isCover = false;

            if (!string.IsNullOrEmpty(_imgSrc))
            {
                if (File.Exists(_imgSrc))
                {
                    File.Delete(_imgSrc);
                }
            }

            string userFolderPath = Path.Combine(WebHostEnvironment.WebRootPath, $@"Content\Images", _role, "Posts", _id);
            if (!Directory.Exists(userFolderPath))
            {
                dirExist = Directory.CreateDirectory(userFolderPath).Exists;
            }
            if (dirExist)
            {
                string postPath = Path.Combine(userFolderPath, _postUpload.PostId.ToString());
                string imagePath = Path.Combine(postPath, e.File.Name);

                if (Directory.Exists(postPath))
                {
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                else
                {
                    Directory.CreateDirectory(postPath);
                }

                _imgSrc = await FileUpload.UploadAsync(e.File, imagePath);
                if (!string.IsNullOrEmpty(_imgSrc))
                {
                    if (!_isOpen)
                    {
                        _isOpen = await JSRuntime.InvokeAsync<bool>("ShowModal", "imageUploadModal");
                    }
                    _postUpload.File = _imgSrc;
                }
                else
                {
                }
            }
        }

        private async Task OnChangeProfilePicture(InputFileChangeEventArgs e)
        {
            var dirExist = true;
            _isProfile = true;
            _isCover = false;

            if (!string.IsNullOrEmpty(_imgSrc))
            {
                if (File.Exists(_imgSrc))
                {
                    File.Delete(_imgSrc);
                }
            }

            string userFolderPath = Path.Combine(WebHostEnvironment.WebRootPath, $@"Content\Images", _role, "ProfilePictures", _id);
            if (!Directory.Exists(userFolderPath))
            {
                dirExist = Directory.CreateDirectory(userFolderPath).Exists;
            }
            if (dirExist)
            {
                string postPath = Path.Combine(userFolderPath, _postUpload.PostId.ToString());
                string imagePath = Path.Combine(postPath, e.File.Name);

                if (Directory.Exists(postPath))
                {
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                else
                {
                    Directory.CreateDirectory(postPath);
                }

                _imgSrc = await FileUpload.UploadAsync(e.File, imagePath);
                if (!string.IsNullOrEmpty(_imgSrc))
                {
                    if (!_isOpen)
                    {
                        _isOpen = await JSRuntime.InvokeAsync<bool>("ShowModal", "imageUploadModal");
                    }
                    _postUpload.File = _imgSrc;
                }
                else
                {
                }
            }
        }

        private async Task RemoveProfile()
        {
            var response = await Service.SendRequest("Get", "Users/RemoveProfile");
            if (response.IsSuccessStatusCode)
            {
                _profile.ProfilePicture = "Images/profile.png";
                RefreshService.CallImageRequest("Jobie/images/profile/17.jpg");
            }
        }

        private async Task RemoveCover()
        {
            var response = await Service.SendRequest("Get", "Users/RemoveCover");
            if (response.IsSuccessStatusCode)
            {
                _profile.CoverPicture = "Images/cover.jpg";
            }
        }

        private void EditPost(Guid id, string title)
        {
            _editPost = new PostViewModel { PostId = id, Title = title };
            _temp = title;
            var temp = _profile.Posts.Where(x => x.PostId.Equals(id)).FirstOrDefault();
            _profile.Posts.Remove(temp);
            temp.InEditMode = true;
            _profile.Posts.Add(temp);
        }

        private async Task ChangePostTitle()
        {
            await EditPostSubmit();
        }

        private async Task EditPostSubmit()
        {
            if (_editPost != null)
            {
                if (!_temp.Equals(_editPost.Title))
                {
                    var response = await Service.SendRequest("Put", $"Post/EditPost?id={_editPost.PostId}", false, JsonConvert.SerializeObject(_editPost));
                    if (response.IsSuccessStatusCode)
                    {
                    }
                }
                var temp = _profile.Posts.Where(x => x.PostId.Equals(_editPost.PostId)).FirstOrDefault();
                _profile.Posts.Remove(temp);
                temp.InEditMode = false;
                temp.Title = _editPost.Title;
                _profile.Posts.Add(temp);
            }
        }

        private async Task DeletePost(Guid id)
        {
            var response = await Service.SendRequest("Get", $"Put/DeletePost?id={id}", false, JsonConvert.SerializeObject(new PostViewModel()));
            if (response.IsSuccessStatusCode)
            {
                var temp = _profile.Posts.Where(x => x.PostId.Equals(id)).FirstOrDefault();
                _profile.Posts.Remove(temp);
            }
        }

        private async Task OnChangeCoverPicture(InputFileChangeEventArgs e)
        {
            var dirExist = true;
            _isProfile = false;
            _isCover = true;

            if (!string.IsNullOrEmpty(_imgSrc))
            {
                if (File.Exists(_imgSrc))
                {
                    File.Delete(_imgSrc);
                }
            }

            string userFolderPath = Path.Combine(WebHostEnvironment.WebRootPath, $@"Content\Images", _role, "CoverPictures", _id);
            if (!Directory.Exists(userFolderPath))
            {
                dirExist = Directory.CreateDirectory(userFolderPath).Exists;
            }
            if (dirExist)
            {
                string postPath = Path.Combine(userFolderPath, _postUpload.PostId.ToString());
                string imagePath = Path.Combine(postPath, e.File.Name);

                if (Directory.Exists(postPath))
                {
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                else
                {
                    Directory.CreateDirectory(postPath);
                }

                _imgSrc = await FileUpload.UploadAsync(e.File, imagePath);
                if (!string.IsNullOrEmpty(_imgSrc))
                {
                    if (!_isOpen)
                    {
                        _isOpen = await JSRuntime.InvokeAsync<bool>("ShowModal", "imageUploadModal");
                    }
                    _postUpload.File = _imgSrc;
                }
                else
                {
                }
            }
        }

        private async Task DeleteFile(bool force = false)
        {
            if (!force)
            {
                if (FileUpload.DeleteImage(_imgSrc))
                {
                    _imgSrc = string.Empty;
                    _isOpen = await JSRuntime.InvokeAsync<bool>("HideModal", "imageUploadModal");
                }
                else
                {
                    //await JSRuntime.InvokeVoidAsync("TryAgain");
                }
            }
        }

        /// <summary>
        /// Post is Currently removed and then added again after liked
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        private async Task Like(Guid postId)
        {
            var response = await Service.SendRequest("Post", "Post/Like", false,
                JsonConvert.SerializeObject(new LikePostViewModel
                {
                    PostId = postId,
                    UserId = Guid.Parse(_id)
                }));
            if (response.IsSuccessStatusCode)
            {
                int likes = await Service.Get<int>(response);
                var temp = _profile.Posts.Where(x => x.PostId.Equals(postId)).FirstOrDefault();
                _profile.Posts.Remove(temp);
                temp.Likes = likes;
                temp.LikedThisPost = true;
                _profile.Posts.Add(temp);
            }
        }

        /// <summary>
        /// /// Post is Currently removed and then added again after Unlike
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        private async Task UnLike(Guid postId)
        {
            var response = await Service.SendRequest("Post", "Post/UnLike", false,
                JsonConvert.SerializeObject(new LikePostViewModel
                {
                    PostId = postId,
                    UserId = Guid.Parse(_id)
                }));
            if (response.IsSuccessStatusCode)
            {
                int likes = await Service.Get<int>(response);
                var temp = _profile.Posts.Where(x => x.PostId.Equals(postId)).FirstOrDefault();
                _profile.Posts.Remove(temp);
                temp.Likes = likes;
                temp.LikedThisPost = false;
                _profile.Posts.Add(temp);
            }
        }

        private async Task Follow(Guid? followerId)
        {
            var response = await Service.SendRequest("Post", "Post/Follow", false,
                JsonConvert.SerializeObject(new FollowViewModel
                {
                    FollowerId = (Guid)followerId,
                    UserId = Guid.Parse(_id)
                }));
            if (response.IsSuccessStatusCode)
            {
                _profile.Followers = await Service.Get<int>(response);
            }
        }

        private async Task UnFollow(Guid? followerId)
        {
            var response = await Service.SendRequest("Post", "Post/UnFollow", false,
                JsonConvert.SerializeObject(new FollowViewModel
                {
                    FollowerId = (Guid)followerId,
                    UserId = Guid.Parse(_id)
                }));
            if (response.IsSuccessStatusCode)
            {
                _profile.Followers = await Service.Get<int>(response);
            }
        }

        private async Task AboutMe()
        {
            var response = await Service.SendRequest("Get", $"AboutMe/{(_isOwner ? _id : Id)}");
            if (response.IsSuccessStatusCode)
            {
                _aboutMe = await Service.Get<AboutMeViewModel>(response);
            }
            else
            {

            }
        }

        private async Task Offers()
        {
            var response = await Service.SendRequest("Get", $"Subscriptions/GetAllActive?id={(_isOwner ? _id : Id)}");
            if (response.IsSuccessStatusCode)
            {
                _subscriptions = await Service.GetAll<SubscriptionViewModel>(response);
            }
            else
            {
            }
        }

        private async Task Subscribe(SubscriptionViewModel model)
        {
            if (Id != null && Id != default)
            {
                var response = await Service.SendRequest("Post", "Subscriptions/Subscribe?", false,
                    JsonConvert.SerializeObject(new MemberSubscriptionViewModel
                    {
                        SubscriberId = model.SubscriptionOwnerMemberId,
                        SubscriptionId = model.SubscriptionId,
                        SubscriptionMonths = model.SubscriptionMonths
                    }));
                if (response.IsSuccessStatusCode)
                {

                }
            }
        }

        //private async Task EmojiPicker()
        //{
        //    _showEmoji = !_showEmoji;
        //    //await JSRuntime.InvokeVoidAsync("EmojiPicker");
        //}

        private async Task Done()
        {
            try
            {
                if (!string.IsNullOrEmpty(_imgSrc))
                {
                    if (!File.Exists(_imgSrc))
                    {
                        await JSRuntime.InvokeVoidAsync("FolderRemoved", _imgSrc);
                    }
                    else
                    {
                        _processing = true;
                        _postUpload.File = _imgSrc;
                        _postUpload.Text = "text";
                        _postUpload.UserId = _id;

                        var response = await Service.SendRequest("Post", $"Profile/{(_isProfile ? "UserProfile" : "UserCover")}", false, JsonConvert.SerializeObject(_postUpload));
                        if (response.IsSuccessStatusCode)
                        {
                            _postUpload.Text = string.Empty;
                            string logoImg = string.Empty;
                            if (_isProfile)
                            {
                                logoImg = await FileUpload.SaveLogoImage(_imgSrc);
                                if (!string.IsNullOrEmpty(logoImg))
                                {
                                    var result = await FileUpload.SaveMainSiteImage(_imgSrc);
                                }
                                _profile.ProfilePicture = _imgSrc.Split(Constant.WwwRoot)[1].Replace('\\', '/');
                            }
                            else
                            {
                                _profile.CoverPicture = _imgSrc.Split(Constant.WwwRoot)[1].Replace('\\', '/');
                            }
                            _imgSrc = string.Empty;
                            _isOpen = await JSRuntime.InvokeAsync<bool>("ClickCancel", "ClickCancel");
                            if (!string.IsNullOrEmpty(logoImg))
                            {
                                var result = await ProtectedLocalStorage.GetAsync<string>("LogoImage");
                                if (result.Success)
                                {
                                    await ProtectedLocalStorage.DeleteAsync("LogoImage");
                                    await ProtectedLocalStorage.SetAsync("LogoImage", logoImg);
                                }
                                RefreshService.CallImageRequest(logoImg);
                            }
                        }
                        else
                        {

                        }
                    }
                    _processing = false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task HandleValidSubmit()
        {
            _processing = true;

            try
            {
                if (!string.IsNullOrEmpty(_imgSrc))
                {
                    _postUpload.PostImages.Add(new PostImageViewModel
                    {
                        PostId = _postUpload.PostId,
                        Picture = _imgSrc,
                        ContentType = Path.GetExtension(_imgSrc),
                        SortOrder = 1
                    });
                }

                var response = await Service.SendRequest("Post", "Post", false, JsonConvert.SerializeObject(_postUpload));

                //var response = await Service.SendRequest("Post", "Post", JsonConvert.SerializeObject(_postUpload.Content));
                if (response.IsSuccessStatusCode)
                {
                    _postUpload.Text = string.Empty;

                    _profile.Posts.Add(await Service.Get<PostViewModel>(response));

                    _imgSrc = string.Empty;
                    _isOpen = await JSRuntime.InvokeAsync<bool>("ClickCancel", "ClickCancel");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            _processing = false;
        }
    }

}
