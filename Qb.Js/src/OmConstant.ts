import {OmDataType} from "./Enums"
import {Utils} from "./Utils"

interface IOmConstant {
  Type: OmDataType;
  Value: any;
}

export class OmConstant implements IOmConstant {
  Type: OmDataType;
  public StringValue: string;
  public NumericValue: number;
  public DateValue: Date;


  constructor(val: any, type: OmDataType) {

    if (!Utils.IsEmpty(type)) {
      this.Type = type;
    }

    if (!Utils.IsEmpty(val)) {
      if (typeof val == "number") {
        this.NumericValue = val;
      } else if (val instanceof Date) {
        this.DateValue = val;
      } else {
        this.StringValue = val;
      }
    }

    if (val == null && type != OmDataType.String)
      throw new Error("val");
  }

  get Value(): any {
    switch (this.Type) {
      case OmDataType.Date:
        return this.DateValue;
      case OmDataType.Numeric:
        return this.NumericValue;
      default:
        return this.StringValue;
    }
  }

  static Number(val: number): OmConstant {
    return new OmConstant(val, OmDataType.Numeric);
  }

  static String(val: string): OmConstant {
    return new OmConstant(val, OmDataType.String);
  }

  static Date(val: Date): OmConstant {
    return new OmConstant(val, OmDataType.Date);
  }

}

