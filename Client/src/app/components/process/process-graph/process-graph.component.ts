/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
import {  AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import panzoom, { PanZoom } from 'panzoom';
import { LineBorderData } from 'src/app/Interfaces/LineBorderData';
import { ProcessData } from 'src/app/Interfaces/ProcessData';
import { ProcessElementActionsData } from 'src/app/Interfaces/ProcessElementActionsData';
import { Process } from 'src/app/Shared/Process';
declare var LeaderLine: any;

@Component({
  selector: 'app-process-graph',
  templateUrl: './process-graph.component.html',
  styleUrls: ['./process-graph.component.css']
})
export class ProcessGraphComponent implements AfterViewInit, OnInit {
  @Input() processData: ProcessData;
  @ViewChild("viewContainerRef", { read: ViewContainerRef }) vcr!: ViewContainerRef;
  @ViewChild('processPanel', { read: ElementRef }) processPanel: ElementRef;
  @ViewChild('processLines', { read: ElementRef }) processLines: ElementRef;
  @ViewChild('processPanelBorder', { read: ElementRef }) processPanelBorder: ElementRef;
  @ViewChild('LinePrototypeEnd', { read: ElementRef }) linePrototypeEnd: ElementRef;
  @ViewChild('lineStart', { read: ElementRef }) lineStart: ElementRef;
  @ViewChild('lineEnd', { read: ElementRef }) lineEnd: ElementRef;

  
  processLinesPos: {x: number, y: number} = {x: 0, y: 0}
  process: Process;

  formdata: any;
  panzoomInstance: PanZoom;
  draggableSpeedK: number = 1;
  panzoomMatix: DOMMatrix;
  lineBorderData: LineBorderData=  
  {
    ActiveElement: null,
    Position: {x: 0, y: 0},
    Size: {x: 0, y: 0},
    Element: null
  };

  processElementActionsData: ProcessElementActionsData =  
  {
    ActiveElement: null,
    Position: {x: 500, y: 0},
    Type: null,
    ActiveElementType: null
  };

  linePrototypeData:
  {
    position: {x: number, y: number},
    offset: {x: number, y: number},
    StartElement: string,
    EndElement: string,
    active: boolean,
    line: any,
    existing: {start: string, end: string}
  } =  {
    position :  {x : 0,y : 0},
    offset: {x : 0, y : 0},
    StartElement : null,
    EndElement : null,
    active : false,
    line: null,
    existing : null
  }

  lineStartData: {
    Position : {x: number, y: number},
    ActiveElement: string
  } =
  {
    Position : {x: 0, y: 0},
    ActiveElement: null
  } 
  lineEndData: {
    Position : {x: number, y: number},
    ActiveElement: string
  } =
  {
    Position : {x: 0, y: 0},
    ActiveElement: null
  } 
  
  constructor() { }

  ngOnInit(): void {
    this.process = new Process(this.processData, this);
    this.formdata = new FormGroup({ 
        first: new FormControl("Draggable_0"),
        second: new FormControl("Draggable_1"),
    }); 
    this.process.DrawElements();
    this.processLinesPos.x =  -document.getElementById('processLines').getBoundingClientRect().left;
    this.processLinesPos.y = -document.getElementById('processLines').getBoundingClientRect().top;
  }

  ngAfterViewInit() {
   
    let inst = this;
    this.panzoomInstance = panzoom(this.processPanel.nativeElement, {
      
      beforeMouseDown: function(e) {
      
      if((e.target as Element).id === "processPanel" || (e.target as Element).id === "processPanelBorder")
      {
        inst.draggableClicked(null);
        return false;
      }
        return true;
      },
      transformOrigin: {x: 0.5, y: 0.5}
    });
  
    this.panzoomInstance.on('transform', (e : Event) => {
      this.calcZoom();
      this.drawAllTheLines();  
      this.calcLineBorders();    
      if(this.processElementActionsData.Type === "Element")
      {
        this.calcProcessElementActionsPos("Element");
      }
    });

    this.process.DrawLines();
  }

  lineEndClicked(event: MouseEvent){
    this.beginProtoExistingLine("end");
  }

  lineStartClicked(event: MouseEvent){
    this.beginProtoExistingLine("start");
  }

  beginProtoExistingLine(point: string){
    this.linePrototypeData.offset.x = this.processPanelBorder.nativeElement.getBoundingClientRect().x;
    this.linePrototypeData.offset.y = this.processPanelBorder.nativeElement.getBoundingClientRect().y;
    this.linePrototypeData.active = true;
    this.linePrototypeData.StartElement = this.lineStartData.ActiveElement;
    this.linePrototypeData.existing = {start: this.lineStartData.ActiveElement, end: this.lineEndData.ActiveElement}
    this.processElementActionsData.ActiveElement = null;
    this.process.DeleteLine(this.lineBorderData.ActiveElement)
    this.lineBorderData.ActiveElement = null;
    this.lineStartData.ActiveElement = null;
    this.lineEndData.ActiveElement = null;
    this.createProtoLine();
  }

  contextmenuA(event: MouseEvent){
    event.preventDefault();
  }

  pointerdownA(event: MouseEvent){
    if(this.linePrototypeData.active && event.button === 2){
      this.linePrototypeData.active = false;
      this.linePrototypeData.line.remove();
      if(this.linePrototypeData.existing){
        this.process.createLine(this.linePrototypeData.existing.start, this.linePrototypeData.existing.end)
      }
      this.linePrototypeData.line = null;
    }
  }

  pointerenterA(event: MouseEvent){
    this.linePrototypeData.offset.x = this.processPanelBorder.nativeElement.getBoundingClientRect().x;
    this.linePrototypeData.offset.y = this.processPanelBorder.nativeElement.getBoundingClientRect().y;
  }

  pointermoveA(event: MouseEvent){
    if(this.linePrototypeData.active){
      this.linePrototypeData.position.x = (event.clientX - this.linePrototypeData.offset.x - this.panzoomMatix.e) / this.draggableSpeedK;
      this.linePrototypeData.position.y = (event.clientY - this.linePrototypeData.offset.y - this.panzoomMatix.f) / this.draggableSpeedK;
      this.linePrototypeData.line.position();
    }
  }

  draggableClicked(Code: string | null){
    if(this.linePrototypeData.active && !this.linePrototypeData.EndElement.includes("StartProcessElement")){

      if(this.linePrototypeData.EndElement === Code && 
          !this.process.lines.has(`${this.linePrototypeData.StartElement}_${this.linePrototypeData.EndElement}`)){
        this.process.createLine(this.linePrototypeData.StartElement,this.linePrototypeData.EndElement);
        this.linePrototypeData.line.remove();
        this.linePrototypeData = 
        {
          position :  {x : 0,y : 0},
          offset: {x : 0, y : 0},
          StartElement : null,
          EndElement : null,
          active : false,
          line: null,
          existing: null
        };
      }

      return;
    }
    this.lineBorderData.ActiveElement = null;
    this.lineStartData.ActiveElement = null;
    this.lineEndData.ActiveElement = null;
    if(Code){
      this.processElementActionsData.ActiveElement = Code;
      this.processElementActionsData.ActiveElementType = this.process.elementsData.find(el => el.Code === Code).Type;
    }else{
      this.processElementActionsData.ActiveElement = null;
      this.processElementActionsData.ActiveElementType = null;
    }
    this.calcProcessElementActionsPos("Element");
  }

  calcProcessElementActionsPos(type: string, size: {x: number, y: number} = null){
    this.processElementActionsData.Type = type;
    switch (type) {
      case "Element":
        if(this.processElementActionsData.ActiveElement){
          let elPos = this.process.elementsData.find(p => p.Code === this.processElementActionsData.ActiveElement).Position;
          this.processElementActionsData.Position.x = (elPos.x + 64) * this.panzoomMatix.a + this.panzoomMatix.e;
          this.processElementActionsData.Position.y = elPos.y * this.panzoomMatix.a + this.panzoomMatix.f;
        }
        break;
      case "Line":
        if(this.lineBorderData.ActiveElement){
          this.processElementActionsData.Position.x = (this.lineBorderData.Position.x+ size.x);
          this.processElementActionsData.Position.y = this.lineBorderData.Position.y ;
        }
        break;
    
      default:
        break;
    }
    
  }


  returnToCenter(){
    this.panzoomInstance.moveTo(0, 0);
    this.panzoomInstance.zoomAbs(0, 0, 1);
  }


  addChild(type: string, icon: string) {
    let position = {
      x: this.processPanelBorder.nativeElement.offsetWidth/2 ,
      y: this.processPanelBorder.nativeElement.offsetHeight/2 
    };
    
    if(this.panzoomMatix)
    {
      position.x = (this.panzoomMatix.e  - this.processPanelBorder.nativeElement.offsetWidth/2) / this.draggableSpeedK * -1;
      position.y = (this.panzoomMatix.f  - this.processPanelBorder.nativeElement.offsetHeight/2) / this.draggableSpeedK * -1;
    }
    this.process.AddChild(type, {
      Code: type,
      Position: position,
      Type: type,
      Icon: icon,
      Cursor: "auto"
    });
   
  }

  setElement(data : {Code: string, Element: ElementRef})
  {
    this.process.setElement(data);
  }

  draggableDropped(Code: string){
    
    if(this.linePrototypeData.active){
      return;
    }
    this.processElementActionsData.ActiveElement = Code;
    this.processElementActionsData.ActiveElementType = this.process.elementsData.find(el => el.Code === Code).Type;
    this.calcProcessElementActionsPos("Element");
    this.process.RecalcLines(Code);
  }

  draggableMoved(Code: string){
    this.draggableDropped(Code);
    this.processElementActionsData.ActiveElement = null;
    this.processElementActionsData.ActiveElementType = null;
  }

  drawAllTheLines()
  {
    this.process.RecalcAllLines();
  }

  calcZoom(){
    var style = window.getComputedStyle(this.processPanel.nativeElement);
    var matrix = new WebKitCSSMatrix(style.webkitTransform);
    this.draggableSpeedK = matrix.a;
    this.panzoomMatix = matrix;
  }

  lineClicked(key: string, element: Element){
    
    this.processElementActionsData.ActiveElement = key;
    this.lineBorderData.ActiveElement = key;
    this.lineBorderData.Element = element;
    this.calcLineBorders();
  }


  get_angle(center : {x: number, y: number}, point: {x: number, y: number}){
    var x = point.x - center.x;
    var y = point.y - center.y;
    if(x==0) return (y>0) ? 180 : 0;
    var a = Math.atan(y/x)*180/Math.PI;
    a = (x > 0) ? a+90 : a+270;
    return a;
    
  };

  getSide(angle: number){
    
    if(angle > 315)
      return "top";
    if(angle > 225)
      return "left";
    if(angle > 135)
      return "bottom";
    if(angle > 45)
      return "right";

    return "top";
  }

  getConnectionSide(center : {x: number, y: number}, point: {x: number, y: number}){
    let angle = this.get_angle(
      center, 
      point
      );
      return this.getSide(angle);
  }

  getCenterPoint(point : {x: number, y: number, width: number, height: number}){
    return {x: point.x + point.width/2, y: point.y + point.height/2}
  }

  getConnectionPoint(center : {x: number, y: number, width: number, height: number}, point: {x: number, y: number, width: number, height: number}){
    
    let elementCenter = this.getCenterPoint(center)
    let lineCenter =  point
    let side = this.getConnectionSide(elementCenter, lineCenter)
    elementCenter.x -= (this.lineEnd.nativeElement.getBoundingClientRect().width/2 + this.processPanelBorder.nativeElement.getBoundingClientRect().left);
    elementCenter.y -= (this.lineEnd.nativeElement.getBoundingClientRect().height/2 + this.processPanelBorder.nativeElement.getBoundingClientRect().top);
    switch (side) {
      case "left":
        elementCenter.x -= center.width/2;
        return elementCenter;
      case "right":
        elementCenter.x += center.width/2;
        return elementCenter;
      case "top":
        elementCenter.y -= center.height/2;
        return elementCenter;
      case "bottom":
        elementCenter.y += center.height/2;
        return elementCenter;
      default:
        return elementCenter;
    }
  }

  calcCenter(point1 : {x: number, y: number}, point2 : {x: number, y: number})
  {
    return {
      x: (point1.x + point2.x)/2,
      y: (point1.y + point2.y)/2
    }
  }

  calcLineBorders()
  {

    if( this.lineBorderData.ActiveElement)
    {
      let points = this.lineBorderData.ActiveElement.split("_")
      let point1Box = this.process.elementsRefs.get(points[0]).nativeElement.getBoundingClientRect();
      let point1 = {
        x : point1Box.left,
        y : point1Box.top,
        width : point1Box.width,
        height : point1Box.height
      };

      let point2Box = this.process.elementsRefs.get(points[1]).nativeElement.getBoundingClientRect();
      let point2 = {
        x : point2Box.left,
        y : point2Box.top,
        width : point2Box.width,
        height : point2Box.height
      };

      let line = this.process.lines.get(this.lineBorderData.ActiveElement).element.getBoundingClientRect();
      let lineCenter = this.calcCenter(point1, point2); 

      let StartPoint = this.getConnectionPoint(point1,
        {x: lineCenter.x, y: lineCenter.y, width: line.width, height: line.height});
      let EndPoint = this.getConnectionPoint(point2,
        {x: lineCenter.x, y: lineCenter.y, width: line.width, height: line.height});
      
      const borderParentBound = this.processPanelBorder.nativeElement.getBoundingClientRect();
      
      this.lineStartData = {
        Position : StartPoint,
        ActiveElement : points[0]
      }

      this.lineEndData = {
        Position: EndPoint,
        ActiveElement: points[1]
      }

      this.lineBorderData.Size = 
      {
        x: this.lineBorderData.Element.clientWidth + 50 * this.panzoomMatix.a,
        y: this.lineBorderData.Element.clientHeight + 50 * this.panzoomMatix.a
      }
      const elementBound = this.lineBorderData.Element.getBoundingClientRect();
      this.lineBorderData.Position = 
      {
        x: +elementBound.left - 25 * this.panzoomMatix.a - borderParentBound.left,
        y: +elementBound.top - 25 * this.panzoomMatix.a - borderParentBound.top
      }
      this.calcProcessElementActionsPos("Line", {x: elementBound.width + borderParentBound.left + 25* this.panzoomMatix.a, y: elementBound.height + borderParentBound.top});
    }
  }

 

  actionClicked(data : {actionType:string, actionName: string})
  {
    switch(data.actionType)
    {
      case "Line":
        this.lineAction(data.actionName);
      break;
      case "Element":
        this.elementAction(data.actionName);
      break;
    }
   
  }

  lineAction(actionName: string){
    switch (actionName) {
      case "delete":
          this.process.DeleteLine(this.lineBorderData.ActiveElement)
          this.processElementActionsData.ActiveElement = null;
          this.processElementActionsData.ActiveElementType = null;
          this.lineBorderData.ActiveElement = null;
          this.lineStartData.ActiveElement = null;
          this.lineEndData.ActiveElement = null;
        break;
      default:
        break;
    }
  }

  elementAction(actionName: string){
    switch (actionName) {
      case "delete":
        this.process.DeleteElement(this.processElementActionsData.ActiveElement)
        this.processElementActionsData.ActiveElement = null;
        break;
      case "createLine":
        this.linePrototypeData.active = true;
        this.linePrototypeData.StartElement = this.processElementActionsData.ActiveElement;
        this.processElementActionsData.ActiveElement = null;
        this.lineStartData.ActiveElement = null;
        this.lineEndData.ActiveElement = null;
        this.createProtoLine();
        break;
      default:
        break;
    }
  }

  hoverRequest(data: { Code: string; action: string; }) {
    if(this.linePrototypeData.active && this.linePrototypeData.StartElement !== data.Code){
      switch (data.action) {
        case "enter":
          if(data.Code.includes("StartProcessElement")){
            this.process.elementsData.find(el => el.Code === data.Code).Cursor = "not-allowed"
          }
          this.linePrototypeData.EndElement = data.Code;
          this.linePrototypeData.line.remove();
          this.linePrototypeData.line = new LeaderLine(
            this.process.elementsRefs.get(this.linePrototypeData.StartElement).nativeElement, 
            this.process.elementsRefs.get(this.linePrototypeData.EndElement).nativeElement, 
            {
              path: "grid",
              color: 'black',
            })
          break;
        case "out":
          this.process.elementsData.find(el => el.Code === data.Code).Cursor = "auto"
          this.linePrototypeData.EndElement = null;
          this.linePrototypeData.line.remove();
          this.createProtoLine();
          break;
      
        default:
          break;
      }
    }
  }

  createProtoLine(){
    this.linePrototypeData.line = new LeaderLine(
      this.process.elementsRefs.get(this.linePrototypeData.StartElement).nativeElement, 
      this.linePrototypeEnd.nativeElement, 
      {
        path: "straight",
        color: 'grey',
        dash: {animation: true}
      })
  }
}
