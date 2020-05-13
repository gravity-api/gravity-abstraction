# Gravity Abstraction
Gravity API, Web Driver abstraction. Allows to create any type and or combination of Web Driver, using a simple API abstraction which can also be send over HTTP.

# More Information
* https://github.com/gravity-api/gravity-actions
* https://github.com/gravity-api/gravity-macros

# Quick Start
```csharp
// local driver
var driverParams = "{'driver':'ChromeDriver','driverBinaries':'C:\\myDrivers'}";
var driver = new DriverFactory(driverParams).Create();

// remote driver
var driverParams = "{'driver':'ChromeDriver','driverBinaries':'http://localhost:4444/wd/hub'}";
driver = new DriverFactory(driverParams).Create();
```