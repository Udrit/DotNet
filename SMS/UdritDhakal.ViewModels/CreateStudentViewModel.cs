﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdritDhakal.Models;
using UdritDhakal.UtiIities;

namespace UdritDhakal.ViewModels
{
    public class CreateStudentViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DateOfJoin { get; set; }
        public bool Selected { get; set; }
        public string KeyId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Student ConvertModel(CreateStudentViewModel student) 
        {
            return new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                DOB = student.DOB,
                DateofJoin = student.DateOfJoin,
                Selected = student.Selected,
                KeyId = student.KeyId,

            };
        }

    }
}

