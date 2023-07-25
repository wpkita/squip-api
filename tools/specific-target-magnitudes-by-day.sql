SELECT
    date (d), coalesce (avg (te."Magnitude"), 0)
FROM
    generate_series(
    timestamp without time zone '2023-01-01', timestamp without time zone '2023-07-25', '1 day'
    ) as gs(d)
    left outer join "TargetEntries" as te
    inner join "Targets" as t
on t."Id" = te."TargetId" and t."Name" = 'NAME' on te."InstantCreatedAt" :: Date = date (d)
    and t."IsArchived" = false
group by
    date (d)
order by
    date (d) ASC
