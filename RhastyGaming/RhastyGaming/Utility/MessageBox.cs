using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utility
{
    public static class MessageBox
    {
        public static string Error(string title, string content)
        {
            return string.Format("UserPrompt('{0}', '{1}', '{2}', '{3}');",
                                                title,
                                                content,
                                                "alert",
                                                "warning");
        }
        public static string Saved
        {
            get
            {
                return "$.notify('<strong>Record Saved!</strong> " +
                       "You have sucessfuly saved a record.', { type: 'success' });";
            }
        }

        public static string InputError
        {
            get
            {
                return "$.notify('<strong>Unable to Save!</strong> " +
                      "Please fill in all the required fields.', { type: 'success' });";
            }
        }

        public static string Error(string exception)
        {
            return "$.notify('<strong>Unable to Edit!</strong> " + exception.Replace("'", "") + ".', { type: 'danger' });";
        }

        public static string InvalidWeek
        {
            get { return "InvalidWeek();"; }
        }
    }
}