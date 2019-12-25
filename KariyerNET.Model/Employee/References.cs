﻿using KariyerNET.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerNET.Model
{
    public class References : BaseEntity
    {
        //Referanslar
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }

        //mapping

        public int ResumeID { get; set; }
        public Resume Resume { get; set; }



    }
}
