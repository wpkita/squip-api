CREATE OR REPLACE FUNCTION get_daily_habit_total_by_percentile(input_percentile double precision, input_user_id uuid)
    RETURNS double precision AS
$$
select percentile_cont(input_percentile) within group (order by c) as foo
from (select date(hib.instant_occurred_at - interval '5 hours'),
             count(*) c
      from hibits as hib
               inner join habits as hab
                          on hab.id = hib.habit_id
      where hab.user_id = input_user_id
        and hab.is_archived = false
      group by date(hib.instant_occurred_at - interval '5 hours')) as c
$$
LANGUAGE SQL;
