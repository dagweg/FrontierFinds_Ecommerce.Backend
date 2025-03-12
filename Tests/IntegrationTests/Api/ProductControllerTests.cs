using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Contracts.Authentication;
using Ecommerce.Infrastructure.Persistence.EfCore;
using Ecommerce.Tests.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Sdk;

namespace Ecommerce.IntegrationTests.Api;

[Collection("Sequential")]
public class ProductControllerTests : IntegrationTestBase
{
  public ProductControllerTests(EcommerceWebApplicationFactoryFixture factoryFixture)
    : base(factoryFixture) { }

  // create empty test methods for CreateProduct, GetProducts, and DeleteProduct

  [Fact]
  public async Task CreateProduct_ShouldReturnCreated_WhenCreationIsSuccessful()
  {
    await Authenticate();

    //arrange
    var multiPartData = Utils.Product.CreateProductRequest();

    //act
    var response = await _httpClient.PostAsync("products/", multiPartData);

    LogPretty.Log(await response.Content.ReadFromJsonAsync<ProblemDetails>());

    //assert
    response.StatusCode.Should().Be(HttpStatusCode.Created);
  }

  // [Fact]
  // public async Task GetProducts_ShouldReturnOk_WhenProductsExist()
  // {
  //   //arrange
  //   var request = Utils.Product.CreateProductRequest();

  //   //act
  //   var response = await _httpClient.PostAsJsonAsync("products/", request);

  //   //assert
  //   response.StatusCode.Should().Be(HttpStatusCode.Created);
  // }

  // [Fact]
  // public async Task DeleteProduct_ShouldReturnOk_WhenDeletionIsSuccessful()
  // {
  //   //arrange
  //   var request = Utils.Product.CreateProductRequest();

  //   //act
  //   var response = await _httpClient.PostAsJsonAsync("products/", request);

  //   //assert
  //   response.StatusCode.Should().Be(HttpStatusCode.Created);
  // }
}
