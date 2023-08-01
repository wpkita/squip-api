select date(hib.instant_occurred_at - interval '5 hours'),
       count(*)
from hibits as hib
         inner join habits as hab
                    on hab.id = hib.habit_id
where hab.is_archived = false
group by date(hib.instant_occurred_at - interval '5 hours')
order by count(*) asc;
