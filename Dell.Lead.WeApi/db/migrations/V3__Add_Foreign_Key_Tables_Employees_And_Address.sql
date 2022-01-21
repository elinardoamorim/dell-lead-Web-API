ALTER TABLE employees
ADD CONSTRAINT FK_employees_address
FOREIGN KEY (address_id) REFERENCES address(id);