select d.NAME, sum(e.SALARY)
from EMPLOYEE as e
         inner join DEPARTMENT as d
                    on d.ID = e.DEPARTMENT_ID
group by d.ID, d.NAME
order by sum(e.SALARY) desc
limit 1;