using Viten.QueryBuilder.Culture;
using Viten.QueryBuilder.SqlOm;
using System.Collections.Generic;
using System.Linq;

namespace Viten.QueryBuilder
{
  /// <summary>Класс команды вставки информации</summary>
  public class Insert
  {
    internal InsertQuery Query;

    internal Insert(string tableName)
    {
      Query = new InsertQuery(tableName);
    }

    /// <summary>Определение значений полей для вставки</summary>
    public Insert Values(params UpdateVal[] values)
    {
      if (this.Query.Terms.Count > 0)
        throw new InvalidQueryException(SR.Err_RepeatValues);
      List<UpdateTerm> terms = new List<UpdateTerm>();
      if (values != null)
        for (int i = 0; i < values.Length; i++)
          terms.Add(values[i].Term);
      this.Query.Terms.AddRange(terms.ToArray());
      return this;
    }
    /// <summary>
    /// Указывает объекту о необходимости генерации кода для возвращения значения поля с атрибутом Identity (AutoIncrement).
    /// </summary>
    /// <param name="fieldName">Имя поля с атрибутом Identity</param>
    /// <returns></returns>
    /// <remarks>Объект DBCommand, выполняющий эту команду, должен иметь OUT параметр с именем fieldName и 
    /// соответствующим типом данных. В значение этого параметра и будет помещено значение поля с атрибутом Identity.
    /// Эта команда должна выполняться методом DBCommand.ExecuteNonQuery(Insert query). Методом ReturnIdentity() 
    /// может быть указано только одно поле для команды Insert</remarks>
    /// <example>
    /// Для вставки записи в таблицу с определением
    /// create table [MyTable] 
    /// (
    ///   [Id] integer not null identity,
    ///   [Name] nvarchar(50)
    /// )
    /// и возвратом автоматически сгенерированного значения поля Id команда Insert выглядит следующим образом
    /// Insert ins = QBuilder.Insert("MyTable")
    ///   .Values(UpdateVal.Val("Name", "Some name"))
    ///   .ReturnIdentity("Id")
    ///   .Params(Param.New("Id", Sys.DataType.Int32, ParamDirection.Output, 0));
    /// </example>
    public Insert ReturnIdentity(string fieldName)
    {
      this.Query.IdentityField = fieldName;
      return this;
    }

    /// <summary>Список параметров команды</summary>
    /// <param name="parameters">Параметры</param>
    /// <returns></returns>
    public Insert Params(params Param[] parameters)
    {
      if (parameters != null)
      {
        this.Query.CommandParams.AddRange(parameters);
      }
      return this;
    }

  }
}
