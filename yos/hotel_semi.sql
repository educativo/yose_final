use hotel
create database hotel


create table Usuario(
 id int not null primary key identity(1,1),
 nombre varchar(250)not null,
 apellidos varchar(250) not null,
  idser int not null,
  UserId uniqueidentifier not null,
   foreign key(idser) references servicios(id),
   foreign key(UserId)references dbo.Users(UserId)
  );
create table cliente(
  id int not null primary key identity(1,1),
  nombre varchar(250)not null,
  telefono varchar(250)not null,
  direccion varchar(250)not null,
  email varchar(250),
  ciudad varchar(250),
  estado varchar(250),
  pais varchar(520),
);
create table empresa(
  id int not null primary key identity(1,1),
  idcli int not null,
  nit varchar(250)not null,
  pago varchar(250)not null,
  contacto varchar(250),
  foreign key(idcli) references cliente(id)
);

create table agencia(
  id int not null primary key identity(1,1),
  idcli int not null,
  nit varchar(250)not null,
  contacto varchar(250),
  foreign key(idcli) references cliente(id)
);
create table persona(
  id int not null primary key identity(1,1),
  idcli int not null,
  apellidos varchar(250)not null,
  pasaporte varchar(250),
  cumpleaños varchar(250),
  comentario varchar(250),
  foreign key(idcli) references cliente(id)
);

create table habitacion(
   id int not null primary key identity(1,1),
   idtipo int not null,
   tipo varchar(250)not null,
   estado varchar(250)not null,
   foreign key(idtipo) references tipo(id)
);
 create table tipo(
  id int not null primary key identity(1,1),
  nombre varchar(250)not null,
  dercripcion varchar(250)not null,
  precio float not null,
  
);
create table mantenimiento(
   id int not null primary key identity(1,1),
   idhabi int not null,
   fechaini varchar(250)not null,
   fechafin varchar(250)not null,
   foreign key (idhabi)references habitacion(id)
   
);
create table recerva(
   id int not null primary key identity(1,1),
   idhabi int not null,
   idcliente int not null,
   fechainiciorecer datetime not null,
   fechafinrecer datetime not null,
   dia int not null,
   precio varchar(250) not null,
   foreign key(idhabi)references habitacion(id),
   foreign key(idcliente)references cliente(id),

  
);
create table factura(
   id int not null primary key identity(1,1),
   idrecer int not null,
   fecha datetime not null,
   precio_unitario float,
   precio_total float,
   foreign key(idrecer)references recerva(id),
);

create table categoria(
  id int not null primary key identity(1,1),
  nombre varchar(250)not null,  
);
create table servicios(
 id int not null primary key identity(1,1),
 idcate int not null,
 servicio_nombre varchar(250),
 categoria_nombre varchar(250)not null,
 precio int not null,
 foreign key(idcate)references categoria(id),
); 

drop table servicio_cliente(
id int not null primary key identity(1,1),
idcli int not null,
idrecer int not null,
idser int not null,
foreign key(idcli)references cliente(id),
foreign key(idrecer)references recerva(id),
foreign key(idser)references servicios(id),
	
);

 

create table archivo(
	nro int not null primary key identity(1,1),
	rutafisica varchar(250),
	fecha varchar(250)
);


--probando

select * from empresa
select * from agencia
select * from persona
select * from cliente
select * from factura
select * from recerva
select * from habitacion
select * from tipo
select * from mantenimiento
select * from servicios
select * from categoria
	select * from archivo

insert into servicios values('1','restaurant','comedor','50')

insert into categoria values('comedor')
insert into categoria values('deportes')


insert into recerva values('3','1','1','29/11/12','02/12/12','3','nose','50','ocupado')
insert into recerva values('2','2','9','11-28-2012','11-28-2012','1','nose','30','ocupado')

insert into servicio_cliente values('gastronomia','2','1')

insert into servicios values('Gastronomia','restaurant','50')
insert into factura values('2','2','11-29-2012','pago general','150','250')


select * from recerva
select * from cliente
select * from servicio_cliente
select * from servicios
select * from habitacion
select * from factura
insert into recerva values('1','1','1','29/11/12','02/11/12','3','nose','50','ocupado');

delete from cliente 
delete from persona

insert into cliente values('Yoselin','43777','h. players','karen@hotmail.com','Potosi','habilitado','Bolivia')
insert into cliente values('colegio "arce"','6245632','arce nro 12','cole@hotmail.com','Sucre','habilitado','Bolivia')
insert into cliente values('delegacion Lapaz','654123','Uyustus s/n','dele@hotmail.com','La Paz','habilitado','Bolivia')
insert into persona values('1','Oruro','si','21/03/90','nuevo')
insert into empresa values('2','121212','bs','SI')	
insert into agencia values('3','111111','SI')

select * from habitacion
insert into habitacion values('1','Simple','libre')
insert into tipo values('Simple','cama simple','30')
insert into tipo values('Completo','completosimple','60')
