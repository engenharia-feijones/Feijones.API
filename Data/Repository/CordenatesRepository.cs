using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Cliente.API.Data.Base;
using Cliente.API.Model;


namespace Cliente.API.Data.Repository
{
    public class CordenatesRepository
	{

		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<CordenatesRepository> _log;
		private readonly IConfiguration _config;

		public CordenatesRepository(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<CordenatesRepository>();
			_config = config;
		}

		#region LoadModel

		private List<CordenatesModel> Load(DataSet data)
		{
			List<CordenatesModel> cordenates;
			CordenatesModel cordenate;

			try
			{
				cordenates = new List<CordenatesModel>();

				foreach (DataRow row in data.Tables[0].Rows)
				{
					cordenate = new CordenatesModel();

					cordenate.ID = row.Field<int>("ID");
					cordenate.ID_end = row.Field<int>("ID_end");
					cordenate.Latitude = row.Field<string>("Latitude");
					cordenate.Longitude = row.Field<string>("Longitude");



					cordenates.Add(cordenate);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return cordenates;
		}

		#endregion

		#region Change Data

		public CordenatesModel Insert(CordenatesModel Cordenate)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"INSERT INTO cordenadas
											(
												 
												ID_end
												,Latitude
												,Longitude
												
											)
										 OUTPUT inserted.ID 
										 VALUES
											(
												 
												@ID_end
												,@Latitude
												,@Longitude
												
											)");

				
				command.Parameters.AddWithValue("ID_end", Cordenate.ID_end.AsDbValue());
				command.Parameters.AddWithValue("Latitude", Cordenate.Latitude.AsDbValue());
				command.Parameters.AddWithValue("Longitude", Cordenate.Longitude.AsDbValue());
			


				Cordenate.ID = (int)dataConnection.ExecuteScalar(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Cordenate;
		}

		public CordenatesModel Update(CordenatesModel Cordenate)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"UPDATE cordenadas SET
 
												ID_end
												,Latitude
												,Longitude

											WHERE ID = @ID");

				command.Parameters.AddWithValue("ID", Cordenate.ID.AsDbValue());
				command.Parameters.AddWithValue("Latitude", Cordenate.Latitude.AsDbValue());
				command.Parameters.AddWithValue("Longitude", Cordenate.Longitude.AsDbValue());
			

				dataConnection.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Cordenate;
		}

		public bool Delete(int id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			int result;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"DELETE from cordenadas WHERE ID = @ID");

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

		public CordenatesModel Get(int id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			CordenatesModel Cordenate;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM cordenadas WHERE ID_end = @ID");
				command.Parameters.AddWithValue("ID", id.AsDbValue());

				dataSet = dataConnection.ExecuteDataSet(command);

				Cordenate = Load(dataSet).FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Cordenate;
		}

		public List<CordenatesModel> Get()
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			List<CordenatesModel> Cordenates;
	

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM cordenadas");				

				dataSet = dataConnection.ExecuteDataSet(command);

				Cordenates = Load(dataSet);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Cordenates;
		}

		#endregion
	}

}

