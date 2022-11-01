import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { ProcessAvailibleElement } from 'src/app/Interfaces/ProcessAvailibleElement';
import { ProcessParam } from 'src/app/Interfaces/ProcessParam';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProcessElementsService {
  baseUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  GetAvailibleProcessElements() 
  {
    // let array : Array<ProcessElement> =
    // [
    //   {
    //     Name: "StartProcessElement", 
    //     Type: "StartProcessElement", 
    //     Icon: "bpmn-icon-start-event-none", 
    //     Params: new Array<ProcessParam>()
    //   },
    //   {
    //     Name: "EndProcessElement", 
    //     Type: "EndProcessElement", 
    //     Icon: "bpmn-icon-end-event-none", 
    //     Params: new Array<ProcessParam>()
    //   },
    //   {
    //     Name: "SumProcessElement", 
    //     Type: "SumProcessElement", 
    //     Icon: "bpmn-icon-intermediate-event-catch-parallel-multiple", 
    //     Params: [
    //       {
    //           "id": 0,
    //           "name": "Число 1",
    //           "code": "FirstValue",
    //           "paramRouteType": 0,
    //           "paramType": 1,
    //           "condition": "",
    //           "stringParam": "",
    //           "intParam": 0,
    //           "doubleParam": 0,
    //           "boolParam": false,
    //           "status": 0
    //       },
    //       {
    //           "id": 0,
    //           "name": "Число 2",
    //           "code": "SecondValue",
    //           "paramRouteType": 0,
    //           "paramType": 1,
    //           "condition": "",
    //           "stringParam": "",
    //           "intParam": 0,
    //           "doubleParam": 0,
    //           "boolParam": false,
    //           "status": 0
    //       }
    //     ]
    //   },
    // ]  
    // return of(array);
    return this.httpClient.get<Array<ProcessAvailibleElement>>(`${this.baseUrl}/ProcessRedactor/types`);
  }
}
