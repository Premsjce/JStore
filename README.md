# JStore
Jewelry store application

Code was written using Visual Studio 2019 and .NET 4.6.1 framework was used
Its a WPF application

Users are stored in json file under Resources/all_users.json file

Notes :
Since it was a small application, i've not done the validation part and not written unit test cases
For password i'm using text directly instead of passwordbox control.

MVVM pattern has been used
Have made UserService singleton, because there should only one instance needed for application lifecycle
For different print type, a factory has been created, new type can be easily added
Command pattern has been used for button clicks, such that even if shortcuts are created then there commands can be easily assigned

