-- DROP PROCEDURE insert_into_cust;
DELIMITER $$
CREATE PROCEDURE insert_into_cust(prows int)
BEGIN
    DECLARE maxId INT DEFAULT ifnull((SELECT max(C11_CUSTOMER_ID) FROM T11_CUSTOMER), 0);
	DECLARE i INT DEFAULT maxId + 1;
    SET prows = maxId + prows; 
    
	WHILE i <= prows DO
		INSERT INTO T11_CUSTOMER(
			C11_CUSTOMER_ID,
			C11_CUSTOMER_NAME,
			C11_CUSTOMER_EMAIL,
			C11_CUSTOMER_ADDRESS,
			C11_CUSTOMER_PHONE,
			C11_CUSTOMER_USERNAME,
			C11_CUSTOMER_PASSWORD
		)
		SELECT i,
			   concat('Le Van Teo ', i),
               concat('c', i, '@gmail.com'),
               concat('Địa chỉ', i),
               '123456789',
               concat('c', i, if(i MOD 2 = 0, 'auto', 'def')),
               '$2a$12$w0bs0MW/O3nTyMhuv0r1jOjq2gOaxLxkZgms7u/khHRmtCh3S/Hpu';
         SET i = i + 1;      
    END WHILE;
END $$


-- DROP PROCEDURE insert_into_employee;
DELIMITER $$
CREATE PROCEDURE insert_into_employee(prows int)
BEGIN
    DECLARE maxId INT DEFAULT ifnull((SELECT max(C07_EMPLOYEE_ID) FROM T07_EMPLOYEE), 0);
    DECLARE i INT DEFAULT maxId + 1;
    SET prows = maxId + prows;
    
	WHILE i <= prows DO
		INSERT INTO T07_EMPLOYEE(
			C07_EMPLOYEE_ID,
            C07_EMPLOYEE_NAME,
            C07_EMPLOYEE_EMAIL,
            C07_EMPLOYEE_ADDRESS,
            C07_EMPLOYEE_PHONE,
            C07_EMPLOYEE_USERNAME,
            C07_EMPLOYEE_PASSWORD,
            C07_TITLE_ID
		)
		SELECT i,
			   concat('Nhân viên ', i),
               concat('nv', i, '@gmail.com'),
               concat('Địa chỉ', i),
               '123456789',
               concat('nv', i, if(i MOD 2 = 0, 'auto', 'def')),
               '$2a$12$w0bs0MW/O3nTyMhuv0r1jOjq2gOaxLxkZgms7u/khHRmtCh3S/Hpu',
               1;
         SET i = i + 1;      
    END WHILE;
END $$