using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Workflow.Media;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/sounds")]
    public class SoundController : ApiController
    {
        [HttpGet]
        [Route("khmer")]
        public HttpResponseMessage Khmer(string empId) {
            ISoundManager manager = new SoundManger(AppDomain.CurrentDomain.BaseDirectory + "\\Sounds\\Khmer\\");
            return ReturnBytes(manager.GetKhmerSound(empId), "khmer");
        }

        [HttpGet]
        [Route("english")]
        public HttpResponseMessage English(string empId)
        {
            ISoundManager manager = new SoundManger(AppDomain.CurrentDomain.BaseDirectory + "\\Sounds\\English\\");
            return ReturnBytes(manager.GetEnglishSound(empId), "english");
        }

        public HttpResponseMessage ReturnBytes(byte[] bytes, string fileName) {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);            
            result.Content = new ByteArrayContent(bytes);
            result.Content.Headers.Add("Content-Disposition", string.Format("attachment; filename={0}.mp3", fileName));
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }
    }
}
