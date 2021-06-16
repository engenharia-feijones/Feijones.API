using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using MultipliqueV2.API.Data.Repository;
using MultipliqueV2.API.Model;

namespace MultipliqueV2.API.Business
{
	public class IncomesBO
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<IncomesBO> _log;
		private readonly IConfiguration _config;

		public IncomesBO(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<IncomesBO>();
			_config = config;
		}

		#region Change Data

		public Income Insert(Income income)
		{
			IncomeRepository IncomeRepository;

			try
			{
				IncomeRepository = new IncomeRepository(_loggerFactory, _config);

				if (income.ID == 0)
				{
					income = IncomeRepository.Insert(income);
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

			return income;
		}

		public Income Update(Income income)
		{
			IncomeRepository incomeRepository;

			try
			{
				incomeRepository = new IncomeRepository(_loggerFactory, _config);

				if (income.ID == 0)
				{
					throw new Exception("ID diferente de 0, avalie a utilização do POST");
				}
				else
				{
					incomeRepository.Update(income);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return income;
		}

		public void Delete(int id)
		{
			IncomeRepository incomeRepository;

			Income income;

			try
			{
				if (id == 0)
				{
					throw new Exception("ID inválido");
				}
				else
				{
					incomeRepository = new IncomeRepository(_loggerFactory, _config);
					income = Get(id);
					if (incomeRepository != null)
					{

						incomeRepository.Delete(id);
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

		public Income Get(int id)
		{
			IncomeRepository incomeRepository;
			Income income;

			try
			{
				incomeRepository = new IncomeRepository(_loggerFactory, _config);

				income = incomeRepository.Get(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return income;
		}

		public List<Income> Get()
		{
			IncomeRepository incomeRepository;
			List<Income> incomes;

			try
			{
				incomeRepository = new IncomeRepository(_loggerFactory, _config);

				incomes = incomeRepository.Get();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return incomes;
		}

		#endregion
	}

	
}
