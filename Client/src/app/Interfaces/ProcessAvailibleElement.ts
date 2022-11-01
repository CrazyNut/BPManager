import { ProcessParam } from "./ProcessParam";

export interface ProcessAvailibleElement
{
    name: string,
    type: string,
    icon: string,
    params: Array<ProcessParam>,
}