SET FOREIGN_KEY_CHECKS = 0;

-- Phân biệt DELETE, TRUNCATE, DROP
-- Nên dùng DELETE hay TRUNCATE để xóa dữ liệu

-- TRUNCATE: drop and recreate table without data (DML, DDL tùy hệ quản trị)
-- DELETE: xóa dữ liệu (DML)

TRUNCATE t01_item;
TRUNCATE t02_item_group;
TRUNCATE t03_order;
TRUNCATE t04_payment_method;
TRUNCATE t05_order_status;
TRUNCATE t06_size;
TRUNCATE t07_employee;
TRUNCATE t08_title;
TRUNCATE t09_item_statistic;
TRUNCATE t10_revenue;
TRUNCATE t11_customer;
TRUNCATE t12_item_detail;
TRUNCATE t13_gallery;
TRUNCATE t14_order_detail;
TRUNCATE t15_order_status_detail;

SET FOREIGN_KEY_CHECKS = 1;