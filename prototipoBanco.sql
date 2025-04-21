drop database prototipobancoOceanid;
create database prototipobancoOceanid;
use prototipobancoOceanid;

create table tbEndereco(
	idEnd int primary key auto_increment,
	cepEnd varchar(10) not null,
	numeroEnd int unsigned not null,
	logradouro varchar(250) not null,
	complemento varchar(100),
	bairro varchar(100) not null,
    estado varchar(100) not null,
	cidade varchar(100) not null
);


create table tbAdm(
    idAdm int primary key auto_increment,
	nomeAdm varchar(70) not null,
    senhaAdm varchar(30) not null unique,
    emailAdm enum ('adm1@gmail.com','adm2@gmail.com','adm3@gmail.com','adm4@gmail.com','adm5@gmail.com') unique not null
);

create table tbCliente (
	idCliente int primary key auto_increment,
    cpf varchar(11) unique,
    nomeCompleto varchar(200) not null,
    senhaCliente varchar(30) not null unique,
    emailCliente varchar(50) not null unique,
    dataNasc date not null,
    idEnd int,
    foreign key (idEnd) references tbEndereco(idEnd)
);

  
  create table tbCategoria(
	idCategoria int primary key auto_increment,
	nomeCategoria varchar(50) not null
);

create table tbProduto (
	idProd int primary key auto_increment,
	codBar varchar(15),
	nomeProd varchar(200) not null,
	precoProd decimal(10,2) not null,
	qtdProd int unsigned not null,
	marcaProd varchar(50) not null,
	descricaoProd varchar(200) not null,
	idCategoria int not null,
	foreign key (idCategoria) references tbCategoria(idCategoria)
);

create table tbPromocao(
	idPromocao int primary key auto_increment,
	promoAtiv boolean not null default true,
	idProd int not null,
    foreign key (idProd) references tbProduto(idProd) on delete cascade,
    idCategoria int not null,
	foreign key (idCategoria) references tbCategoria(idCategoria) on delete cascade
);




create table tbClienteFavoritos(
    idClienteFav int primary key auto_increment,
    idCliente int not null,
    foreign key (idCliente) references tbCliente(idCliente) on delete cascade,
    idProd int not null,
    foreign key (idProd) references tbProduto(idProd) on delete cascade,
    ativo boolean not null default true,
    constraint unique_client_product unique (idCliente, idProd)
);




/*Esta é uma restrição de unicidade composta que garante que cada combinação de idCliente e idProd seja única na tabela tbClienteFavoritos.*/





create table tbPagamento(
	idPag int primary key auto_increment,
	statusPag enum('Pago', 'Pendente', 'Não Realizado') not null default 'Pendente',
	metodoPag varchar(50) not null
);


create table tbItemPedido (
    idProdutoPedido int primary key auto_increment,
    idPedido int not null,
	foreign key(idPedido) references tbPedido(idPed),
    idProd int not null,
    foreign key (idProd) references tbProduto(idProd),
    quantidade int not null,
    precoUnitario DECIMAL(10, 2)  not null
);

create table tbPedido(
    idPed int primary key auto_increment,
    idEnd int not null,
	foreign key (idEnd) references tbEndereco(idEnd),
    idPag int not null,
	foreign key (idPag) references tbPagamento(idPag),
    idCliente int not null,
    foreign key (idCliente) references tbCliente(idCliente),
    dataPed datetime not null,
    totalPed decimal(10,2) not null
);










/*JOINS*/

-- JOINS -- Consulta detalhada de compras por cliente

SELECT
    tbCliente.nomeCompleto as cliente, -- Nome do cliente 
    tbPedido.idPed, -- ID do pedido
    tbProduto.idProd, -- Código do produto
    tbProduto.nomeProd, -- Nome do produto
    tbProduto.qtdProd, -- Quantidade do produto no pedido
    tbItemPedido.precoUnitario, -- Preço unitário do produto no Itempedido
    tbPedido.dataPed, -- Data do pedido
    tbPedido.totalPed -- Total do pedido
    
FROM tbCliente
INNER JOIN tbPedido  ON tbPedido.idCliente = tbPedido.idCliente
INNER JOIN tbItemPedido  ON tbItemPedido.idPedido = tbItemPedido.idPedido
INNER JOIN tbProduto  ON tbProduto.idProd = tbProduto.idProd;


