﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BackOfficeBase.Application.Authorization.Users;
using BackOfficeBase.Application.Authorization.Users.Dto;
using BackOfficeBase.Application.Shared.Dto;
using BackOfficeBase.Modules.Authorization.Controllers;
using BackOfficeBase.Utilities.Collections;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BackOfficeBase.Tests.Web.Api.modules.Authorization
{
    public class UsersControllerTests : WebApiTestBase
    {
        [Fact]
        public async Task Should_Get_User_Async()
        {
            var mockUserAppService = new Mock<IUserAppService>();
            mockUserAppService.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(new UserOutput
            {
                UserName = "test_user",
                Email = "test_user@mail.com",
                Id = Guid.NewGuid()
            });

            var usersController = new UsersController(mockUserAppService.Object);
            var actionResult = await usersController.GetUsers(Guid.NewGuid());

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var userOutput = Assert.IsType<UserOutput>(okObjectResult.Value);

            Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
            Assert.Equal("test_user", userOutput.UserName);
        }

        [Fact]
        public async Task Should_Get_User_List_Async()
        {
            var mockUserAppService = new Mock<IUserAppService>();
            mockUserAppService.Setup(x => x.GetListAsync(It.IsAny<PagedListInput>())).ReturnsAsync(new PagedListResult<UserListOutput>
            {
                Items = new List<UserListOutput>
                {
                    new UserListOutput {UserName = "test_user_1", Email = "test_user_mail_1@mail", Id = Guid.NewGuid()},
                    new UserListOutput {UserName = "test_user_2", Email = "test_user_mail_2@mail", Id = Guid.NewGuid()},
                    new UserListOutput {UserName = "test_user_3", Email = "test_user_mail_3@mail", Id = Guid.NewGuid()},
                    new UserListOutput {UserName = "test_user_4", Email = "test_user_mail_4@mail", Id = Guid.NewGuid()},
                    new UserListOutput {UserName = "test_user_5", Email = "test_user_mail_5@mail", Id = Guid.NewGuid()}
                },
                TotalCount = 10
            });

            var usersController = new UsersController(mockUserAppService.Object);
            var actionResult = await usersController.GetUsers(new PagedListInput());

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var userPagedListResult = Assert.IsType<PagedListResult<UserListOutput>>(okObjectResult.Value);

            Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
            Assert.Equal(10, userPagedListResult.TotalCount);
            Assert.Equal(5, userPagedListResult.Items.Count());
        }

        [Fact]
        public async Task Should_Create_User_Async()
        {
            var mockUserAppService = new Mock<IUserAppService>();
            mockUserAppService.Setup(x => x.CreateAsync(It.IsAny<CreateUserInput>())).ReturnsAsync(new UserOutput
            {
                UserName = "test_user",
                Email = "test_user@mail.com",
                Id = Guid.NewGuid()
            });

            var usersController = new UsersController(mockUserAppService.Object);
            var actionResult = await usersController.PostUsers(new CreateUserInput());

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var userOutput = Assert.IsType<UserOutput>(okObjectResult.Value);

            Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
            Assert.Equal("test_user", userOutput.UserName);
        }

        [Fact]
        public void Should_Update_User()
        {
            var mockUserAppService = new Mock<IUserAppService>();
            mockUserAppService.Setup(x => x.Update(It.IsAny<UpdateUserInput>())).Returns(new UserOutput
            {
                UserName = "test_user",
                Email = "test_user@mail.com",
                Id = Guid.NewGuid()
            });

            var usersController = new UsersController(mockUserAppService.Object);
            var actionResult = usersController.PutUsers(new UpdateUserInput());

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var userOutput = Assert.IsType<UserOutput>(okObjectResult.Value);

            Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
            Assert.Equal("test_user", userOutput.UserName);
        }

        [Fact]
        public async Task Should_Delete_User_Async()
        {
            var mockUserAppService = new Mock<IUserAppService>();
            mockUserAppService.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(new UserOutput
            {
                UserName = "test_user",
                Email = "test_user@mail.com",
                Id = Guid.NewGuid()
            });

            var usersController = new UsersController(mockUserAppService.Object);
            var actionResult = await usersController.DeleteUsers(Guid.NewGuid());

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var userOutput = Assert.IsType<UserOutput>(okObjectResult.Value);

            Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
            Assert.Equal("test_user", userOutput.UserName);
        }
    }
}
