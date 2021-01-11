using DocumentManagement.Models;
using DocumentManagement.Service;
using DocumentManagementSolution.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DocumentManagement.Tests.Unit.Api.Controllers
{
    public class DocumentControllerFixture
    {
        [Test]
        public async Task AddPdf_PassedNull_Returns400()
        {
            var controller = CreateDocumentController();

            var response = await controller.AddPdf(null);

            Assert.That((response.Result as ObjectResult).StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task AddPdf_PassedNull_ReturnsAppropriateErrorMessage()
        {
            var controller = CreateDocumentController();

            var response = await controller.AddPdf(null);

            Assert.That((response.Result as ObjectResult).Value, Is.EqualTo("Please provide PDF"));
        }

        [Test]
        public async Task AddPdf_PassedNullFile_Returns400()
        {
            var controller = CreateDocumentController();
            var pdf = new PdfDocument();
            MimicControllerModelValidation(controller, pdf);

            var response = await controller.AddPdf(pdf);

            Assert.That((response.Result as ObjectResult).StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task AddPdf_PassedNullFile_ReturnsAppropriateErrorMessage()
        {
            var controller = new DocumentController(new DocumentService(), new ModelStateErrorHandler());
            var pdf = new PdfDocument();

            MimicControllerModelValidation(controller, pdf);
            var response = await controller.AddPdf(pdf);
            var actual = ((SingleResponse<Document>)((ObjectResult)response.Result).Value).ErrorMessage;

            Assert.That(actual, Is.EqualTo("Model Keys In Error:\r\nFile\r\n\r\nModel Error Messages:\r\nPlease provide PDF file.\r\n"));
        }
        private DocumentController CreateDocumentController()
        {
            var documentServiceMock = new Mock<IDocumentService>();
            var mModelStateErrorHandlerMock = new Mock<IModelStateErrorHandler>();


            return new DocumentController(documentServiceMock.Object, mModelStateErrorHandlerMock.Object);
        }

        private void MimicControllerModelValidation(ControllerBase controller, object model)
        {
            // For Testing only to mimic the model binder which is responsible for Validating the Model
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault(), validationResult.ErrorMessage);
            }
        }
    }
}