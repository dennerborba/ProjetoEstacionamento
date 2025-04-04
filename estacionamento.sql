create database estacionamento;

use estacionamento;

create table vaga (
	id bigint primary key not null auto_increment,
	placa varchar(8) not null, 
	hr_chegada datetime,
    hr_saida datetime,
    preco decimal (10,2)
)
