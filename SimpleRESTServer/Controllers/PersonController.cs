﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleRESTServer.Models;
using System.Collections;


namespace SimpleRESTServer.Controllers
{


    public class PersonController : ApiController
    {
        /// <summary>
        /// Get all persons
        /// </summary>
        /// <returns></returns>
        // GET: api/Person
        public ArrayList Get()
        {
            PersonPersistence pp = new PersonPersistence();
            return pp.getPersons();
        }

        /// <summary>
        /// Get a specific person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Person/5
        public Person Get(long id)
        {
            PersonPersistence pp = new PersonPersistence();
            Person person = pp.getPerson(id);
            if (person == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return person;
        }

        // POST: api/Person
        public HttpResponseMessage Post([FromBody]Person value)
        {

            PersonPersistence pp = new PersonPersistence();
            long id;
            id = pp.savePerson(value);
            value.ID = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("person/{0}", id));
            return response;

        }

        // PUT: api/Person/5
        public HttpResponseMessage Put(long id, [FromBody]Person value)
        {
            PersonPersistence pp = new PersonPersistence();
            bool recordExisted = false;
            recordExisted = pp.updatePerson(id, value);
            HttpResponseMessage response;

            if (recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;

        }

        // DELETE: api/Person/5
        public HttpResponseMessage Delete(long id)
        {
            PersonPersistence pp = new PersonPersistence();
            bool recordExisted = false;

            recordExisted = pp.deletePerson(id);

            HttpResponseMessage response;

            if (recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}
