### Step-by-Step Guide to Restore a *.bak File in SQL Server 2025 on Windows 11

As a novice, restoring a backup (.bak file) means bringing back a database from a saved file. It's like loading a saved game. We'll use **SSMS (SQL Server Management Studio)** since it's easy for beginners. This works the same in SQL Server 2025 CTP2.1 as in earlier versions—no big changes here.

**Prerequisites (Quick Check):**
- Make sure SQL Server 2025 is installed and running (check in Services app: search "services" in Windows, look for "SQL Server (your instance name)").
- You have SSMS v21.4.8 open.
- You have the .bak file downloaded (e.g., WideWorldImporters.bak from GitHub).
- Run SSMS as Administrator if you face permission issues (right-click SSMS icon > Run as administrator).

#### Step 1: Open SSMS and Connect to Your Server
- Launch **SSMS** from your Start menu.
- In the "Connect to Server" window:
  - **Server type**: Database Engine.
  - **Server name**: Use `.` (dot) for local default instance, or `.\SQLEXPRESS` if it's Express edition.
  - **Authentication**: Windows Authentication (easiest for local PC).
- Click **Connect**. You'll see the Object Explorer on the left.

#### Step 2: Start the Restore Process
- In Object Explorer, expand the server (click the + sign).
- Right-click the **Databases** folder.
- Select **Restore Database...** from the menu.
  - This opens the Restore Database dialog box.

#### Step 3: Select the Backup File
- In the dialog, go to the **General** page (left side).
- Under "Source", choose **Device** (instead of Database).
- Click the **...** button next to it to open "Select backup devices".
- In the new window, click **Add**.
- Browse to your .bak file location (e.g., Downloads folder), select it, and click **OK**.
- Click **OK** again to close the devices window.
- The .bak file details (like database name) will load automatically.

#### Step 4: Configure Restore Options
- Still in **General** page:
  - **Database**: It auto-fills from the .bak—keep it or change if needed (e.g., to avoid overwriting an existing DB).
- Switch to the **Files** page (left side):
  - Check file paths—SQL Server suggests where to put data/log files. Change if needed (e.g., to your SQL data folder like C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA).
- Switch to the **Options** page:
  - Check **Overwrite the existing database (WITH REPLACE)** if restoring over an old DB.
  - Under "Recovery state", choose **RESTORE WITH RECOVERY** (default for full use).
  - Leave other options default unless you know what you're doing.

#### Step 5: Run the Restore
- Click **OK** at the bottom.
- A progress bar shows up—wait for it to finish (might take minutes depending on file size).
- If successful, you'll see a "Restore completed" message.
- In Object Explorer, refresh (right-click Databases > Refresh)—your new database appears!

#### Common Tips & Troubleshooting
- **Error: File access denied?** Ensure the .bak file isn't read-only and SQL Server service has permissions (run SSMS as Admin).
- **Error: Version mismatch?** If .bak is from older SQL Server, it might work, but test first.
- **Using T-SQL instead?** For code lovers: Open a New Query window in SSMS, run this (replace paths/names):
  ```sql
  RESTORE DATABASE YourDatabaseName
  FROM DISK = 'C:\Path\To\YourFile.bak'
  WITH MOVE 'LogicalDataFileName' TO 'C:\SQLData\YourDatabaseName.mdf',
  MOVE 'LogicalLogFileName' TO 'C:\SQLData\YourDatabaseName.ldf',
  REPLACE, RECOVERY;
  ```
  - Get logical names first: `RESTORE FILELISTONLY FROM DISK = 'C:\Path\To\YourFile.bak';`
- **Diagram for Visual Help:**
  ```
  [SSMS] --> Connect --> Right-click Databases --> Restore Database...
             |
             +--> Select Device --> Add .bak --> OK
             |
             +--> General/Files/Options --> Configure --> OK (Restore!)
  ```

If issues arise (e.g., specific error), share the error message for more help!