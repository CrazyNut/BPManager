import { ElementRef } from "@angular/core";
import { DraggableElement } from "../Interfaces/DraggableElement";
import { ProcessData } from "../Interfaces/ProcessData";
import { ProcessAvailibleElement } from "../Interfaces/ProcessAvailibleElement";
declare var LeaderLine: any;

export class Process{
    processData : ProcessData
    elementsRefs: Map<string,ElementRef> = new Map<string,ElementRef>();
    lines: Map<string,{line:any, element: Element}> = new Map<string,any>()
    lineInConnections : Map<string, Array<string>>= new Map<string, Array<string>>()
    lineOutConnections : Map<string, Array<string>>= new Map<string, Array<string>>()
    elementsData: Array<DraggableElement> = new Array<DraggableElement>();
    availibleProcessElements: Array<ProcessAvailibleElement>
    elementTypesId: Map<string, number> = new Map<string,number>();
    parentContext: any;

    linePrototype: any;

    constructor(processData: ProcessData, parentContext: any)
    {
      this.processData = processData;
      this.availibleProcessElements = processData.processAvailibleElements;
      this.parentContext = parentContext;
    }
    
    setElement(data : {Code: string, Element: ElementRef})
    {
      this.elementsRefs.set(data.Code, data.Element);
    }

    DrawElements(){
        if(this.processData.processId)
        {

        }else{
            this.elementsData.push({
                Code: "StartProcessElement1",
                Position: {
                 x: window.document.getElementById('processPanelBorder').offsetWidth/4*1 ,
                 y: window.document.getElementById('processPanelBorder').offsetHeight/2 
                 },
                Type: "StartProcessElement",
                Icon: "bpmn-icon-start-event-none",
                Cursor: "auto"
            });
            this.elementTypesId.set("StartProcessElement",2);
            this.elementsData.push({
            Code: "EndProcessElement1",
            Position: {
                x: window.document.getElementById('processPanelBorder').offsetWidth/4*3 ,
                y: window.document.getElementById('processPanelBorder').offsetHeight/2 
                },
            Type: "EndProcessElement",
            Icon: "bpmn-icon-end-event-none",
            Cursor: "auto"
            });
            this.elementTypesId.set("EndProcessElement",2);
        }
    }

    DrawLines(){
        if(this.processData.processId)
        {

        }else{
            this.createLine("StartProcessElement1", "EndProcessElement1");
        }
    }

    RecalcLines(Code: string) {
        if(this.lineOutConnections.has(Code))
        {
        this.lineOutConnections.get(Code).forEach(element => {
            this.createLine(Code, element);
        });
        }
        if(this.lineInConnections.has(Code))
        {
        this.lineInConnections.get(Code).forEach(element => {
            this.createLine(element,Code);
        });
        }
    }

    RecalcAllLines() {
        this.lines.forEach(element => {
            element.line.position();
          });
    }
    
    AddChild(type:string, elementData: DraggableElement) {
        if(!this.elementTypesId.has(type))
        {
          this.elementTypesId.set(type,1);
        }
        elementData.Code = type + this.elementTypesId.get(type);
        this.elementsData.push(elementData);
        this.elementTypesId.set(type,this.elementTypesId.get(type)+1);
    }

    createLine(Code1: string, Code2: string)
    {
      var key = `${Code1}_${Code2}`;
      if(this.lines.has(key))
      {
        this.lines.get(key).line.position()
      }else
      {
      let line = new LeaderLine(
        window.document.getElementById('StartProcessElement1'), 
        window.document.getElementById('EndProcessElement1'), 
        {
          path: "grid",
          color: 'black',
        }
      );

      let last = Array.from(
        window.document.querySelectorAll('.leader-line')
      ).pop();

      let inst = this;
      this.lines.set(key,
        {line: line, element: last}
      );
      
      last.addEventListener("click", function(e){ 
        e.stopPropagation();
        console.log(e);
        inst.parentContext.lineClicked(key, last);
      }, false);
      this.appendCSSToLine(last)
      }
  
      if(!this.lineOutConnections.has(Code1))
      {
        this.lineOutConnections.set(Code1, new Array<string>());
      }
      if(!this.lineOutConnections.get(Code1).includes(Code2))
      {
        this.lineOutConnections.get(Code1).push(Code2);
      }
  
      if(!this.lineInConnections.has(Code2))
      {
        this.lineInConnections.set(Code2, new Array<string>());
      }
      if(!this.lineInConnections.get(Code2).includes(Code1))
      {
        this.lineInConnections.get(Code2).push(Code1);
      }
    }
   

    appendCSSToLine(element: Element)
    {
        var existingCSS = element.getAttribute("style");
  
          if(existingCSS == undefined) existingCSS = "";
  
          existingCSS += "pointer-events:all!important;"
  
        element.setAttribute("style", existingCSS);
    }

    DeleteLine(code: string)
    {
      let splitted = code.split("_");
      this.lineOutConnections.set(splitted[0], this.lineOutConnections.get(splitted[0]).filter(el => el !== splitted[1]));
      this.lineInConnections.set(splitted[1], this.lineInConnections.get(splitted[1]).filter(el => el !== splitted[0]));
      this.lines.get(code).line.remove();
      this.lines.delete(code);
    }

    DeleteElement(code: string)
    {
      if(this.lineOutConnections.has(code))
      {
        this.lineOutConnections.get(code).forEach(element => {
            var key = `${code}_${element}`;
            this.lines.get(key).line.remove();

            this.lineInConnections.set(element,this.lineInConnections.get(element).filter(el => el !== code));

            this.lines.delete(key);
        });
        this.lineOutConnections.delete(code);
      }
      if(this.lineInConnections.has(code))
      {
        this.lineInConnections.get(code).forEach(element => {
          var key = `${element}_${code}`;
          this.lines.get(key).line.remove();
          
          this.lineOutConnections.set(element,this.lineOutConnections.get(element).filter(el => el !== code));
          this.lines.delete(key);
        });
        this.lineInConnections.delete(code);
      }
      this.elementsData = this.elementsData.filter(el => el.Code !== code);
      this.elementsRefs.delete(code);
    }
}