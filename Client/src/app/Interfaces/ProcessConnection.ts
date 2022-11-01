import { DTOStatus } from "../Enums/DTOStatus";

export interface ProcessConnection{
    startElement: string,
    endElement: string,
    isMain: boolean,
    status: DTOStatus,
    condition: string,
}