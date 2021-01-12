using DocumentManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using DocumentManagement.Service;

namespace DocumentManagementSolution.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _service;
        private readonly IModelStateErrorHandler _modelStateErrorHandler;

        public DocumentController(IDocumentService service, IModelStateErrorHandler modelStateErrorHandler)
        {
            _service = service;
            _modelStateErrorHandler = modelStateErrorHandler;
        }

        /// <summary>
        /// Adds PDF to Azure blob storage and detail to SQL
        /// </summary>
        /// <param name="pdf">PDF Document</param>
        /// <returns>An ActionResult of Document</returns>
        /// <response code="201">Returns Document object</response>
        /// <response code="400">If there is bad data passed in </response>
        /// <response code="500">If there was an internal server error</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("AddPdf")]
        public async Task<ActionResult<SingleResponse<Document>>> AddPdf([FromForm] PdfDocument pdf)
        {
            if (pdf == null)
            {
                return BadRequest("Please provide PDF");
            }

            var response = new SingleResponse<Document>();

            if (!ModelState.IsValid)
            {
                response.ErrorMessage = _modelStateErrorHandler.GetValues(ModelState);
                return new ObjectResult(response) { StatusCode = (int)HttpStatusCode.BadRequest };
            }

            var location = await _service.AddDocument(pdf.File);
            response.Model = await _service.AddDocumentRecord(pdf.File.FileName, location, pdf.File.Length);
            return new ObjectResult(response) { StatusCode = (int)HttpStatusCode.Created };
        }
    }
}


