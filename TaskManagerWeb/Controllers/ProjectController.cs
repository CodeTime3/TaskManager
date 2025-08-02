using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerDataAccess.DataAccess.Services;
using TaskManagerDataAccess.Models;
using TaskManagerWeb.Models;

namespace TaskManagerWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectController : ControllerBase
{   
    private readonly ProjectService _project;

    public ProjectController(ProjectService project)
    {
        _project = project;
    }

    [HttpPost("create-project")]
    public async Task<IActionResult> CreateProject([FromBody] ProjectModel projectModel)
    {
        if (projectModel is null)
        {
            return BadRequest("Insert the name of the project");
        }

        var project = new Project(projectModel.ProjectName);
        project = await _project.AddProject(project);

        return Ok(project);
    }

    [HttpPut("update-project")]
    public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectModel projectModel)
    {
        if (projectModel is null)
        {
            return BadRequest("Insert the name or change the status");
        }

        var project = await _project.GetProject(projectModel.ProjectId);
        
        project.ProjectName = projectModel.ProjectName;

        if (projectModel.ProjectStatus != TaskManagerDataAccess.Status.NotValid)
        {
            project.ProjectStatus = projectModel.ProjectStatus;
        }

        project = await _project.UpdateProject(project);

        return Ok(project);
    }

    [HttpDelete("delete-project")]
    public async Task<IActionResult> DeleteProject([FromBody] DeleteProjectModel projectModel)
    {
        if (projectModel.ProjectId == 0)
        {
            return BadRequest("Project not found");
        }

        await _project.DeleteProject(projectModel.ProjectId);

        return Ok("Project deleted");
    }
}