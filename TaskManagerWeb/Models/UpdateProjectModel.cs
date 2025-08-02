using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerDataAccess;

namespace TaskManagerWeb.Models;

public class UpdateProjectModel
{   
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public Status ProjectStatus { get; set; }
}