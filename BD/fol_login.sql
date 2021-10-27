Create table FOL_LOGIN

(
    id int identity(1,1) primary key ,
    nm_login varchar(45) not null unique(nm_login),
	dc_senha varchar(45) not null 
)
