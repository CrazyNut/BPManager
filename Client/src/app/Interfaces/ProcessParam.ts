import { DTOStatus } from "../Enums/DTOStatus";

export interface ProcessParam
{
    id: number;
    name: string;
    code: string;
    paramRouteType: number;
    paramType: number;
    condition: string;
    stringParam: string;
    intParam: number;
    doubleParam: number;
    boolParam: boolean;
    status: DTOStatus;
}