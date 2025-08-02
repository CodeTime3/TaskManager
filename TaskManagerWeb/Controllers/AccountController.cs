using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManagerDataAccess.DataAccess.Services;
using TaskManagerDataAccess.Models;
using TaskManagerWeb.EmailService;
using TaskManagerWeb.Models;
using TaskManagerWeb.Services.JwtService;

namespace TaskManagerWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserService _user;
    private readonly EmailConfirmService _confirm;
    private readonly SendEmailConfirmService _sendEmail;
    private readonly JwtService _jwtService;

    public AccountController(UserService user, EmailConfirmService confirm, SendEmailConfirmService sendEmail, JwtService jwtService)
    {
        _user = user;
        _confirm = confirm;
        _sendEmail = sendEmail;
        _jwtService = jwtService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpModel signUp)
    {
        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        User user = new User();
        var users = await _user.GetAllUsers();

        foreach (var usr in users)
        {
            if (usr.UserName.Equals(signUp.Username))
            {
                return BadRequest("Username already exist");
            }

            if (usr.UserMail.Equals(signUp.UserMail))
            {
                return BadRequest("Email already exist");
            }
        }

        string hash = passwordHasher.HashPassword(user, signUp.UserHash);
        user = new User(signUp.Username, signUp.UserMail, hash);
        await _user.AddUser(user);
        user = await _user.GetUserByUserMail(signUp.UserMail);
        var token = CreateToken();
        EmailConfirm email = new EmailConfirm(user.UserId, token, GetDate());
        await _confirm.AddEmailConfirm(email);
        _sendEmail.SendMail(user.UserMail, token);

        return Ok("Check your email");
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery]string token)
    {
        if (token is null)
        {
            return BadRequest("Token expired or inavlid. If you signed up before click here to receive a new verify email");
        }

        var confirmEmail = await _confirm.GetEmailConfirmByToken(token);


        if ((DateTime.Now - confirmEmail.EmailConfirmCreateAt).TotalHours > 1)
        {
            return BadRequest("Token expired. If you signed up before click here to receive a new verify email");
        }

        if (!confirmEmail.IsEmailConfirmed)
        {
            confirmEmail.IsEmailConfirmed = true;
            await _confirm.UpdateEmailConfirm(confirmEmail);
        }

        var user = await _user.GetUser(confirmEmail.UserId);
        user.IsUsed = true;
        await _user.UpdateUser(user);

        var jwt = _jwtService.CreateJwt(user);

        return Ok(jwt);
    }

    [HttpPost("resend-email")]
    public async Task<IActionResult> ResendEmail([FromBody] ResendEmailModel resendEmail)
    {   
        var user = await _user.GetUserByUserMail(resendEmail.UserMail);

        if (user is null)
        {
            return BadRequest("Credentials non-existent. Retry to sign up");
        }

        if (user.IsUsed)
        {
            return BadRequest("You are verify. Try to sign in");
        }

        var token = CreateToken();
        EmailConfirm email = new EmailConfirm(user.UserId, token, GetDate());
        await _confirm.AddEmailConfirm(email);
        _sendEmail.SendMail(user.UserMail, token);

        return Ok("Check your email");
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInModel signIn)
    {
        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        var user = await _user.GetUserByUserMail(signIn.UserMail);

        if (user is null)
        {
            return BadRequest("User not exist. Try to Sign up");
        }

        if (!user.IsUsed)
        {
            return BadRequest("user is not valid. Click here to verify your email");
        }

        var hash = passwordHasher.VerifyHashedPassword(user, user.UserHash, signIn.UserHash);

        if (user.UserMail.Equals(signIn.UserMail) && hash is PasswordVerificationResult.Success)
        {
            var jwt = _jwtService.CreateJwt(user);

            return Ok(jwt);
        }

        return BadRequest("Incorrect email or password");
    }

    [HttpPut("update-user")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel updateUser)
    {   
        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        User user;
        
        if (updateUser is null)
        {
            return BadRequest("User not valid or not exist");
        }

        if (updateUser.UserOldMail.IsNullOrEmpty())
        {
            return BadRequest("Insert your old/actually email");
        }

        user = await _user.GetUserByUserMail(updateUser.UserOldMail);

        if (user is null)
        {
            return BadRequest("User dosen't exist");
        }

        if (!updateUser.Username.IsNullOrEmpty())
        {
            user.UserName = updateUser.Username;
        }

        if (!updateUser.UserNewMail.IsNullOrEmpty())
        {
            if (!updateUser.UserOldMail.Equals(updateUser.UserNewMail))
            {
                user.UserMail = updateUser.UserNewMail;
                user.IsUsed = false;
            }
        }

        if (!updateUser.UserHash.IsNullOrEmpty())
        {
            user.UserHash = passwordHasher.HashPassword(user, updateUser.UserHash);
        }
        
        await _user.UpdateUser(user);

        return Ok("Try to sign in");
    }

    //[HttpDelete("delete-user")]
    public async Task<IActionResult> DeleteUser()
    {   
        return Ok();
    }

    private string CreateToken()
    {
        Guid token = Guid.NewGuid();
        return token.ToString();
    }

    private DateTime GetDate()
    {
        DateTime date = DateTime.Now;
        return date;
    }
}