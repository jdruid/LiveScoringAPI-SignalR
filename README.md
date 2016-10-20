# Live Scoring Web API with Signal R #

There are times where your website or mobile app would like to show live data, such as scores. This repo consists of an API and a few demo projects that make that happen. Using .NET Web API and Signal R.

Working Demo [http://livescoringwebapp.azurewebsites.net/](http://livescoringwebapp.azurewebsites.net/)


## Installation ##

1. Clone repo
2. LiveScoringWebAPI needs to be published to Azure API App Services or some other host
3. LiveScoringWeb is the web test/demo project for live web scoring
4. LiveScoringMobile is the mobile Xamarin apps for live scoring

## Usage ##

**LiveScoringAPI**

This is the API app that will take a "post" of scores and process them out to all of the connected clients via SignalR. 

*Startup.cs* - currently there is an option configured for CORS to be enabled. This is needed for access to the Web.

    map.UseCors(Cors.Options.AllowAll)

*Controllers/API/PlayerScoreController.cs* - Endpoint to post scores to. Currently there is no authentication. That needs to be added still. There are 2 endpoints /GET and /POST
/GET is a test method to generate random scores in the Player model. This can be removed.
/POST is the main method. It is expecting a Player object and instantiates the PlayerScoreTickerHub to "update scores"

*PlayerScoreTickerHub.cs* - this is the SignalR hub clients will connect to. It also has a few exposed methods. The main one, for Website client script is GetAllScores. This happens at init(). I did not add that to the Mobile demos yet.

*PlayerScoreTicker.cs* - main file for updating scores. Currently the player object is a concurrent dictionary that just keeps getting updated with a score. This most likely will need to change at some point either time based or upon another call to clear out scores. There are also some test methods in there as well. **BroadcastPlayerScores** calls a method for all connected clients.

*Models/PlayerScore.cs* - sample player score model

FYI - Swagger is enabled as well http://webapi-url/swagger

**LiveScoringWeb**

This project serves 2 purposes

1. Form to post to the API - http://website-url/demo/record 
2. Demo page to show the scoring updates in realtime - http://website-url/demo/scores

*/Controller/DemoController.cs* - When a user records a score it will post using the APIClient for the LiveScoringAPI

*/Scripts/PlayerTicker.js *- this is the JS on the demo score page that "gets" the data on init and then has a JS function that is called by the hub to update the scores in realtime.

**LiveScoringMobile**

This is a Xamarin project. Contains shared code and 3 others (iOS, Droid and WinPhone). iOS is not complete.

*LiveScoringMobile* has some shared code - a Player Model and a Signal R Client. You may have to update the domain name and player hub accordingly.

LiveScoringMobile.WinPhone - has an example of displaying scores. App.xaml.cs has the R Client connecting OnLaunched. MainPage.xaml.cs has a method when data is received from the hub it will use a dispatcher to update the UI

LiveScoringMobile.Droid - has an example of displaying scores. Similar to WinPhone, MainActivity kicks off the R Client and has a listener for data that is recieved. It will process the data with RunOnUiThread for real time updating.

LiveScoringMobile.iOS - not implemented but will follow same pattern

## History ##

None at this time

## Credits ##

Joshua Drew
Sr Technical Evangelist - Microsoft
@jdruid
Drew5.net

## License ##

Apache License 

Version 2.0, January 2004 

http://www.apache.org/licenses/ 

## DISCLAIMER: ##

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

