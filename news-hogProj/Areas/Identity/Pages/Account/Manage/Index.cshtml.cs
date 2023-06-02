// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using news_hogProj.Objects;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace news_hogProj.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private BlobServiceClient _blobServiceClient;
        public string ProfilePhoto { get; set; }


        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            BlobServiceClient blobServiceClient)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _blobServiceClient = blobServiceClient;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            public string ProfilePhoto { get; set; }
            [Display(Name = "Profile Photo")]
            public IFormFile ProfilePhotoImage { get; set; }
        }
        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                ProfilePhoto = user.ProfilePhoto
            };

            ProfilePhoto = user.ProfilePhoto;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.ProfilePhotoImage != null && Input.ProfilePhotoImage.Length > 0)
            {
                var blobContainer = _blobServiceClient.GetBlobContainerClient("profile-photos");
                var blobClient = blobContainer.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(Input.ProfilePhotoImage.FileName));

                await blobClient.UploadAsync(Input.ProfilePhotoImage.OpenReadStream(), overwrite: true);

                user.ProfilePhoto = blobClient.Uri.AbsoluteUri;

                var updateProfilePhotoResult = await _userManager.UpdateAsync(user);
                if (!updateProfilePhotoResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set profile photo.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostResetPhotoAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            user.ProfilePhoto = "https://newshog.blob.core.windows.net/profile-photos/default.png";

            var updateProfilePhotoResult = await _userManager.UpdateAsync(user);
            if (!updateProfilePhotoResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to reset profile photo.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Profile photo reset successful.";
            return RedirectToPage();
        }
    }
}