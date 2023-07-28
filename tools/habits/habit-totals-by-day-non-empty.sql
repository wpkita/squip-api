select date(hib.instant_created_at),
       count(*)
from hibits as hib
         inner join habits as hab
                    on hab.id = hib.habit_id
where hab.is_archived = false
  and date(hib.instant_created_at) <> '2022-08-27'
group by date(hib.instant_created_at)
order by date(hib.instant_created_at) asc;


