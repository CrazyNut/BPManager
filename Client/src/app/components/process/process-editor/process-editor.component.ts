/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { DTOStatus } from 'src/app/Enums/DTOStatus';
import { ProcessParamType, ProcessParamTypeMapping } from 'src/app/Enums/ParamType';
import { EditableElement } from 'src/app/Interfaces/EditableElement';
import { ProcessData } from 'src/app/Interfaces/ProcessData';
import { ProcessParam } from 'src/app/Interfaces/ProcessParam';
import { ProcessElementsService } from 'src/app/Services/ProcessRedactor/process-elements.service';

@Component({
  selector: 'app-process-editor',
  templateUrl: './process-editor.component.html',
  styleUrls: ['./process-editor.component.css']
})
export class ProcessEditorComponent implements OnInit {
  processData: ProcessData;
  editableElement: EditableElement;

  elementForm : FormGroup;
  choosingParamType: boolean;
  
  ProcessParamTypeMapping = ProcessParamTypeMapping;
  paramTypesNames = Object.values(ProcessParamType).filter((v) => isNaN(Number(v)));
  paramTypesValues = Object.values(ProcessParamType).filter((v) => !isNaN(Number(v)));
  constructor(private fb: FormBuilder, private processElementsService: ProcessElementsService) { }

  ngOnInit(): void {
    this.processElementsService.GetAvailibleProcessElements().subscribe({
      next: list => {
        console.log("ASD")
        this.processData = {
          processId : null,
          name: $localize`Process`,
          code: "Process", 
          processParams: null,
          processElements: null,
          processAvailibleElements: list,
          processElementsConnections: null,
          status: DTOStatus.Created
        };
        this.editableElement = {
          paramsCanBeAdded : true,
          name: this.processData.name,
          code: this.processData.code,
          params: new Array<ProcessParam>
        }

        this.instForm();
      }
    });
  }

  instForm(){
    this.elementForm = this.fb.group({
      name: [this.processData.name],
      code: [this.processData.code], 
      params: this.fb.array([]),
      paramtype: new FormControl(''),
    });
    this.elementForm.get('paramtype').valueChanges.subscribe(val => {
      if(this.choosingParamType){
        this.addParam(val);
      }
    });
  }

  get params() {
    return this.elementForm.controls["params"] as FormArray;
  }
  chooseParam(){
    this.choosingParamType = true;
  }
  addParam(val: any){
    this.choosingParamType = false;
    console.log(val);
    this.elementForm.patchValue({
      paramtype: '',
    });
    this.params.push(this.fb.group({
      type: new FormControl(this.paramTypesValues[val]),
      name: new FormControl(ProcessParamTypeMapping[val] + " 1"),
      code: new FormControl(ProcessParamTypeMapping[val] + " 1"),
    }));
  }

}
