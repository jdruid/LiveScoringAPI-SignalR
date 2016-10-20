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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

using LiveScoringAPI.Models;

namespace LiveScoringAPI.Hubs
{
    [HubName("PlayerScoreTickerHub")]
    public class PlayerScoreTickerHub : Hub
    {
        private readonly PlayerScoreTicker _playerScoreTicker;

        public PlayerScoreTickerHub() : this(PlayerScoreTicker.Instance) { }

        public PlayerScoreTickerHub(PlayerScoreTicker playerScoreTicker)
        {
            _playerScoreTicker = playerScoreTicker;
        }
        
        /// <summary>
        /// For testing of HTML and calling via JS to get all scores
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Player> GetAllScores()
        {
            return _playerScoreTicker.GetAllScores();
        }

        /// <summary>
        /// Test method to refresh scores when WEB API is called with a GET
        /// </summary>
        public void RandomScores()
        {
            _playerScoreTicker.RandomlyIncreaseScores();
        }

        /// <summary>
        /// Main method called by the WebAPI POST
        /// </summary>
        /// <param name="playerScore"></param>
        public void UpdateScore(Player playerScore)
        {
            _playerScoreTicker.UpdatePlayerScore(playerScore);
        }
    }
}