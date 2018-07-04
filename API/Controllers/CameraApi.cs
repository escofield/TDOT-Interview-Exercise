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
    public class CameraApiController : Controller
    { 
        private readonly IRoadwayCameras _roadwaysCameras;
        /// Constructor
        public CameraApiController(IRoadwayCameras roadwaysCameras){
            _roadwaysCameras = roadwaysCameras;
        }

        /// <summary>
        /// Get a list of cameras
        /// </summary>
        /// <response code="200">successful operation</response>
        /// <response code="404">No results found</response>
        [HttpGet]
        [Route("/v1/Camera")]
        [SwaggerOperation("GetCameras")]
        [SwaggerResponse(200, type: typeof(List<Camera>))]
        public IActionResult GetCameras()
        { 
            var cameras = _roadwaysCameras.Fetch(null);
            if (cameras.Count <= 0) return StatusCode(404);
            return new JsonResult(cameras);
        }

        /// <summary>
        /// Get the camera info for this id
        /// </summary>
        /// <param name="cameraId">Camera ID</param>
        /// <response code="200">successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpGet]
        [Route("/v1/Camera/{cameraId}")]
        [SwaggerOperation("GetCameraId")]
        [SwaggerResponse(200, type: typeof(Camera))]
        public IActionResult GetCameraId([FromRoute]int cameraId)
        { 
            var camera = _roadwaysCameras.Fetch(cameraId);
            if(camera == null) return StatusCode(405);
            return new ObjectResult(camera);
        }

        /// <summary>
        /// Get a list of cameras
        /// </summary>
        /// <param name="search">Search camera query</param>
        /// <response code="200">successful operation</response>
        /// <response code="404">No results found</response>
        [HttpGet]
        [Route("/v1/Camera/Filter")]
        [SwaggerOperation("GetCameras")]
        [SwaggerResponse(200, type: typeof(List<Camera>))]
        public IActionResult GetCameras([FromQuery]string search = null)
        { 
            var cameras = _roadwaysCameras.Fetch(search);
            if (cameras.Count <= 0) return StatusCode(404);
            return new JsonResult(cameras);
        }

        /// <summary>
        /// Returns avaliable GetKeywords used in filter
        /// </summary>
        /// <response code="200">successful operation</response>
        [HttpGet]
        [Route("/v1/Camera/Keywords")]
        [SwaggerOperation("GetKeywords")]
        [SwaggerResponse(200, type: typeof(List<string>))]
        public IActionResult GetKeywords()
        { 
            return new ObjectResult(_roadwaysCameras.Keywords());
        }
    }
}
