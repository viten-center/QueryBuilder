import {SelectQuery} from "./SelectQuery"
import {DistinctModifier} from "./Enums"

interface IOmUnionItem {
    Query: SelectQuery;
    RepeatingAction: DistinctModifier;
  }

  export class OmUnionItem implements IOmUnionItem{
    Query: SelectQuery;
    RepeatingAction: DistinctModifier;

    setQuery(query: SelectQuery) {
      this.Query = query;
    }


    setRepeatingAction(repeatingAction: DistinctModifier) {
      this.RepeatingAction = repeatingAction;
    }

    constructor(query: SelectQuery, repeatingAction: DistinctModifier) {
      this.Query = query;
      this.RepeatingAction = repeatingAction;
    }
  }

  interface IOmUnion {
    add(query: SelectQuery, repeatingAction: DistinctModifier);
    Items: Array<OmUnionItem>;
  }
  export class OmUnion implements IOmUnion {
    Items = new Array<OmUnionItem>();
    add(query: SelectQuery, repeatingAction: DistinctModifier) {
      this.Items.push(new OmUnionItem(query, repeatingAction));
    }
  }
