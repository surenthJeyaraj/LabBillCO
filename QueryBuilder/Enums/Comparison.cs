

namespace Dapper.QueryBuilder.Enums
{
    /// <summary>
    /// Represents comparison operators for WHERE, HAVING and JOIN clauses
    /// </summary>
    public enum Comparison
    {
        Equals,
        NotEquals,
        Like,
        NotLike,
        GreaterThan,
        GreaterOrEquals,
        LessThan,
        LessOrEquals,
        In
    }
}
