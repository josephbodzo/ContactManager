using AutoMapper;
using ContactManager.Core.Entities;

namespace ContactManager.Web.Models.Mappings
{
    public class PhoneEntryProfile : Profile
    {
        public PhoneEntryProfile()
        {
            CreateMap<PhoneEntry, ApiPhoneBookEntry>();
            CreateMap<ApiEditPhoneEntry, PhoneEntry>()
                .ForMember(x => x.BookEntries, opt => opt.Ignore())
                .ForMember(x => x.DateCreated, opt => opt.Ignore())
                .ForMember(x => x.DateModified, opt => opt.Ignore());
        }
    }
}
