Create table endereco

(
    cep varchar(45) primary key ,
    rua varchar(45) not null,
	bairro varchar(45) not null,
	cidade varchar(45) not null,
	estado varchar(45) not null
)
