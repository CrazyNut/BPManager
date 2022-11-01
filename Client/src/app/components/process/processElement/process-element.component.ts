import { CdkDragEnd, CdkDragMove } from '@angular/cdk/drag-drop';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { DraggableElement } from 'src/app/Interfaces/DraggableElement';
@Component({
  selector: 'app-process-element',
  templateUrl: './process-element.component.html',
  styleUrls: ['./process-element.component.css']
})
export class ProcessElementComponent implements  AfterViewInit {
  index : number = 0;
  @Input() data: DraggableElement;
  @Output() dataChange = new EventEmitter<DraggableElement>();
  @Input() scrollK: number;
  @Output() moveRequest = new EventEmitter<string>();
  @Output() droppedRequest = new EventEmitter<string>();
  @Output() hoverRequest = new EventEmitter<{Code: string, action: string}>();
  @Output() clickRequest = new EventEmitter<string>();
  @Output() elementOut = new EventEmitter<{Code: string, Element: ElementRef}>();
  @ViewChild('draggable', { read: ElementRef }) element: ElementRef;

  constructor() { }


  ngAfterViewInit(): void {
    this.elementOut.emit({Code: this.data.Code, Element: this.element});
  }
  positionBefore: {x: number, y: number} = {x:0,y:0};
  moving: boolean = false;
  pointerdownA(event: PointerEvent)
  {
    event.stopImmediatePropagation();
    this.moving = true;
    this.positionBefore.x = event.screenX;
    this.positionBefore.y = event.screenY;
    this.clickRequest.emit(this.data.Code);
  }

  pointermoveA(event: PointerEvent){
    if(this.moving)
    {
      event.stopImmediatePropagation();
      this.data.Position.x += (event.screenX - this.positionBefore.x) / this.scrollK;
      this.data.Position.y += (event.screenY - this.positionBefore.y) / this.scrollK;
      this.positionBefore.x = event.screenX;
      this.positionBefore.y = event.screenY;
      this.moveRequest.emit(this.data.Code);
      this.dataChange.emit(this.data);
    }
  }
  pointerenterA(event: PointerEvent){
    this.hoverRequest.emit({Code: this.data.Code, action: "enter"})
  }
  pointeroutA(event: PointerEvent){
    this.hoverRequest.emit({Code: this.data.Code, action: "out"})
    if(this.moving)
    {
      event.stopImmediatePropagation();
      this.moving = false;
      this.data.Position.x += (event.screenX - this.positionBefore.x) / this.scrollK;
      this.data.Position.y += (event.screenY - this.positionBefore.y) / this.scrollK;
      this.droppedRequest.emit(this.data.Code);
      this.dataChange.emit(this.data);
    }
  }

  pointerupA(event: PointerEvent){
    if(this.moving)
    {
      event.stopImmediatePropagation();
      this.moving = false;
      this.data.Position.x += (event.screenX - this.positionBefore.x) / this.scrollK;
      this.data.Position.y += (event.screenY - this.positionBefore.y) / this.scrollK;
      this.droppedRequest.emit(this.data.Code);
      this.dataChange.emit(this.data);
    }
  }
}
