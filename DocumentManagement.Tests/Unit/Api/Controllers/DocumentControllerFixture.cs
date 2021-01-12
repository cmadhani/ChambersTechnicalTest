using DocumentManagement.Models;
using DocumentManagement.Service;
using DocumentManagementSolution.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
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
        public async Task AddPdf_CallsAddDocumentRecord_ReturnsTrue()
        {
            var documentServiceMock = new Mock<IDocumentService>();
            var modelStateErrorHandlerMock = new Mock<IModelStateErrorHandler>();
            var controller = new DocumentController(documentServiceMock.Object, modelStateErrorHandlerMock.Object);
            var file = new Mock<IFormFile>();
            file.Setup(f => f.FileName).Returns("something.pdf");
            file.Setup(f => f.Length).Returns(1000);
            var pdf = new PdfDocument { File = file.Object };
            MimicControllerModelValidation(controller, pdf);

            var response = await controller.AddPdf(pdf);

            documentServiceMock.Verify(x => x.AddDocumentRecord(pdf.File.FileName, It.IsAny<Guid>() , pdf.File.Length), Times.Once);
        }

        [Test]
        public async Task AddPdf_CallsAddDocument_ReturnsTrue()
        {
            var documentServiceMock = new Mock<IDocumentService>();
            var modelStateErrorHandlerMock = new Mock<IModelStateErrorHandler>();
            var controller = new DocumentController(documentServiceMock.Object, modelStateErrorHandlerMock.Object);
            var file = new Mock<IFormFile>();
            file.Setup(f => f.FileName).Returns("something.pdf");
            var pdf = new PdfDocument { File = file.Object };
            MimicControllerModelValidation(controller, pdf);

            var response = await controller.AddPdf(pdf);

            documentServiceMock.Verify(x => x.AddDocument(pdf.File), Times.Once);
        }


        private DocumentController CreateDocumentController()
        {
            var documentServiceMock = new Mock<IDocumentService>();
            var modelStateErrorHandlerMock = new Mock<IModelStateErrorHandler>();
            return new DocumentController(documentServiceMock.Object, modelStateErrorHandlerMock.Object);
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