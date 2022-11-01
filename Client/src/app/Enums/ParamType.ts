export enum ProcessParamType
{
    stringParam = 0,
    intParam = 1,
    doubleParam = 2,
    boolParam = 3,
    conditionParam = 4,
}


export const ProcessParamTypeMapping: Record<any, string> = {
    [ProcessParamType.stringParam]: "String",
    [ProcessParamType.intParam]: "Integer",
    [ProcessParamType.doubleParam]: "Double",
    [ProcessParamType.boolParam]: "Boolean",
    [ProcessParamType.conditionParam]: "Condition"
};