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
    [Route("api/phonebooks")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookService _phoneBookService;
        private readonly IMapper _mapper;

        public PhoneBookController(IPhoneBookService phoneBookService, IMapper mapper)
        {
            _phoneBookService = phoneBookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var phoneBooks = await _phoneBookService.GetPhoneBooksAsync();
                return new OkObjectResult(_mapper.Map<IList<ApiPhoneBook>>(phoneBooks)?.OrderBy(p => p.Name));

            }
            catch (ValidateException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        
        }

        [HttpGet("{id}", Name = "PhoneBookGet")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var phoneBook = await _phoneBookService.GetPhoneBookAsync(id);

                if (phoneBook == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(_mapper.Map<ApiPhoneBook>(phoneBook));

            }
            catch (ValidateException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ApiCreatePhoneBook phoneBookModel)
        {
            try
            {
               var phoneBook = await _phoneBookService.CreatePhoneBookAsync(phoneBookModel.Name);
               return CreatedAtRoute(
                    routeName: "PhoneBookGet",
                    routeValues: new {id = phoneBook.Id},
                    value: new {Name = phoneBook.Name});
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
        public async Task<IActionResult> Put(ApiEditPhoneBook phoneBookModel)
        {
            try
            {
                var phoneBook = _mapper.Map<PhoneBook>(phoneBookModel);
                await _phoneBookService.EditPhoneBookAsync(phoneBook);
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
                await _phoneBookService.DeletePhoneBookAsync(id);
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
