import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import {Headers} from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { RoadwayCamera } from './RoadwayCamera';
import { DEFAULT_INTERPOLATION_CONFIG } from '@angular/compiler';

// The roadway Camera service contains all API related functionality.
const Cameras : RoadwayCamera[] = [
      {id: 1, title: 'camera 1', route: 'route spot 1', jurisdiction: 'city 1', thumbnailUrl: '', videoUrl: '', isActive: true},
      {id: 2, title: 'camera 2', route: 'route spot 2', jurisdiction: 'city 2', thumbnailUrl: '', videoUrl: '', isActive: true},
      {id: 3, title: 'camera 3', route: 'route spot 1', jurisdiction: 'city 1', thumbnailUrl: '', videoUrl: '', isActive: true}
    ];

@Injectable()
export class RoadwayCameraService{
  private roadwayServiceUrl: string = 'http://localhost:5000/v1/';
  constructor(private http: Http){
  }
  // getAll roadway Cameras.
  getAll() : Observable<RoadwayCamera[]> {
    let r = this.http
                .get(`${this.roadwayServiceUrl}Camera`, { headers: this.getHeaders() })
                .map(mapRoadwayCameras)
                .catch(handleError);
    return r;
  }

  // filter the roadway cameras by route or jursdiction.
  filter(filter: string) : Observable<RoadwayCamera[]> {
    let r = this.http
                .get(`${this.roadwayServiceUrl}Camera/Filter?search=${filter}`, { headers: this.getHeaders() })
                .map(mapRoadwayCameras)
                .catch(handleError);
    return r;
  }

  // get a single camera by ID
  get(id: number) : RoadwayCamera {
    return this.clone(Cameras.find(p => p.id === id));
  }

  // clones an object.
  private clone(object: any){
    // hack
    return JSON.parse(JSON.stringify(object));
  }

  // set the headers in a single location for re-use.
  private getHeaders(){
    let headers = new Headers();
    headers.append('Accept', 'application/json');
    return headers;
  }
}

// Maps a response to a list of roadwayCamera object
function mapRoadwayCameras(response:Response): RoadwayCamera[]{
  return response.json().map(toRoadwayCamera)
}
// Maps a single item response to a single RoadwayCamera
function toRoadwayCamera(r:any): RoadwayCamera{
  let rc = <RoadwayCamera>({
    id: r.id
    ,title: r.title
    ,route: r.route
    ,jurisdiction: r.jurisdiction
    ,thumbnailUrl: r.thumbnailUrl
    ,videoUrl: r.httpVideoUrl
    ,isActive: r.active
  });
  console.log('Parsed roadwayCamera:', rc);
  return rc;
}

// this could also be a private method of the component class
function handleError (error: any) {
  // log error
  let errorMsg = error.message || `a technical error occured!`
  console.error(errorMsg);

  // throw an application level error
  return Observable.throw(errorMsg);
} 