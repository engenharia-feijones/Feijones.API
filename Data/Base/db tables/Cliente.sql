--------------------------------------------------------------------------
CREATE TABLE [dbo].[Cliente](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Cnpj] [varchar](20) NULL,
	[Cpf] [varchar](20) NULL,
	[Nome] [varchar](250) NULL,
	[DtNas] [varchar](100) NULL,
	[RazaoSoci] [nvarchar](50) NULL,
	[Email] [varchar](100) NOT NULL,
	[Tel] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE UNIQUE INDEX foo ON cliente(cnpj) WHERE cnpj IS NOT NULL;
CREATE UNIQUE INDEX foo2 ON cliente(cpf) WHERE cpf IS NOT NULL;

---------------------------------------------------------------------------

CREATE TABLE Endereco(
	[ID_end] [int] IDENTITY(1,1) NOT NULL,
	[ID_cliente] [int] NULL,
	[Nome_end] [varchar](250) NOT NULL,
	[Tel_end] [varchar](15) NOT NULL,
	[Cep] [varchar](9) NOT NULL,
	[Endereco] [varchar](250) NOT NULL,
	[Numero] [varchar](15) NOT NULL,
	[Complemento] [varchar](100) NOT NULL,
	[Bairro] [varchar](50) NOT NULL,
	[Cidade] [varchar](50) NOT NULL,
	[Estado] [char](2) NOT NULL,
	[Longitude] [varchar](100) NOT NULL,
	[Latitude] [varchar](100) NOT NULL,
) ON [PRIMARY]


ALTER TABLE [Endereco]  WITH CHECK ADD  CONSTRAINT [FK_Endereco] FOREIGN KEY([ID_cliente])
REFERENCES [Cliente] ([ID])
ON DELETE CASCADE

-- lembrar de adicionar primary key em produto-- 
ALTER TABLE [produto]  WITH CHECK ADD  CONSTRAINT [PK_endereco] PRIMARY KEY([ID_end])

ALTER TABLE [Endereco] CHECK CONSTRAINT [FK_Endereco]

---------------------------------------------------------------------------

CREATE TABLE produto(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[descricao] [int] NOT NULL,
	[preco] money NOT NULL,
	[qtd] [int] NULL,	

) ON [PRIMARY]

ALTER TABLE [produto]  WITH CHECK ADD  CONSTRAINT [PK_produto] PRIMARY KEY([ID])

---------------------------------------------------------------------------


CREATE TABLE Venda_item(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_prod] [int] NOT NULL,
	[qtd] [int] NOT NULL,
	
)ON [PRIMARY]

ALTER TABLE [Venda_item] WITH CHECK ADD CONSTRAINT [PK_Venda_item] PRIMARY KEY ([ID])


---------------------------------------------------------------------------

CREATE TABLE venda(
	
	[ID] [int] IDENTITY (1,1) NOT NULL,
	[ID_prod] [int] NOT NULL,
	[ID_cliente] [int] NOT NULL,
	[ID_end] [int] NOT NULL,
	[total] money NOT NULL,
	[ID_venda_item] [int] NOT NULL,
	

)ON [PRYMARY]

ALTER TABLE [venda]  WITH CHECK ADD  CONSTRAINT [PK_venda] PRIMARY KEY([ID])	
ALTER TABLE [venda]  WITH CHECK ADD  CONSTRAINT [FK_venda_prod] FOREIGN KEY([ID_prod])REFERENCES [Cliente] ([ID])
ALTER TABLE [venda]  WITH CHECK ADD  CONSTRAINT [FK_venda_cli] FOREIGN KEY([ID_cliente])REFERENCES [Cliente] ([ID])
ALTER TABLE [venda]  WITH CHECK ADD  CONSTRAINT [FK_venda_end] FOREIGN KEY([ID_end])REFERENCES [Cliente] ([ID_end])
ALTER TABLE [venda]  WITH CHECK ADD  CONSTRAINT [FK_venda_venda_item] FOREIGN KEY([ID_venda_item]) REFERENCES [Venda_item] ([ID])


---------------------------------------------------------------------------



