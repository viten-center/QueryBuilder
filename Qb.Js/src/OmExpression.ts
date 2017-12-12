﻿import {OmExpressionType, OmAggregationFunction, OmDataType, ExprValCode} from "./Enums"
import {CaseClause} from "./CaseClause"
import {FromTerm} from "./FromTerm"
import {OmConstant} from "./OmConstant"
import {SelectQuery} from "./SelectQuery"

interface IOmExpression {
    TableAlias: string;
    Type: OmExpressionType;
    Value: any;
    AggFunction: OmAggregationFunction;
    CaseClause: CaseClause;
    SubExpr1: OmExpression;
    SubExpr2: OmExpression;
  }
    

  export class OmExpression implements IOmExpression{
    Type: OmExpressionType;
    Table: FromTerm;
    AggFunction: OmAggregationFunction;
    SubExpr1: OmExpression;
    SubExpr2: OmExpression;
    CaseClause = new CaseClause();

    static Number(val: number): OmExpression 
		{
      return OmExpression.Constant(OmConstant.Number(val), null);
    }

    static String(val: string): OmExpression {
      return OmExpression.Constant(OmConstant.String(val), null);
    }

    static Date(val: Date): OmExpression {
      return OmExpression.Constant(OmConstant.Date(val), null);
    }

    static Constant(val: OmConstant, dataType: OmDataType): OmExpression
		{
      var expr = new OmExpression();
      if (dataType == undefined || dataType == null) {
        expr.ValueCode = ExprValCode.SqlConst;
        expr.ConstantValue = val;
      } else {
        expr.ConstantValue = new OmConstant(val, dataType);
      }
      expr.Type = OmExpressionType.Constant;
      return expr;
		}

    static Field(fieldName: string, table: FromTerm): OmExpression {
      var expr = new OmExpression();
      expr.ValueCode = ExprValCode.String;
      expr.StringValue = fieldName;
      expr.Table = table;
      expr.Type = OmExpressionType.Field;
      return expr;
    }

    static Case(caseClause: CaseClause): OmExpression {
      var expr = new OmExpression();
      expr.Type = OmExpressionType.Case;
      expr.CaseClause = caseClause;
      return expr;
    }

    static IfNull(test: OmExpression, val: OmExpression): OmExpression {
      var expr = new OmExpression();
      expr.Type = OmExpressionType.IfNull;
      expr.SubExpr1 = test;
      expr.SubExpr2 = val;
      return expr;
    }

    static Func(func: OmAggregationFunction, param: OmExpression): OmExpression {
      var expr = new OmExpression();
      expr.Type = OmExpressionType.Function;
      expr.SubExpr1 = param;
      expr.AggFunction = func;
      return expr;
    }

    static Null(): OmExpression {
      var expr = new OmExpression();
      expr.Type = OmExpressionType.Null;
      return expr;
    }

    static PseudoField(fieldName: string): OmExpression {
      var expr = new OmExpression();
      expr.ValueCode = ExprValCode.String;
      expr.StringValue = fieldName;
      expr.Type = OmExpressionType.PseudoField;
      return expr;
    }

    static SubQuery(query: string|SelectQuery): OmExpression {
      var expr = new OmExpression();

      if (typeof query == "string") {
        expr.ValueCode = ExprValCode.String;
        expr.StringValue = <string>query;
        expr.Type = OmExpressionType.SubQueryText;
      } else {
        expr.QueryValue = <SelectQuery>query;
        expr.ValueCode = ExprValCode.SelQuery;
        expr.Type = OmExpressionType.SubQueryObject;
      }
      return expr;
    }

    static Parameter(paramName: string): OmExpression {
      var expr = new OmExpression();
      expr.ValueCode = ExprValCode.String;
      expr.StringValue = paramName;
      expr.Type = OmExpressionType.Parameter;
      return expr;
    }

    static Raw(sql: string): OmExpression {
      var expr = new OmExpression();
      expr.ValueCode = ExprValCode.String;
      expr.StringValue = sql;
      expr.Type = OmExpressionType.Raw;
      return expr;
    }

    get TableAlias(): string {
      return this.Table == null ? null : this.Table.RefName;
    }

    get Value(): any {
      switch (this.ValueCode) {
        case ExprValCode.SelQuery:
          return this.QueryValue;
        case ExprValCode.SqlConst:
          return this.ConstantValue;
        default:
          return this.StringValue;
      }
    }

    public ValueCode: ExprValCode;
    public StringValue: string;
    public ConstantValue: OmConstant;
    public QueryValue: SelectQuery;

  }
