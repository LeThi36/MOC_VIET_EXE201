-- Tạo database
CREATE DATABASE java21_shopping CHAR SET utf8mb4;

-- Idea để tạo patchess cho cơ sở dữ liệu

-- Mỗi file .sql gọi là 1 file patch

-- 01.sql --> Liên quan đến ITEM 
-- T01_ITEM, T02_ITEM_GROUP, T06_SIZE, T12_ITEM_DETAIL, T13_GALLERY


-- 02.sql --> Liên quan đến EMPLOYEE, TITLE
-- T07_EMPLOYEE, T08_TITLE

-- 03.sql --> Liên quan đến ORDER, STATUS, CUSTOMER
-- T03_ORDER, T04_PAYMENT_METHOD, T05_ORDER_STATUS, T11_CUSTOMER
-- T14_ORDER_DETAIL, T15_ORDER_STATUS_DETAIL
-- Thiếu bảng ??? BILL ???

-- 04.sql --> Liên quan đến thống kê STATISTIC
-- T09_ITEM_STATISTIC, T10_REVENUE