-- Total gasto por cada cliente
SELECT 
    tbcliente.nomecompleto AS cliente,
    SUM(tbpedido.totalped) AS 'total gasto'
FROM tbcliente
JOIN tbpedido ON tbcliente.idCliente = tbpedido.idCliente
GROUP BY tbcliente.idCliente, tbcliente.nomecompleto
ORDER BY SUM(tbpedido.totalped) DESC;


-- INSERTSS
insert into tbEndereco (cepEnd, numeroEnd, logradouro, complemento, bairro, estado, cidade) values
('01001000', 100, 'Praça da Sé', 'Lado ímpar', 'Sé', 'SP', 'São Paulo'),
('20040002', 45, 'Rua Primeiro de Março', 'Sala 302', 'Centro', 'RJ', 'Rio de Janeiro'),
('30120010', 789, 'Avenida Afonso Pena', 'Conjunto 101', 'Centro', 'MG', 'Belo Horizonte'),
('40010010', 12, 'Rua Chile', 'Loja A', 'Comércio', 'BA', 'Salvador'),
('50030020', 500, 'Rua da Aurora', 'Apto 501', 'Boa Vista', 'PE', 'Recife'),
('70040900', 23, 'SHS Quadra 2', 'Bloco C', 'Asa Sul', 'DF', 'Brasília'),
('80010000', 67, 'Rua XV de Novembro', 'Sobreloja', 'Centro', 'PR', 'Curitiba'),
('90010000', 89, 'Rua dos Andradas', 'Fundos', 'Centro Histórico', 'RS', 'Porto Alegre'),
('50000000', 34, 'Rua do Imperador', 'Sala 2', 'Santo Antônio', 'PE', 'Recife'),
('29010020', 123, 'Rua Graciano Neves', 'Apto 302', 'Centro', 'ES', 'Vitória');

insert into tbUsuario (emailUser, senhaUser, telefoneUser, datacad_User) values
('joao.silva@email.com', 'senha123', '11987654321', '2023-01-15'),
('maria.souza@email.com', 'mariA321', '21976543210', '2023-02-20'),
('carlos.pereira@email.com', 'carlosP@1', '31987654321', '2023-03-10'),
('ana.oliveira@email.com', 'anaOliveira2', '71987654321', '2023-01-25'),
('pedro.alves@email.com', 'pedroA#3', '81987654321', '2023-04-05'),
('julia.rodrigues@email.com', 'juliaR123', '61987654321', '2023-02-15'),
('lucas.santos@email.com', 'lucasS@4', '85987654321', '2023-03-20'),
('fernanda.lima@email.com', 'ferLima5', '61987654321', '2023-04-10'),
('rafael.costa@email.com', 'rafaCosta6', '21987654321', '2023-01-30'),
('amanda.martins@email.com', 'amandaM7', '11987654321', '2023-02-28');

insert into tbAdm (nomeAdm, senhaAdm, emailAdm, idUser) VALUES
('Admin Master', 'AdmMaster@1', 'adm1@gmail.com', 1),
('Gerente Sistema', 'GerenteSys@2', 'adm2@gmail.com', 2),
('Supervisor TI', 'SupTI2023', 'adm3@gmail.com', 3),
('Coordenador Vendas', 'CoordVendas#1', 'adm4@gmail.com', 4),
('Analista Financeiro', 'Financas2023', 'adm5@gmail.com', 5);


insert into tbCliente (idUser, cpf, nomeCompleto, dataNasc) values
(1, '12345678901', 'João da Silva', '1985-05-15'),
(2, '23456789012', 'Maria Souza', '1990-08-20'),
(3, '34567890123', 'Carlos Pereira', '1982-11-30'),
(4, '45678901234', 'Ana Oliveira', '1995-03-25'),
(5, '56789012345', 'Pedro Alves', '1978-07-10'),
(6, '67890123456', 'Julia Rodrigues', '1992-09-15'),
(7, '78901234567', 'Lucas Santos', '1989-12-05'),
(8, '89012345678', 'Fernanda Lima', '1980-04-18'),
(9, '90123456789', 'Rafael Costa', '1993-06-22'),
(10, '01234567890', 'Amanda Martins', '1987-10-08');

