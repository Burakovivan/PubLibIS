﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.Models
{
    public class ApplicationUser: IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }
    }
}