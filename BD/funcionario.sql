Create table funcionario

(
    id int identity(1,1) primary key ,
    nome varchar(45) not null,
	cpf varchar(45) not null unique(cpf),
	salario varchar(45) not null,
	empresa int not null,
	endereco	varchar(45) not null
)

ALTER TABLE funcionario
ADD CONSTRAINT FU_EM_FK FOREIGN KEY (empresa) REFERENCES empresa (id);

ALTER TABLE funcionario
ADD CONSTRAINT FU_EN_FK FOREIGN KEY (endereco) REFERENCES endereco (cep);

