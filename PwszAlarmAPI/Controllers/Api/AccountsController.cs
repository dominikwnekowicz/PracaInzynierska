

using Microsoft.AspNet.Identity;
using PwszAlarmAPI.Infrastructure;
using PwszAlarmAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using PwszAlarmAPI.Providers;
using System.Configuration;
using System.Net.Http.Formatting;
namespace PwszAlarmAPI.Controllers.Api
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        //
        //GET api/accounts/users
        //
        [HttpGet]
        [Authorize]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }
        //
        //GET api/accounts/user/{id}
        //
        [HttpGet]
        [Authorize]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await this.AppUserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();
        }
        //
        //GET api/accounts/user/{username}
        //
        [HttpGet]
        [Authorize]
        [Route("user")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await this.AppUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(TheModelFactory.Create(user));
            }

            return NotFound();

        }
        //
        //POST api/accounts/user/notification
        //
        [HttpGet]
        [Authorize]
        [Route("user/notifications")]
        public async Task<IHttpActionResult> ChangeSendNotificationsAsync(string userId, bool sendNotifications)
        {
            var user = await this.AppUserManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.SendNotifications = sendNotifications;
                IdentityResult result = await this.AppUserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();
            }

            return NotFound();
        }
        //
        //POST api/accounts/create
        //
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = createUserModel.UserName,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                JoinDate = DateTime.Now.Date,
                SendNotifications = true
            };

            IdentityResult addUserResult = await AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            string code = await this.AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, code = code }));

            await this.AppUserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            return Created(locationHeader, TheModelFactory.Create(user));

        }
        //
        //GET: api/accounts/ConfirmEmail
        //
        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
        }
        //
        //POST: api/accounts/ChangePassword
        //
        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        public class FirebaseToken
        {
            public string UserId { get; set; }
            public string Token { get; set; }
        }
        //
        //POST: api/accounts/user/token
        //
        [HttpPost]
        [Authorize]
        [Route("user/fcmtoken")]
        public async Task<IHttpActionResult> PostFirebaseToken(FirebaseToken firebaseToken)
        {
            var user = await this.AppUserManager.FindByIdAsync(firebaseToken.UserId);
            if (user != null)
            {
                user.FirebaseToken = firebaseToken.Token;
                IdentityResult result = await this.AppUserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();
            }

            return NotFound();
        }
        //
        //DELETE: api/accounts/user/id
        //
        [HttpDelete]
        [Authorize]
        [Route("user/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {

            //Only SuperAdmin or Admin can delete users (Later when implement roles)

            var appUser = await this.AppUserManager.FindByIdAsync(id);

            if (appUser != null)
            {
                IdentityResult result = await this.AppUserManager.DeleteAsync(appUser);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();

            }

            return NotFound();

        }
    }
}
