using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactManager.Core.Entities;
using ContactManager.Core.Services;
using ContactManager.Infrastructure.Services.Exceptions;
using ContactManager.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Web.Controllers
{
    [Route("api/phoneentries")]
    [ApiController]
    public class PhoneEntryController : ControllerBase
    {
        private readonly IPhoneEntryService _phoneEntryService;
        private readonly IMapper _mapper;

        public PhoneEntryController(IPhoneEntryService phoneEntryService, IMapper mapper)
        {
            _phoneEntryService = phoneEntryService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getEntries/{phoneBookId}", Order = 1)]
        public async Task<IActionResult> GetByBookId(int phoneBookId)
        {
            try
            {
                var phoneEntries = await _phoneEntryService.GetPhoneEntriesAsync(phoneBookId);
                return new OkObjectResult(_mapper.Map<IList<ApiPhoneBookEntry>>(phoneEntries)?.OrderBy(p => p.Name));

            }
            catch (ValidateException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        
        }

        [HttpGet("{id}", Name = "PhoneEntryGet")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var phoneEntry = await _phoneEntryService.GetPhoneEntryAsync(id);

                if (phoneEntry == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(_mapper.Map<ApiPhoneBookEntry>(phoneEntry));

            }
            catch (ValidateException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost("{phoneBookId}")]
        public async Task<IActionResult> Post(int phoneBookId, ApiCreatePhoneEntry phoneEntryModel)
        {
            try
            {
               var phoneEntry = await _phoneEntryService.CreatePhoneEntryAsync(phoneEntryModel.Name, phoneEntryModel.PhoneNumber, phoneBookId);
               return CreatedAtRoute(
                    routeName: "PhoneEntryGet",
                    routeValues: new {phoneEntry.Id, phoneBookId},
                    value: new
                    {
                        phoneEntry.Name,
                        phoneEntry.PhoneNumber
                    });
            }
            catch (NotFoundException)
            {
                return new NotFoundResult();
            }
            catch (ValidateException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(ApiEditPhoneEntry phoneEntryModel)
        {
            try
            {
                var phoneEntry = _mapper.Map<PhoneEntry>(phoneEntryModel);
                await _phoneEntryService.EditPhoneEntryAsync(phoneEntry);
                return new OkResult();
            }
            catch (NotFoundException)
            {
                return new NotFoundResult();
            }
            catch (ValidateException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _phoneEntryService.DeletePhoneEntryAsync(id);
                return new OkResult();
            }
            catch (NotFoundException)
            {
                return new NotFoundResult();
            }
            catch (ValidateException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
