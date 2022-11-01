import { ProcessParam } from "./ProcessParam";

export interface EditableElement{
    name: string,
    code: string,
    params: Array<ProcessParam>,
    paramsCanBeAdded: boolean
}