using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.QuizResponses.Dto
{
    public class QuizResponseGetDto
    {
        public string CustomerSubject { get; set; }
        public string CustomerBody { get; set; }

        public string AdminSubject { get; set; }    
        public string AdminBody { get; set; }
    }
}
