-- 04.sql --> Liên quan đến thống kê statistic
-- T09_ITEM_STATISTIC, T10_REVENUE

-- disable foreign key check
-- SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS T09_ITEM_STATISTIC;
CREATE TABLE T09_ITEM_STATISTIC (
	C09_ITEM_ID INT,
	C09_DATE_FROM DATE,
	C09_DATE_UNTIL DATE,
	C09_SALES_AMOUNT INT NOT NULL,
	C09_REVENUE FLOAT NOT NULL,
    PRIMARY KEY (C09_ITEM_ID, C09_DATE_FROM, C09_DATE_UNTIL),
    CONSTRAINT FK_T09_ITEM_STATISTIC_T01_ITEM FOREIGN KEY (C09_ITEM_ID) REFERENCES T01_ITEM(C01_ITEM_ID)
);

DROP TABLE IF EXISTS T10_REVENUE;
CREATE TABLE T10_REVENUE (
	C10_RID INT,
	C10_DATE_FROM DATE,
	C10_DATE_UNTIL DATE,
	C10_TOTAL_OF_MONEY FLOAT NOT NULL,
    PRIMARY KEY (C10_RID),
    CONSTRAINT UNQ_FROM_UNTIL UNIQUE(C10_DATE_FROM, C10_DATE_UNTIL)
);

-- enable foreign key check
-- SET FOREIGN_KEY_CHECKS = 1;