using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;


namespace Gdp_problem.Tests
{
    public class UnitTest1
    {
        string expectedPath = @"../../../expected_output.json";
        string actualPath = @"../../../actual_output.json";

        [Fact]
        public void Test1()
        {

            Program.GdpSol();


            bool right = false;
            if (File.Exists(expectedPath))
            {

                JObject xpctJSON = JObject.Parse(expectedPath);
                JObject actJSON = JObject.Parse(actualPath);

                right = JToken.DeepEquals(xpctJSON, actJSON);

                Assert.True(right);
            }
        }
        
        [Fact]
        public void Test2()
        {
            if (! File.Exists("../../../../actual_output.json"))
            {
                throw new FileNotFoundException(string.Format("file not found"));
            }
            
        }
        [Fact]
        public void Test3()
        {
            bool flag = true;
            if(new FileInfo("../../../../actual_output.json").Length==0)
            {
                flag= false;
            }
            Assert.True(flag);
        }

    }
}
