using System;
using ContactManager.Controllers;
using ContactManager.Models;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Services;

namespace ContactManager.Tests
{
    public class ContactsControllerTests
    {
        ContactsController _controller;
        Mock<IContactService> _mock;

        public ContactsControllerTests()
        {
            _mock = new Mock<IContactService>();
            _controller = new ContactsController(_mock.Object);
        }

        #region Add tests

        [Fact]
        public void AddContact_ValidObject_ReturnsCreatedResult()
        {
            // Arrange
            var newContact = new Contact()
            {
                LastName = "King",
                Name = "Stephen",
                BirthDate = Convert.ToDateTime("21/09/1947"),
                Tag = "author"
            };

            // Act
            var result = _controller.Post(newContact);

            // Assert
            var createdAtAction = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("Get", createdAtAction.ActionName);
            _mock.Verify(r => r.Add(newContact));
        }

        [Fact]
        public void AddContact_InvalidName_ReturnsBadRequest()
        {
            // Arrange
            var invalidNameContact = new Contact()
            {
                LastName = "King123",
                Name = "Stephen",
                BirthDate = Convert.ToDateTime("21/09/1947"),
                Tag = "author"
            };
            _controller.ModelState.AddModelError("Name", "Numbers aren't allowed");

            // Act
            var result = _controller.Post(invalidNameContact);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void AddContact_InvalidBirthDate_ReturnsBadRequest()
        {
            // Arrange
            var invalidBirthDateContact = new Contact()
            {
                LastName = "King",
                Name = "Stephen",
                Tag = "author"
            };
            _controller.ModelState.AddModelError("BirthDate", "Required");

            // Act
            var result = _controller.Post(invalidBirthDateContact);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        #endregion Add tests


    }
}
