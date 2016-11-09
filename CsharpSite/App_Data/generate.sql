drop table if exists users;

create table users(
	user_id int not null primary key,
	username varchar(255) not null unique,
	pass varchar(255) not null,
	registration_date timestamp not null default (select GET_DATE()),
	
	);
