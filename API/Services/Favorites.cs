using IO.Swagger.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using IO.Swagger.Helpers;

namespace IO.Swagger.Services
{
    /// "Repository" for Favorites
    public class Favorites : IFavorites
    {
        private readonly Dictionary<string, Dictionary<Guid, Favorite>> _favorites;
        private readonly IRoadwayCameras _roadwaysCameras;
        /// Constructor
        public Favorites(IRoadwayCameras roadwaysCameras)
        {
            _favorites = new Dictionary<string, Dictionary<Guid, Favorite>>();
            _roadwaysCameras = roadwaysCameras;
        }
        /// fetch's all favorites by userName
        public List<Favorite> Fetch(string userName)
        {
            if(_favorites.ContainsKey(userName)){
                return _favorites[userName].Values.ToList();
            }else{
                return new List<Favorite>();
            }
        }
        /// adds a new favorite and returns the entire list.
        public (List<Favorite> cameras, bool success) Add(string userName, int cameraId, string title)
        {
            if(!_roadwaysCameras.Contains(cameraId)) return (cameras: Fetch(userName), success: false);
            
            if(!_favorites.ContainsKey(userName)) _favorites.Add(userName, new Dictionary<Guid, Favorite>());

            var camera = _roadwaysCameras.Fetch(cameraId);

            var guid = Guid.NewGuid();
            _favorites[userName].Add(guid, new Favorite(){
                                        Id = guid
                                        ,Camera = camera
                                        ,Title = title.NullIf() ?? camera.Title
                                        });
            return (cameras: Fetch(userName), success: true); 
        }
        /// remove a favorite by Id
        public (List<Favorite> cameras, bool success) Remove(string userName, Guid id)
        {
            if(_favorites.ContainsKey(userName) == false 
                || !_favorites[userName].ContainsKey(id)) {
                return (cameras: Fetch(userName), success:false);
            }
            _favorites[userName].Remove(id);
            return (cameras: Fetch(userName), success:true);
        }
    }
}

