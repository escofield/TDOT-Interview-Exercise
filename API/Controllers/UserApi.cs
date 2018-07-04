using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.SwaggerGen.Annotations;
using IO.Swagger.Models;
using IO.Swagger.Services;
using System.Linq.Expressions;

namespace IO.Swagger.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    public class UserApiController : Controller
    { 
        private readonly IRoadwayCameras _roadwaysCameras;
        private readonly IFavorites _favorates;
        /// Constructor
        public UserApiController(IRoadwayCameras roadwaysCameras
                                ,IFavorites favorites){
            _roadwaysCameras = roadwaysCameras;
            _favorates = favorites;
        }

        
        /// <summary>
        /// Add camera to favorites
        /// </summary>
        /// <remarks>No auth, in memmory state favorites.</remarks>
        /// <param name="userName">Username</param>
        /// <param name="cameraId">CameraId</param>
        /// <param name="title">title</param>
        /// <response code="201">successful operation</response>
        /// <response code="410">Camera has been removed or never existed</response>
        [HttpGet]
        [Route("/v1/user/favorite/{userName}")]
        [SwaggerOperation("Favorites")]
        [SwaggerResponse(201, type: typeof(List<Favorite>))]
        [SwaggerResponse(410, type: typeof(List<Favorite>))]
        public virtual IActionResult Favorites([FromRoute] string userName, [FromQuery]int? cameraId = null, [FromQuery]string title = null)
        { 
            List<Favorite> cameras;
            if(cameraId.HasValue){
                var result = _favorates.Add(userName, cameraId.Value, title);
                if(result.success)
                {
                    Response.StatusCode = 201;
                    return new ObjectResult(result.cameras);
                }
                Response.StatusCode = 410;
                cameras = result.cameras;
            }
            else{
                cameras = _favorates.Fetch(userName);
            }
            return new ObjectResult(cameras);
        }
    }
}
