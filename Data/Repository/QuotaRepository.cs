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
    public class QuotaRepository
	{

		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<QuotaRepository> _log;
		private readonly IConfiguration _config;

		public QuotaRepository(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<QuotaRepository>();
			_config = config;
		}

		#region LoadModel

		private List<Quota> Load(DataSet data)
		{
			List<Quota> quotas;
			Quota quota;

			try
			{
				quotas = new List<Quota>();

				foreach (DataRow row in data.Tables[0].Rows)
				{
					quota = new Quota();

					quota.ID = row.Field<int>("ID");
					quota.CustomerID = row.Field<int>("CustomerID");
					quota.Value = row.Field<decimal>("Value");
	
					quotas.Add(quota);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return quotas;
		}

		#endregion

		#region Change Data

		public Quota Insert(Quota quota)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"INSERT INTO Quotas
											(
												 
												CustomerID
												,Value
											
											)
										 OUTPUT inserted.ID 
										 VALUES
											(
												 
												@CustomerID
												,@Value
											
											)");

				
				command.Parameters.AddWithValue("CustomerID", quota.CustomerID.AsDbValue());
				command.Parameters.AddWithValue("Value", quota.Value.AsDbValue());


				quota.ID = (int)dataConnection.ExecuteScalar(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return quota;
		}

		public Quota Update(Quota quota)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"UPDATE Quotas SET
 
												CustomerID = @CustomerID
												,Value = @Value

											WHERE ID = @ID");

				command.Parameters.AddWithValue("ID", quota.ID.AsDbValue());
				command.Parameters.AddWithValue("CustomerID", quota.CustomerID.AsDbValue());
				command.Parameters.AddWithValue("Value", quota.Value.AsDbValue());

				dataConnection.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return quota;
		}

		public bool Delete(long id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			int result;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"DELETE from Quotas WHERE ID = @ID");

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

		public Quota Get(int id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			Quota quota;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM Quotas WHERE id = @ID");
				command.Parameters.AddWithValue("ID", id.AsDbValue());

				dataSet = dataConnection.ExecuteDataSet(command);

				quota = Load(dataSet).FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return quota;
		}

		public List<Quota> Get()
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			List<Quota> quotas;
			List<string> clauses;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM Quotas");

				clauses = new List<string>();

				dataSet = dataConnection.ExecuteDataSet(command);

				quotas = Load(dataSet);
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

