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
var RoadwayCamera_Service_1 = require("./RoadwayCamera.Service");
var http_1 = require("@angular/http");
var Observable_1 = require("rxjs/Observable");
var http_2 = require("@angular/http");
require("rxjs/add/operator/map");
require("rxjs/add/operator/catch");
require("rxjs/add/observable/throw");
var AppComponent = (function () {
    function AppComponent(roadwayCameraService, http) {
        this.roadwayCameraService = roadwayCameraService;
        this.http = http;
        this.filter = '';
        // create an empty selectedRoadways.
        this.selectedRoadways = { id: 0,
            title: '',
            route: '',
            jurisdiction: '',
            thumbnailUrl: '',
            videoUrl: '',
            isActive: false };
    }
    // initialize our component
    AppComponent.prototype.ngOnInit = function () {
        var _this = this;
        // keywords intent was to be utilized in an autocomplete text field for search but TODO.
        this.roadwayCameraService.getAll().subscribe(function (p) { return _this.roadways = p; });
        var r = this.http
            .get("http://localhost:5000/v1/Camera/Keywords", { headers: this.getHeaders() })
            .catch(handleError)
            .subscribe(function (response) {
            return _this.keywords = response.json();
        });
        // initialize the favorites.  Hardcoded user "TestUser"
        r = this.http
            .get("http://localhost:5000/v1/user/favorite/testUser", { headers: this.getHeaders() })
            .catch(handleError)
            .map(mapFavorites)
            .subscribe(function (favs) { return _this.favorites = favs; });
    };
    // handles the text box change event
    // Only fetches once 3 characters have been entered.
    AppComponent.prototype.onFilter = function (newValue) {
        var _this = this;
        var i = newValue;
        if (newValue.length > 3) {
            this.roadwayCameraService.filter(newValue).subscribe(function (p) { return _this.roadways = p; });
        }
        if (newValue.length == 0) {
            this.roadwayCameraService.getAll().subscribe(function (p) { return _this.roadways = p; });
        }
    };
    // handle selection of the roadway Camera Cards in either the Main content area or the favorites.
    AppComponent.prototype.onSelect = function (event, roadwayCamera) {
        this.selectedRoadways = roadwayCamera;
    };
    // event when the users selects a camera as a favorite.
    AppComponent.prototype.onFavorite = function (event) {
        var _this = this;
        var r = this.http
            .get("http://localhost:5000/v1/user/favorite/testUser?cameraId=" + this.selectedRoadways.id, { headers: this.getHeaders() })
            .catch(handleError)
            .map(mapFavorites)
            .subscribe(function (favs) { return _this.favorites = favs; });
    };
    // set the headers in a single location for re-use. TODO: put this in a place where I don't have to copy it around.
    AppComponent.prototype.getHeaders = function () {
        var headers = new http_2.Headers();
        headers.append('Accept', 'application/json');
        return headers;
    };
    AppComponent = __decorate([
        core_1.Component({
            selector: 'my-app',
            templateUrl: 'app/app.component.html'
        }),
        __metadata("design:paramtypes", [RoadwayCamera_Service_1.RoadwayCameraService,
            http_1.Http])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
// Mapping of a response to a roadwayCamera - again copied and should be more modular.
function mapFavorites(response) {
    return response.json().map(toRoadwayCamera);
}
// copied as well.
function toRoadwayCamera(r) {
    var rc = ({
        id: r.camera.id,
        title: r.camera.title,
        route: r.camera.route,
        jurisdiction: r.camera.jurisdiction,
        thumbnailUrl: r.camera.thumbnailUrl,
        videoUrl: r.camera.httpVideoUrl,
        isActive: r.camera.active
    });
    console.log('favorites roadwayCamera:', rc);
    return rc;
}
// more copy
function handleError(error) {
    // log error
    var errorMsg = error.message || "A technical error occured!";
    console.error(errorMsg);
    // throw an application level error
    return Observable_1.Observable.throw(errorMsg);
}
//# sourceMappingURL=app.component.js.map