using System;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class Builder
    {
        [TestMethod]
        public void Fields()
        {
            List<string> strings = new List<string>{
           "hello",
           "hi",
           "my friend"
           };

            QueryBuilder.Fields(strings);
        }
    }
}
