select date(hib.instant_occurred_at - interval '5 hours'),
       count(*)
from hibits as hib
         inner join habits as hab
                    on hab.id = hib.habit_id
where hab.is_archived = false
  and date(hib.instant_occurred_at - interval '5 hours') <> '2022-08-27'
group by date(hib.instant_occurred_at - interval '5 hours')
order by count(*) asc;
