using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactManager.Core.Entities;
using ContactManager.Core.Services;
using ContactManager.Common.Exceptions;
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

        [HttpGet]
        [Route("getByPhoneNumber/", Order = 1)]
        public async Task<IActionResult> GetByPhoneNumber(string phoneNumber)
        {
            try
            {
                var phoneEntry = await _phoneEntryService.GetPhoneEntryByNumberAsync(phoneNumber);
                if (phoneEntry == null)
                {
                    return NotFound();
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
                await _phoneEntryService.CreatePhoneEntryAsync(phoneEntryModel.Name, phoneEntryModel.PhoneNumber, phoneBookId);

                //TODO: Return created response with the url of the created object
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

        [HttpDelete("{phoneBookId}/{id}")]
        public async Task<IActionResult> Delete(int phoneBookId, int id)
        {
            try
            {
                await _phoneEntryService.RemovePhoneEntryAsync(phoneBookId, id);
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
