/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProcessAction } from 'src/app/Interfaces/ProcessAction';
import { ProcessElementActionsData } from 'src/app/Interfaces/ProcessElementActionsData';

@Component({
  selector: 'app-process-element-actions',
  templateUrl: './process-element-actions.component.html',
  styleUrls: ['./process-element-actions.component.css']
})
export class ProcessElementActionsComponent implements OnInit {

  @Input() data: ProcessElementActionsData;
  @Output() actionClicked  = new EventEmitter<{actionType:string, actionName: string}>();
  availibleElementActions: Array<ProcessAction> = [
    {
      actionName: "delete",
      Icon: "bpmn-icon-trash",
      disableFor : new Array<string>()
    },
    {
      actionName: "createLine",
      Icon: "bpmn-icon-connection",
      disableFor: ["EndProcessElement"]
    }
  ] 
  constructor() { }

  ngOnInit(): void {
  }


  onActionClicked(actionType: string, actionName: string){
    this.actionClicked.emit({actionType: actionType, actionName: actionName});
  }

}
