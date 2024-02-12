with recursive ManagerChain as (
    select ID, CHIEF_ID, 1 as ChainLength
    from EMPLOYEE
    where CHIEF_ID is null

    union all

    select e.ID, e.CHIEF_ID, mc.ChainLength + 1
    from EMPLOYEE e
             join ManagerChain mc on e.CHIEF_ID = mc.ID
)
select max(ChainLength) asMaxChainLength
from ManagerChain;