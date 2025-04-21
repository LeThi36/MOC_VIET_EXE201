-- random an element from array https://stackoverflow.com/questions/41885936/get-random-array-value-in-mysql
-- random records in table https://www.educba.com/mysql-order-by-random/

SET FOREIGN_KEY_CHECKS = 0;

INSERT INTO t02_item_group(C02_ITEM_GROUP_ID, C02_ITEM_GROUP_NAME) VALUES
(1,	'Áo'),
(2,	'Quần'),
(3,	'Giày'),
(4,	'Dép'),
(5,	'Mũ'),
(6,	'Thắt lưng'),
(7,	'Túi xách');

-- SELECT từ 1/TABLE trong database --> SELECT * FROM T02_ITEM_GROUP
-- SELECT dữ liệu ngẫu nhiên        --> SELECT 'Teo' ST_NAME, '12TK5'ST_CLASS UNION SELECT 'Nam', '13TK2'
-- SELECT biến, hàm của database    --> SELECT current_date();
-- Dữ liệu trả về của SELECT là 1 TABLE

-- CTE: common table expression --> bảng tạm
-- phạm vi sử dụng: chỉ trong 1 câu lệnh sql

INSERT INTO t04_payment_method(C04_PMETHOD_ID, C04_PMETHOD_DESC)
WITH cte_payment_method AS (
	SELECT 1 P_ID, 'Tiền mặt' P_DESC UNION
	SELECT 2, 'Thẻ tín dụng' UNION
	SELECT 3, 'Thẻ ghi nợ'   UNION
	SELECT 4, 'Ví điện tử'
)
SELECT P_ID, P_DESC FROM cte_payment_method;

INSERT INTO t05_order_status(C05_ORDER_STATUS_ID, C05_ORDER_STATUS_DESC) VALUES
(1,	'Chờ xác nhận'),
(2,	'Đang đóng gói'),
(3,	'Đóng gói hoàn thành'),
(4,	'Đang vận chuyển'),
(5,	'Giao hàng thành công'),
(6,	'Giao hàng thất bại'),
(7,	'Hủy đơn hàng');

INSERT INTO t06_size(C06_SIZE_ID, C06_SIZE_NAME, C06_GENDER, C06_SIZE_DESC) VALUES
(1,	 'S',	0,	'Size S cho Nữ'),
(2,	 'M',	0,	'Size M cho Nữ'),
(3,	 'L',	0,	'Size L cho Nữ'),
(4,	 'XL',	0,	'Size XL cho Nữ'),
(5,	 'XXL',	0,	'Size XXL cho Nữ'),
(6,	 'S',	1,	'Size S cho Nam'),
(7,	 'M',	1,	'Size M cho Nam'),
(8,	 'L',	1,	'Size L cho Nam'),
(9,	 'XL',	1,	'Size XL cho Nam'),
(10, 'XXL',	1,	'Size XXL cho Nam');

INSERT INTO t08_title(C08_TITLE_ID, C08_TITLE_NAME) VALUES
(1, 'Nhân viên'),
(2, 'Trưởng bộ phận'),
(3, 'Giám đốc'),
(4, 'Chủ sở hữu');

INSERT INTO t01_item(C01_ITEM_ID, C01_ITEM_NAME, C01_BUY_PRICE, C01_ITEM_GROUP_ID) VALUES
(1,	 'Áo 1',	100,	1),
(2,	 'Áo 2',	60,		1),
(3,	 'Áo 3',	80,		1),
(4,	 'Quần 4',	40,		2),
(5,	 'Quần 5',	60,		2),
(6,	 'Giày 6',	180,	3),
(7,	 'Giày 7',	80,		3),
(8,	 'Giày 8',	126,	3),
(9,	 'Giày 9',	142,	3),
(10, 'Giày 10',	195,	3),
(11, 'Giép 11',	180,	4),
(12, 'Áo 12',	88,		1),
(13, 'Giép 13',	200,	4),
(14, 'Mũ 14',	120,	5),
(15, 'Mũ 15',	160,	5),
(16, 'Thắt lưng 16',140,6),
(17, 'Thắt lưng 17',170,6),
(18, 'Mũ 18',   120,	5),
(19, 'Túi xách 1',175,	7),
(20, 'Túi xách 2',186,	7);

