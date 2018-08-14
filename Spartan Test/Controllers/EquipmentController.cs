using Newtonsoft.Json.Linq;
using Spartan_Test.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Spartan_Test.Controllers
{
    public class EquipmentController : ApiController
    {
        // function to read form json file
        public List<Equipment> LoadJson()
        {
            // create a read stram to the json
            using (StreamReader r = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/EquipmentData.json")))
            {
                //set json to string
                string json = r.ReadToEnd();
                // parse the string to json
                dynamic data = JObject.Parse(json);
               
                // create a list of equipment objects
                List<Equipment> equipment = new List<Equipment>();
                string description = "";
                // go through data
                foreach (var i in data.SerialisedEquipment)
                {
                  //go through equipment types
                    foreach( var j in data.EquipmentType)
                    {
                        // if equipment id is the same as equipment type
                        if (j.Id.Equals(i.EquipmentTypeId))
                        {
                            // set description to the type
                            description = j.Description;
                        }
                    }
                    // add the equipment to the list.
                    equipment.Add(new Equipment {Id = i.Id, Description = description, ExternalId= i.ExternalId, });
                }
                // return the list to the user.
                return equipment;

            }
        }

        // returns data to the front page when an api call is made
        public IEnumerable<Equipment> Get()
        {
            // get json data
            List<Equipment> equipment = LoadJson(); 
            // return json data
            return equipment;
        }

        // get an item by id
        public IHttpActionResult GetEquipmentById(string id)
        {
            // get data
            List<Equipment> equipment = LoadJson();
            // create return object
            Equipment ret = new Equipment();
            // go through equipment list
            foreach (Equipment e in equipment) {
                // check if the ids match and if they do set the object to the equipment object
                if (id.Equals(e.Id)){ ret = e; }
            }
            // return null if nothing is found
            if (ret == null)
            {
                return NotFound();
            }
            //return data if it's found
            return Ok(ret);
        }

        // get data by external id
        public IHttpActionResult GetEquipmentByExternalId(int id)
        {
            // get data to be compared
            List<Equipment> equipment = LoadJson();
            // create nw return object
            Equipment ret = new Equipment();
            // go through the list
            foreach (Equipment e in equipment)
            {
                // if ids match set data to the return object
                if (id == e.ExternalId) { ret = e; }
            }
            // if nothing was found return not found
            if (ret == null)
            {
                return NotFound();
            }
            // if data was found return the data
            return Ok(ret);
        }

    }
}
