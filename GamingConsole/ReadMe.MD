1) Technologies used for building the application
	a) MS SQL Local DB used as database and Entity frame work as ORM.
	b) SignalR is used for web socket connection to push and pull data.
	c) Framework is MVC.

2) Run Application
	a) Preferably open in Visual studio 2017 or 2019. 
	b) F5 to run application (preferably use chrome as web browser)

3) Main Score board
	a) URL: https://localhost:44345/
	b) You can perform below operations
		i) Add Team
		ii) Open new and old consoles
		iii) Activate and Deactivate the console
		iv) Reset scores
		v) Delete console

4) N- number of Basket Ball Consoles can be opened: Up on incrementing the score sheet, it will update the main score board in real time.
	a) Example URL: https://localhost:44345/BasketBallConsole/New?ConsoleNumber=8
	b) You can increase or decrease the scores for teams, under multiple consoles and all data will stream real time in main score board.
	
5) Incase of any failure, data will be recovered from Local DB.


#Note:#
Install Microsoft SQL Server Express LocalDB incase not installed. This is the prerequisites for running the application.
Instllation instructions:
Install LocalDB through the installation wizard or by using the SqlLocalDB.msi program. LocalDB is an option when installing SQL Server 2016(or any version) Express.
Down load URL: https://go.microsoft.com/fwlink/?LinkID=799012
