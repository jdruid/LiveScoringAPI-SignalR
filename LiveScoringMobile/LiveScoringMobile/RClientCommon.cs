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
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;


namespace LiveScoringMobile
{
    public class RClientCommon
    {
        private readonly HubConnection connection;
        private readonly IHubProxy proxy;

        public event EventHandler<dynamic> OnDataReceived;

        public RClientCommon()
        {
            connection = new HubConnection("http://livescoringapi.azurewebsites.net");
            proxy = connection.CreateHubProxy("PlayerScoreTickerHub");
        }

        public async Task Connect()
        {
            await connection.Start();

            proxy.On("updatePlayerScoresFormatted", (dynamic data) =>
            {
                if (OnDataReceived != null)
                {
                    OnDataReceived(this, data);
                    System.Diagnostics.Debug.WriteLine("Got message " + (string)data);
                }
                
            });
        }

    }
}