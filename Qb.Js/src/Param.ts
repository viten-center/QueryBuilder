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

  static New(parameterName: string, dataType: DbType, value: any): Param {
    var p = new Param();
    p.Name = parameterName;
    p.DbType = dataType;
    p.Value = value;
    return p;
  }
}
