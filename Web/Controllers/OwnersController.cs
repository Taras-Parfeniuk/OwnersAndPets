using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Threading.Tasks;
using Entities;
using DAL.Abstraction;
using DAL.EFRepositories;
using Newtonsoft.Json;

namespace Web.Controllers
{
    [RoutePrefix("api/owners")]
    public class OwnersController : ApiController
    {
        private IOwnersRepository _owners;
        private IOwnersRepository Owners => _owners ?? (_owners = new OwnersRepository());
        private IPetsRepository _pets;
        private IPetsRepository Pets => _pets ?? (_pets = new PetsRepository());


        [Route("")]
        [HttpGet]
        public JsonResult<List<Owner>> Get()
        {
            return Json(Owners.GetAll().ToList());
        }

        [Route("")]
        [HttpGet]
        public JsonResult<List<Owner>> Get([FromUri] int page, [FromUri] int per_page)
        {
            page--;
            List<Owner> owners = Owners.GetRange(page * per_page, per_page);
            owners.ForEach(o => o.PetsCount = Pets.GetByOwnerId(o.Id).Count);
            return Json(owners);
        }

        [HttpGet]
        [Route("{ownerId}")]
        public JsonResult<List<Pet>> Get(string ownerId)
        {
            return Json(Pets.GetByOwnerId(ownerId).ToList());
        }

        [HttpGet]
        [Route("{ownerId}")]
        public JsonResult<List<Pet>> Get(string ownerId, [FromUri] int page, [FromUri] int per_page)
        {
            page--;
            return Json(Pets.GetByOwnerId(ownerId).OrderBy(p => p.Id).Skip(page * per_page).Take(per_page).ToList());
        }

        // POST api/values
        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostOwner(HttpRequestMessage request)
        {
            var data = await request.Content.ReadAsStringAsync();
            if (data != null)
            {
                Owners.Add(new Owner(data));
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [Route("{ownerId}")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostPet(string ownerId, HttpRequestMessage request)
        {
            var data = await request.Content.ReadAsStringAsync();
            Pet pet = JsonConvert.DeserializeObject<Pet>(data);
            if (data != null)
            {
                Pets.Add(new Pet(pet.Name, pet.OwnerId));
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        // PUT api/values/5
        [Route("{ownerId}")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateOwner(string ownerId, HttpRequestMessage request)
        {
            if (Owners.GetById(ownerId) == null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            var data = await request.Content.ReadAsStringAsync();
            Owner owner = JsonConvert.DeserializeObject<Owner>(data);
            owner.Id = ownerId;
            Owners.Update(new Owner { Id = ownerId, Name = owner.Name });
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Route("{ownerId}/{petId}")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdatePet(string ownerId, string petId, HttpRequestMessage request)
        {
            if (Pets.GetByOwnerId(ownerId) == null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            var data = await request.Content.ReadAsStringAsync();
            Pet pet = JsonConvert.DeserializeObject<Pet>(data);
            if(Pets.GetById(petId) == null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            pet.Id = ownerId;
            Pets.Update(new Pet { Id = petId, Name = pet.Name, OwnerId = ownerId });
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE api/values/5
        [Route("{ownerId}")]
        [HttpDelete]
        public HttpStatusCode DeleteOwner(string ownerId)
        {
            Owner owner = Owners.GetById(ownerId);
            Owners.Remove(owner);
            return HttpStatusCode.OK;
        }

        [Route("{ownerId}/{petId}")]
        [HttpDelete]
        public HttpStatusCode DeletePet(string ownerId, string petId)
        {
            Pet pet = Pets.GetById(petId);
            Pets.Remove(pet);
            return HttpStatusCode.OK;
        }
    }
}
