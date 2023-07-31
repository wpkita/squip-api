select corr("Happiness", "Hopefulness")
from (select date(me.instant_created_at),
             avg(me.magnitude) filter (
                 where
                 m.name = 'Happiness'
                 ) as "Happiness",
             avg(me.magnitude) filter (
                 where
                 m.name = 'Hopefulness'
                 ) as "Hopefulness",
             avg(me.magnitude) filter (
                 where
                 m.name = 'Sadness'
                 ) as "Sadness",
             avg(me.magnitude) filter (
                 where
                 m.name = 'Pain'
                 ) as "Pain",
             avg(me.magnitude) filter (
                 where
                 m.name = 'Guilt/Shame'
                 ) as "Guilt/Shame",
             avg(me.magnitude) filter (
                 where
                 m.name = 'Fear/Anxiety'
                 ) as "Fear/Anxiety",
             avg(me.magnitude) filter (
                 where
                 m.name = 'Numb/Empty'
                 ) as "Numb/Empty",
             avg(me.magnitude) filter (
                 where
                 m.name = 'Irritable/Agitated'
                 ) as "Irritable/Agitated",
             avg(me.magnitude) filter (
                 where
                 m.name = 'Anger'
                 ) as "Anger"
      from moods m
               inner join mood_entries me on m.id = me.mood_id
      where m.user_id = '55ce7706-7cac-47d0-90ca-1273d28bb1b6'
        and m.is_archived = false
        and me.is_archived = false
      group by date(me.instant_created_at)
      order by date(me.instant_created_at)) as sq
