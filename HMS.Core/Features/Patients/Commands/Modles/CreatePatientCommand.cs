using HMS.Core.Bases;
using HMS.Data.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Core.Features.Patients.Commands.Modles
{
    public class CreatePatientCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
    }
}
