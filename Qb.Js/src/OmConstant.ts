import {DataType} from "./Enums"
import {Utils} from "./Utils"

// interface IOmConstant {
//   Type: OmDataType;
//   Value: any;
// }

export class OmConstant /*implements IOmConstant*/ {
  Type: DataType;
  public StringValue: string;
  public NumericValue: number;
  public DateValue: Date;


  constructor(val: string|number|Date/*, type: DataType*/) {

    // if (!Utils.IsEmpty(type)) {
    //   this.Type = type;
    // }

    if (!Utils.IsEmpty(val)) {
      if (typeof val == "number") {
        this.NumericValue = val;
        this.Type = DataType.Numeric;
      } else if (val instanceof Date) {
        this.DateValue = val;
        this.Type = DataType.Date;
      } else {
        this.StringValue = val;
        this.Type = DataType.String;
      }
    }

    // if (val == null && type != DataType.String)
    //   throw new Error("val");
  }

  get Value(): string|number|Date {
    switch (this.Type) {
      case DataType.Date:
        return this.DateValue;
      case DataType.Numeric:
        return this.NumericValue;
      default:
        return this.StringValue;
    }
  }

  static Number(val: number): OmConstant {
    return new OmConstant(val);
  }

  static String(val: string): OmConstant {
    return new OmConstant(val);
  }

  static Date(val: Date): OmConstant {
    return new OmConstant(val);
  }

}

