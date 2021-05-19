
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.Authentication;
using CHS.Services.IService;

using MatBlazor;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;

using Newtonsoft.Json;

namespace CHS.Web.Areas.Students.Pages.Home
{
    [Authorize(Roles = "Gym,Trainer")]
    public partial class SurveyForm : ComponentBase
    {
        [Parameter]
        public Guid Id { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public ProtectedLocalStorage LocalStorage { get; set; }

        [Inject]
        public IFileUpload FileUpload { get; set; }

        private SurveyGetViewModel _getModel;

        private SurveyPostViewModel _postModel = new SurveyPostViewModel
        {
            PersonalInfo = new PersonalInfoViewModel(),
            Contact = new ContactViewModel(),
            TrainerComfortZone = new TrainerComfortZoneViewModel(),
            Skills = new List<SkillViewModel>(),
            Certifications = new List<CertificationViewModel>(),
            TimeSlot = new TimeSlotViewModel(),
            PreferredLocation = new PreferredLocationViewModel()
        };

        private ICollection<SkillViewModel> _skills = new List<SkillViewModel>();

        private ICollection<CertificationViewModel> _certifications = new List<CertificationViewModel>();

        private ICollection<StateViewModel> _states;
        private ICollection<CityViewModel> _cities;
        private ICollection<DistrictViewModel> _districts;

        private string _newSkill = string.Empty;
        private string _personalInfoMessage = string.Empty;
        private string _contactInfoMessage = string.Empty;
        private string _skillInfoMessage = string.Empty;
        private string _certificationInfoMessage = string.Empty;
        private string _newCertification = string.Empty;
        private string _skillMessage = string.Empty;
        private string _certificationMessage = string.Empty;
        private string _countryMessage = string.Empty;
        private string _stateMessage = string.Empty;
        private string _cityMessage = string.Empty;
        private string _districtMessage = string.Empty;
        private string _role = string.Empty;
        private string _id = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (user.User.Claims.Any(x => x.Type.Equals("all") && x.Value.Split(',').Contains("FilledSurveyForm")))
            {
                await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsNotAuthenticated();
            }

            RefreshService.CallRequestRefresh();

            var response = await Service.SendRequest("Get", "Survey/SurveyForm");
            if (response.IsSuccessStatusCode)
            {
                _getModel = await Service.Get<SurveyGetViewModel>(response);
                _skills = _getModel.Skills;
                _certifications = _getModel.Certifications;
                var result = await LocalStorage.GetAsync<string>("roles");
                if (result.Success)
                {
                    _role = result.Value;
                }
                result = await LocalStorage.GetAsync<string>("id");
                if (result.Success)
                {
                    _id = result.Value;
                }
                if (_role.Equals("Gym"))
                {
                    _postModel.PersonalInfo.Height = decimal.Zero;
                    _postModel.PersonalInfo.GenderId = Guid.Parse("ebf2d399-d7b3-48f3-99b1-2a81fd833652");
                    _postModel.PersonalInfo.YearsOfExperience = decimal.Zero;
                    _postModel.PersonalInfo.YearsAsTraining = decimal.Zero;
                    _postModel.PersonalInfo.Weight = decimal.Zero;
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //await JSRuntime.InvokeVoidAsync("window.Base1");
            //await JSRuntime.InvokeVoidAsync("window.Base2");
            //await JSRuntime.InvokeVoidAsync("window.Base3");
            if (!firstRender)
            {
                await JSRuntime.InvokeVoidAsync("AddTabClasses");
                await JSRuntime.InvokeVoidAsync("AddAcceptAttribute");
            }
            //await JSRuntime.InvokeVoidAsync("window.ValidateIcon");
            //await JSRuntime.InvokeVoidAsync("window.LoadForm");
        }

        private void AddSkill(Guid id, string name)
        {
            _skills.Remove(_skills.FirstOrDefault(x => x.Id == id));
            _postModel.Skills.Add(new SkillViewModel { Id = id, Name = name });
        }

        private void RemoveSkill(Guid id, string name)
        {
            _skills.Add(new SkillViewModel { Id = id, Name = name });
            _postModel.Skills.Remove(_postModel.Skills.FirstOrDefault(x => x.Id == id));
        }

        private void AddCertification(Guid id, string name)
        {
            _postModel.Certifications.Add(new CertificationViewModel { Id = id, Name = name });
            _certifications.Remove(_certifications.FirstOrDefault(x => x.Id == id));
        }

        private void RemoveCertification(Guid id, string name)
        {
            _certifications.Add(new CertificationViewModel { Id = id, Name = name });
            _postModel.Certifications.Remove(_postModel.Certifications.FirstOrDefault(x => x.Id == id));
        }

        private async Task AddSkillWithEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await AddNewSkill();
            }
        }

        private void SetSkill(string e)
        {
            _newSkill = e;
        }

