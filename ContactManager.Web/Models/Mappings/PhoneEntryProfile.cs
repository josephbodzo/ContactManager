using AutoMapper;
using ContactManager.Core.Entities;
using ContactManager.Infrastructure.Services.Extensions;

namespace ContactManager.Web.Models.Mappings
{
    public class PhoneEntryProfile : Profile
    {
        public PhoneEntryProfile()
        {
            CreateMap<PhoneEntry, ApiPhoneBookEntry>()
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(f => f.PhoneNumber.ToPhoneNumberFormat()));
            CreateMap<ApiEditPhoneEntry, PhoneEntry>()
                .ForMember(x => x.BookEntries, opt => opt.Ignore())
                .ForMember(x => x.DateCreated, opt => opt.Ignore())
                .ForMember(x => x.DateModified, opt => opt.Ignore());
        }
    }
}
