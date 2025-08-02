using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManagerDataAccess.DataAccess.Services;
using TaskManagerDataAccess.Models;
using TaskManagerWeb.Models;
using TaskManagerWeb.Services.EmailService;

namespace TaskManagerWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserProjectController : ControllerBase
{
    private readonly UserProjectService _userProject;
    private readonly SendEmailAddUserProject _sendEmail;

    public UserProjectController(UserProjectService userProject, SendEmailAddUserProject sendEmail)
    {
        _userProject = userProject;
        _sendEmail = sendEmail;
    }

    [HttpPost("add-userproject")]
    public async Task<IActionResult> AddUserProject([FromBody] UserProjectModel userProjectModel)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId.IsNullOrEmpty())
        {
            return BadRequest("User not found. Retry to sign in");
        }

        if (userProjectModel.ProjectId == 0)
        {
            return BadRequest("There is a problem with the project");
        }

        var userProject = new UserProject(userProjectModel.ProjectId, Int32.Parse(userId), userProjectModel.Role);
        var userPrj = await _userProject.AddUserProject(userProject);

        return Ok(userPrj);
    }

    [HttpPost("send-invite")]
    public async Task<IActionResult> SendInvite([FromBody] SendInviteModel sendInvite)
    {
        if (sendInvite.UserMail.IsNullOrEmpty())
        {
            return BadRequest("Email not valid");
        }

        if (sendInvite.ProjectId == 0)
        {
            return BadRequest("Project not found. Retry");
        }

        _sendEmail.SendMail(sendInvite.UserMail, sendInvite.ProjectId);

        return Ok("Check out your email. Try in the spam section");
    }

    [HttpGet("get-projects")]
    public async Task<IActionResult> GetProjects()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId.IsNullOrEmpty())
        {
            return BadRequest("User not found. Retry to sign in");
        }
        //potrebbe servire una join, informarsi sull'oggetto nell'entity per la fk
        var projects = await _userProject.GetProjectsByUserId(Int32.Parse(userId));

        return Ok(projects);
    }

    [HttpGet("get-users")]
    public async Task<IActionResult> GetUsersByProject([FromBody] GetUserProjectModel userProjectModel)
    {   //potrebbe servire una join, informarsi sull'oggetto nell'entity per la fk
        var users = await _userProject.GetUsersByProjectId(userProjectModel.ProjectId);

        return Ok(users);
    }

    [HttpPut("update-userproject")]
    public async Task<IActionResult> UpdateUserProject(UpdateUserProjectModel userProjectModel)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId.IsNullOrEmpty())
        {
            return BadRequest("User not found. Retry to sign in");
        }

        if (userProjectModel.ProjectId == 0)
        {
            return BadRequest("There is a problem with the project");
        }

        var userProject = new UserProject(userProjectModel.ProjectId, Int32.Parse(userId), userProjectModel.Role);
        var userPrj = await _userProject.UpdateUserProject(userProject);

        return Ok(userPrj);
    }

    [HttpDelete("delete-userfromproject")]
    public async Task<IActionResult> DeleteUserFromProject([FromBody] DeleteUserProjectModel userProjectModel)
    {   
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId.IsNullOrEmpty())
        {
            return BadRequest("User not found. Retry to sign in");
        }

        if (userProjectModel.ProjectId == 0)
        {
            return BadRequest("There is a problem with the project");
        }

        var userProject = new UserProject(userProjectModel.ProjectId, Int32.Parse(userId));
        await _userProject.DeleteUserFromProject(userProject);

        return Ok("User removed");
    }

    [HttpDelete("delete-allusersfromproject")]
    public async Task<IActionResult> DeleteAllUsersFromProject([FromBody] DeleteUserProjectModel userProjectModel)
    {   
        if (userProjectModel.ProjectId == 0)
        {
            return BadRequest("There is a problem with the project");
        }

        await _userProject.DeleteAllUsers(userProjectModel.ProjectId);

        return Ok("User removed");
    }
}