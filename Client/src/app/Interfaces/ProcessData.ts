
import { ProcessConnection } from "./ProcessConnection";
import { ProcessAvailibleElement } from "./ProcessAvailibleElement";
import { ProcessParam } from "./ProcessParam";
import { ProcessElement } from "./ProcessElement";
import { DTOStatus } from "../Enums/DTOStatus";

export interface ProcessData{
    processId: number,
    name: string,
    code: string,
    processParams: ProcessParam[],
    processElements: Array<ProcessElement>,
    processElementsConnections: Array<ProcessConnection>,
    status: DTOStatus,
    processAvailibleElements: Array<ProcessAvailibleElement>,
}