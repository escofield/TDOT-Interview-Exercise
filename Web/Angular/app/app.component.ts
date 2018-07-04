import { Component, OnInit} from '@angular/core';
import { RoadwayCameraService } from './RoadwayCamera.Service';
import { RoadwayCamera } from './RoadwayCamera';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import {Headers} from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Component({
  selector: 'my-app'
  ,templateUrl: 'app/app.component.html'
  
})
export class AppComponent  implements OnInit{
  roadways: RoadwayCamera[];
  favorites: RoadwayCamera[];
  keywords: string[];
  private filter: string = '';
  selectedRoadways: RoadwayCamera;

  constructor(private roadwayCameraService: RoadwayCameraService
             ,private http: Http) {
    // create an empty selectedRoadways.
    this.selectedRoadways =  { id: 0
                               ,title: ''
                               ,route: ''
                               ,jurisdiction: ''
                               ,thumbnailUrl: ''
                               ,videoUrl: ''
                               ,isActive: false };
  }

  // initialize our component
  ngOnInit() {
    // keywords intent was to be utilized in an autocomplete text field for search but TODO.
    this.roadwayCameraService.getAll().subscribe(p => this.roadways = p);
        let r = this.http
                .get(`http://localhost:5000/v1/Camera/Keywords`, { headers: this.getHeaders() })
                .catch(handleError)
                .subscribe(response => 
                      this.keywords = response.json()
                      );
      // initialize the favorites.  Hardcoded user "TestUser"
      r = this.http
          .get(`http://localhost:5000/v1/user/favorite/testUser`, { headers: this.getHeaders() })
          .catch(handleError)
          .map(mapFavorites)
          .subscribe(favs => this.favorites = favs);
  }

  // handles the text box change event
  // Only fetches once 3 characters have been entered.
  onFilter(newValue){
    var i = newValue;
    if(newValue.length > 3)
    {
      this.roadwayCameraService.filter(newValue).subscribe(p => this.roadways = p);
    }
    if(newValue.length == 0){
      this.roadwayCameraService.getAll().subscribe(p => this.roadways = p);
    }
  }

  // handle selection of the roadway Camera Cards in either the Main content area or the favorites.
  onSelect(event, roadwayCamera)
  {
    this.selectedRoadways = roadwayCamera;
  }

  // event when the users selects a camera as a favorite.
  onFavorite(event)
  {
      let r = this.http
          .get(`http://localhost:5000/v1/user/favorite/testUser?cameraId=${this.selectedRoadways.id}`, { headers: this.getHeaders() })
          .catch(handleError)
          .map(mapFavorites)
          .subscribe(favs => this.favorites = favs);
  }
  // set the headers in a single location for re-use. TODO: put this in a place where I don't have to copy it around.
  private getHeaders(){
    let headers = new Headers();
    headers.append('Accept', 'application/json');
    return headers;
  }
}
// Mapping of a response to a roadwayCamera - again copied and should be more modular.
function mapFavorites(response:Response): RoadwayCamera[]{
  return response.json().map(toRoadwayCamera)
}
// copied as well.
function toRoadwayCamera(r:any): RoadwayCamera{
  let rc = <RoadwayCamera>({
    id: r.camera.id
    ,title: r.camera.title
    ,route: r.camera.route
    ,jurisdiction: r.camera.jurisdiction
    ,thumbnailUrl: r.camera.thumbnailUrl
    ,videoUrl: r.camera.httpVideoUrl
    ,isActive: r.camera.active
  });
  console.log('favorites roadwayCamera:', rc);
  return rc;
}
// more copy
function handleError (error: any) {
  // log error
  let errorMsg = error.message || `A technical error occured!`
  console.error(errorMsg);

  // throw an application level error
  return Observable.throw(errorMsg);
} 