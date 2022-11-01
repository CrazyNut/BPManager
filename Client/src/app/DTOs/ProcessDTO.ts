import { DTOStatus } from "../Enums/DTOStatus";

    export interface ProcessParamDTO {
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
    export interface ProcessElementDTO {
        id: number;
        name: string;
        code: string;
        processElementInstanseType: string;
        processElementParams: ProcessParamDTO[];
        status: DTOStatus;
    }

    export interface ProcessElementsConnectionDTO {
        id: number;
        outElementId: number;
        inElementId: number;
        isMain: boolean;
        condition: string;
        status: DTOStatus;
    }

    export interface ProcessDTO {
        id: number;
        name: string;
        code: string;
        processParams: ProcessParamDTO[];
        processElements: ProcessElementDTO[];
        processElementsConnections: ProcessElementsConnectionDTO[];
        status: DTOStatus;
    }
