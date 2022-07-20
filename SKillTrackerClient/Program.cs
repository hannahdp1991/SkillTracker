using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SKillTrackerClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var associate = new Associate();
                associate.Name = "test test";
                associate.AssociateId = 200001;
                associate.Email = "test@gmail.com";
                associate.Mobile = 1234567899;

                var skillDetails = new List<Skill>();
                var skill = new Skill();
                skill.SkillName = "html";
                skill.ExpertiseLevel = 13;
                skillDetails.Add(skill);

                var requestObj = new SkillProfile();
                requestObj.AssociateInfo = associate;
                requestObj.SkillInfo = skillDetails;

                var jsonObj = JsonConvert.SerializeObject(requestObj);
                System.Diagnostics.Debug.WriteLine(jsonObj.ToString());
                var data = new StringContent(jsonObj, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:59796/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = new HttpResponseMessage();

                    response = await client.PostAsync("/add-profile", data).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {

                    }
                    else
                    {
                       System.Diagnostics.Debug.WriteLine(response.StatusCode);
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
