﻿CREATE TABLE address(
	id BIGINT IDENTITY(1,1) PRIMARY KEY,
    street VARCHAR(70) NOT NULL,
    number INTEGER,
    cep VARCHAR(11) NOT NULL,
    district VARCHAR(25) NOT NULL,
    city VARCHAR(40) NOT NULL,
    state VARCHAR(30) NOT NULL
);
