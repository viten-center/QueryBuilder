import { OmConstant } from "./OmConstant";
export class Constant {
  private Const: OmConstant;

  static Date(val: Date): Constant {
    var res = new Constant();
    res.Const = OmConstant.Date(val);
    return res;
  }

  static String(val: string): Constant {
    var res = new Constant();
    res.Const = OmConstant.String(val);
    return res;
  }

  static Number(val: number): Constant {
    var res = new Constant();
    res.Const = OmConstant.Number(val);
    return res;
  }

}