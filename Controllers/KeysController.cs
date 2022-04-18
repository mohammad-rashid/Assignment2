using KeyValueIoT.Data;
using KeyValueIoT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace KeyValueIoT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class KeysController : Controller
    {
        private readonly AppDbContext _repository;
        
        public KeysController(AppDbContext repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.KeyValueRepo);
        }

        [HttpGet("{key}")]
        public IActionResult Get(string key)
        {
            var keyvalueFromDb = _repository.KeyValueRepo.Find(key);
            if (keyvalueFromDb == null)
            {
                return NotFound();
            }
            return Ok(keyvalueFromDb);
        }

        [HttpPost]
        [HttpPut]
        public IActionResult Post(KeyValueModel data)
        {
            var keyvalueFromDb = _repository.KeyValueRepo.Find(data.Key);
            if(keyvalueFromDb != null)
            {
                return Conflict();
            }

            try
            {
                _repository.KeyValueRepo.Add(data);
                _repository.SaveChanges();
                return Ok();
            }
            catch(Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);   
            }
        }

        [HttpPatch("{key}/{value}")]
        public IActionResult Patch(string key, string value)
        {
            var inputData = new KeyValueModel();
            inputData.Key = key;
            inputData.Value = value;
            var keyvalueFromDb = _repository.KeyValueRepo.Find(key);
            if (keyvalueFromDb == null)
            {
                return NotFound();
            }
            try
            {
                _repository.KeyValueRepo.Update(inputData);
                _repository.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(string key)
        {
            var keyvalueFromDb = _repository.KeyValueRepo.Find(key);
            if (keyvalueFromDb == null)
            {
                return NotFound();
            }
            try
            {
                _repository.KeyValueRepo.Remove(keyvalueFromDb);
                _repository.SaveChanges();
                return Ok();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
