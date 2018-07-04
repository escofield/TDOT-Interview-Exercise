"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var Observable_1 = require("rxjs/Observable");
var http_2 = require("@angular/http");
require("rxjs/add/operator/map");
require("rxjs/add/operator/catch");
require("rxjs/add/observable/throw");
// The roadway Camera service contains all API related functionality.
var Cameras = [
    { id: 1, title: 'camera 1', route: 'route spot 1', jurisdiction: 'city 1', thumbnailUrl: '', videoUrl: '', isActive: true },
    { id: 2, title: 'camera 2', route: 'route spot 2', jurisdiction: 'city 2', thumbnailUrl: '', videoUrl: '', isActive: true },
    { id: 3, title: 'camera 3', route: 'route spot 1', jurisdiction: 'city 1', thumbnailUrl: '', videoUrl: '', isActive: true }
];
var RoadwayCameraService = (function () {
    function RoadwayCameraService(http) {
        this.http = http;
        this.roadwayServiceUrl = 'http://localhost:5000/v1/';
    }
    // getAll roadway Cameras.
    RoadwayCameraService.prototype.getAll = function () {
        var r = this.http
            .get(this.roadwayServiceUrl + "Camera", { headers: this.getHeaders() })
            .map(mapRoadwayCameras)
            .catch(handleError);
        return r;
    };
    // filter the roadway cameras by route or jursdiction.
    RoadwayCameraService.prototype.filter = function (filter) {
        var r = this.http
            .get(this.roadwayServiceUrl + "Camera/Filter?search=" + filter, { headers: this.getHeaders() })
            .map(mapRoadwayCameras)
            .catch(handleError);
        return r;
    };
    // get a single camera by ID
    RoadwayCameraService.prototype.get = function (id) {
        return this.clone(Cameras.find(function (p) { return p.id === id; }));
    };
    // clones an object.
    RoadwayCameraService.prototype.clone = function (object) {
        // hack
        return JSON.parse(JSON.stringify(object));
    };
    // set the headers in a single location for re-use.
    RoadwayCameraService.prototype.getHeaders = function () {
        var headers = new http_2.Headers();
        headers.append('Accept', 'application/json');
        return headers;
    };
    RoadwayCameraService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http])
    ], RoadwayCameraService);
    return RoadwayCameraService;
}());
exports.RoadwayCameraService = RoadwayCameraService;
// Maps a response to a list of roadwayCamera object
function mapRoadwayCameras(response) {
    return response.json().map(toRoadwayCamera);
}
// Maps a single item response to a single RoadwayCamera
function toRoadwayCamera(r) {
    var rc = ({
        id: r.id,
        title: r.title,
        route: r.route,
        jurisdiction: r.jurisdiction,
        thumbnailUrl: r.thumbnailUrl,
        videoUrl: r.httpVideoUrl,
        isActive: r.active
    });
    console.log('Parsed roadwayCamera:', rc);
    return rc;
}
// this could also be a private method of the component class
function handleError(error) {
    // log error
    var errorMsg = error.message || "a technical error occured!";
    console.error(errorMsg);
    // throw an application level error
    return Observable_1.Observable.throw(errorMsg);
}
//# sourceMappingURL=RoadwayCamera.Service.js.map