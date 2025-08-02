using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerDataAccess;

namespace TaskManagerWeb.Models;

public class ProjectModel
{
    public string ProjectName { get; set; } = string.Empty;
    public Status ProjectStatus { get; set; } = Status.NotStarted;
}