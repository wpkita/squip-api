select date(d),
       count(hib.instant_created_at)
from generate_series(
             timestamp without time zone '2022-01-01',
             timestamp without time zone '2024-01-01',
             '1 day'
         ) as gs(d)
         left outer join hibits as hib
         inner join habits as hab
                    on hab.id = hib.habit_id on hib.instant_created_at :: Date = date(d)
    and hab.is_archived = false
    and date(hib.instant_created_at) <> '2022-08-27'
group by date(d)
order by date(d) asc
