UPDATE  [AppSocialConflicts] SET
	[Count] = CONVERT(INT, LTRIM(RTRIM(SUBSTRING([Code], 0, CHARINDEX('-', [Code]))))),
	[Year] = CONVERT(INT, LTRIM(RTRIM(SUBSTRING([Code], CHARINDEX('-', [Code]) + 1, LEN([Code])))))
WHERE ISNUMERIC(LTRIM(RTRIM(SUBSTRING([Code], CHARINDEX('-', [Code]) + 1, LEN([Code]))))) = 1 AND
	  ISNUMERIC(LTRIM(RTRIM(SUBSTRING([Code], 0, CHARINDEX('-', [Code]))))) = 1