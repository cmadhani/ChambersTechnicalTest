using System;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;

namespace DocumentManagement.Tests.Unit.Services.Helper
{
    class ModelStateErrorHandlerFixture
    {
        ModelStateErrorHandler _subject;

        public ModelStateErrorHandlerFixture()
        {
            _subject = new ModelStateErrorHandler();
        }

        [Test]
        public void GetValues_NullParameter_ThrowsArgumentNullExceptionException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _subject.GetValues(null));
            Assert.That(ex.Message, Is.EqualTo("ModelStateDictionary Expected (Parameter 'modelState')"));
        }

        [Test]
        public void GetValues_EmptyModelStateDictionary_ReturnsEmptyString()
        {
            var actual = _subject.GetValues(new ModelStateDictionary());
            Assert.That(actual, Is.Empty);
        }

        [Test]
        public void GetValues_PopulatedModelStateDictionary_ReturnsValidString()
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("FirstName", "First Name is Required");
            var actual = _subject.GetValues(modelState);
            Assert.That(actual, Is.EqualTo("Model Keys In Error:\r\nFirstName\r\n\r\nModel Error Messages:\r\nFirst Name is Required\r\n"));
        }
    }
}

