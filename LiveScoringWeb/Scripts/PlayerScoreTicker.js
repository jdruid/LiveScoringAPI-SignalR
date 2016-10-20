// A simple templating method for replacing placeholders enclosed in curly braces.
if (!String.prototype.supplant) {
    String.prototype.supplant = function (o) {
        return this.replace(/{([^{}]*)}/g,
            function (a, b) {
                var r = o[b];
                return typeof r === 'string' || typeof r === 'number' ? r : a;
            }
        );
    };
}

$(function () {

    var ticker = $.connection.PlayerScoreTickerHub, // the generated client-side hub proxy
        $scoreTable = $('#scoreTable'),
        $scoreTableBody = $scoreTable.find('tbody'),
        rowTemplate = '<tr data-symbol="{Name}"><td>{Name}</td><td>{Score}</td><td>{Match}</td></tr>';

    function formatScore(playerScore) {
        return $.extend(playerScore, {
            Score: playerScore.Score,
            Name: playerScore.Name,
            Match: playerScore.Match
        });
    }

    function init() {
        ticker.server.getAllScores().done(function (playerScore) {
            $scoreTableBody.empty();
            $.each(playerScore, function () {
                var score = formatScore(this);
                $scoreTableBody.append(rowTemplate.supplant(score));
            });
        });
    }

    // Add a client-side hub method that the server will call
    ticker.client.updatePlayerScores = function (playerScore) {
        $.each(playerScore, function () {
            
            var displayScore = formatScore(this),
                $row = $(rowTemplate.supplant(displayScore));

            if ($scoreTableBody.find('tr[data-symbol=' + displayScore.Name + ']').length > 0) {
                $scoreTableBody.find('tr[data-symbol=' + displayScore.Name + ']').replaceWith($row);
            } else {
                $scoreTableBody.append(rowTemplate.supplant(displayScore));
            }
        });

    }

    // Start the connection
    $.connection.hub.url = "http://livescoringapi.azurewebsites.net/signalr"
    $.connection.hub.logging = true;
    $.connection.hub.start().done(init);

});