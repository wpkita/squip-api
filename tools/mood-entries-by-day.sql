SELECT
    date(me."InstantCreatedAt"),
    AVG(me."Magnitude") FILTER (
        WHERE
            m."Name" = 'Happiness'
    ) AS "Happiness",
    AVG(me."Magnitude") FILTER (
        WHERE
            m."Name" = 'Hopefulness'
    ) AS "Hopefulness",
    AVG(me."Magnitude") FILTER (
        WHERE
            m."Name" = 'Sadness'
    ) AS "Sadness",
    AVG(me."Magnitude") FILTER (
        WHERE
            m."Name" = 'Pain'
    ) AS "Pain",
    AVG(me."Magnitude") FILTER (
        WHERE
            m."Name" = 'Guilt/Shame'
    ) AS "Guilt/Shame",
    AVG(me."Magnitude") FILTER (
        WHERE
            m."Name" = 'Fear/Anxiety'
    ) AS "Fear/Anxiety",
    AVG(me."Magnitude") FILTER (
        WHERE
            m."Name" = 'Numb/Empty'
    ) AS "Numb/Empty",
    AVG(me."Magnitude") FILTER (
        WHERE
            m."Name" = 'Irritable/Agitated'
    ) AS "Irritable/Agitated",
    AVG(me."Magnitude") FILTER (
        WHERE
            m."Name" = 'Anger'
    ) AS "Anger"
from
    "Moods" m
    inner join "MoodEntries" me on m."Id" = me."MoodId"
where
    m."UserId" = '55ce7706-7cac-47d0-90ca-1273d28bb1b6'
    and m."IsArchived" = false
    and me."IsArchived" = false
GROUP BY
    date(me."InstantCreatedAt")
order by
    date(me."InstantCreatedAt")