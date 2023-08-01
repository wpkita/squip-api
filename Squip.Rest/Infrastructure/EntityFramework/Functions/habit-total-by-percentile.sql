create or replace function habit_total_by_percentile(input_percentile double precision, input_time_zone text,
                                                     input_user_id uuid)
    returns int as
$$
select percentile_disc(input_percentile) within group (order by output_count)
from habit_totals_by_day_with_time_zone(input_time_zone, input_user_id)
$$
    language sql;

select *
from habit_total_by_percentile(0.7, 'America/Chicago', '55ce7706-7cac-47d0-90ca-1273d28bb1b6')
