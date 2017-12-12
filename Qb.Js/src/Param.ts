import {DbType, ParamDirection} from "./Enums"
interface IParam {
    Name: string;
    DataType: DbType;
    Value: any;
    Size: number;
    IsNullable: boolean;
    Direction: ParamDirection;
    Precision: number;
    Scale: number;
  }

  export class Param implements IParam {
    Name: string;
    DataType: DbType;
    Value: any;
    Size: number;
    IsNullable: boolean;
    Direction: ParamDirection;
    Precision: number;
    Scale: number;

    static New(parameterName: string, dataType: DbType, value: any, direction?: ParamDirection, size?: number, isNullable?: boolean, 
      precision?: number, scale?: number): Param {
      var p = new Param();
      p.Name = parameterName;
      p.DataType = dataType;
      p.Size = size;
      p.IsNullable = isNullable;
      p.Direction = direction;
      p.Precision = precision;
      p.Scale = scale;
      p.Value = value;
      return p;
    }
  }
