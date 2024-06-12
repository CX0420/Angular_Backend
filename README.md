**Requirements:**
1. Visual Studio
2. .Net version 7.0


**Create a database table by following SQL:**
```
CREATE TABLE Customer(
    CustomerId INT UNSIGNED PRIMARY KEY AUTO_INCREMENT COMMENT 'Customer Id',
    CustomerName VARCHAR(255) NOT NULL COMMENT 'Customer Name',
    CustomerGender INT UNSIGNED NOT NULL COMMENT 'Gender: 1. Male, 2. Female',
    CustomerPhoneNumber VARCHAR(20) NOT NULL COMMENT 'Customer Phone Number',
    CustomerEmail VARCHAR(50) NOT NULL COMMENT 'Customer Email',
    IsDeleted INT UNSIGNED NOT NULL DEFAULT 0 COMMENT 'Is Customer Deleted: 0. Not Deleted, 1. Deleted',
    CreatedTime TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Created Time',
    UpdatedTime TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Updated Time'
);
```
**Steps to open:**
1. Create a database and create a table with the above SQL.
1. Open the solution with Visual Studio.
2. Head to appsetting.json, change the below connection with your connection.
   ```
   "DefaultConnection": "Server=yourServer;Database=yourDatabaseName;User Id=yourUserId;Password=yourPassword;"
   ```
3. Click on run, that's it!

**High Level Vertical Slice**
* User Interface - By using Controller
* Business Logic - By using Service
* Data Access Layer - By using DAO
* Organized code followed by module
