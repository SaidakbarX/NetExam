using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Domain.Entity;


    public class CodeQuestion
    {
        public long Id { get; set; }
        public string Title { get; set; }

      
        public string Description { get; set; }

        public string FunctionName { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<TestCase> TestCases { get; set; } 
        public virtual ICollection<StarterTemplate> StarterTemplates { get; set; } 
        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; }
    }


