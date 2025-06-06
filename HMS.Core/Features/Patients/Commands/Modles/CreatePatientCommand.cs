﻿using HMS.Core.Bases;
using HMS.Data.Entities.Enums;
using MediatR;

namespace HMS.Core.Features.Patients.Commands.Modles
{
    public class CreatePatientCommand : IRequest<Response<int>>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
    }
}
