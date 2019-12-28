﻿using KariyerNET.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerNET.Model.EmployeeSide
{
    public class Perfection:BaseEntity
    {
        //Yetkinlik
        public string PerfectionName { get; set; }
        //nav prop
        public int ResumeID { get; set; }
        public Resume Resume{ get; set; }
    }
}