using DocumentManagement.Dal;
using DocumentManagement.Models;
using DocumentManagement.Service;
using DocumentManagementSolution.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Tests.Acceptance
{
    class Tests
    {
        /*
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
        */

        [Test]
        public async Task HappyPath()
        {
            PdfDocument pdf = new PdfDocument();
            var ms = new MemoryStream(File.ReadAllBytes(@".\gpg_brochure.pdf"));
            pdf.File = new FormFile(ms, 0, ms.Length, "gpg_brochure.pdf", "gpg_brochure.pdf"); ;

            var options = new DbContextOptionsBuilder<DocumentDbContext>()
            .UseInMemoryDatabase(databaseName: "DocumentDbContext")
            .Options;

            IDocumentService documentService = new DocumentService(
                new DocumentRepository(new DocumentDbContext(options)));
            var controller = new DocumentController(documentService, new ModelStateErrorHandler());

            var response = await controller.AddPdf(pdf);

        }

        //[Test]
        //public async Task Test1()
        //{

        //    //Pdf pdf = new Pdf();
        //    //using (var stream = File.OpenRead("..\\..\\..\\..\\gpg_brochure.pdf"))
        //    //{
        //    //    var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
        //    //    {
        //    //        Headers = new HeaderDictionary(),
        //    //        ContentType = "application/pdf"
        //    //    };

        //    //    pdf.File = file ;
        //    //}
        //    var pdfMock = new Mock<PdfDocument>();
        //    var fileMock = new Mock<IFormFile>();
        //    pdfMock.Object.File = fileMock.Object;
        //    var documentServiceMock = new Mock<IDocumentService>();
        //    var mModelStateErrorHandlerMock = new Mock<IModelStateErrorHandler>();


        //    var controller = new DocumentController(documentServiceMock.Object, mModelStateErrorHandlerMock.Object);
        //    var response = await controller.AddPdf(pdfMock.Object);

        //    documentServiceMock.Verify(x => x.AddPdf(It.IsAny<Document>()));
        //    //Assert.That(response.Value , Is.EqualTo((int)HttpStatusCode.NotFound) );

        //    //Microsoft.AspNetCore.Mvc.ActionResult<Document> response = await controller.AddPdf(pdf) as ObjectResult;

        //    //// Assert
        //    //Assert.That(response.StatusCode, Is.EqualTo( (int)HttpStatusCode.NotFound);

        //}


        //[Test]
        //public async Task Test2()
        //{

        //    PdfDocument pdf = new PdfDocument();
        //    using (var stream = File.OpenRead("ChambersTechnicalTest.xlsx"))
        //    {
        //        var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
        //        {
        //            Headers = new HeaderDictionary(),
        //            ContentType = "application/vnd. ms-excel"
        //        };

        //        pdf.File = file;
        //    }

        //    //var pdfMock = new Mock<Pdf>();
        //    //var fileMock = new Mock<IFormFile>();

        //    //pdfMock.Object.File = fileMock.Object;
        //    //var documentServiceMock = new Mock<IDocumentService>();


        //    var controller = new DocumentController(new DocumentService(), new ModelStateErrorHandler());
        //    var response = await controller.AddPdf(pdf);


        //    // Assert
        //    Assert.That(response.Value, Is.EqualTo((int)HttpStatusCode.NotFound));


        //}
    }
}
