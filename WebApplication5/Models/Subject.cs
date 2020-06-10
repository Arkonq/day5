﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}