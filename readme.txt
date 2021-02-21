Endpoint Handling:
inside the endpoint_handlers folder we have the Song , Tplayer and User classes which act as our endpoint handlers that 
serve all http requests from the TcpClients. for example the song class includes action methods like GetPlaylist ,
GetUserlibrary etc.



Database:
The handling between server and database happends in this layer of project inside the DatabaseRepositories folder.
inside we have Songrepo ,Playerrepo and Userrepo which all inherit from the DBconnection class, the DBconnection class makes a connection to 
the database and get the string connection parameters from the settings class.
The songreps (song repository) is responsible for getting the user library including all songs inside,  updating
and deleting the songs from a library, as well as getting the party palylist and sending and deleting songs from playlist.
The TPlayerreps (tournament player repository) is responsible for getting the list of all the players inside the 
tournament , setting the ready players as ready , checking if the player is already inside the tornament , inserting
and deleting players inside the tournament as well as emptying the tournament table when the tournamet is finished.
the Userreps (User repository) is responsible for athenticating a user when loging to the server (using the specific token)
, getting user score , getting user record from user table in db, checking of the user is admin or not , as well as
updating the user Set.



Tournament:
Tournament class includes the battle logic of players in the tournament (battle between each 2 players of tournament 
by taking each player set and confronting each element of each set with other player, and it does it for all the players
in the tournament.) 
when all the players inside the tornament press "Start Tournament", the battle starts.
the tournament class will start the battle if the number of players are 2 or more, it will calculate the winner and set
him as the admin as well as giving him 5 scores. 
after that it will return the battle log including the detailed event of the battle to all the players that participate
in the tournament.



Project feature decisions:
- if the player desides to enter the tournament without defining a set , he will get an error message to first complete his 
set and then he can enter a tournament.
- after entering the tournament if you are the only person in the tournament and you try to start a tournament you will get
a message that you cannot start the tournament with only one player!
- after you ready in the tournament, if other players are not ready in the tournament, you will get the message:
please wait for other players to ready.



extra feature:
after a user enters the tournament he/she can see the a list of all players(including their username and point) in the 
current tournament. each user can also sees his/her defined set for the tournament but set of other players are stared out and
are not visible. 



unit testing:
the unit tests for the tournament checks different outcomes depending on different set of inputs and it will insure that
the project and the battle logic works under any circumstances before it's deployed. unit testing these methods are critical
because it save us the time to manually check all the posibilities , and it makes the project reliable. it also helps us
to imagine all the posible outcomes so we can find out if the code has any kind of bug or inconsistencies.


Estimated time:
 ___________________________________________________
| Database Design:                    ~ 5 hours     |
| initial Project analyse design:     ~ 10 hours    |
| Http server classes:                ~ 10 hours    |
| implementing battle logic:          ~ 15 hours    |
| implementing repository classes:    ~ 25 hours    |
| database implementations:           ~ 10 hours    |
| client classes:                     ~ 10 hours    |   
| unit testing:                       ~ 5 hours     |
| debugging all kind of bugs!:        ~ 40 hours    |               
| total time                          ~ 130 hours   |
|___________________________________________________|



challanges, failures and lessons learned:
there was a problem by loading the pgadmin which would have not loaded..: the solution was to change the value of
"HKEY_CLASSES_ROOT\.js\Content Type" key in the windows registry from  "text/plain" to "text/javascript".
when using NpgsqlConnection class i would get the error: The type 'DbConnection' is defined in an assembly that is not referenced
, the solution was to install odbc driver for postgresql and to uninstall/instal Npgsql package again.
there was a problem when sending a querystring from httpwebrequest from client to httpserver, that extra controll characters
would have added to the string and would have make it unusable.. , the solution was to use the UrlDecode method of the 
httpUtility assembly to decode to string, and we needed to also add this assembly to the project.

