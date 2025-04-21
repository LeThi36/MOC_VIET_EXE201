USE java21_shopping;

-- 01.sql --> Liên quan đến ITEM
-- T01_ITEM, T02_ITEMGROUP, T06_SIZE, T12_ITEM_DETAIL, T13_GALLERY

-- USE java21_shopping; -- set database java21_shopping as default

-- disable foreign key check
-- SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS T02_ITEM_GROUP;
CREATE TABLE T02_ITEM_GROUP(
	C02_ITEM_GROUP_ID INT PRIMARY KEY,
    C02_ITEM_GROUP_NAME VARCHAR(255) NOT NULL,
    -- nên có constraint name để sau có cập nhật, xóa constraint
    -- sử dụng name đã khai báo thay vì name mặc định
    CONSTRAINT UNQ_ITEM_GROUP_NAME UNIQUE(C02_ITEM_GROUP_NAME)
);

DROP TABLE IF EXISTS T01_ITEM;
CREATE TABLE T01_ITEM(
	C01_ITEM_ID INT,
	C01_ITEM_NAME VARCHAR(255) NOT NULL,
	C01_BUY_PRICE FLOAT NOT NULL,
	C01_ITEM_GROUP_ID INT NOT NULL,
    -- không cần khai báo constrain name cho khóa chính
    -- vì 1 table chỉ có duy nhất 1 khóa chính và luôn lấy name = PRIMARY_KEY
    -- chỉ dùng cách này khi bảng có khóa chính(chứa nhiều hơn 1 COLUMN)
    PRIMARY KEY (C01_ITEM_ID),
    -- mặc định ON DELETE(NO ACTION = RESTRICT), ON UPDATE(NO ACTION = RESTRICT)
    -- khi xóa dòng dữ liệu ở bảng cha(bảng 1) nếu dữ liệu đó đang được sử dụng
    -- ở bảng con --> báo lỗi
    CONSTRAINT FK_ITEM_ITEM_GROUP FOREIGN KEY (C01_ITEM_GROUP_ID)
                                  REFERENCES  T02_ITEM_GROUP(C02_ITEM_GROUP_ID),
	CONSTRAINT UNQ_ITEM_NAME UNIQUE(C01_ITEM_NAME),
    CONSTRAINT CHECK_BUY_PRICE_POSITIVE CHECK (C01_BUY_PRICE >= 0)
);

DROP TABLE IF EXISTS T06_SIZE;
CREATE TABLE T06_SIZE(
	C06_SIZE_ID   INT PRIMARY KEY,
	C06_SIZE_NAME VARCHAR(10) NOT NULL,
    -- BIT(1)     --> chỉ nhận giá trị 0,1 --> tối đa 1 giá trị
    -- BIT(5)     --> chỉ nhận giá trị 0,1 --> tối đa 5 giá trị
    -- TINYINT(1) --> chỉ nhật 0 -> 9      --> tối đa 1 ký tự, muốn 0,1 thì phải thêm CHECK
	C06_GENDER    BIT(1) NOT NULL,
	C06_SIZE_DESC TEXT NOT NULL,
    CONSTRAINT CHECK_SIZE_NAME CHECK (C06_SIZE_NAME IN ('S', 'M', 'L', 'XL', 'XXL'))
);

DROP TABLE IF EXISTS T12_ITEM_DETAIL;
CREATE TABLE T12_ITEM_DETAIL(
	C12_ITEM_DETAIL_ID INT PRIMARY KEY,
	C12_ITEM_ID INT NOT NULL,
	C12_SIZE_ID INT NOT NULL,
	C12_COLOR   VARCHAR(50) NOT NULL,
	C12_SALES_PRICE FLOAT,
	C12_AMOUNT INT NOT NULL DEFAULT 0,
    CONSTRAINT UNQ_ITEM_SIZE_COLOR UNIQUE(C12_ITEM_ID, C12_SIZE_ID, C12_COLOR),
    CONSTRAINT CHECK_SALES_PRICE_POSITIVE CHECK (C12_SALES_PRICE >= 0),
    CONSTRAINT CHECK_AMOUNT_POSITIVE CHECK (C12_AMOUNT >= 0)
);

DROP TABLE IF EXISTS T13_GALLERY;
CREATE TABLE T13_GALLERY(
    -- C13_PATH    TEXT PRIMARY KEY  
    -- PRIMARY KEY yêu cầu KDL TEXT/BLOB ko được vì chưa chỉ định LENGTH cho column
	C13_PATH    VARCHAR(255) PRIMARY KEY,
	C13_ITEM_ID INT NOT NULL
);

-- Sử dụng ALTER TABLE để thêm khóa ngoại
ALTER TABLE T13_GALLERY
ADD CONSTRAINT FK_GALLERY_ITEM FOREIGN KEY (C13_ITEM_ID)
						       REFERENCES T01_ITEM(C01_ITEM_ID);
                               
-- enable foreign key check
-- SET FOREIGN_KEY_CHECKS = 1;