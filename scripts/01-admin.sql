CREATE DATABASE IF NOT EXISTS gestao;

USE gestao;

CREATE TABLE usuario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(255) NOT NULL,
	username VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
	role INT NOT NULL
);

/*INSERTS*/
INSERT INTO usuario (nome, username, email, password, role) VALUES('Igor Matos','iigormatos','igormatos.andrade@hotmail.com','$2a$11$0NRDY40LaH6uW/vCLfH.G.9x5GUnCo66zpGsSm2FRGYEAoOP8rr8O', 0);
/*INSERT INTO usuario (nome, username, email, password, role) VALUES('Igor Matos','iigormatos','igormatos.andrade@hotmail.com','$2a$11$qmjt5hlpLMJiOoKrLNcj6Otd0Azu875nkdCoHMmtalU0QF3GIt.By', 1);
INSERT INTO usuario (nome, username, email, password, role) VALUES('Igor Matos','iigormatos','igormatos.andrade@hotmail.com','$2a$11$qmjt5hlpLMJiOoKrLNcj6Otd0Azu875nkdCoHMmtalU0QF3GIt.By', 2);*/