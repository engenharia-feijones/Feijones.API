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

namespace Endereco.API.Controllers
{
	[EnableCors("Policy1")]
	[ApiController]
	[Route("api/[controller]")]

	public class ClienteEnderecoController : ControllerBase
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<ClienteEnderecoController> _log;
		private readonly IConfiguration _config;

		public ClienteEnderecoController(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<ClienteEnderecoController>();
			_config = config;
		}


		[HttpGet("{id}")]
		
		public IActionResult Get(int id)
		{
			ClienteEnderecoBO clienteEnderecoBO;
			List<EnderecoModel> enderecos;
			ObjectResult response;

			try
			{
				_log.LogInformation("Starting Get()");

				clienteEnderecoBO = new ClienteEnderecoBO(_loggerFactory, _config);
				enderecos = clienteEnderecoBO.Get(id);

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

		[HttpPost("{documento}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Post([FromBody] EnderecoModel endereco, string documento)
		{
			ClienteEnderecoBO clienteEnderecoBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Post('{JsonConvert.SerializeObject(endereco, Formatting.None)}')");

				clienteEnderecoBO = new ClienteEnderecoBO(_loggerFactory, _config);

				endereco = clienteEnderecoBO.Insert(endereco, documento);

				response = Ok(endereco);

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
		public IActionResult Put(int id, EnderecoModel endereco)
		{
			ClienteEnderecoBO clienteEnderecoBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(endereco, Formatting.None)}')");

				clienteEnderecoBO = new ClienteEnderecoBO(_loggerFactory, _config);

				endereco.ID_end = id;
				endereco = clienteEnderecoBO.Update(endereco);

				response = Ok(endereco);

				_log.LogInformation($"Finishing Put( {id} )");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}
/*
		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Delete(int id)
		{
			ClienteEnderecoBO clienteEnderecoBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Delete( {id} )");

				clienteEnderecoBO = new ClienteEnderecoBO(_loggerFactory, _config);
				clienteEnderecoBO.Delete(id);

				response = Ok(string.Empty);

				_log.LogInformation($"Finishing Delete( {id} )");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}*/
	}








}
