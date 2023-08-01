create or replace function habit_totals_by_day_with_time_zone(input_time_zone text, input_user_id uuid)
    returns table
            (
                output_date  date,
                output_count int
            )
as
$$
select date(instant_occurred_at at time zone input_time_zone) as output_date,
       count(*)                                               as output_count
from hibits
         inner join habits on habits.id = hibits.habit_id
where habits.is_archived = false
  and hibits.is_archived = false
  and habits.user_id = input_user_id
  and hibits.user_id = input_user_id
group by date(instant_occurred_at at time zone input_time_zone)
$$
    language sql;

select *
from habit_totals_by_day_with_time_zone('America/Chicago', '55ce7706-7cac-47d0-90ca-1273d28bb1b6')
