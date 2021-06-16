using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using MultipliqueV2.API.Data.Repository;
using MultipliqueV2.API.Model;

namespace MultipliqueV2.API.Business
{
	public class CustomersBO
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<CustomersBO> _log;
		private readonly IConfiguration _config;

		public CustomersBO(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<CustomersBO>();
			_config = config;
		}

		#region Change Data

		public Customer Insert(Customer customer)
		{
			CostumerRepository ClienteRepository;

			try
			{
				ClienteRepository = new CostumerRepository(_loggerFactory, _config);

				if (customer.ID == 0)
				{
					customer = ClienteRepository.Insert(customer);
				}
				else
				{
					throw new Exception("ID diferente de 0, avalie a utilização do PUT");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return customer;
		}

		public Customer Update(Customer customer)
		{
			CostumerRepository costumerRepository;

			try
			{
				costumerRepository = new CostumerRepository(_loggerFactory, _config);

				if (customer.ID == 0)
				{
					throw new Exception("ID diferente de 0, avalie a utilização do POST");
				}
				else
				{
					costumerRepository.Update(customer);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return customer;
		}

		public void Delete(int id)
		{
			CostumerRepository costumerRepository;

			Customer customer;

			try
			{
				if (id == 0)
				{
					throw new Exception("ID inválido");
				}
				else
				{
					costumerRepository = new CostumerRepository(_loggerFactory, _config);
					customer = Get(id);
					if (costumerRepository != null)
					{

						costumerRepository.Delete(id);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		#endregion

		#region Retrieve Repository

		public Customer Get(int id)
		{
			CostumerRepository costumerRepository;
			Customer customer;

			try
			{
				costumerRepository = new CostumerRepository(_loggerFactory, _config);

				customer = costumerRepository.Get(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return customer;
		}

		public List<Customer> Get(string name = null)
		{
			CostumerRepository costumerRepository;
			List<Customer> customers;

			try
			{
				costumerRepository = new CostumerRepository(_loggerFactory, _config);

				customers = costumerRepository.Get(name);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return customers;
		}

		#endregion
	}

	
}
