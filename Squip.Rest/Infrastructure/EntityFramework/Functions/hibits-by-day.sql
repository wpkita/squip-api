create or replace function hibits_by_day(input_time_zone text, input_user_id uuid)
returns table
(
        output_date date,
        exercise int,
        meditate int,
        pray int,
        meeting int,
        social int,
        flow_state int,
        talk_to_family int,
        nightly int,
        dbt_diary_card int
)
as
$$
select date(hi.instant_created_at at time zone input_time_zone) as output_date,
       count(hi.id) filter (
           where
           h.name = 'Exercise'
           ) as "exercise",
       count(hi.id) filter (
           where
           h.name = 'Meditate'
           ) as "meditate",
       count(hi.id) filter (
           where
           h.name = 'Pray'
           ) as "pray",
       count(hi.id) filter (
           where
           h.name = 'Meeting'
           ) as "meeting",
       count(hi.id) filter (
           where
           h.name = 'Social'
           ) as "social",
       count(hi.id) filter (
           where
           h.name = 'Flow state'
           ) as "flow_state",
       count(hi.id) filter (
           where
           h.name = 'Talk to family'
           ) as "talk_to_family",
       count(hi.id) filter (
           where
           h.name = 'Nightly'
           ) as "nightly",
       count(hi.id) filter (
           where
           h.name = 'DBT Diary Card'
           ) as "dbt_diary_card"
from habits h
         inner join hibits hi on h.id = hi.habit_id
where h.user_id = input_user_id
  and h.is_archived = false
  and hi.is_archived = false
group by date(hi.instant_created_at at time zone input_time_zone)
order by date(hi.instant_created_at at time zone input_time_zone)
$$ language sql;

select * from hibits_table_by_day('America/Chicago', '55ce7706-7cac-47d0-90ca-1273d28bb1b6')
