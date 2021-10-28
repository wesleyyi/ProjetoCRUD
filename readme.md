/***Inicialização do projeto

Para iniciar o projeto é necessário restaurar o banco de dados que se encontra no caminho "\ProjSE\BD" ou executar os scripts que estão na pasta "\ProjSE\BD\Scripts" na sequencia numerada ambas no microsoft sql server management studio.

O segundo passo é entrar na classe do projeto chamada “Conexao.cs” na linha 14 e alterar a “ConnectionString” de acordo com suas credencias do banco.


/***Arquitetura do projeto

O projeto foi feito utilizando o “Windows Forms” e ele possui as seguintes classes:

“Program” a classe inicial do projeto, usada para fazer a inicialização do projeto através do método “Main”.

“Form1” é a classe responsável por fazer a chamada de todas as outras classes e dar função ao sistema através do método “button1_Click”.

“Conexao” a classe que conecta o sistema ao banco de dados, ela possui três métodos sendo eles:
	1 “Conexao” método que suporta a ConnectionString, nele é passado o servidor, o banco de dados e as credenciais.
	2 “Conectar” método que abre a conexão do banco de dados com o projeto
	3 “Desconectar” método que fecha a conexão do banco de dados com o projeto

“Login” a classe responsável por validar as credenciais de acesso do usuário através de seu método “Login” que recebe como parâmetro o username e a senha passadas pelo usuário, ela busca na tabela do banco de dados “fol_login” se as credencias existem.

“Funcionalidades” é classe que recebe dois objetos que foram tirados de arquivos json, ele faz a validação e a inserção dos dados dos objetos no banco de dados.

“CadastroRecebido” é a classe alimentada através do json enviado pelo usuário afim de transformar em um objeto.

“EnderecoViaCep” é a classe alimentada através da API dos correios afins de transformar o endereço em um objeto.

/***Retornos possíveis

Caso o login ou senha estejam incorretos a classe “Login” envia para a classe “Form1” a mensagem “Code 403” que faz a aplicação retornar a mensagem “Usuário e/ou senha inválidos”. 

Caso ocorra algum problema na conexão do banco de dados entre “Login” e banco será emitida a mensagem “Falha na Conexão”.

Assim que o Json for anexado será recebido a mensagem “Upload Json com sucesso”.

Caso ocorra algum problema no anexo do Json será enviada a mensagem “Não foi possível abrir o seu arquivo”.

Caso o arquivo Json não seja anexado, a classe “Form1” irá retornar a mensagem “Por favor realizar Upload do Json”.

Caso ocorra algum problema de conexão entre “Funcionalidades” e banco será emitida a mensagem “Falha na Conexão”.

Caso o CPF do funcionário esteja duplicado será emitido uma mensagem de aviso.

Caso a operação seja feita com sucesso a classe “Funcionalidades” retornará para a classe “Form1” a mensagem “code 201” que emitirá uma ao usuário uma mensagem de sucesso.
	
