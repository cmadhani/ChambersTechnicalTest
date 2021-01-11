using DocumentManagement.Models;
using DocumentManagement.Service;
using DocumentManagementSolution.Controllers;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Tests.Acceptance
{
    class Tests
    {
        [Test]
        public async Task Test5()
        {

            PdfDocument pdf = new PdfDocument();
            using (var stream = File.OpenRead(@"..\gpg_brochure.pdf"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/pdf"
                };
                pdf.File = file;
            }

            var documentServiceMock = new Mock<IDocumentService>();


            var controller = new DocumentController(documentServiceMock.Object, new ModelStateErrorHandler());
            var response = await controller.AddPdf(pdf);

            documentServiceMock.Verify(x => x.AddPdf(It.IsAny<Document>()));
            //Assert.That(response.Value , Is.EqualTo((int)HttpStatusCode.NotFound) );

            //Microsoft.AspNetCore.Mvc.ActionResult<Document> response = await controller.AddPdf(pdf) as ObjectResult;

            //// Assert
            //Assert.That(response.StatusCode, Is.EqualTo( (int)HttpStatusCode.NotFound);

        }



        [Test]
        public async Task Test1()
        {

            //Pdf pdf = new Pdf();
            //using (var stream = File.OpenRead("..\\..\\..\\..\\gpg_brochure.pdf"))
            //{
            //    var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            //    {
            //        Headers = new HeaderDictionary(),
            //        ContentType = "application/pdf"
            //    };

            //    pdf.File = file ;
            //}
            var pdfMock = new Mock<PdfDocument>();
            var fileMock = new Mock<IFormFile>();
            pdfMock.Object.File = fileMock.Object;
            var documentServiceMock = new Mock<IDocumentService>();
            var mModelStateErrorHandlerMock = new Mock<IModelStateErrorHandler>();


            var controller = new DocumentController(documentServiceMock.Object, mModelStateErrorHandlerMock.Object);
            var response = await controller.AddPdf(pdfMock.Object);

            documentServiceMock.Verify(x => x.AddPdf(It.IsAny<Document>()));
            //Assert.That(response.Value , Is.EqualTo((int)HttpStatusCode.NotFound) );

            //Microsoft.AspNetCore.Mvc.ActionResult<Document> response = await controller.AddPdf(pdf) as ObjectResult;

            //// Assert
            //Assert.That(response.StatusCode, Is.EqualTo( (int)HttpStatusCode.NotFound);

        }


        [Test]
        public async Task Test2()
        {

            PdfDocument pdf = new PdfDocument();
            using (var stream = File.OpenRead("ChambersTechnicalTest.xlsx"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/vnd. ms-excel"
                };

                pdf.File = file;
            }

            //var pdfMock = new Mock<Pdf>();
            //var fileMock = new Mock<IFormFile>();

            //pdfMock.Object.File = fileMock.Object;
            //var documentServiceMock = new Mock<IDocumentService>();


            var controller = new DocumentController(new DocumentService(), new ModelStateErrorHandler());
            var response = await controller.AddPdf(pdf);


            // Assert
            Assert.That(response.Value, Is.EqualTo((int)HttpStatusCode.NotFound));


        }
    }
}
