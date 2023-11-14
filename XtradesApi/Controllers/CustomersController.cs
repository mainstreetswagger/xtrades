using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using XtradesApi.Db;
using XtradesApi.Dtos;
using XtradesApi.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XtradesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _appDb;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersController> _logger;
        public CustomersController(AppDbContext appDb,
            IMapper mapper,
            ILogger<CustomersController> logger)
        {
            _appDb = appDb;
            _mapper = mapper;
            _logger = logger;
        }
        // GET: api/<CustomersController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var customers = _appDb.Customers;
                var customerData = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerData>>(customers);
                return Ok(customerData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Unknown Error");
            }
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var customer = _appDb.Customers.Find(id);
                if (customer == null)
                {
                    _logger.LogError($"Customer id: {id} is not found.");
                    return NotFound($"Customer id: {id} is not found.");
                }
                var customerData = _mapper.Map<Customer, CustomerData>(customer);
                return Ok(customerData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Unknown Error");
            }
        }

        // POST api/<CustomersController>
        [HttpPost]
        public IActionResult Post([FromBody] UpsertCustomer createCustomer)
        {
            try
            {
                if (_appDb.Customers.Any(cu => cu.Email == createCustomer.Email))
                {
                    _logger.LogError($"Email: {createCustomer.Email} already exists.");
                    return BadRequest($"Email: {createCustomer.Email} already exists.");
                }
                if (_appDb.Customers.Any(cu => cu.Phone == createCustomer.Phone))
                {
                    _logger.LogError($"Phone: {createCustomer.Phone} already exists.");
                    return BadRequest($"Phone: {createCustomer.Phone} already exists.");
                }
                var customer = _mapper.Map<UpsertCustomer, Customer>(createCustomer);
                _appDb.Customers.Add(customer);
                _appDb.SaveChanges();

                var customerData = _mapper.Map<Customer, CustomerData>(customer);

                return CreatedAtAction(nameof(GetById), new { id = customerData.CustomerId }, customerData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Unknown Error");
            }
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpsertCustomer updateCustomer)
        {
            try
            {
                var customer = _appDb.Customers.Find(id);
                if (customer == null)
                {
                    _logger.LogError($"Customer id: {id} is not found.");
                    return NotFound($"Customer id: {id} is not found.");
                }
                if (_appDb.Customers.Any(cu => cu.Email == updateCustomer.Email && cu.Id != id))
                {
                    _logger.LogError($"Email: {updateCustomer.Email} already exists.");
                    return BadRequest($"Email: {updateCustomer.Email} already exists.");
                }
                if (_appDb.Customers.Any(cu => cu.Phone == updateCustomer.Phone && cu.Id != id))
                {
                    _logger.LogError($"Phone: {updateCustomer.Phone} already exists.");
                    return BadRequest($"Phone: {updateCustomer.Phone} already exists.");
                }
                customer.Email = updateCustomer.Email;
                customer.Phone = updateCustomer.Phone;
                customer.Name=updateCustomer.Name;
                _appDb.Customers.Update(customer);
                _appDb.SaveChanges();

                var customerData = _mapper.Map<Customer, CustomerData>(customer);

                return Ok(customerData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Unknown Error");
            }
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var customer = _appDb.Customers.Find(id);
                if (customer == null)
                {
                    _logger.LogError($"Customer id: {id} is not found.");
                    return NotFound($"Customer id: {id} is not found.");
                }
              
                _appDb.Customers.Remove(customer);
                _appDb.SaveChanges();

                var customerData = _mapper.Map<Customer, CustomerData>(customer);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Unknown Error");
            }
        }
    }
}
