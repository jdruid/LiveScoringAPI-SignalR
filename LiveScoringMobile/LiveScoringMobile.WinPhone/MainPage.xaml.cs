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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Newtonsoft.Json;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace LiveScoringMobile.WinPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Player playerData;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            playerData = new Player();
            this.DataContext = playerData;

            var app = App.Current as App;
            app.Client.OnDataReceived += Client_OnDataReceived;

        }

        /// <summary>
        /// Update Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Client_OnDataReceived(object sender, dynamic e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
             {

                 List<Player> players = JsonConvert.DeserializeObject<List<Player>>(e);

                 ScoreGridView.ItemsSource = players;
                
                 StringBuilder sb = new StringBuilder();
                 sb.Append("<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">");
                 sb.Append("<Grid Width=\"200\" Height=\"100\">");
                 sb.Append("<StackPanel>");
                 sb.Append("<StackPanel Orientation=\"Horizontal\" Margin=\"10,3,0,3\"><TextBlock Text=\"Name:\" Style=\"{StaticResource BodyTextBlockStyle}\" Margin=\"0,0,5,0\"/><TextBlock Text=\"{Binding Name}\" Style=\"{StaticResource BodyTextBlockStyle}\"/></StackPanel>");
                 sb.Append("<StackPanel Orientation=\"Horizontal\" Margin=\"10,3,0,3\"><TextBlock Text=\"Score:\" Style=\"{StaticResource BodyTextBlockStyle}\" Margin=\"0,0,5,0\"/><TextBlock Text=\"{Binding Score}\" Style=\"{StaticResource BodyTextBlockStyle}\"/></StackPanel>");
                 sb.Append("<StackPanel Orientation=\"Horizontal\" Margin=\"10,3,0,3\"><TextBlock Text=\"Match:\" Style=\"{StaticResource BodyTextBlockStyle}\" Margin=\"0,0,5,0\"/><TextBlock Text=\"{Binding Match}\" Style=\"{StaticResource BodyTextBlockStyle}\"/></StackPanel>");
                 sb.Append("</StackPanel>");
                 sb.Append("</Grid>");
                 sb.Append("</DataTemplate>");

                 DataTemplate datatemplate = (DataTemplate)XamlReader.Load(sb.ToString());
                 ScoreGridView.ItemTemplate = datatemplate;
                 
             });
            //throw new NotImplementedException();
        }



        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

          
        }
    }
}
