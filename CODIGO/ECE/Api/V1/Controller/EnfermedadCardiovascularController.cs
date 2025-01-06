using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TsaakAPI.Entities;
using TsaakAPI.Model.DAO;

namespace TsaakAPI.Api.V1.Controller
{
    [ApiController]
    [Route("tsaak/api/v1/[controller]")]
    public class EnfermedadCardiovascularController : ControllerBase
    {
        private readonly EnfermedadCardiovascularDao _enfermedadCardiovascularDao;
        private readonly IConfiguration _configuration;

        public EnfermedadCardiovascularController(EnfermedadCardiovascularDao enfermedadCardiovascularDao, IConfiguration configuration )
        {
            _enfermedadCardiovascularDao = enfermedadCardiovascularDao;
            _configuration = configuration;
           
        }

        [HttpGet]
        public async Task<IActionResult> GetEnfermedadCardiovascular()
        {
            var result = await _enfermedadCardiovascularDao.GetAll();

            if (!result.Success || result.Result == null)
            {
                return NotFound(result); // Return 404 if no records are found
            }

            return Ok(result); // Return 200 with the list of results
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Llamada al DAO para obtener el registro
            var result = await _enfermedadCardiovascularDao.GetByIdAsync(id);

            // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Si es exitosa, devuelve el resultado con un estado 200 OK
                return Ok(result.Result);
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return BadRequest(new { message = result.Messages });
            }
        }
        [HttpPost]
    public async Task<ActionResult<IActionResult>> PostEnfermedad([FromBody]EnfermedadCardiovascular  enfermedad)
    {
        var result = await _enfermedadCardiovascularDao.Create(enfermedad);
        // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Si es exitosa, devuelve el resultado con un estado 200 OK
                return Ok(result);
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return BadRequest(new { message = result.Messages });
            }
    }
    [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEnfermedad([FromBody] EnfermedadCardiovascular enfermedad, int id)
        {
            // Llamada al DAO para actualizar el registro
            var result = await _enfermedadCardiovascularDao.Update(enfermedad, id);

            // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Si es exitosa, devuelve el resultado con un estado 200 OK
                return Ok(result.Result);
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return BadRequest(new { message = result.Messages });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnfermedad(int id)
        {

            // Llamada al DAO para actualizar el registro
            var result = await _enfermedadCardiovascularDao.Delete(id);

            // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Si es exitosa, devuelve el resultado con un estado 200 OK
                return Ok(result.Result);
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return BadRequest(new { message = result.Messages });
            }
        }
        [HttpGet("Page")]
        public async Task<IActionResult> PageFecht(int page, int fecth)
        {
            // Llamada al DAO para obtener el registro
            var result = await _enfermedadCardiovascularDao.GetData(page, fecth);

            // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Si es exitosa, devuelve el resultado con un estado 200 OK
                return Ok(result.Result);
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return BadRequest(new { message = result.Messages });
            }
        }
        [HttpGet("Complete")]
        public async Task<IActionResult> GetComplete()
        {
            // Llamada al DAO para obtener el registro
            var result = await _enfermedadCardiovascularDao.Complete();

            // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Si es exitosa, devuelve el resultado con un estado 200 OK
                return Ok(result.Result);
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return BadRequest(new { message = result.Messages });
            }
        }
        [HttpGet("Diccionario")]
        public async Task<IActionResult> GetDiccionario()
        {
            // Llamada al DAO para obtener el registro
            var result = await _enfermedadCardiovascularDao.Diccionario();

            // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Si es exitosa, devuelve el resultado con un estado 200 OK
                return Ok(result.Result);
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return BadRequest(new { message = result.Messages });
            }
        }
 
    }
}