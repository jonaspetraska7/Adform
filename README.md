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







