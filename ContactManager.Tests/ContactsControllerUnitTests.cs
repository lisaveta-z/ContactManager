using System;
using ContactManager.Controllers;
using ContactManager.Models;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ContactManager.Tests
{
    public class ContactsControllerUnitTests
    {
        ContactsController _controller;
        Mock<IContactService> _mock;

        public ContactsControllerUnitTests()
        {
            _mock = new Mock<IContactService>();
            _controller = new ContactsController(_mock.Object);
        }

        #region Add tests

        [Fact]
        public void AddContact_Valid_ReturnsCreatedAtActionResult()
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
            Assert.IsType<Contact>(createdAtAction.Value);
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

        #region Update tests

        [Fact]
        public void UpdateContact_Valid_ReturnsNoContent()
        {
            // Arrange
            int id = 1;
            var contactToUpdate = new Contact()
            {
                Id = 1,
                LastName = "King",
                Name = "S.",
                BirthDate = Convert.ToDateTime("21/09/1947"),
                Tag = "author"
            };
            _mock.Setup(x => x.GetById(id)).Returns(() => Task.FromResult(contactToUpdate));
            contactToUpdate.Name = "Stephen";

            // Act
            var result = _controller.Put(id, contactToUpdate);
            var getUpdatedResult = _controller.Get(id);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
            var updated = Assert.IsType<OkObjectResult>(getUpdatedResult.Result);
            var contact = Assert.IsType<Contact>(updated.Value);
            Assert.Equal("Stephen", contact.Name);
        }

        [Fact]
        public void UpdateContact_InvalidName_ReturnsBadRequest()
        {
            // Arrange
            int id = 1;
            var invalidNameContact = new Contact()
            {
                Id = 1,
                LastName = "King123",
                Name = "Stephen",
                BirthDate = Convert.ToDateTime("21/09/1947"),
                Tag = "author"
            };
            _mock.Setup(x => x.GetById(id)).Returns(() => Task.FromResult(invalidNameContact));
            _controller.ModelState.AddModelError("Name", "Numbers aren't allowed");

            // Act
            var result = _controller.Put(id, invalidNameContact);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateContact_InvalidBirthDate_ReturnsBadRequest()
        {
            // Arrange
            int id = 1;
            var invalidNameContact = new Contact()
            {
                Id = 1,
                LastName = "King",
                Name = "Stephen",
                Tag = "author"
            };
            _mock.Setup(x => x.GetById(id)).Returns(() => Task.FromResult(invalidNameContact));
            _controller.ModelState.AddModelError("BirthDate", "Required");

            // Act
            var result = _controller.Put(id, invalidNameContact);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        #endregion Update tests

        #region Get tests

        [Fact]
        public void GetContactsByTag_Valid_ReturnsOkResult()
        {
            // Arrange
            _mock.Setup(x => x.GetByTag("developer")).Returns(() => Task.FromResult(GetTestContacts()));

            // Act
            var result = _controller.GetByTag("developer");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var contacts = Assert.IsAssignableFrom<IEnumerable<Contact>>(okResult.Value);
            Assert.Equal(2, contacts.Count());
        }

        private IEnumerable<Contact> GetTestContacts()
        {
            var contacts = new List<Contact>
            {
                new Contact {
                    Id = 1,
                    LastName = "Petrov",
                    Name = "Alexey",
                    MiddleName = "Ivanovich",
                    BirthDate = Convert.ToDateTime("10/02/1987"),
                    Tag = "developer"
                },
                new Contact {
                    Id = 2,
                    LastName = "Ivanova",
                    Name = "Irina",
                    MiddleName = "Stepanovna",
                    BirthDate = Convert.ToDateTime("01/03/1993"),
                    Tag = "developer"
                }
            };
            return contacts;
        }

        #endregion Get tests

        #region Delete tests

        [Fact]
        public void DeleteContact_Existing_ReturnsOkResult()
        {
            // Arrange
            int id = 1;
            var deletedContact = new Contact()
            {
                Id = 1,
                LastName = "King",
                Name = "Stephen",
                BirthDate = Convert.ToDateTime("21/09/1947"),
                Tag = "author"
            };
            _mock.Setup(x => x.GetById(id)).Returns(() => Task.FromResult(deletedContact));

            // Act
            var result = _controller.Delete(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<Contact>(okResult.Value);
        }

        [Fact]
        public void DeleteContact_NotExisting_ReturnsNotFoundResult()
        {
            // Arrange
            int id = 1;

            // Act
            var result = _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        #endregion Delete tests
    }
}
