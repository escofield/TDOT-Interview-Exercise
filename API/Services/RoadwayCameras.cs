using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using IO.Swagger.Models;
using System.Net.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace IO.Swagger.Services
{

    /// Concrete class of IRoadwayCameras
    public class RoadwayCameras : IRoadwayCameras
    {
        private readonly Settings _settings;
        /// Constructor for RoadwayCameras
        public RoadwayCameras(IOptions<Settings> optionsAccessor)
        {
            _settings = optionsAccessor.Value;
        }
        private List<Camera> _cameras = null;
 
        /// cameras
        /// I spent hours trying to find the openData documentation that works
        /// with "http://www.tdot.tn.gov/opendata/api/public/RoadwayCameras"
        /// but Soda, Soql lie.  I was not able to perform any filtering
        /// or query's on this endpoint according to their specification.
        /// i then resorted to storing the camears staticly in a lazy manor.
        private async Task<List<Camera>> Fetch()
        {
            if(_cameras == null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add(_settings.RoadwaysKeyName,_settings.RoadwaysKeyValue);
                    using(var  response = await client.GetAsync(_settings.RoadwaysUrl))
                    {
                        using(var content = response.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            if(data != null)
                            {
                                var datas = JArray.Parse(data);
                                _cameras = datas.ToObject<List<RoadwayCamera>>()
                                                .Select( x => new Camera() {
                                                    Id = x.id,
                                                    ThumbnailUrl = x.thumbnailUrl,
                                                    Title = x.title,
                                                    Jurisdiction =  x.jurisdiction,
                                                    Route = x.route,
                                                    HttpVideoUrl = x.httpVideoUrl
                                                })
                                                .Where(x => x.Id != null && string.IsNullOrEmpty(x.Title) == false)
                                                .ToList();
                            }
                        }
                    }
                }
            }
            return _cameras;
        }
       /// returns a set of Cameras based on search parameters.
        public List<Camera> Fetch(string search = null)
        {
            Func<Camera,bool> searchMatch = (camera) => string.IsNullOrEmpty(search) 
                                                            || (camera.Jurisdiction != null && camera.Jurisdiction.IndexOf(search,0,StringComparison.CurrentCultureIgnoreCase) >= 0)
                                                            || (camera.Route != null && camera.Route.IndexOf(search,0,StringComparison.CurrentCultureIgnoreCase) >= 0);
            return Fetch().Result
                          .Where(searchMatch)
                          .ToList();
        }
        /// Fetch a camera by id
        public Camera Fetch(int cameraId)
        {
            return Fetch().Result
                    .FirstOrDefault(x => x.Id == cameraId);
        }
        /// shortcut to find if restuls contains a camera
        public bool Contains(int cameraId)
        {
            return Fetch().Result.Any(x => x.Id == cameraId);
        }
        /// return distinct set of Jurisdictions.
        public List<string> Keywords()
        {
            var retVal =  Fetch().Result
                          .Select(x => x.Jurisdiction)
                          .Where(x => !string.IsNullOrEmpty(x))
                          .Distinct()
                          .OrderBy(x => x)
                          .ToList();
            var r =  Fetch().Result
                          .Select(x => x.Route)
                          .Where(x => !string.IsNullOrEmpty(x))
                          .Distinct()
                          .OrderBy(x => x)
                          .ToList();
            retVal.AddRange(r);
            retVal.Sort();
            return retVal;
        }
    }
}