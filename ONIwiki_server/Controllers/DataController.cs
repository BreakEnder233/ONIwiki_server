using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Interact;
using Interact.DataSafety;
using ONIwiki_server.Database;
using Interact.Database.Models;

namespace ONIwiki_server.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            var dataBase = new DataSetDBContext();
            dataBase.dataSets.Add(new DataSet
            {
                name = "0",
                hashedPassword = "0",
                data = JsonConvert.SerializeObject(new List<NamedObject>
                {
                    new NamedObject
                    {
                        name = "testItem",
                        normalizedName = "testItem",
                        description = "testDescription",
                        children = null
                    }

                })
            });
            dataBase.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult GetData(string dataSetName)
        {
            if (string.IsNullOrWhiteSpace(dataSetName))
            {
                return new JsonResult(new WikiQueryResponse(dataSetName, WikiResponseStatus.FAIL, null).ToString());
            }
            var dataBase = new DataSetDBContext();
            var data = dataBase.dataSets.FirstOrDefault((ds) => ds.name == dataSetName);
            if(data == null)
            {
                var result = new ContentResult();
                result.Content = new WikiQueryResponse(dataSetName, WikiResponseStatus.NOTEXIST, null).ToString();
                Console.WriteLine(result.Content);
                return result;
            }
            return new JsonResult(new WikiQueryResponse(dataSetName, WikiResponseStatus.SUCCESS, data.data).ToString());
        }

        [HttpPost]
        public IActionResult SetData(string data)
        {
            WikiUpdateQuery query;
            try
            {
                query = JsonConvert.DeserializeObject<WikiUpdateQuery>(data);
                if (HashEncrypter.GetHash(query.dataString) != query.hashedData) return new JsonResult(new WikiUpdateResponse(query.dataSetName, WikiResponseStatus.DATA_DISTORTED).ToString());
                var dataBase = new DataSetDBContext();

                bool authorizationSuccess = false;
                if(dataBase.dataSets.FirstOrDefault((ds) => ds.name == query.dataSetName) == null)
                {
                    //Add a new record
                    dataBase.dataSets.Add(new DataSet
                    {
                        name = query.dataSetName,
                        hashedPassword = HashEncrypter.GetHash(query.password),
                        data = query.dataString
                    });
                    dataBase.SaveChanges();
                    authorizationSuccess = true;
                    return new JsonResult(new WikiUpdateResponse(query.dataSetName, WikiResponseStatus.CREATE_NEW_RECORD).ToString());
                }

                if(dataBase.dataSets.FirstOrDefault((ds) => ds.name == query.dataSetName).hashedPassword == HashEncrypter.GetHash(query.password))
                {
                    authorizationSuccess = true;
                }

                if (!authorizationSuccess)
                {
                    return new JsonResult(new WikiUpdateResponse(query.dataSetName, WikiResponseStatus.PASSWORD_UNMATCH).ToString());
                }

                dataBase.dataSets.First((ds) => ds.name == query.dataSetName).data = query.dataString;
                dataBase.SaveChanges();
                return new JsonResult(new WikiUpdateResponse(query.dataSetName, WikiResponseStatus.SUCCESS).ToString());

            }
            catch(Exception e){
                return new JsonResult(new WikiUpdateResponse(e.Message, WikiResponseStatus.FAIL).ToString());
            }
            
        }
    }
}