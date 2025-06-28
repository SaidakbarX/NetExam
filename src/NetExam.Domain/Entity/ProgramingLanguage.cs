using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Domain.Entity;

public class ProgrammingLanguage
{
    public int Id { get; set; }
    public string Name { get; set; }        
    public string DisplayName { get; set; } 
    public string Version { get; set; }     
    public bool IsActive { get; set; }

    public ICollection<StarterTemplate> Templates { get; set; } 
}

