﻿using System;

namespace ContactManager.Web.Models
{
    public class ApiPhoneBook
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ContactCount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
