using System;
using System.Xml;

namespace ReactiveDb
{

    public static class Mappers
    {

        public static Func<object, DateTime> ToDate = (o) =>
        {
            return Convert.ToDateTime(o);
        };

        public static Func<object, string> ToStr = (o) =>
        {
            return Convert.ToString(o);
        };

        public static Func<object, decimal> ToDecilmal = (o) =>
        {
            return Convert.ToDecimal(o);
        };

        public static Func<object, int> ToInt = (o) =>
        {
            return Convert.ToInt32(o);
        };

        public static Func<object, long> ToLong = (o) =>
        {
            return Convert.ToInt64(o);
        };

        public static Func<object, Guid> ToGuid = (o) =>
        {
            return (Guid)(o);
        };

        public static Func<object, XmlDocument> ToXml = (o) =>
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Mappers.ToStr(o));
            return doc;
        };

        public static T ToVal<T>(this object o)
        {
            return (T)o;
        }
    }
}