        private async Task AddNewSkill()
        {
            if (string.IsNullOrEmpty(_newSkill))
            {

            }
            else
            {
                var response = await Service.SendRequest("Post", $"Skills/AddSkill?skill={_newSkill}", false, JsonConvert.SerializeObject(_newSkill));
                if (response.IsSuccessStatusCode)
                {
                    _newSkill = string.Empty;
                    var skillResponse = await Service.SendRequest("Get", "Skills");
                    if (skillResponse.IsSuccessStatusCode)
                    {
                        _skills = await Service.GetAll<SkillViewModel>(skillResponse);
                    }
                    foreach (var item in _postModel.Skills.ToArray())
                    {
                        _skills.Remove(_skills.FirstOrDefault(x => x.Id == item.Id));
                    }
                }
                else
                {
                    _skillMessage = $"Skill {_newSkill} Cannot be added";
                    //await JSRuntime.InvokeVoidAsync("");
                    _newSkill = string.Empty;
                }
            }
        }

        private async Task AddCertificationWithEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await AddNewCertification();
            }
        }

        private void SetCertification(string e)
        {
            _newCertification = e;
        }

        private async Task AddNewCertification()
        {
            if (string.IsNullOrEmpty(_newCertification))
            {

            }
            else
            {
                var response = await Service.SendRequest("Post", $"Certifications/AddCertification?certification={_newCertification}", false, JsonConvert.SerializeObject(_newCertification));
                if (response.IsSuccessStatusCode)
                {
                    _newCertification = string.Empty;
                    var certificationResponse = await Service.SendRequest("Get", "Certifications");
                    if (certificationResponse.IsSuccessStatusCode)
                    {
                        _certifications = await Service.GetAll<CertificationViewModel>(certificationResponse);
                    }
                    foreach (var item in _postModel.Certifications.ToArray())
                    {
                        _certifications.Remove(_certifications.FirstOrDefault(x => x.Id.Equals(item.Id)));
                    }
                }
                else
                {
                    _skillMessage = $"Certification {_newCertification} Cannot be added";
                    //await JSRuntime.InvokeVoidAsync("");
                    _newCertification = string.Empty;
                }
            }
        }

        //private bool _submit;
        private async Task HandleValidSubmit()
        {
            if (_postModel.Skills.Count > 0 && _postModel.Certifications.Count > 0 && ValidatePersonalInfo() && ValidateContactInfo())
            {
                _certificationInfoMessage = string.Empty;
                var response = await Service.SendRequest("Post", "Survey", false, JsonConvert.SerializeObject(_postModel));
                if (response.IsSuccessStatusCode)
                {
                    //var responseStatus = await Service.Post(response);
                    //if (responseStatus == HttpStatusCode.OK)
                    //{
                    await LocalStorage.SetAsync("FilledSurveyForm", "True");
                    await LocalStorage.SetAsync($"all", $"memberid,id,FilledSurveyForm");
                    await FileUpload.SaveLogoImage(_postModel.PersonalInfo.ProfilePicture);
                    await FileUpload.SaveMainSiteImage(_postModel.PersonalInfo.ProfilePicture);
                    await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsNotAuthenticated();
                    //}
                    return;
                }
                else
                {
                    FileUpload.DeleteImage(_postModel.PersonalInfo.ProfilePicture);
                }
            }
            else
            {
                _certificationInfoMessage = "Please Fill All Required Field *";
            }
        }

        private bool ValidatePersonalInfo()
        {
            if (string.IsNullOrEmpty(_postModel.PersonalInfo.FirstName) || string.IsNullOrEmpty(_postModel.PersonalInfo.LastName)
                || _postModel.PersonalInfo.GenderId == default || string.IsNullOrEmpty(_postModel.PersonalInfo.AboutMySelf)
                || _postModel.PersonalInfo.DateOfBirth == default)
            {
                if (_role.Equals("Gym") && string.IsNullOrEmpty(_postModel.PersonalInfo.FirstName))
                {
                    _personalInfoMessage = "Fill All Personal Information Fields";
                }
                else
                {
                    _personalInfoMessage = "Fill All Personal Information Fields";
                }
                return false;
            }
            _personalInfoMessage = string.Empty;
            return true;
        }

        private bool ValidateContactInfo()
        {
            if (string.IsNullOrEmpty(_postModel.Contact.PhoneNumber) || string.IsNullOrEmpty(_postModel.Contact.Email)
                || _postModel.Contact.CityId == default || _postModel.Contact.DistrictId == default
                || string.IsNullOrEmpty(_postModel.Contact.ZipCode) || string.IsNullOrEmpty(_postModel.Contact.Address))
            {
                _contactInfoMessage = "Fill All Contact Information Fields";
                return false;
            }
            _contactInfoMessage = string.Empty;
            return true;
        }

        private async Task SetCountry(Guid e)
        {
            _postModel.Contact.CountryId = e;
            if (_postModel.Contact.CountryId != default)
            {
                _countryMessage = string.Empty;
                var response = await Service.SendRequest("Get", $"States/GetStatesByCountry?countryId={e}");
                if (response.IsSuccessStatusCode)
                {
                    _states = await Service.GetAll<StateViewModel>(response);
                }
            }
            else
            {
                _countryMessage = "Country is Required";
            }
            _postModel.Contact.StateId = default;
            _postModel.Contact.CityId = default;
            _postModel.Contact.DistrictId = default;
        }

        private async Task SetState(Guid e)
        {
            _postModel.Contact.StateId = e;
            if (_postModel.Contact.StateId != default)
            {
                _stateMessage = string.Empty;
                var response = await Service.SendRequest("Get", $"Cities/GetCitiesByState?stateId={e}");
                if (response.IsSuccessStatusCode)
                {
                    _cities = await Service.GetAll<CityViewModel>(response);
                }
            }
            else
            {
                _stateMessage = "State is Required";
            }
            _postModel.Contact.CityId = default;
            _postModel.Contact.DistrictId = default;
        }

        private async Task SetCity(Guid e)
        {
            _postModel.Contact.CityId = e;
            if (_postModel.Contact.CityId != default)
            {
                _cityMessage = string.Empty;
                var response = await Service.SendRequest("Get", $"Districts/GetDistrictsByCity?cityId={e}");
                if (response.IsSuccessStatusCode)
                {
                    _districts = await Service.GetAll<DistrictViewModel>(response);
                }
            }
            else
            {
                _cityMessage = "City is Required";
            }
            _postModel.Contact.DistrictId = default;
        }

        private void SetDistrict(Guid e)
        {
            _postModel.Contact.DistrictId = e;
            if (_postModel.Contact.DistrictId == default)
            {
                _districtMessage = "District is Required";
            }
            else
            {
                _districtMessage = string.Empty;
            }
        }

        private string _imgSrc = string.Empty;
        async Task FileChange(IMatFileUploadEntry[] e)
        {
            //_imgSrc = await FileUpload.UploadAsync(e.FirstOrDefault(), "gym");
            //_postModel.PersonalInfo.Picture = Path.GetFileName(_imgSrc);
            var dirExist = true;

            string userFolderPath = Path.Combine(WebHostEnvironment.WebRootPath, $@"Content\Images", _role, "ProfilePictures", _id);
            if (!Directory.Exists(userFolderPath))
            {
                dirExist = Directory.CreateDirectory(userFolderPath).Exists;
            }
            if (dirExist)
            {
                PostUploadViewModel post = new();
                string postPath = Path.Combine(userFolderPath, post.PostId.ToString());
                string imagePath = Path.Combine(postPath, e.FirstOrDefault().Name);

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

                _postModel.PersonalInfo.ProfilePicture = await FileUpload.UploadAsync(e.FirstOrDefault(), imagePath);
                if (!string.IsNullOrEmpty(_postModel.PersonalInfo.ProfilePicture))
                {
                    post.File = _postModel.PersonalInfo.ProfilePicture;
                    post.UserId = _id;
                    post.Text = "text";
                    var response = await Service.SendRequest("Post", "Profile/UserProfile", false, JsonConvert.SerializeObject(post));
                    if (response.IsSuccessStatusCode)
                    {
                        _imgSrc = _postModel.PersonalInfo.ProfilePicture.Split(Constant.WwwRoot)[1].Replace('\\', '/');
                    }
                    //if (!_isOpen)
                    //{
                    //    _isOpen = await JSRuntime.InvokeAsync<bool>("ShowModal", "imageUploadModal");
                    //}
                }
                else
                {
                }
            }
        }

        private bool ValidateSkill()
        {
            var result = _postModel.Skills.Count > 0;
            if (!result)
            {
                _skillInfoMessage = "Select At least One Skill";
                return false;
            }
            else
            {
                _skillInfoMessage = string.Empty;
                return true;
            }
        }

        private bool ValidateCertification()
        {
            var result = _postModel.Certifications.Count > 0;
            if (!result)
            {
                _certificationInfoMessage = "Select At least One Certification";
                return false;
            }
            else
            {
                _certificationInfoMessage = string.Empty;
                return true;
            }
        }

        protected void FromMonday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.FromMonday = e;
            }
        }

        protected void ToMonday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.ToMonday = e;
            }
        }

        protected void FromTuesday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.FromTuesday = e;
            }
        }

        protected void ToTuesday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.ToTuesday = e;
            }
        }

        protected void FromWednesday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.FromWednesday = e;
            }
        }

        protected void ToWednesday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.ToWednesday = e;
            }
        }

        protected void FromThursday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.FromThursday = e;
            }
        }
        protected void ToThursday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.ToThursday = e;
            }
        }

        protected void FromFriday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.FromFriday = e;
            }
        }

        protected void ToFriday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.ToFriday = e;
            }
        }

        protected void FromSaturday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.FromSaturday = e;
            }
        }

        protected void ToSaturday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.ToSaturday = e;
            }
        }

        protected void FromSunday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.FromSunday = e;
            }
        }

        protected void ToSunday(string e)
        {
            if (string.IsNullOrEmpty(e))
            {

            }
            else
            {
                _postModel.TimeSlot.ToSunday = e;
            }
        }
    }
}

