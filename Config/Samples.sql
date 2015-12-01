-- Sample Queries:
--   Highlight query, select corresponding query type and execute. 
--   For more help see the LogParser 2.2 documentation (www.logparser.com).
--   You will need to have LogParser 2.2 installed.

-- IIS Log - Page not found errors
SELECT * 
FROM '[this]'
WHERE sc-status = 404

-- IIS Log - Pages run
SELECT count(*) as number, cs-uri-stem
FROM C:\WINNT\system32\LogFiles\W3SVC1\ex060106.log
WHERE cs-uri-stem like '%.asp%'
GROUP BY cs-uri-stem
ORDER BY number desc

-- IIS Log - Page errors
SELECT count(*) as num_errors,
       sc-status,
       cs-uri-stem
FROM C:\WINNT\system32\LogFiles\W3SVC1\ex060106.log
WHERE sc-status >= 400 
GROUP BY cs-uri-stem,
      sc-status
ORDER BY num_errors desc

-- IIS Log - Timings (across all logs)
SELECT TOP 10 cs-uri-stem, COUNT(*)
FROM 'C:\WINNT\system32\LogFiles\W3SVC1\ex*.log' 
WHERE time-taken > 10000
GROUP BY cs-uri-stem 
ORDER BY COUNT(*) DESC

-- Event Log - Top 50 from system
SELECT TOP 50 * FROM system

SELECT SourceName, 
       EventID, 
       MUL(PROPCOUNT(*) ON (SourceName), 100.0) AS Percent
FROM System
GROUP BY SourceName, EventID
ORDER BY SourceName, Percent DESC

-- Registry
SELECT TOP 5 * FROM \HKLM

-- File System - Large 100 meg+ text files in c:\temp
SELECT * FROM c:\temp\*.txt where Size > 100000000 

-- Text File - Remote web page
SELECT * FROM http://www.google.com

-- Text File - Local file
SELECT Text 
FROM c:\temp\mtlog.txt 
WHERE Text LIKE '%[ERROR%' AND
      Text LIKE '%[MTMSIX%'

-- CSV File - Supports Excel export and Perfmon logs
SELECT * FROM 'c:\temp\perfmonlog.csv'

-------------------------------------------------
-- Other IIS Queries
-------------------------------------------------
-- IIS Log - Win32 error codes by total and page
SELECT cs-uri-stem AS Url, 
WIN32_ERROR_DESCRIPTION(sc-win32-status) AS Error, Count(*) AS Total
FROM C:\WINNT\system32\LogFiles\W3SVC1\ex*.log 
WHERE (sc-win32-status > 0) 
GROUP BY Url, Error 
ORDER BY Total DESC

-- IIS Log - HTTP methods (GET, POST, etc) used per Url
SELECT cs-uri-stem AS Url, cs-method AS Method, 
Count(*) AS Total 
FROM C:\WINNT\system32\LogFiles\W3SVC1\ex*.log
WHERE (sc-status < 400 or sc-status >= 500)
GROUP BY Url, Method
ORDER BY Url, Method

-- IIS Log - Bytes sent from the server
SELECT cs-uri-stem AS Url, Count(*) AS Hits, 
AVG(sc-bytes) AS Avg, Max(sc-bytes) AS Max, 
Min(sc-bytes) AS Min, Sum(sc-bytes) AS TotalBytes 
FROM C:\WINNT\system32\LogFiles\W3SVC1\ex*.log
GROUP BY cs-uri-stem 
HAVING (Hits > 100) ORDER BY [Avg] DESC

-- IIS Log - Bytes sent from the client
SELECT cs-uri-stem AS Url, Count(*) AS Hits, 
AVG(cs-bytes) AS Avg, Max(cs-bytes) AS Max, 
Min(cs-bytes) AS Min, Sum(cs-bytes) AS TotalBytes 
FROM C:\WINNT\system32\LogFiles\W3SVC1\ex*.log 
GROUP BY Url 
HAVING (Hits > 100) 
ORDER BY [Avg] DESC

