using Viten.QueryBuilder.SqlOm;

namespace Viten.QueryBuilder
{

  /// <summary>Класс определения логических операций</summary>
  public class Logic
  {
    internal WhereClause Clause;

    internal Logic(WhereRel whereRel)
    {
      Clause = new WhereClause(whereRel);
    }

    /// <summary>Определение операции AND</summary>
    public static Logic And(params Cond[] opers)
    {
      return AndOr(WhereRel.And, opers);
    }

    /// <summary>Определение операции OR</summary>
    public static Logic Or(params Cond[] opers)
    {
      return AndOr(WhereRel.Or, opers);
    }

    static Logic AndOr(WhereRel relationship, params Cond[] opers)
    {
      Logic retVal = new Logic(relationship);
      if (opers != null)
        for (int i = 0; i < opers.Length; i++)
          retVal.Clause.Terms.Add(opers[i].Term);
      return retVal;
    }

    /// <summary>Определение операции AND</summary>
    public static Logic And(params Logic[] logics)
    {
      return AndOr(WhereRel.And, logics);
    }

    /// <summary>Определение операции OR</summary>
    public static Logic Or(params Logic[] logics)
    {
      return AndOr(WhereRel.Or, logics);
    }

    static Logic AndOr(WhereRel relationship, params Logic[] logics)
    {
      Logic retVal = new Logic(relationship);
      if (logics != null)
        for (int i = 0; i < logics.Length; i++)
          retVal.Clause.SubClauses.Add(logics[i].Clause);
      return retVal;
    }

  }
}