INSERT INTO t12_item_detail(C12_ITEM_ID, C12_SIZE_ID, C12_COLOR, C12_SALES_PRICE, C12_AMOUNT)
WITH CTE_ITEM_DETAL_ODD AS (
	SELECT it.C01_ITEM_ID ITEM_ID,
		   s.C06_SIZE_ID  SIZE_ID,
		   (SELECT elt(floor(rand() * 7 + 1), 'Đỏ', 'Cam', 'Vàng', 'Lục', 'Lam', 'Chàm', 'Tím')) COLOR,
		   it.C01_BUY_PRICE * 2 + s.C06_SIZE_ID * 5 PRICE,
		   125 AMOUNT
	  FROM t01_item it, t06_size s
	 WHERE (it.C01_ITEM_ID MOD 2 != 0)
	   AND (s.C06_SIZE_ID MOD 2 != 0)
	 ORDER BY it.C01_ITEM_ID, s.C06_SIZE_ID
), CTE_ITEM_DETAL_EVEN AS (
	SELECT it.C01_ITEM_ID ITEM_ID,
		   s.C06_SIZE_ID  SIZE_ID,
		   (SELECT elt(floor(rand() * 7 + 1), 'Đỏ', 'Cam', 'Vàng', 'Lục', 'Lam', 'Chàm', 'Tím')) COLOR,
		   it.C01_BUY_PRICE * 2 + s.C06_SIZE_ID * 5 + 20 PRICE,
		   280 AMOUNT
	  FROM t01_item it, t06_size s
	 WHERE (it.C01_ITEM_ID MOD 2 = 0)
	   AND (s.C06_SIZE_ID MOD 2 = 0)
	 ORDER BY it.C01_ITEM_ID, s.C06_SIZE_ID
), CTE_ITEM_DETAILS AS (
   SELECT * FROM CTE_ITEM_DETAL_ODD
   UNION
   SELECT * FROM CTE_ITEM_DETAL_EVEN
)
SELECT * 
  FROM CTE_ITEM_DETAILS
 ORDER BY ITEM_ID, SIZE_ID;

-- T11_CUSTOMER
CALL insert_into_cust(10);

-- T03_ORDER
INSERT INTO t03_order(C03_ORDER_ID, C03_ORDER_TIME,	C03_DELIVERY_FEE, C03_DELIVERY_ADDRESS,	C03_RECEIVER_PHONE,	C03_TOTAL_OF_MONEY,	C03_PMETHOD_ID,	C03_CUSTOMER_ID)
VALUES
(1,		str_to_date('10.04.2024 08:10:20', '%d.%m.%Y %h:%i:%s'),	20,	'Địa chỉ 1',	'258369741',	0,	1,	1),
(2,		str_to_date('12.04.2024 08:10:20', '%d.%m.%Y %h:%i:%s'),	20,	'Địa chỉ 2',	'258369741',	0,	1,	2),
(3,		str_to_date('14.04.2024 08:10:20', '%d.%m.%Y %h:%i:%s'),	20,	'Địa chỉ 3',	'258369741',	0,	2,	3),
(4,		str_to_date('16.04.2024 08:10:20', '%d.%m.%Y %h:%i:%s'),	30,	'Địa chỉ 4',	'258369741',	0,	2,	4),
(5,		str_to_date('18.04.2024 08:10:20', '%d.%m.%Y %h:%i:%s'),	30,	'Địa chỉ 5',	'258369741',	0,	3,	5),
(6,		str_to_date('12.04.2024 08:10:20', '%d.%m.%Y %h:%i:%s'),	40,	'Địa chỉ 6',	'258369741',	0,	3,	6),
(7,		str_to_date('14.04.2024 08:10:20', '%d.%m.%Y %h:%i:%s'),	40,	'Địa chỉ 7',	'258369741',	0,	4,	7),
(8,		str_to_date('18.04.2024 08:10:20', '%d.%m.%Y %h:%i:%s'),	50,	'Địa chỉ 8',	'258369741',	0,	4,	8),
(9,		str_to_date('18.04.2024 08:10:20', '%d.%m.%Y %h:%i:%s'),	50,	'Địa chỉ 9',	'258369741',	0,	4,	1),
(10,	str_to_date('18.04.2024 08:10:20', '%d.%m.%Y %h:%i:%s'),	20,	'Địa chỉ 10',	'258369741',	0,	4,	2),
(11,	str_to_date('20.04.2024 08:10:20', '%d.%m.%Y %h:%i:%s'),	30,	'Địa chỉ 11',	'258369741',	0,	3,	9),
(12,	str_to_date('26.04.2024 08:10:20', '%d.%m.%Y %h:%i:%s'),	20,	'Địa chỉ 12',	'258369741',	0,	3,	10),
(13,	str_to_date('28.04.2024 08:10:20', '%d.%m.%Y %h:%i:%s'),	40,	'Địa chỉ 13',	'258369741',	0,	1,	5);

-- T07_EMPLOYE
CALL insert_into_employee(10);
UPDATE t07_employee SET C07_TITLE_ID = 2 WHERE C07_EMPLOYEE_ID IN (3,7);
UPDATE t07_employee SET C07_TITLE_ID = 3 WHERE C07_EMPLOYEE_ID IN (8); 
UPDATE t07_employee SET C07_TITLE_ID = 4 WHERE C07_EMPLOYEE_ID IN (10);

