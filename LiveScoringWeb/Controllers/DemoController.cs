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
using System.Web.Mvc;

using LiveScoringWeb.LiveScoringAPI;
using LiveScoringWeb.LiveScoringAPI.Models;

namespace LiveScoringWeb.Controllers
{
    public class DemoController : Controller
    {

        private static LiveScoringAPIClient APIClient()
        {
            var client = new LiveScoringAPIClient();
            return client;
        }

        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

        // GET: Demo/Record
        public ActionResult Record()
        {
            return View();
        }

        // POST: Demo/Record
        [HttpPost]
        public ActionResult Record(Player player)
        {
            try
            {
                using (var client = APIClient())
                {
                    client.RecordScore(player);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        

        // GET: Demo/Scores
        public ActionResult Scores()
        {
            return View();
        }

        
    }
}
