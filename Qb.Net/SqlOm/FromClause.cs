using System;

namespace Viten.QueryBuilder.SqlOm
{

  /// <summary>
  /// Represents the FROM clause of a select statement
  /// </summary>
  /// <remarks>
  /// FromClause consists of a base table set by the <see cref="FromClause.BaseTable">BaseTable</see> property
  /// and optional joins defined using the FromClause.Join method.
  /// <para>
  /// SqlOM supports inner, outer and cross joins. 
  /// Inner join between two tables returns only rows which exist in both tables.
  /// Outer (Left, Right and Full) joins return rows when at least one of the tables has a matching row. 
  /// Left outer joins returns all rows from the left table and while the missing rows from the right are filled with nulls.
  /// Right outer join is the opposite of left. Full outer join returns all the rows from the left and the right tables while the missing rows from the opposite table are filled with nulls.
  /// Cross join does not match any keys and returns the cartesian product of both tables.
  /// For more information about joins consult SQL documentation.
  /// </para>	
  /// </remarks>

  public class FromClause : ICloneable
	{
		JoinCollection joins = new JoinCollection();
    public JoinCollection Joins
    {
      get { return joins; }
    }

    public FromClause()
		{
		}
    /// <summary>
		/// Gets or sets the base table for the FromClause
		/// </summary>
		/// <remarks>
		///	The base table begins the serie of joins. 
		///	If no joins are specified for the query the base table is the only table in the select statement.
		///	BaseTable must be set before <see cref="SelectQuery">SelectQuery</see> can be rendered.
		/// </remarks>
		public FromTerm BaseTable { get; set; }

		/// <summary>
		/// Checks if a term with the specified RefName already exists in the FromClause.
		/// </summary>
		/// <param name="alias">The name of the term to be checked.</param>
		/// <returns>true if the term exists or false otherwise</returns>
		/// <remarks>
		/// TermExists matches <paramref name="alias">alias</paramref> to <see cref="FromTerm.RefName">RefName</see> of all participating FromTerms.
		/// </remarks>
		public bool TermExists(string alias)
		{
			if (joins.Count == 0 && BaseTable != null)
				return string.Compare(BaseTable.RefName, alias) == 0;

			foreach(Join join in joins)
			{
				if (string.Compare(join.RightTable.RefName, alias) == 0)
					return true;
			}
			
			return false;
		}

		/// <overloads>Use the following methods to define a join between two FromTerms.</overloads>
		/// <summary>
		/// Joins two tables using on a single join condition
		/// </summary>
		/// <param name="type">The type of join to be created.</param>
		/// <param name="leftTable">The left table</param>
		/// <param name="rightTable">The right table</param>
		/// <param name="leftField">Name of the field in the left table to join on</param>
		/// <param name="rightField">Name of the field in the right table to join on</param>
		/// <example>
		/// <code>
		/// query.FromClause.Join(JoinType.Left, tCustomers, tOrders, "customerId", "customerId");
		/// </code>
		/// </example>
		public void Join(JoinType type, FromTerm leftTable, FromTerm rightTable, string leftField, string rightField)
		{
			Join(type, leftTable, rightTable, new JoinCondition(leftField, rightField));
		}

		/// <summary>
		/// Creates an uncoditional join
		/// </summary>
		/// <param name="type">Must be JoinType.CrossJoin</param>
		/// <param name="leftTable"></param>
		/// <param name="rightTable"></param>
		public void Join(JoinType type, FromTerm leftTable, FromTerm rightTable)
		{
			Join(type, leftTable, rightTable, new JoinCondition[] {});
		}

		/// <summary>
		/// Joins two tables using on a single join condition
		/// </summary>
		/// <param name="type">The type of join to be created.</param>
		/// <param name="leftTable">The left table</param>
		/// <param name="rightTable">The right table</param>
		/// <param name="cond">Equality condition to be applied on the join</param>
		/// <remarks>
		/// This overload is used to create the most common type of join, when two tables
		/// are joined on a single equality condition.
		/// </remarks>
		/// <example>
		/// <code>
		/// query.FromClause.Join(JoinType.Inner, tCustomers, tOrders, new JoinCondition("customerId", "customerId"));
		/// </code>
		/// </example>
		public void Join(JoinType type, FromTerm leftTable, FromTerm rightTable, JoinCondition cond)
		{
			Join(type, leftTable, rightTable, new JoinCondition[] {cond});
		}

