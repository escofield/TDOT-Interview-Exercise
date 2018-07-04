using IO.Swagger.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace IO.Swagger.Services
{
    /// Contract for IRoadwayCameras
   public interface IRoadwayCameras
   {
        /// fetch
        List<Camera> Fetch(string search = null);
        /// fetch by Id
        Camera Fetch(int cameraId);
        /// shortcut to find if restuls contains a camera
        bool Contains(int cameraId);
        /// return distinct set of Jurisdictions and routes.
        List<string> Keywords();

   }
}