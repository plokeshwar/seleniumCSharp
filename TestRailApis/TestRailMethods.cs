using Newtonsoft.Json.Linq;
using SeleniumCSharp.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharp.TestRailApis
{
    public class TestRailMethods
    {
        private static string uid = ConfigurationHelper.Get<String>("TestRailUsername");
        private static string pwd = ConfigurationHelper.Get<String>("TestRailPassword");
        private static string url = ConfigurationHelper.Get<String>("TestRailUrl");
        private static Boolean testRailIndicator = ConfigurationHelper.Get<bool>("TestRailIndicator");

        private static string runID;

        private static APIClient getApiClient()
        {

            APIClient client = new APIClient(url);
            client.User = uid;
            client.Password = pwd;
            return client;
        }

        public string createTestRun(String projectID)
        {

            if (!testRailIndicator)
            {
                return null;
            }

            var browserStackIndicator = ConfigurationHelper.Get<bool>("UseBrowserStack");
            string userName = Environment.UserName;
            string osVersion = Environment.OSVersion.ToString();
            String environment = "Local Execution - ";
            String description = "UserName: " + userName + "\n OS:" + osVersion;

            if (browserStackIndicator)
            {
                environment = "BrowserStack Execution - ";
                description = "This run is executed on BrowserStack";
            }

            var data = new Dictionary<string, object>
            {
                { "suite_id", 1},
                { "name", environment+" : "+Utilities.ExtentReporter.getTimeStamp() },
                 { "assignedto_id", 1 },
                 { "description", description },
                  { "include_all", false }
            };

            APIClient client = getApiClient();
            JObject c = (JObject)client.SendPost("add_run/" + projectID, data);
            Console.WriteLine("Test Run Created Successfully : " + c.GetValue("id").ToString());
            runID = c.GetValue("id").ToString();
            return runID;
        }

        public void updateTestRun(String caseID)
        {
            if (testRailIndicator)
            {

                JArray array = getTestCasesInRun(runID);
                array.Add(caseID);


                var data = new Dictionary<string, object>
            {
                { "include_all", false },
                { "case_ids", array }
            };

                APIClient client = getApiClient();
                JObject c = (JObject)client.SendPost("update_run/" + runID, data);
                Console.WriteLine("Test Run Updated successfully.");
                Console.WriteLine(c.ToString());
            }
        }

        public void addTestCaseStatus(String status, String caseID, String message)
        {

            if (testRailIndicator)
            {


                var data = new Dictionary<string, object>
            {
                { "status_id", getStatusID(status)},
                { "comment", message },
                { "defects", "" }
            };

                APIClient client = getApiClient();
                JObject c = (JObject)client.SendPost("add_result_for_case/" + runID + "/" + caseID, data);
                Console.WriteLine(c.ToString());
            }
        }

        private static int getStatusID(String status)
        {
            if (status.ToLower() == "pass" || status.ToLower() == "passed")
            {
                return 1;
            }
            else if (status.ToLower() == "fail" || status.ToLower() == "failed")
            {
                return 5;
            }
            else
            {
                return 3;
            }

        }

        private static JArray getTestCasesInRun(String runID)
        {
            if (!testRailIndicator)
            {
                return null;
            }
            JArray array = new JArray();

            APIClient client = getApiClient();
            JArray c = (JArray)client.SendGet("get_tests/" + runID);
            foreach (JObject item in c)
            {
                string caseID = item.GetValue("case_id").ToString();
                array.Add(caseID);
                Console.WriteLine(caseID);
            }
            return array;
        }

    }
}
