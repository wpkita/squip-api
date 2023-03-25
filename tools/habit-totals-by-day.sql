select
    date(hib."InstantCreatedAt"),
    count(*)
from
    "Hibits" as hib
    inner join "Habits" as hab on hab."Id" = hib."HabitId"
where
    hab."IsArchived" = false
    and date(hib."InstantCreatedAt") <> '2022-08-27'
group by
    date(hib."InstantCreatedAt")
order by
    date(hib."InstantCreatedAt") ASC
    /*-- All days in range*/
SELECT
    date(d),
    count(hib."InstantCreatedAt")
FROM
    generate_series(
        timestamp without time zone '2022-12-01',
        timestamp without time zone '2023-03-10',
        '1 day'
    ) as gs(d)
    left outer join "Hibits" as hib
    inner join "Habits" as hab on hab."Id" = hib."HabitId" on hib."InstantCreatedAt" :: Date = date(d)
    and hab."IsArchived" = false
    and date(hib."InstantCreatedAt") <> '2022-08-27'
group by
    date(d)
order by
    date(d) ASC
    /*-- by day*/
select
    count(*)
from
    "Habits" as hab
    inner join "Hibits" as hib ON hib."HabitId" = hab."Id"
where
    hib."InstantCreatedAt" >= '2023-03-10 05:00:00'
    and hib."InstantCreatedAt" < '2023-03-11 05:00:00'
