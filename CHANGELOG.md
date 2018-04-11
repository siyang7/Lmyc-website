# Changelog
All notable changes to this project will be documented in this file.

##Team member: Castiel Li (CL) , Jason Chen (JC) , Harrison Milbradt (HM) , Mu Chen (Paul) Li (ML), Nate Chiang (NC)

## [0.2.7]
### 2018-04-11
#### Added
* Add GET method that returns the user from a given username - NC
* Add method to calculate Allocated hours - NC
* Add GET method that get all members - JC

### Changed
* Route for volunteer api to /VolunteersAPI - NC

## [0.2.6]
### 2018-04-10
#### Added
* Add volunteer Controller and API - NC
* Add associative table UserBooking - JC
* Add more users - JC
* Add Booking API - JC
* Modify Get Booking API - JC

#### Changed
* Fix volunteer model - JC
* Re-migrate the database - JC
* Fix booking model - JC

## [0.2.5]
### 2018-04-09
#### Added
* Add booking policy - JC
* Add login policy - JC
* Add Admin policy - JC
* Add member status to role - JC
* Add skipper status to role - JC

#### Changed

#### Removed
* Delete Member status - JC
* Delete Skipper status - JC
* Remove old migrations - JC

## [0.2.4]
### 2018-04-09
#### Changed
* Edited the reservation model as we dont need AllocatedCredits - NC
* Created view models for Reservations - NC

### 2018-04-05
#### Changed
* Clean up migrations - JC
* Fix boat model - JC
* Fix reservation model - JC
* Fix reservation mvc - JC

## [0.2.3]
### 2018-04-02
#### Changed
* Fix boat seeding data - JC
* Delete old tests - JC

## [0.2.2]
### 2018-03-31
#### Added
* Add user roles - JC
* Modify user model - JC
* fix user controller - JC

## [0.2.1]
### 2018-03-31
#### Added
* VolunteerAPI controller - CL
* VolunteerMVC controller - CL
* VolunteerModel controller - CL
* Added Test project - CL     
* Create one test case - CL

#### Changed
* Fixed Castiel's mistake as he mistakenly dragged and dropped the entire model folder inside the DataLib library even thought there are no references inside the DataLib project - NC

## [0.2.0]
### 2018-03-26
#### Added
* Add online test database connection string - JC

#### Changed
* Modify reservation model - JC
* re-migrate reservation model - JC

## [0.1.4]
### 2018-03-24
#### Changed
* updated program.cs (run the seed method when program starts) - CL
#### Added
* BoatDummyData.cs - CL
* DbInitializer.cs - CL

## [0.1.3]
### 2018-03-22
#### Changed
* Fix the BoatPicture Max size to Int.maxValue so it fits the amount of bytes when uploading pictures - NC
* Edited the Detail and Index views to display the image - NC
* Edited the Create and Edit views to include input type file - NC
* Fix the Edit view to display status correctly - NC
#### Added
* Added control for image upload inside the BoatController.Create and Edit function - NC
* Add Login and Register API
* Use OpenIddict token authentication
* Add token view models

## [0.1.2]
### 2018-03-20
#### Changed
* Fix boat and user models - JC
* Fix boat Controllers and Views - JC
* Fix Boat status dropdown menu - JC

## [0.1.1]
### 2018-03-20
#### Changed
* Clean up docker configuration files - JC
* Update README - JC

## [0.1.0]
### 2018-03-13
#### Added
* Boat API Controller, regular controller and views - ML
* A workaround in Startup.cs to the "No parameterless constructor defined for this object" error when trying to create a controller. Check Startup.cs - ML
* Add user, boat, and reservation models - JC
* setup docker yml files - JC
* Add enumerators status - JC

#### Changed
* Updated Error message for Boat Model - CL
* Updated Error message for User Model - CL
* Updated Error message for Reservation Model - CL
* Modify register view and controller - JC

## [0.0.2]
### 2018-03-09
#### Added
* Setup Travis - HM
* Added Unit testing project - HM
* Added travis to readme - HM

## [0.0.1]
### 2018-03-01
#### Added
* Setup MVC template - JC

### 2018-02-27
#### Added
* project folder - JC

#### Changed
* readme.md (Added addtional information for readme) - CL
