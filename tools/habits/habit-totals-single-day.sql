select hab.name, hib.instant_occurred_at - interval '5 hours'
from hibits hib
         inner join habits hab on hab.id = hib.habit_id
where date(hib.instant_occurred_at - interval '5 hours') = '2023-03-12'
  and hab.is_archived = false
  and hib.is_archived = false
order by hib.instant_occurred_at
