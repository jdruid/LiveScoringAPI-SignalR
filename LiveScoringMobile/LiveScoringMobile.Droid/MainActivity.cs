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
using System.Threading;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LiveScoringMobile.Droid
{
	[Activity (Label = "LiveScoringMobile.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

        private ListView listscores;
        private Player playerData;


        public RClientCommon Client { get; set; }
        private async Task StartHub()
        {
            Client = new RClientCommon();
            await Client.Connect();
        }

        protected override async void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);
            
            await StartHub();
            Client.OnDataReceived += Client_OnDataReceived;
        }

        private void Client_OnDataReceived(object sender, dynamic e)
        {
            
            List<LiveScoringMobile.Player> players = JsonConvert.DeserializeObject<List<Player>>(e);
            
            listscores = FindViewById<ListView>(Resource.Id.listscores);

            RunOnUiThread(() =>
                listscores.Adapter = new PlayerScoreAdapter(this, players)  

            );
            throw new NotImplementedException();
        }
    }
}


