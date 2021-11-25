CREATE TABLE accounts (
	account_id INT IDENTITY (1, 1) PRIMARY KEY,
	account_type VARCHAR (255) NOT NULL,
	dicount INT DEFAULT NULL,
	price_per_month DECIMAL(10,2) NOT NULL DEFAULT '0.00',
	create_date datetime NOT NULL,
	update_date datetime DEFAULT NULL
);

CREATE TABLE car_images (
	car_image_id INT IDENTITY (1, 1) PRIMARY KEY,
	filename VARCHAR (255) NOT NULL,
	content VARBINARY(MAX) NULL,
	created_by VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	updated_by VARCHAR (255) DEFAULT NULL,
	update_date datetime DEFAULT NULL
);

CREATE TABLE brands (
	brand_id INT IDENTITY (1, 1) PRIMARY KEY,
	brand_name VARCHAR (255) NOT NULL,
	created_by VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	updated_by VARCHAR (255) DEFAULT NULL,
	update_date datetime DEFAULT NULL
);

CREATE TABLE car_models (
	car_model_id INT IDENTITY (1, 1) PRIMARY KEY,
	car_model_name VARCHAR (255) NOT NULL,
	brand_id INT NOT NULL,
	created_by VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	updated_by VARCHAR (255) DEFAULT NULL,
	update_date datetime DEFAULT NULL,
	
	FOREIGN KEY (brand_id) 
        REFERENCES brands (brand_id) 
        ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE cars (
	car_id INT IDENTITY (1, 1) PRIMARY KEY,
	car_image_id INT DEFAULT NULL,
	car_model_id INT NOT NULL,
	production_date datetime NOT NULL,	
	created_by VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	updated_by VARCHAR (255) DEFAULT NULL,
	update_date datetime DEFAULT NULL,
	user_rating INT DEFAULT NULL,
	mileage INT NOT NULL,
	price_per_hour DECIMAL(10,2) NOT NULL DEFAULT '0.00',
	localization VARCHAR (255) NOT NULL,
	
	FOREIGN KEY (car_image_id) 
        REFERENCES car_images (car_image_id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (car_model_id) 
        REFERENCES car_models (car_model_id) 
        ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE users (
	user_id INT IDENTITY (1, 1) PRIMARY KEY,
	account_id INT NOT NULL,
	car_id INT DEFAULT NULL,
	first_name VARCHAR (255) NOT NULL,
	last_name VARCHAR (255) NOT NULL,
	gender VARCHAR (1) NOT NULL DEFAULT 'u',
	phone VARCHAR (25) DEFAULT NULL,
	email VARCHAR (255) NOT NULL UNIQUE,
	street VARCHAR (255) DEFAULT NULL,
	city VARCHAR (255) DEFAULT NULL,
	state VARCHAR (255) DEFAULT NULL,
	country VARCHAR (255) DEFAULT NULL,
	zip_code VARCHAR (5) DEFAULT NULL,
	available_credit DECIMAL(10,2) NOT NULL DEFAULT '0.00',
	driving_license_id VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	update_date datetime DEFAULT NULL,
		
	FOREIGN KEY (account_id) 
        REFERENCES accounts (account_id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (car_id) 
        REFERENCES cars (car_id) 
        ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE coupons (
	coupon_id INT IDENTITY (1, 1) PRIMARY KEY,
	user_id INT NOT NULL,
	code VARCHAR (255) NOT NULL,
	money_value DECIMAL(10,2) NOT NULL DEFAULT '0.00',
	enabled BIT NOT NULL,
	expiration_date datetime NOT NULL,
	created_by VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	updated_by VARCHAR (255) DEFAULT NULL,
	update_date datetime DEFAULT NULL,
	
	FOREIGN KEY (user_id) 
        REFERENCES users (user_id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
);

CREATE TABLE tickets (
	ticket_id INT IDENTITY (1, 1) PRIMARY KEY,
	car_id INT NOT NULL,
	expiration_date datetime NOT NULL,
	created_by VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	updated_by VARCHAR (255) DEFAULT NULL,
	update_date datetime DEFAULT NULL,
	
	FOREIGN KEY (car_id) 
        REFERENCES cars (car_id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
);