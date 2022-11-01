import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ProcessEditorComponent } from './components/process/process-editor/process-editor.component';
import { ProcessListComponent } from './components/process/process-list/process-list.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: "always",
    canActivate: [],
    children: [
      {path: 'processes', component: ProcessListComponent},
      {path: 'processes/create', component: ProcessEditorComponent},
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