-- T13_GALLERY
INSERT INTO t13_gallery(C13_PATH, C13_ITEM_ID)
SELECT concat('https://images.pexels.com/photos/736230/', C01_ITEM_ID), C01_ITEM_ID FROM t01_item;

-- T14_ORDER_DETAIL
INSERT INTO t14_order_detail(C14_ORDER_ID, C14_ITEM_DETAIL_ID, C14_AMOUNT) VALUES
(1,	1,	2), (1,	3,	4),
(2,	4,	2), (2,	5,	2),
(3,	6,	4), (3,	2,	4),
(4,	8,	2), (4,	12,	4),
(5,	88, 1), (5,	22,	3), (5,	11,	1), (5,	33,	1),
(6,	2,	1),
(7,	1,	2),
(8,	27,	1), (8,	23,	1), (8,	98,	2),
(9,	100,6), (9,	11,	7), (9,	45,	2), (9,	22,	2), (9,	32,	1),
(10,18,	2), (10,29,	4),
(11,33,	2),(11,	65,	1), (11,1,	1),(11,	2,	2),(11,	5,	5),(11,	7,	10),
(12,55,	1),
(13,92, 1), (13,14,	2);

INSERT INTO `t15_order_status_detail`(C15_ORDER_ID, C15_ORDER_STATUS_ID, C15_EMPLOYEE_ID, C15_LAST_UPDATED)
WITH CTE_ORDER_DELIVERY_STATUS_DETAIL AS (
	-- 1. Đơn hàng từ 1 - 5 --> Giao thành công (trạng thái từ 1 - 5)
	SELECT dorder.C03_ORDER_ID ORDER_ID,
		   dstatus.C05_ORDER_STATUS_ID STATUS_ID,
		   1 EMPLOYEE_ID,
		   date_sub(current_timestamp(), INTERVAL (5 - dstatus.C05_ORDER_STATUS_ID) DAY) LAST_UPDATED_AT 
	  FROM t03_order dorder, t05_order_status dstatus
	 WHERE dorder.C03_ORDER_ID BETWEEN 1 AND 5
	   AND dstatus.C05_ORDER_STATUS_ID BETWEEN 1 AND 5

	UNION
	 
	-- 2. Đơn hàng từ 6 - 8 --> Đóng gói thành công (trạng thái từ 1 - 3)
	SELECT dorder.C03_ORDER_ID ORDER_ID,
		   dstatus.C05_ORDER_STATUS_ID STATUS_ID,
		   2 EMPLOYEE_ID,
		   date_sub(current_timestamp(), INTERVAL (3 - dstatus.C05_ORDER_STATUS_ID) DAY) LAST_UPDATED_AT 
	FROM t03_order dorder, t05_order_status dstatus
	WHERE dorder.C03_ORDER_ID BETWEEN 6 AND 8
	AND dstatus.C05_ORDER_STATUS_ID BETWEEN 1 AND 3

	UNION

	-- 3. Đơn hàng từ 9 - 10 --> Đang giao hàng (trạng thái từ 1 - 4)
	SELECT dorder.C03_ORDER_ID ORDER_ID,
		   dstatus.C05_ORDER_STATUS_ID STATUS_ID,
		   3 EMPLOYEE_ID,
		   date_sub(current_timestamp(), INTERVAL (4 - dstatus.C05_ORDER_STATUS_ID) DAY) LAST_UPDATED_AT 
	FROM t03_order dorder, t05_order_status dstatus
	WHERE dorder.C03_ORDER_ID BETWEEN 9 AND 10
	AND dstatus.C05_ORDER_STATUS_ID BETWEEN 1 AND 4

	UNION

	-- 4. Đơn hàng từ 11 - 12 --> Hủy đơn hàng (trạng thái 7)
	SELECT dorder.C03_ORDER_ID ORDER_ID,
		   7 STATUS_ID, -- cancel
		   4 EMPLOYEE_ID,
		   current_timestamp() LAST_UPDATED_AT
	  FROM t03_order dorder
	 WHERE dorder.C03_ORDER_ID BETWEEN 11 AND 12
     
     UNION
     
     -- 5. Đơn hàng từ 13 --> Giao hàng thất bại (trạng thái từ 1,2,3,4,6)
	SELECT dorder.C03_ORDER_ID ORDER_ID,
		   dstatus.C05_ORDER_STATUS_ID STATUS_ID,
		   5 EMPLOYEE_ID,
		   date_sub(current_timestamp(), INTERVAL (6 - dstatus.C05_ORDER_STATUS_ID) DAY) LAST_UPDATED_AT 
	FROM t03_order dorder, t05_order_status dstatus
	WHERE dorder.C03_ORDER_ID = 13
	AND dstatus.C05_ORDER_STATUS_ID IN (1,2,3,4,6)
     
     
)
SELECT * FROM CTE_ORDER_DELIVERY_STATUS_DETAIL
ORDER BY ORDER_ID, STATUS_ID;


SET FOREIGN_KEY_CHECKS = 1;