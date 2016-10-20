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
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

using LiveScoringAPI.Hubs;
using LiveScoringAPI.Models;

namespace LiveScoringAPI.API.Controllers
{
    public class PlayerScoreController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Test operation to kick off random scores of players
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation("RandomizeScores")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public HttpResponseMessage Get()
        {
            PlayerScoreTickerHub pth = new PlayerScoreTickerHub();
            pth.RandomScores();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        
        // POST api/<controller>
        /// <summary>
        /// Creates a updated player score
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">List of player objects</param>
        /// <returns></returns>
        [SwaggerOperation("RecordScore")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public HttpResponseMessage Post(Player player)
        {

            if (ModelState.IsValid)
            {
                PlayerScoreTickerHub pth = new PlayerScoreTickerHub();
                pth.UpdateScore(player);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
        }
        
    }
}