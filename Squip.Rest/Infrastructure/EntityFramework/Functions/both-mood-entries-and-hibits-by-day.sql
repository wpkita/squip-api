select *
from mood_entries_by_day(:'time_zone', :'user_id') as mood_entries
inner join
hibits_by_day(:'time_zone', :'user_id') as hibits
on mood_entries.output_date = hibits.output_date
