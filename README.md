# Squares API Solution
### Prerequisites
* Microsoft Visual Studio with support for the newest stable version of .NET (currently .NET6).
* Microsoft SQL Server.
* Preparation - Create a database in your MSSQL server called AdformDb.
### Launch
* Standard - Open the Adform.sln solution select Adform launch profile in Visual Studio and launch the project.
* Docker - (WIP) Configure the ports on your local machine to allow connections from docker container to your local database and launch the project with Docker launch profile.
### The task
The Squares UI team is taking care of the front-end side of things, however they need your help for the backend solution.

Create an API that enables the consumer to find out what and how many squares can be drawn from a given set of points in a 2D plane. A point is a pair of integer X and Y coordinates. A square is a set of 4 points that when connected make up, well, a square. 

### Example of a list of points that make up a square:
```[(-1;1), (1;1), (1;-1), (-1;-1)]```

# Decisions made / Performace considerations
### Linq2db 
* Using Linq2db lightweight ORM instead of Entity framework  for more performant DB operations 
* Not storing individual point values (no sense to introduce lots of queries)
* Points are stored in a Text MSSQL data type, capable of handling up to 2GB of point imports
* Once the squares are calculated the value is cached in the database, secondary queries will be instant
* (PLAN TODO) : Add a new method for calculating squares just for the newly added point (so you won't need to recalculate all squares when only one point is added)

### Common
* All bussiness and persistance layer logic moved to a Common class library, so that the solution is not tied to the implementing project (Could make a Winforms/MVC or any other type of app, not only API if needed)

### Timeouts
* Due to a requirement that a request should take no longer than 5 seconds, imposed a hard limit on a request
* Introduced cancelation tokens for async operations and also incorporated cancelation token to a square calculation synchronous method

### Square calculation algorithm
* After searching for a solution, found an O(n^2) solution, which also uses multithreaded approach to split up computing time.
* Improved the solution compute time safety (useful for lambda computing time) by introducing cancelation token, so that when given too complex task, it would not compute for too long (5sec max Opt-in/Opt-out)

### Performance statistics
* Test data located in TEST_DATA folder. 

    All import / add / delete :
    
    around 1 sec. due to fast Linq2db ORM and small number of db operations.
    
    0.5K_Squares_1K_Points : 
    
    Pasted to URL in browser : ~ 1 sec.
    Swagger : ~ 5 sec.
    
    1K_Squares_4K_Points : 
    
    Pasted to URL in browser : ~ 3 sec.
    Swagger : ~ 18 sec.
    
    2K_Squares_8K_Points : (fails. 5 sec. response rule)
    
    Pasted to URL in browser : ~ 9 sec.
    Swagger : ~ 48 sec.

# API Usage
### /Points/Import
* Takes in a list of point objects ```   {
    "x": -1,
    "y": 1
  }``` and returns a unique Id (Guid) of the created ```PointList``` in a string form.
* Example request : ```/Points/Import```
* Post request.
* Example request body :
```
[
  {
    "x": -1,
    "y": 1
  },
  {
    "x": 1,
    "y": 1
  },
  {
    "x": 1,
    "y": -1
  },
  {
    "x": -1,
    "y": -1
  }
]
```
* Example response :
```
"ab29088c-abe2-411e-8c14-da507c1ce3f8"
```
### /Points/Add
* Takes in the url the ID of the ```PointList``` and the point object ```   {
    "x": -1,
    "y": 1
  }``` in the body and adds the point to a ```PointList``` in the database
* Example request : ```/Points/Add?pointListId=AB29088C-ABE2-411E-8C14-DA507C1CE3F8```
* Post request
* Example request body :
```
{
  "x": 1,
  "y": 0
}
```
* Example response :
* Point added successfully
```
1
```
* Point was not added successfully (usually when point is already entered)
```
-1
```
### /Points/Delete
* Takes in the url the ID of the ```PointList``` and the point object ```   {
    "x": -1,
    "y": 1
  }``` in the body and deletes the point from a ```PointList``` in the database
* Example request : ```/Points/Delete?pointListId=AB29088C-ABE2-411E-8C14-DA507C1CE3F8```
* Post request
* Example request body :
```
{
  "x": 1,
  "y": 0
}
```
* Example response :
* Point removed successfully
```
1
```
* Point was not removed successfully (usually when point was never entered)
```
-1
```
### /Points/GetSquares
* Takes in the url the ID of the ```PointList``` and returns a list of squares (squares are a list of points).
* Get request
* Example request : ``` Points/GetSquares?pointListId=76AC95F9-2566-43B2-A6F9-3A0683D3A008 ```
* Example response :
* A list of a squares found. (a list of a list of points)
```
[
  [
    {
      "x": -1,
      "y": 1
    },
    {
      "x": -1,
      "y": -1
    },
    {
      "x": 1,
      "y": -1
    },
    {
      "x": 1,
      "y": 1
    }
  ],
  [
    {
      "x": -1,
      "y": 1
    },
    {
      "x": 1,
      "y": 1
    },
    {
      "x": 1,
      "y": 3
    },
    {
      "x": -1,
      "y": 3
    }
  ]
]
```
* When no squares are found :
```
[]
```