INSERT INTO tbCategoria (nomeCategoria) VALUES
('Eletrônicos'),
('Informática'),
('Celulares'),
('Eletrodomésticos'),
('Móveis'),
('Decoração'),
('Livros'),
('Brinquedos'),
('Esportes'),
('Beleza');

insert into tbProduto (codBar, nomeProd, precoProd, qtdProd, marcaProd, descricaoProd, idCategoria) values
('7891234567890', 'Smartphone Galaxy S23', 4999.99, 50, 'Samsung', 'Smartphone flagship da Samsung', 3),
('7892345678901', 'Notebook Dell Inspiron', 3599.90, 30, 'Dell', 'Notebook com i5 e 8GB RAM', 2),
('7893456789012', 'TV LED 55" 4K', 2899.00, 25, 'LG', 'TV Smart com resolução 4K', 1),
('7894567890123', 'Geladeira Frost Free', 2199.50, 15, 'Brastemp', 'Geladeira duplex 375L', 4),
('7895678901234', 'Sofá 3 Lugares', 1599.99, 10, 'Madesa', 'Sofá retrátil em couro sintético', 5),
('7896789012345', 'Quadro Decorativo', 199.90, 100, 'Arte & Estilo', 'Quadro moldurado 50x70cm', 6),
('7897890123456', 'Livro SQL para Iniciantes', 89.90, 200, 'Editora Tech', 'Guia completo de SQL', 7),
('7898901234567', 'Boneco Action Figure', 129.99, 80, 'Hasbro', 'Personagem de filme famoso', 8),
('7899012345678', 'Bicicleta Mountain Bike', 1299.00, 20, 'Caloi', 'Bicicleta aro 29 21 marchas', 9),
('7890123456789', 'Kit Maquiagem Profissional', 299.90, 40, 'Avon', 'Kit com 12 itens de maquiagem', 10);


insert into tbClienteFavoritos (idCliente, idProd) values
(1, 1), -- Cliente 1 favoritou Produto 1
(1, 3), -- Cliente 1 favoritou Produto 3
(2, 2), -- Cliente 2 favoritou Produto 2
(3, 5), -- Cliente 3 favoritou Produto 5
(4, 4), -- Cliente 4 favoritou Produto 4
(5, 7), -- Cliente 5 favoritou Produto 7
(6, 6), -- Cliente 6 favoritou Produto 6
(7, 9), -- Cliente 7 favoritou Produto 9
(8, 8), -- Cliente 8 favoritou Produto 8
(9, 10), -- Cliente 9 favoritou Produto 10
(10, 1); -- Cliente 10 favoritou Produto 1

INSERT INTO tbPagamento (statusPag, metodoPag) VALUES
('Pago', 'Cartão de Crédito'),
('Pago', 'PIX'),
('Pendente', 'Boleto'),
('Pago', 'Cartão de Débito'),
('Não Realizado', 'Cartão de Crédito'),
('Pago', 'PIX'),
('Pendente', 'Boleto'),
('Pago', 'Cartão de Crédito'),
('Pago', 'Cartão de Débito'),
('Pendente', 'PIX');



INSERT INTO tbPedido (idEnd, idPag, idCliente, dataPed, totalPed) VALUES
( 1, 1, 1, '2023-05-01 10:30:00', 4999.99),
( 2, 2, 2, '2023-05-02 14:15:00', 3599.90),
( 3, 3, 3, '2023-05-03 09:45:00', 2899.00),
( 4, 4, 4, '2023-05-04 16:20:00', 2199.50),
( 5, 5, 5, '2023-05-05 11:10:00', 1599.99),
( 6, 6, 6, '2023-05-06 13:25:00', 199.90),
( 7, 7, 7, '2023-05-07 15:30:00', 89.90),
( 8, 8, 8, '2023-05-08 17:45:00', 129.99),
( 9, 9, 9, '2023-05-09 10:00:00', 1299.00),
( 10, 10, 10, '2023-05-10 14:00:00', 299.90);

INSERT INTO tbItemPedido (idPedido, idProd, quantidade, precoUnitario) VALUES
(1, 1, 1, 4999.99),
(2, 2, 1, 3599.90),
(3, 3, 1, 2899.00),
(4, 4, 1, 2199.50),
(5, 5, 1, 1599.99),
(6, 6, 1, 199.90),
(7, 7, 1, 89.90),
(8, 8, 1, 129.99),
(9, 9, 1, 1299.00),
(10, 10, 1, 299.90);