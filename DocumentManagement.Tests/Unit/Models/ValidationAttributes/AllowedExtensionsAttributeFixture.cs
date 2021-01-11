using DocumentManagement.Models.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Tests.Unit.Models.ValidationAttributes
{
    public class AllowedExtensionsAttributeFixture
    {

        [Test]
        public void ctor_NullParameter_ThrowsArgumentNullExceptionException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new AllowedExtensionsAttribute(null));
            Assert.That(ex.Message, Is.EqualTo("extensions (Parameter 'provide valid extensions')"));
        }

        [Test]
        public void IsValid_NullParameter_ThrowsArgumentExceptionException()
        {
            var subject = new AllowedExtensionsAttribute(new string[] { "" });
            var ex = Assert.Throws<ArgumentException>(() => subject.IsValid(null));
            Assert.That(ex.Message, Is.EqualTo("Invalid argument type, expected type : IFormFile (Parameter 'value')"));
        }

        [Test]
        public void IsValid_InvalidObjectarameter_ThrowsArgumentExceptionException()
        {
            var subject = new AllowedExtensionsAttribute(new string[] { "" });
            var ex = Assert.Throws<ArgumentException>(() => subject.IsValid(new object()));
            Assert.That(ex.Message, Is.EqualTo("Invalid argument type, expected type : IFormFile (Parameter 'value')"));
        }

        [Test]
        public void IsValid_IncorrectFileExtension_ReturnFalse()
        {
            // arrange
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("FileName.xlsx");
            var subject = new AllowedExtensionsAttribute(new string[] { ".pdf" });

            // act
            var result = subject.IsValid(fileMock.Object);

            // assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsValid_NoFileExtension_ReturnFalse()
        {
            // arrange
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("FileName");
            var subject = new AllowedExtensionsAttribute(new string[] { ".pdf" });

            // act
            var result = subject.IsValid(fileMock.Object);

            // assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsValid_NoFileNameOrExtension_ReturnFalse()
        {
            // arrange
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns(string.Empty);
            var subject = new AllowedExtensionsAttribute(new string[] { ".pdf" });

            // act
            var result = subject.IsValid(fileMock.Object);

            // assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsValid_ValueNameAndExtension_ReturnFalse()
        {
            // arrange
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("filename.pdf");
            var subject = new AllowedExtensionsAttribute(new string[] { ".pdf" });

            // act
            var result = subject.IsValid(fileMock.Object);

            // assert
            Assert.That(result, Is.True);
        }


    }
}
