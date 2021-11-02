import { DbType, DataType } from "./Enums"
// interface IParam {
//     Name: string;
//     DataType: DbType;
//     Value: any;
//     Size: number;
//     IsNullable: boolean;
//     Direction: ParamDirection;
//     Precision: number;
//     Scale: number;
//   }

export class Param /*implements IParam*/ {
  Name: string;
  DbType: DbType;
  Value: any;

  constructor (parameterName: string, dataType: DbType, value: any){
    this.DbType = dataType;
    this.Name = parameterName;
    this.Value = value;
  }

  static New(parameterName: string, dataType: DbType, value: any): Param {
    var p = new Param(parameterName, dataType, value);
    return p;
  }
}
