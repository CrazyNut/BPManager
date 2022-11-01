import { DTOStatus } from "../Enums/DTOStatus";
import { ProcessParam } from "./ProcessParam";

export interface ProcessElement {
    id: number;
    name: string;
    code: string;
    processElementInstanseType: string;
    processElementParams: ProcessParam[];
    status: DTOStatus;
}