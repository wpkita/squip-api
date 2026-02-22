select corr("Happiness", "habittotal")          happiness,
       corr("Hopefulness", "habittotal")        hopefulness,
       corr("Sadness", "habittotal")            sadness,
       corr("Pain", "habittotal")               pain,
       corr("Guilt/Shame", "habittotal")        guilt_shame,
       corr("Fear/Anxiety", "habittotal")       fear_anxiety,
       corr("Numb/Empty", "habittotal")         numb_empty,
       corr("Irritable/Agitated", "habittotal") irritable_agitated,
       corr("Anger", "habittotal")              anger
from (select mods.*,
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
                               and mo.user_id = :'user_id'
                               and mo.is_archived = false
                           group by date(me.instant_created_at)) mods on habs.habitDate = mods.moodDate
      order by mods.moodDate) dailyTotals
