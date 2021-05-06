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
using Cliente.API.Business;
using Cliente.API.Model;

namespace Cliente.API.Controllers
{
	[EnableCors("Policy1")]
	[ApiController]
	[Route("api/[controller]")]

	public class VendaItemController : ControllerBase
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<VendaItemController> _log;
		private readonly IConfiguration _config;

		public VendaItemController(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<VendaItemController>();
			_config = config;
		}


		[HttpGet]
		
		public IActionResult Get()
		{
			VendaItemBO vendaItemBO;
			List<VendaItemModel> enderecos;
			ObjectResult response;

			try
			{
				_log.LogInformation("Starting Get()");

				vendaItemBO = new VendaItemBO(_loggerFactory, _config);
				enderecos = vendaItemBO.Get();

				response = Ok(enderecos);

				_log.LogInformation($"Finishing Get() with '{enderecos.Count}' results");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// GET: api/Cliente/5
		[HttpGet("{id}", Name = "GetVendaItem")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Get(int id)
		{
			VendaItemBO vendaItemBO;
			VendaItemModel vendaItem;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Get( {id} )");

				vendaItemBO = new VendaItemBO(_loggerFactory, _config);

				vendaItem = vendaItemBO.Get(id);

				if (vendaItem != null)
				{
					response = Ok(vendaItem);
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
		public IActionResult Post([FromBody] VendaItemModel vendaItem)
		{
			VendaItemBO vendaItemBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Post('{JsonConvert.SerializeObject(vendaItem, Formatting.None)}')");

				vendaItemBO = new VendaItemBO(_loggerFactory, _config);

				vendaItem = vendaItemBO.Insert(vendaItem);

				response = Ok(vendaItem);

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
		public IActionResult Put(int id, VendaItemModel vendaItem)
		{
			VendaItemBO vendaItemBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(vendaItem, Formatting.None)}')");

				vendaItemBO = new VendaItemBO(_loggerFactory, _config);

				vendaItem.ID = id;
				vendaItem = vendaItemBO.Update(vendaItem);

				response = Ok(vendaItem);

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
			VendaItemBO vendaItemBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Delete( {id} )");

				vendaItemBO = new VendaItemBO(_loggerFactory, _config);
				vendaItemBO.Delete(id);

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
