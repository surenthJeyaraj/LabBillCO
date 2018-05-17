using System;
using System.Collections.Generic;
using System.Text;
using Dapper.QueryBuilder.Enums;


namespace Dapper.QueryBuilder
{
    /// <summary>
    /// Represents a TOP clause for SELECT statements
    /// </summary>
    public struct TopClause
    {
        public int Quantity;
        public TopUnit Unit;
        public TopClause(int nr)
        {
            Quantity = nr;
            Unit = TopUnit.Records;
        }
        public TopClause(int nr, TopUnit aUnit)
        {
            Quantity = nr;
            Unit = aUnit;
        }
    }

}
