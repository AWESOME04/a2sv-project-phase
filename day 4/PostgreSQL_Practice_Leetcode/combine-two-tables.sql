# Write your MySQL query statement below
SELECT firstName, lastName, city, state from Person p LEFT JOIN
Address a on a.personId = p.personId
