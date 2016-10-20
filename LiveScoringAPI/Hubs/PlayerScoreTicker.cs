/*
You may obtain a copy of the License at
http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 */
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

using LiveScoringAPI.Models;

namespace LiveScoringAPI.Hubs
{
    public class PlayerScoreTicker
    {
        // Singleton instance
        private readonly static Lazy<PlayerScoreTicker> _instance = new Lazy<PlayerScoreTicker>(() => new PlayerScoreTicker(GlobalHost.ConnectionManager.GetHubContext<PlayerScoreTickerHub>().Clients));

        private readonly ConcurrentDictionary<string, Player> _playerScore = new ConcurrentDictionary<string, Player>();

        private readonly object _updatePlayerScoreLock = new object();

        private readonly Random _updateOrNotRandom = new Random();

        private volatile bool _updatingPlayerScores = false;

        /// <summary>
        /// This loads when a connection is made.
        /// </summary>
        /// <param name="clients"></param>
        private PlayerScoreTicker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            _playerScore.Clear();

            //Need to load this dynamically at some point
            var playerScores = new List<Player>
            {
                new Player { Name = "Barney", Match = "Somerville", Score = 0 },
                new Player { Name = "Homer", Match = "Somerville", Score = 0 },
                new Player { Name = "Krusty", Match = "Somerville", Score = 0 }
            };

            playerScores.ForEach(playerScore => _playerScore.TryAdd(playerScore.Name, playerScore));
            
        }

        public static PlayerScoreTicker Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        /// <summary>
        /// Main method to broadcast out to all clients
        /// </summary>
        private void BroadcastPlayerScores()
        {
            //Clients.All.updatePlayerScores(JsonConvert.SerializeObject(_playerScore.Values));
            Clients.All.updatePlayerScores(_playerScore);
            Clients.All.updatePlayerScoresFormatted(JsonConvert.SerializeObject(_playerScore.Values));
        }

        /// <summary>
        /// Main method to broadcast out to all clients
        /// </summary>
        /// <param name="playerScore"></param>
        private void BroadcastPlayerScores(Player playerScore)
        {
            Clients.All.updatePlayerScores(playerScore);
        }

        /// <summary>
        /// Upon inital load. Maybe add a param in for match name
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Player> GetAllScores()
        {
            return _playerScore.Values;
        }
                
        public void UpdatePlayerScore(Player playerScore)
        {
            _playerScore.AddOrUpdate(playerScore.Name, playerScore, (key, value) =>  playerScore );

            lock (_updatePlayerScoreLock)
            {
                if (!_updatingPlayerScores)
                {
                    _updatingPlayerScores = true;

                    BroadcastPlayerScores();
                }

                _updatingPlayerScores = false;
            }
        }

        #region TEST METHODS
        public void RandomlyIncreaseScores()
        {
            UpdateAllPlayerScores(null);
        }

        private void UpdateAllPlayerScores(object state)
        {
            lock (_updatePlayerScoreLock)
            {
                if (!_updatingPlayerScores)
                {
                    _updatingPlayerScores = true;

                    foreach (var playerScore in _playerScore.Values)
                    {
                        if (UpdatePlayerScoresRandom(playerScore))
                        {
                            BroadcastPlayerScores(playerScore);
                        }
                    }

                    _updatingPlayerScores = false;
                }
            }
        }

        private bool UpdatePlayerScoresRandom(Player playerScore)
        {
            // Randomly choose whether to update this player or not
            var r = _updateOrNotRandom.NextDouble();
            if (r > .1)
            {
                return false;
            }

            // Update the player score by a random factor
            var random = new Random(playerScore.Score);

            playerScore.Score += random.Next(0, 30);
            return true;
        }
        #endregion
    }
}