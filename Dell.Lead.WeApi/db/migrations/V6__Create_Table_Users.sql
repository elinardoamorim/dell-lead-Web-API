CREATE TABLE users (
	id BIGINT IDENTITY(1,1) PRIMARY KEY,
	login VARCHAR(50) NOT NULL UNIQUE,
	password VARCHAR(130) NOT NULL,
	full_name VARCHAR(120),
	refresh_token VARCHAR(500),
	refresh_token_expiry_time DATETIME NULL DEFAULT NULL
);