import {Select} from "./Select";
import {UnionMod} from "./Enums";
import {OmUnion} from "./OmUnion";
import {Qb} from "./Qb";
import {Utils} from "./Utils";

// interface IUnion {
//   Add(query: Select): Union;
//   Add(query: Select, modifier: UnionMode): Union;
// }
export class Union /*implements IUnion*/ {
  private Uni: OmUnion;
  constructor() {
    this.Uni = new OmUnion();
  }
  Add(query: Select): Union;
  Add(query: Select, modifier: UnionMod): Union;
  Add(): Union {
    var query: Select|undefined;
    var modifier: UnionMod = UnionMod.All;
    if (arguments.length >= 1) {
      query = arguments[0];
    }
    if (arguments.length >= 2) {
      modifier = arguments[1];
    }
    this.Uni.add(Qb.GetQueryObject(query!), modifier);
    return this;
  }
}