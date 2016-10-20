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
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LiveScoringMobile.Droid
{
    public class PlayerScoreAdapter : BaseAdapter<LiveScoringMobile.Player>
    {
        List<LiveScoringMobile.Player> items;
        Activity context;
        public PlayerScoreAdapter(Activity context, List<LiveScoringMobile.Player> items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override LiveScoringMobile.Player this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.ScoreRowView, null);
            view.FindViewById<TextView>(Resource.Id.Name).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.Score).Text = item.Score.ToString();
            view.FindViewById<TextView>(Resource.Id.Match).Text = item.Match;
            return view;
        }
    }
}