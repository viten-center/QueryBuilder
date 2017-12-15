import { SelectQuery } from "./SelectQuery"
import { UnionMod } from "./Enums"

// interface IOmUnionItem {
//     Query: SelectQuery;
//     RepeatingAction: DistinctModifier;
//   }

export class OmUnionItem /*implements IOmUnionItem*/ {
  Query: SelectQuery;
  RepeatingAction: UnionMod;

  setQuery(query: SelectQuery) {
    this.Query = query;
  }


  setRepeatingAction(repeatingAction: UnionMod) {
    this.RepeatingAction = repeatingAction;
  }

  constructor(query: SelectQuery, repeatingAction: UnionMod) {
    this.Query = query;
    this.RepeatingAction = repeatingAction;
  }
}

// interface IOmUnion {
//   add(query: SelectQuery, repeatingAction: DistinctModifier);
//   Items: Array<OmUnionItem>;
// }
export class OmUnion /*implements IOmUnion*/ {
  Items = new Array<OmUnionItem>();
  add(query: SelectQuery, repeatingAction: UnionMod) {
    this.Items.push(new OmUnionItem(query, repeatingAction));
  }
}
