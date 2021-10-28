Create table empresa

(
    id int identity(1,1) primary key ,
    razao_social varchar(45),
	cnpj varchar(45) not null unique(cnpj)
)



