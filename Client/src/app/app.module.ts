import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {DragDropModule} from '@angular/cdk/drag-drop';
import { ProcessElementComponent } from './components/process/processElement/process-element.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ProcessGraphComponent } from './components/process/process-graph/process-graph.component';
import { HomeComponent } from './components/home/home.component';
import { ProcessEditorComponent } from './components/process/process-editor/process-editor.component';
import { ProcessListComponent } from './components/process/process-list/process-list.component';
import { HttpClientModule } from '@angular/common/http';
import { ProcessElementActionsComponent } from './components/process/process-element-actions/process-element-actions.component';

@NgModule({
  declarations: [
    AppComponent,
    ProcessElementComponent,
    ProcessGraphComponent,
    HomeComponent,
    ProcessEditorComponent,
    ProcessListComponent,
    ProcessElementActionsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    DragDropModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
