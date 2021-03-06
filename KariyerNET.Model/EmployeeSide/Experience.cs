﻿using KariyerNET.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerNET.Model.EmployeeSide
{
    public class Experience : BaseEntity
    {
        //Deneyim
        public string CompanyName { get; set; }
        public string Sector { get; set; }
        public string Title { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeavingReason { get; set; } // ayrılma nedeni

        //nav prop
        public virtual int ResumeID { get; set; }
        public virtual Resume Resume { get; set; }
    }
}
