select mods.*,
       habs.habittotal
from (select date(hib.instant_created_at)     habitDate,
             count(hib.instant_created_at) as HabitTotal
      from hibits as hib
               inner join habits as hab on hab.id = hib.habit_id
          and hab.is_archived = false
      group by date(hib.instant_created_at)) habs
         inner join (select date(me.instant_created_at) moodDate,
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Happiness'
                                ) as                    "Happiness",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Hopefulness'
                                ) as                    "Hopefulness",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Sadness'
                                ) as                    "Sadness",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Pain'
                                ) as                    "Pain",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Guilt/Shame'
                                ) as                    "Guilt/Shame",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Fear/Anxiety'
                                ) as                    "Fear/Anxiety",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Numb/Empty'
                                ) as                    "Numb/Empty",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Irritable/Agitated'
                                ) as                    "Irritable/Agitated",
                            avg(me.magnitude) filter (
                                where
                                mo.name = 'Anger'
                                ) as                    "Anger"
                     from mood_entries as me
                              inner join moods as mo on mo.id = me.mood_id
                         and mo.user_id = '55ce7706-7cac-47d0-90ca-1273d28bb1b6'
                         and mo.is_archived = false
                     group by date(me.instant_created_at)) mods on habs.habitDate = mods.moodDate
order by mods.moodDate
