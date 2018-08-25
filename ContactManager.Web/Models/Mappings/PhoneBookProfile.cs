using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactManager.Core.Entities;
using ContactManager.Infrastructure.Services.Paging;

namespace ContactManager.Web.Models.Mappings
{
    public class PhoneBookProfile: Profile
    {
        public PhoneBookProfile()
        {
            CreateMap<PhoneBook, ApiPhoneBook>();
        }
    }
}
