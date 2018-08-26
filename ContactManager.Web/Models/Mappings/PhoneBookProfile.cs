using AutoMapper;
using ContactManager.Core.Entities;

namespace ContactManager.Web.Models.Mappings
{
    public class PhoneBookProfile: Profile
    {
        public PhoneBookProfile()
        {
            CreateMap<PhoneBook, ApiPhoneBook>();
            CreateMap<ApiEditPhoneBook, PhoneBook>()
                .ForMember(x => x.BookEntries, opt => opt.Ignore())
                .ForMember(x => x.ContactCount, opt => opt.Ignore())
                .ForMember(x => x.DateCreated, opt => opt.Ignore())
                .ForMember(x => x.DateModified, opt => opt.Ignore());
        }
    }
}
