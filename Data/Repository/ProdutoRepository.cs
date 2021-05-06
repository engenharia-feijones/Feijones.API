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
	public class ProdutoRepository
	{

		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<ProdutoRepository> _log;
		private readonly IConfiguration _config;

		public ProdutoRepository(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<ProdutoRepository>();
			_config = config;
		}

		#region LoadModel

		private List<ProdutoModel> Load(DataSet data)
		{
			List<ProdutoModel> produtos;
			ProdutoModel produto;

			try
			{
				produtos = new List<ProdutoModel>();

				foreach (DataRow row in data.Tables[0].Rows)
				{
					produto = new ProdutoModel();

					produto.ID = row.Field<int>("ID");
					produto.descricao = row.Field<string>("descricao");
					produto.preco = row.Field<decimal>("preco");
					produto.qtd = row.Field<int>("qtd");
					produto.nome = row.Field<string>("nome");
					produto.tamanho = row.Field<string>("tamanho");

					produtos.Add(produto);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return produtos;
		}

		#endregion

		#region Change Data

		public ProdutoModel Insert(ProdutoModel Produto)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"SET ANSI_WARNINGS  OFF; INSERT INTO Produto
											(
												
												
												descricao
												,preco
												,qtd
												,nome
												,tamanho
										
											)
										 OUTPUT inserted.ID
										 VALUES
											(
												 
												
												@descricao
												,@preco
												,@qtd
												,@nome
												,@tamanho
										

											)");


				
				command.Parameters.AddWithValue("descricao", Produto.descricao.AsDbValue());
				command.Parameters.AddWithValue("preco", Produto.preco.AsDbValue());
				command.Parameters.AddWithValue("qtd", Produto.qtd.AsDbValue());
				command.Parameters.AddWithValue("nome", Produto.nome.AsDbValue());
				command.Parameters.AddWithValue("tamanho", Produto.tamanho.AsDbValue());



				Produto.ID = (int)dataConnection.ExecuteScalar(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Produto;
		}

		public ProdutoModel Update(ProdutoModel Produto)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@" SET ANSI_WARNINGS  OFF; UPDATE Produto SET

												 
												
												descricao= @descricao
												,preco= @preco
												,qtd= @qtd
												,nome=@nome
												,tamanho=@tamanho
											

											WHERE ID = @ID");

				command.Parameters.AddWithValue("ID", Produto.ID.AsDbValue());
				command.Parameters.AddWithValue("descricao", Produto.descricao.AsDbValue());
				command.Parameters.AddWithValue("preco", Produto.preco.AsDbValue());
				command.Parameters.AddWithValue("qtd", Produto.qtd.AsDbValue());
				command.Parameters.AddWithValue("nome", Produto.nome.AsDbValue());
				command.Parameters.AddWithValue("tamanho", Produto.tamanho.AsDbValue());


				dataConnection.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Produto;
		}

		public bool Delete(int id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			int result;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"DELETE from produto WHERE ID = @ID");

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

        public ProdutoModel Get(int ID)
        {
            SqlHelper dataConnection;
            SqlCommand command;
            DataSet dataSet;

            ProdutoModel Cliente;

            try
            {
                dataConnection = new SqlHelper(_loggerFactory, _config);

                command = new SqlCommand($"SELECT * FROM Produto WHERE ID = @ID");
                command.Parameters.AddWithValue("ID", ID.AsDbValue());

                dataSet = dataConnection.ExecuteDataSet(command);

                Cliente = Load(dataSet).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Cliente;
        }

        public List<ProdutoModel> Get(string nome = null)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			List<ProdutoModel> Produto;
			List<string> clauses;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM Produto");

				clauses = new List<string>();
				if (!string.IsNullOrEmpty(nome))
				{
					clauses.Add($"nome like '%' + @nome + '%'");
					command.Parameters.AddWithValue("descricao", nome.AsDbValue());
				}

				if (clauses.Count > 0)
				{
					command.CommandText += $" WHERE {string.Join(" and ", clauses)}";
				}

				dataSet = dataConnection.ExecuteDataSet(command);

			
				dataSet = dataConnection.ExecuteDataSet(command);

				Produto = Load(dataSet);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Produto;
		}

		#endregion
	}
}