		/// <summary>
		/// Joins two tables using on a double join condition
		/// </summary>
		/// <param name="type">The type of join to be created.</param>
		/// <param name="leftTable">The left table</param>
		/// <param name="rightTable">The right table</param>
		/// <param name="cond1">First equality condition to be applied on the join</param>
		/// <param name="cond2">Second equality condition to be applied on the join</param>
		/// <remarks>
		/// A logical AND will be applied on <paramref name="cond1"/> and <paramref name="cond2"/>.
		/// Schematically, the resulting SQL will be ... x join y on (cond1 and cond2) ...
		/// </remarks>
		public void Join(JoinType type, FromTerm leftTable, FromTerm rightTable, JoinCondition cond1, JoinCondition cond2)
		{
			Join(type, leftTable, rightTable, new JoinCondition[] {cond1, cond2});
		}

		/// <summary>
		/// Joins two tables using on a triple join condition
		/// </summary>
		/// <param name="type">The type of join to be created.</param>
		/// <param name="leftTable">The left table</param>
		/// <param name="rightTable">The right table</param>
		/// <param name="cond1">First equality condition to be applied on the join</param>
		/// <param name="cond2">First equality condition to be applied on the join</param>
		/// <param name="cond3">First equality condition to be applied on the join</param>
		/// <remarks>
		/// A logical AND will be applied on all conditions.
		/// Schematically, the resulting SQL will be ... x join y on (cond1 and cond2 and cond3) ...
		/// </remarks>
		public void Join(JoinType type, FromTerm leftTable, FromTerm rightTable, JoinCondition cond1, JoinCondition cond2, JoinCondition cond3)
		{
			Join(type, leftTable, rightTable, new JoinCondition[] {cond1, cond2, cond3});
		}

		/// <summary>
		/// Joins two tables using on an array join condition
		/// </summary>
		/// <param name="type">The type of join to be created.</param>
		/// <param name="leftTable">The left table</param>
		/// <param name="rightTable">The right table</param>
		/// <param name="conditions">An array of equality condition to be applied on the join</param>
		/// <remarks>
		/// A logical AND will be applied on the conditions.
		/// Schematically, the resulting SQL will be ... x join y on (cond1 and cond2 and cond3 and ... and condN) ...
		/// </remarks>
		public void Join(JoinType type, FromTerm leftTable, FromTerm rightTable, JoinCondition[] conditions)
		{
			WhereClause clause = new WhereClause(WhereRel.And);
			foreach(JoinCondition cond in conditions)
				clause.Terms.Add(WhereTerm.CreateCompare(OmExpression.Field(cond.LeftField, leftTable), OmExpression.Field(cond.RightField, rightTable), CompCond.Equal));
			
			Join(type, leftTable, rightTable, clause);
		}

		/// <summary>
		/// Joins two tables using on an arbitrary join condition
		/// </summary>
		/// <param name="type">The type of join to be created.</param>
		/// <param name="leftTable">The left table</param>
		/// <param name="rightTable">The right table</param>
		/// <param name="conditions">Specifies how the join should be performed</param>
		/// <remarks>
		/// Use this overload to create complex join conditions. 
		/// Note that not all <see cref="WhereClause"/> operators and expressions are supported in joins.
		/// </remarks>
		/// <example>
		/// WhereClause condition = new WhereClause(WhereClauseRelationship.Or);
		/// condition.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("productId", tOrders), SqlExpression.Field("productId", tProducts), CompareOperator.Equal));
		/// condition.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("productName", tOrders), SqlExpression.Field("productName", tProducts), CompareOperator.Equal));
		/// query.FromClause.Join(JoinType.Left, tOrders, tProducts, condition);
		/// </example>
		public void Join(JoinType type, FromTerm leftTable, FromTerm rightTable, WhereClause conditions)
		{
			if (conditions.IsEmpty && type != JoinType.Cross)
				throw new InvalidQueryException("A join must have at least one condition.");
			
			joins.Add(new Join(leftTable, rightTable, conditions, type));
		}


		/// <summary>
		/// Returns true if this FromClause has no terms at all
		/// </summary>
		public bool IsEmpty
		{
			get { return BaseTable == null && joins.Count == 0; }
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// Creates a clone of this FromClause
		/// </summary>
		/// <returns>A new FromClause which exactly the same as the current one.</returns>
		public FromClause Clone()
		{
			FromClause a = new FromClause();
			a.joins = new JoinCollection(joins);
			a.BaseTable = BaseTable;
			return a;
		}


  }
}
