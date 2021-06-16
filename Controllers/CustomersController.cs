	using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MultipliqueV2.API.Business;
using MultipliqueV2.API.Model;

namespace MultipliqueV2.API.Controllers
{
	[EnableCors("Policy1")]
	[ApiController]
	[Route("api/[controller]")]

	public class CustomersController : ControllerBase
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<CustomersController> _log;
		private readonly IConfiguration _config;

		public CustomersController(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<CustomersController>();
			_config = config;
		}


		[HttpGet]
		
		public IActionResult Get()
		{
			CustomersBO customersBO;
			List<Customer> customers;
			ObjectResult response;

			try
			{
				_log.LogInformation("Starting Get()");

				customersBO = new CustomersBO(_loggerFactory, _config);
				customers = customersBO.Get();

				response = Ok(customers);

				_log.LogInformation($"Finishing Get() with '{customers.Count}' results");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// GET: api/Cliente/5
		[HttpGet("{id}", Name = "GetCliente")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Get(int id)
		{
			CustomersBO customersBO;
			Customer customer;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Get( {id} )");

				customersBO = new CustomersBO(_loggerFactory, _config);

				customer = customersBO.Get(id);

				if (customer != null)
				{
					response = Ok(customer);
				}
				else
				{
					response = NotFound(string.Empty);
				}

				_log.LogInformation($"Finishing Get( {id} )");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Post([FromBody] Customer customer)
		{
			CustomersBO customersBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Post('{JsonConvert.SerializeObject(customer, Formatting.None)}')");

				customersBO = new CustomersBO(_loggerFactory, _config);

				customer = customersBO.Insert(customer);

				response = Ok(customer);

				_log.LogInformation($"Finishing Post");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// PUT: api/Cliente/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Put(int id, Customer customer)
		{
			CustomersBO customersBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(customer, Formatting.None)}')");

				customersBO = new CustomersBO(_loggerFactory, _config);

				customer.ID = id;
				customer = customersBO.Update(customer);

				response = Ok(customer);

				_log.LogInformation($"Finishing Put( {id} )");
			}	
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Delete(int id)
		{
			CustomersBO customersBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Delete( {id} )");

				customersBO = new CustomersBO(_loggerFactory, _config);
				customersBO.Delete(id);

				response = Ok(string.Empty);

				_log.LogInformation($"Finishing Delete( {id} )");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}
	}
}
