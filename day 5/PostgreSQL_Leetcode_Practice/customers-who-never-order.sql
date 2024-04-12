# Write your MySQL query statement below
SELECT name as Customers
FROM Customers c
WHERE c.id not in (SELECT customerId from Orders)
