using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MultipliqueV2.API.Data.Base;
using MultipliqueV2.API.Model;


namespace MultipliqueV2.API.Data.Repository
{
    public class IncomeRepository
	{

		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<IncomeRepository> _log;
		private readonly IConfiguration _config;

		public IncomeRepository(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<IncomeRepository>();
			_config = config;
		}

		#region LoadModel

		private List<Income> Load(DataSet data)
		{
			List<Income> incomes;
			Income income;

			try
			{
				incomes = new List<Income>();

				foreach (DataRow row in data.Tables[0].Rows)
				{
					income = new Income();

					income.ID = row.Field<int>("ID");
					income.QuotaID = row.Field<int>("QuotaID");
					income.Percentual = row.Field<decimal>("Percentual");
					income.Date = row.Field<string>("date");

					incomes.Add(income);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return incomes;
		}

		#endregion

		#region Change Data

		public Income Insert(Income income)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"INSERT INTO Incomes
											(
												 
												QuotaID
												,Percentual
												,date

											
											)
										 OUTPUT inserted.ID 
										 VALUES
											(
												 
												@QuotaID
												,@Percentual
												,@date
											
											)");

				
				command.Parameters.AddWithValue("QuotaID", income.QuotaID.AsDbValue());
				command.Parameters.AddWithValue("Percentual", income.Percentual.AsDbValue());
				command.Parameters.AddWithValue("date", income.Date.AsDbValue());


				income.ID = (int)dataConnection.ExecuteScalar(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return income;
		}

		public Income Update(Income income)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"UPDATE Incomes SET
 
												QuotaID = @QuotaID
												,Percentual = @Percentual
												,Date = @Date

											WHERE ID = @ID");

				command.Parameters.AddWithValue("ID", income.ID.AsDbValue());
				command.Parameters.AddWithValue("QuotaID", income.QuotaID.AsDbValue());
				command.Parameters.AddWithValue("Percentual", income.Percentual.AsDbValue());
				command.Parameters.AddWithValue("Date", income.Date.AsDbValue());


				dataConnection.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return income;
		}

		public bool Delete(long id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			int result;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"DELETE from Incomes WHERE ID = @ID");

				command.Parameters.AddWithValue("ID", id.AsDbValue());

				result = dataConnection.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return (result > 0);
		}

		#endregion

		#region Retrieve Data

		public Income Get(int id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			Income income;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM Incomes WHERE id = @ID");
				command.Parameters.AddWithValue("ID", id.AsDbValue());

				dataSet = dataConnection.ExecuteDataSet(command);

				income = Load(dataSet).FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return income;
		}

		public List<Income> Get()
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			List<Income> incomes;
			List<string> clauses;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM Incomes");

				clauses = new List<string>();

				dataSet = dataConnection.ExecuteDataSet(command);

				incomes = Load(dataSet);
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

