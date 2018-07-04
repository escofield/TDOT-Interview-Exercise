using IO.Swagger.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
namespace IO.Swagger.Services
{
    /// Contract for IFavorites
   public interface IFavorites
   {
        /// fetch's all favorites by userName
        List<Favorite> Fetch(string userName);
        /// adds a new favorite and returns the entire list.
        (List<Favorite> cameras, bool success) Add(string userName, int cameraId, string title);
        /// remove a favorite by Id
        (List<Favorite> cameras, bool success) Remove(string userName, Guid id);
   }
}