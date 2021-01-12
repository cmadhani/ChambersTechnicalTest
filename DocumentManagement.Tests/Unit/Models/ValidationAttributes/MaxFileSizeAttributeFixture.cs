using DocumentManagement.Models.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;

namespace DocumentManagement.Tests.Unit.Models.ValidationAttributes
{
    public class MaxFileSizeAttributeFixture
    {

        [Test]
        public void IsValid_NullParameter_ThrowsArgumentNullExceptionException()
        {
            var subject = new MaxFileSizeAttribute(5 * 1024 * 1024);
            var ex = Assert.Throws<ArgumentException>(() => subject.IsValid(null));
            Assert.That(ex.Message, Is.EqualTo("Invalid argument type, expected type : IFormFile (Parameter 'value')"));
        }

        [Test]
        public void IsValid_InvalidObjectarameter_ThrowsArgumentNullExceptionException()
        {
            var subject = new MaxFileSizeAttribute(5 * 1024 * 1024);
            var ex = Assert.Throws<ArgumentException>(() => subject.IsValid(new object()));
            Assert.That(ex.Message, Is.EqualTo("Invalid argument type, expected type : IFormFile (Parameter 'value')"));
        }

        [Test]
        public void IsValid_IncorrectLageFile_ReturnFalse()
        {
            // arrange
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.Length ).Returns(6 * 1024 * 1024);
            var subject = new MaxFileSizeAttribute(5 * 1024 * 1024);

            // act
            var result = subject.IsValid(fileMock.Object);

            // assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsValid_IncorrectSmallFile_ReturnTure()
        {
            // arrange
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.Length).Returns(4 * 1024 * 1024);
            var subject = new MaxFileSizeAttribute(5 * 1024 * 1024);

            // act
            var result = subject.IsValid(fileMock.Object);

            // assert
            Assert.That(result, Is.True);
        }
    }
}
