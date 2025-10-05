BEGIN TRANSACTION;

-- 1. Add new column to Author
ALTER TABLE dbo.Author
ADD email_address VARCHAR(100) NULL;

-- 2. Drop default constraints on min_M and max_M
ALTER TABLE dbo.Job
DROP CONSTRAINT DF__Job__min_M__46E78A0C;

ALTER TABLE dbo.Job
DROP CONSTRAINT DF__Job__max_M__47DBAE45;

-- 3. Drop columns min_M and max_M
ALTER TABLE dbo.Job
DROP COLUMN min_M, max_M;

-- 4. Alter column type in BookAuthor
ALTER TABLE dbo.BookAuthor
ALTER COLUMN royality_percentage INT;

COMMIT;