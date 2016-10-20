/*
You may obtain a copy of the License at
http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 */
using System.ComponentModel;

namespace LiveScoringMobile
{
    public class Player : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _Name;
        private int _Score;
        private string _Match;

        public string Name { get { return _Name; } set { _Name = value; NotifyPropertyChanged("Name"); } }
        public int Score { get { return _Score; } set { _Score = value; NotifyPropertyChanged("Score"); } }
        public string Match { get { return _Match; } set { _Match = value; NotifyPropertyChanged("Match"); } }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
