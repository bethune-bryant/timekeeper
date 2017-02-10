using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace TimeKeeper
{
    [Serializable]
    public class JiraInfo
    {
        public string TaskID { get; set; }

        public string WorkLogID { get; set; }

        public string Summary { get; set; }
        public string Description { get; set; }

        public JiraInfo(string TaskID, string WorkLogID, string Summary, string Description)
        {
            this.TaskID = TaskID;
            this.WorkLogID = WorkLogID;
            this.Summary = Summary;
            this.Description = Description;
        }

        public JiraInfo() : this("", "", "", "") { }

        public JiraInfo(string TaskID) : this(TaskID, "", "", "") { }

        public JiraInfo(string TaskID, string Summary, string Description) : this(TaskID, "", Summary, Description) { }

        public override string ToString()
        {
            return this.TaskID;
        }
    }

    static class JiraInterface
    {
        public static string JiraURL
        {
            get
            {
                return Utilities.ReadFromRegistry("jiraurl");
            }
            set
            {
                Utilities.WriteToRegistry("jiraurl", value);
            }
        }

        public static string Username
        {
            get
            {
                return Utilities.ReadFromRegistry("jirausername");
            }
            set
            {
                Utilities.WriteToRegistry("jirausername", value);
            }
        }

        public static string Password
        {
            get
            {
                return Encryption.Decrypt(Utilities.ReadFromRegistry("jirapassword"));
            }
            set
            {
                Utilities.WriteToRegistry("jirapassword", Encryption.Encrypt(value));
            }
        }

        private static dynamic RunQuery(string Query, string User, string Pass)
        {
            string URL = JiraURL + Query;

            string urlParameters = ""; // "?api_key=123";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
        "Basic",
        Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
                string.Format("{0}:{1}", User, Pass))));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                string s = response.Content.ReadAsStringAsync().Result;

                dynamic stuff = JObject.Parse(s);

                return stuff;
            }
            else
            {
                throw new Exception((int)response.StatusCode + " " + response.ReasonPhrase);
            }
        }

        private static void RunPostQuery(string Query, string PostData, string User, string Pass)
        {
            string URL = JiraURL + Query;

            string urlParameters = ""; // "?api_key=123";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
        "Basic",
        Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
                string.Format("{0}:{1}", User, Pass))));

            HttpContent content = new StringContent(PostData, Encoding.UTF8, "application/json");

            // List data response.
            //HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
            HttpResponseMessage response = client.PostAsync(urlParameters, content).Result;

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                string s = response.Content.ReadAsStringAsync().Result;

                dynamic stuff = JObject.Parse(s);
            }
            else
            {
                throw new Exception((int)response.StatusCode + " " + response.ReasonPhrase);
            }
        }

        private static void RunPutQuery(string Query, string PostData, string User, string Pass)
        {
            string URL = JiraURL + Query;

            string urlParameters = ""; // "?api_key=123";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
        "Basic",
        Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
                string.Format("{0}:{1}", User, Pass))));

            HttpContent content = new StringContent(PostData, Encoding.UTF8, "application/json");

            // List data response.
            //HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
            HttpResponseMessage response = client.PutAsync(urlParameters, content).Result;

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                string s = response.Content.ReadAsStringAsync().Result;

                dynamic stuff = JObject.Parse(s);
            }
            else
            {
                throw new Exception((int)response.StatusCode + " " + response.ReasonPhrase);
            }
        }

        private static void RunDeleteQuery(string Query, string User, string Pass)
        {
            string URL = JiraURL + Query;

            string urlParameters = ""; // "?api_key=123";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
        "Basic",
        Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
                string.Format("{0}:{1}", User, Pass))));

            // List data response.
            //HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
            HttpResponseMessage response = client.DeleteAsync(urlParameters).Result;

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                string s = response.Content.ReadAsStringAsync().Result;

                //dynamic stuff = JObject.Parse(s);
            }
            else
            {
                throw new Exception((int)response.StatusCode + " " + response.ReasonPhrase);
            }
        }

        public static JiraInfo GetJiraInfo(string taskID)
        {
            dynamic queryResult = RunQuery("/issue/" + taskID, Username, Password);

            return new JiraInfo(taskID, queryResult.fields.summary.ToString(), queryResult.fields.description.ToString());
        }

        public static JiraInfo UpdateJiraInfo(JiraInfo PartialInfo)
        {
            JiraInfo retval = GetJiraInfo(PartialInfo.TaskID);
            retval.WorkLogID = PartialInfo.WorkLogID;
            return retval;
        }

        public static List<string> GetJiraTaskIDs()
        {
            List<string> retval = new List<string>();

            dynamic queryResult = RunQuery("/search?jql=project+%3D+TD+AND+status+in+(Open%2C+\"In+Progress\"%2C+Reopened)+AND+assignee+in+(currentUser())&tempMax=1000", Username, Password);

            int total = queryResult.total;

            for (int i = 0; i < total; i++)
            {
                retval.Add(queryResult.issues[i].key.ToString());
            }

            return retval;
        }

        public static JiraInfo UpdateWork(JiraInfo current, DateTime start, DateTime stop)
        {
            if (current.WorkLogID.Length == 0)
            {
                AddWorkLog(current.TaskID, start, stop);
                current.WorkLogID = GetWorklogIDs(current.TaskID)[0];
            }
            else
            {
                try
                {
                    UpdateWorkLog(current.TaskID, current.WorkLogID, start, stop);
                }
                catch
                {
                    AddWorkLog(current.TaskID, start, stop);
                    current.WorkLogID = GetWorklogIDs(current.TaskID)[0];
                }
            }

            return UpdateJiraInfo(current);
        }

        private static List<string> GetWorklogIDs(string taskID)
        {
            List<string> retval = new List<string>();

            dynamic queryResult = RunQuery("/issue/" + taskID + "/worklog", Username, Password);

            int total = queryResult.total;

            for (int i = 0; i < total; i++)
            {
                retval.Add(queryResult.worklogs[i].id.ToString());
            }

            retval.Sort();
            retval.Reverse();

            return retval;
        }

        private const string LOG_COMMENT = "Logging via [TimeKeeper|https://github.com/bethune-bryant/timekeeper]";

        private static string WorkLogToJSON(DateTime started, DateTime stopped)
        {
            StringBuilder josnData = new StringBuilder();
            StringWriter sw = new StringWriter(josnData);

            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.WriteStartObject();
                jsonWriter.WritePropertyName("timeSpent");
                jsonWriter.WriteValue((stopped - started).Hours.ToString() + "h " + (stopped - started).Minutes.ToString() + "m");
                jsonWriter.WritePropertyName("started");
                jsonWriter.WriteValue(started.ToString("yyyy-MM-ddTHH:mm:ss.fffzz00"));
                jsonWriter.WritePropertyName("comment");
                jsonWriter.WriteValue(LOG_COMMENT);
                jsonWriter.WriteEndObject();
            }

            return josnData.ToString();
        }

        private static void AddWorkLog(string taskID, DateTime started, DateTime stopped)
        {
            string postData = WorkLogToJSON(started, stopped);
            RunPostQuery("/issue/" + taskID + "/worklog", postData, Username, Password);
        }

        private static void UpdateWorkLog(string taskID, string workLogID, DateTime started, DateTime stopped)
        {
            string putData = WorkLogToJSON(started, stopped);
            RunPutQuery("/issue/" + taskID + "/worklog/" + workLogID, putData, Username, Password);
        }

        public static void DeleteWorkLog(string taskID, string workLogID)
        {
            RunDeleteQuery("/issue/" + taskID + "/worklog/" + workLogID, Username, Password);
        }
    }
}
