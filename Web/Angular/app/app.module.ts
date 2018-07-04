import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { HttpModule }   from '@angular/http';
import { AppComponent }  from './app.component';
import { RoadwayCameraService } from './RoadwayCamera.Service';

@NgModule({
  imports:      [ BrowserModule
                  ,HttpModule
                  ,FormsModule ],
  declarations: [ AppComponent ],
  bootstrap:    [ AppComponent ],
  providers: [ RoadwayCameraService ]
})
export class AppModule { }