import { OmConstant } from "./OmConstant";

/**
 * Класс описания константы
 */
export class Constant {
  private Const: OmConstant;

  static Date(val: Date): Constant {
    var res = new Constant();
    res.Const = OmConstant.Date(val);
    return res;
  }

  /**
   * Создает экземтляр класса Constant со строковым значением
   * @param  val - строковая константа
   */
  static String(val: string): Constant {
    var res = new Constant();
    res.Const = OmConstant.String(val);
    return res;
  }

  /**
   * Создает экземтляр класса Constant с числовым значением
   * @param val - числовая константа
   */
  static Number(val: number): Constant {
    var res = new Constant();
    res.Const = OmConstant.Number(val);
    return res;
  }

}
