# AutoKillCorporateProcess
App that detects when a corporate process is running on your device and kills it. 
Useful for killing some maintenance programs like an AV scan etc. 
### How to use
![Screenshot](screenshot.jpg) 
When promted input the name of the process you wish to kill. 
The app will kill it if its running and will hide in the background waiting for the process to reappear and kill it again. 
Default time is 10 minutes. (600000 ms) 
Run the app with one argument only - AutoKillCorporateProcess.exe "notepad" - runs with default time. 
Run the app with two arguments - AutoKillCorporateProcess.exe "notepad" 5000 - runs with the user specified time to check if the process is runnning.