CREATE TABLE quotas (
	
	ID int  IDENTITY (1,1),
	CustomerID bigint not null,
	Value decimal 
);
ALTER TABLE quotas ADD CONSTRAINT pk_quotas_id  PRIMARY KEY (ID);
ALTER TABLE quotas ADD CONSTRAINT fk_quotas_CustomerID  FOREIGN KEY (CustomerID) REFERENCES costumer (ID)

CREATE TABLE incomes (
	
	ID int  IDENTITY (1,1),
	QuotaID bigint not null,
	Percentual decimal,
	date varchar(25)
);
ALTER TABLE incomes ADD CONSTRAINT pk_incomes_id  PRIMARY KEY (ID);
ALTER TABLE incomes ADD CONSTRAINT fk_incomes_QuotaID  FOREIGN KEY (QuotaID) REFERENCES quotas (ID)