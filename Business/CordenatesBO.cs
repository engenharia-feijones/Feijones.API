using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Cliente.API.Data.Repository;
using Cliente.API.Model;

namespace Cliente.API.Business
{
	public class CordenatesBO
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<CordenatesBO> _log;
		private readonly IConfiguration _config;

		public CordenatesBO(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<CordenatesBO>();
			_config = config;
		}

		#region Change Data

		public CordenatesModel Insert(CordenatesModel cordenates)
		{
			CordenatesRepository cordenatesRepository;

			try
			{
				cordenatesRepository = new CordenatesRepository(_loggerFactory, _config);

				if (cordenates.ID == 0)
				{
					cordenates = cordenatesRepository.Insert(cordenates);
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

			return cordenates;
		}

		public CordenatesModel Update(CordenatesModel cordenates)
		{
			CordenatesRepository cordenatesRepository;

			try
			{
				cordenatesRepository = new CordenatesRepository(_loggerFactory, _config);

				if (cordenates.ID == 0)
				{
					throw new Exception("ID diferente de 0, avalie a utilização do POST");
				}
				else
				{
					cordenatesRepository.Update(cordenates);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return cordenates;
		}

		public void Delete(int id)
		{
			CordenatesRepository cordenatesRepository;
			
			CordenatesModel cordenates;

			try
			{
				if (id == 0)
				{
					throw new Exception("ID inválido");
				}
				else
				{
					cordenatesRepository = new CordenatesRepository(_loggerFactory, _config);
					cordenates = Get(id);
					if (cordenatesRepository != null)
					{

						cordenatesRepository.Delete(id);
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

		public CordenatesModel Get(int id)
		{
			CordenatesRepository cordenatesRepository;
			CordenatesModel cordenates;

			try
			{
				cordenatesRepository = new CordenatesRepository(_loggerFactory, _config);

				cordenates = cordenatesRepository.Get(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return cordenates;
		}

		public List<CordenatesModel> Get(string name = null)
		{
			CordenatesRepository cordenatesRepository;
			List<CordenatesModel> clientes;

			try
			{
				cordenatesRepository = new CordenatesRepository(_loggerFactory, _config);

				clientes = cordenatesRepository.Get();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return clientes;
		}

		#endregion
	}

	
}
