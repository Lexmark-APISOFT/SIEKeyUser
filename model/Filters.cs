using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIE_KEY_USER.model
{
    public class Filters
    {
        const string AND = "AND";
        const string OR = "OR";
        const string EQUALS = "=";
        const string NOT_EQUALS = "!=";
        const string GREATER_THAN = ">";
        const string LESS_THAN = "<";
        const string GREATER_THAN_OR_EQUALS = ">=";
        const string LESS_THAN_OR_EQUALS = "<=";
        const string LIKE = "LIKE";
        const string NOT_LIKE = "NOT LIKE";
        const string IN = "IN";
        const string NOT_IN = "NOT IN";

        public List<string> conditions { get; set; }

        public Filters()
        {
            conditions = new List<string>();
        }

        public void AddCondition(string field, string value, string condition)
        {
            conditions.Add($"{field} {condition} {value}");
        }

        public void AddCondition(string field, string value)
        {
            conditions.Add($"{field} = {value}");
        }





    }
}