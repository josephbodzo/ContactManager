using System.Threading.Tasks;
using AutoMapper;
using ContactManager.Core.Services;
using ContactManager.Infrastructure.Services.Exceptions;
using ContactManager.Infrastructure.Services.Paging;
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

        [HttpGet(Name = "search")]
        public IActionResult SearchPhoneBooks(SearchModel searchModel)
        {
            try
            {
                var pagedList = _phoneBookService.SearchPhoneBooks(searchModel?.Search?.Value, new PagingOptions
                {
                    Take = searchModel?.Length ?? 0,
                    Skip = searchModel?.Start ?? 0
                });

                return new OkObjectResult(pagedList.Map(_mapper.Map<ApiPhoneBook>));

            }
            catch (ValidateException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        
        }

        [HttpGet("{id}", Name = "PhoneBookGet")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] string name)
        {
            try
            {
               var phoneBook = await _phoneBookService.CreatePhoneBookAsync(name);
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

        // PUT: api/PhoneBook/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
