## 

TfL Coding Challenge



## Getting Started

## Prerequisites

1) Install Visual Studio 2017 (Community/Professional/Enterprise) - https://visualstudio.microsoft.com/downloads/

2) Install PowerShell 

3) Download/Clone the Solution from Git

	- https://github.com/vjsar/RoadStatus.git

4) Open the Solution File RoadStatus.sln

Edit appsettings.json

- Update the below fields in the appsettings.json with correct Values
	- AppId
	- AppKey

- Update the below projects appsettings.json
	- RoadStatus
	- RoadStatus.IntegrationTests


##Running Unit Test and Integration Test
 - Open TestExplorer in VS2017 and Click "Run All" will run all the tests.

##Deploying the Application via Powershell

	- Open Powershell
	- Run the Deploy.ps1 file Under the solution Folder 
		- this will Create a Deploy Folder in C drive
		- Publish the Project to C:\Deploy Folder
		- Get to the C:\Deploy folder Location to run the RoadStatus.exe Application

##Run the Application
	- Type Roadstatus.exe <RoadName>


##Assumptions

	 - Used .Net Core 2.2
	 - To run the Application and Integration Test, AppId and AppKey should be a valid one.
	 - Assumed the client will be having a C drive in their PC if not please update the folder location in Deploy.ps1
	 - Used AutoFac for DI
	 - Used Xunit and Moq for Unit and Integration Tests.
	 - To run the application in Debug mode please update the Application Arguments in the Debug Tab of the Project Property.








