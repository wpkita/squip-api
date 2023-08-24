create or replace function mood_entries_by_day(input_time_zone text, input_user_id uuid)
returns table
(
    output_date date,
    happiness numeric,
    hopefulness numeric,
    sadness numeric,
    pain numeric,
    guilt_shame numeric,
    fear_anxiety numeric,
    numb_empty numeric,
    irritable_agitated numeric,
    anger numeric
)
as
$$

select date(me.instant_created_at at time zone input_time_zone),
       avg(me.magnitude) filter (
           where
           m.name = 'Happiness'
           ) as "happiness",
       avg(me.magnitude) filter (
           where
           m.name = 'Hopefulness'
           ) as "hopefulness",
       avg(me.magnitude) filter (
           where
           m.name = 'Sadness'
           ) as "sadness",
       avg(me.magnitude) filter (
           where
           m.name = 'Pain'
           ) as "pain",
       avg(me.magnitude) filter (
           where
           m.name = 'Guilt/Shame'
           ) as "guilt_shame",
       avg(me.magnitude) filter (
           where
           m.name = 'Fear/Anxiety'
           ) as "fear_anxiety",
       avg(me.magnitude) filter (
           where
           m.name = 'Numb/Empty'
           ) as "numb_empty",
       avg(me.magnitude) filter (
           where
           m.name = 'Irritable/Agitated'
           ) as "irritable_agitated",
       avg(me.magnitude) filter (
           where
           m.name = 'Anger'
           ) as "anger"
from moods m
         inner join mood_entries me on m.id = me.mood_id
where m.user_id = input_user_id
  and m.is_archived = false
  and me.is_archived = false
group by date(me.instant_created_at at time zone input_time_zone)
order by date(me.instant_created_at at time zone input_time_zone)
$$ language sql;


select * from mood_entries_by_day('America/Chicago', '55ce7706-7cac-47d0-90ca-1273d28bb1b6')
