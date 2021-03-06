﻿using KariyerNET.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerNET.Model.CompanySide
{
    public class Sector : BaseEntity
    {
        public string SectorName { get; set; }

        //nav props
        public virtual List<Company> Companies { get; set; }
        public virtual List<JobAdvert> JobAdverts { get; set; }


    }
}
