using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerDataAccess;

namespace TaskManagerWeb.Models;

public class UserProjectModel
{
    public int ProjectId { get; set; }
    public Role Role { get; set; } = Role.Collaborator;
}