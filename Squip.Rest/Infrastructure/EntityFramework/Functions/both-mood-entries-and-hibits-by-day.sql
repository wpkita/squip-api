select *
from mood_entries_by_day('America/Chicago', '55ce7706-7cac-47d0-90ca-1273d28bb1b6') as mood_entries
inner join
hibits_by_day('America/Chicago', '55ce7706-7cac-47d0-90ca-1273d28bb1b6') as hibits
on mood_entries.output_date = hibits.output_date
