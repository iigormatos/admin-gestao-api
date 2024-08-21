CREATE DATABASE IF NOT EXISTS gestao;

USE gestao;

CREATE TABLE usuario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(255) NOT NULL,
	username VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
	role INT NOT NULL,
	is_ativo BOOLEAN NOT NULL,
	is_usuario BOOLEAN NOT NULL,
	celular VARCHAR(14),
	rede_social VARCHAR(255)
);

/*INSERTS*/
INSERT INTO usuario (nome, username, email, password, role, is_ativo, is_usuario) VALUES('Igor Matos','igor','igormatos.andrade@hotmail.com','$2a$11$0NRDY40LaH6uW/vCLfH.G.9x5GUnCo66zpGsSm2FRGYEAoOP8rr8O', 0, 1, 1);
INSERT INTO usuario (nome, username, email, password, role, is_ativo, is_usuario) VALUES('Angela Amancio','angela','angela.silvaamancio@hotmail.com','$2a$11$0NRDY40LaH6uW/vCLfH.G.9x5GUnCo66zpGsSm2FRGYEAoOP8rr8O', 1, 1, 1);
INSERT INTO usuario (nome, username, email, password, role, is_ativo, is_usuario) VALUES('Maria','maria','maria@hotmail.com','$2a$11$0NRDY40LaH6uW/vCLfH.G.9x5GUnCo66zpGsSm2FRGYEAoOP8rr8O', 2, 1, 1);
INSERT INTO usuario (nome, username, email, password, role, is_ativo, is_usuario) VALUES('João','joao','joao@hotmail.com','$2a$11$0NRDY40LaH6uW/vCLfH.G.9x5GUnCo66zpGsSm2FRGYEAoOP8rr8O', 3, 1, 1);
