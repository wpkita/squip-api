select *
from (select date(d),
             count(hib.instant_created_at) as HabitTotal
      from generate_series(
                   timestamp without time zone '2023-01-01',
                   timestamp without time zone '2023-07-31',
                   '1 day'
               ) as gs(d)
               left outer join hibits as hib
               inner join habits as hab on hab.id = hib.habit_id on hib.instant_created_at :: Date = date(d)
          and hab.is_archived = false
      group by date(d)) habs
         inner join (select date(d),
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Happiness'
                                ) as "Happiness",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Hopefulness'
                                ) as "Hopefulness",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Sadness'
                                ) as "Sadness",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Pain'
                                ) as "Pain",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Guilt/Shame'
                                ) as "Guilt/Shame",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Fear/Anxiety'
                                ) as "Fear/Anxiety",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Numb/Empty'
                                ) as "Numb/Empty",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Irritable/Agitated'
                                ) as "Irritable/Agitated",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Anger'
                                ) as "Anger"
                     from generate_series(
                                  timestamp without time zone '2023-01-01',
                                  timestamp without time zone '2023-07-31',
                                  '1 day'
                              ) as gs(d)
                              left outer join mood_entries as me
                              inner join moods as mo on mo.id = me.mood_id
                         and mo.user_id = '55ce7706-7cac-47d0-90ca-1273d28bb1b6'
                                         on me.instant_created_at :: Date = date(d)
                                             and mo.is_archived = false
                     group by date(d)) mods on habs.date = mods.date
