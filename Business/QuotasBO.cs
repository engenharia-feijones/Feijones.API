using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using MultipliqueV2.API.Data.Repository;
using MultipliqueV2.API.Model;

namespace MultipliqueV2.API.Business
{
	public class QuotasBO
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<QuotasBO> _log;
		private readonly IConfiguration _config;

		public QuotasBO(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<QuotasBO>();
			_config = config;
		}

		#region Change Data

		public Quota Insert(Quota quota)
		{
			QuotaRepository ClienteRepository;

			try
			{
				ClienteRepository = new QuotaRepository(_loggerFactory, _config);

				if (quota.ID == 0)
				{
					quota = ClienteRepository.Insert(quota);
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

			return quota;
		}

		public Quota Update(Quota quota)
		{
			QuotaRepository quotaRepository;

			try
			{
				quotaRepository = new QuotaRepository(_loggerFactory, _config);

				if (quota.ID == 0)
				{
					throw new Exception("ID diferente de 0, avalie a utilização do POST");
				}
				else
				{
					quotaRepository.Update(quota);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return quota;
		}

		public void Delete(int id)
		{
			QuotaRepository quotaRepository;

			Quota quota;

			try
			{
				if (id == 0)
				{
					throw new Exception("ID inválido");
				}
				else
				{
					quotaRepository = new QuotaRepository(_loggerFactory, _config);
					quota = Get(id);
					if (quotaRepository != null)
					{

						quotaRepository.Delete(id);
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

		public Quota Get(int id)
		{
			QuotaRepository quotaRepository;
			Quota quota;

			try
			{
				quotaRepository = new QuotaRepository(_loggerFactory, _config);

				quota = quotaRepository.Get(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return quota;
		}

		public List<Quota> Get()
		{
			QuotaRepository quotaRepository;
			List<Quota> quotas;

			try
			{
				quotaRepository = new QuotaRepository(_loggerFactory, _config);

				quotas = quotaRepository.Get();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return quotas;
		}

		#endregion
	}

	
}
