using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using ServiceControl.API;
using ServiceControl.Application.DTOs;
using Xunit;

namespace ServiceControl.Tests.Integration
{
    public class SimpleIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public SimpleIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Test1_GetSaoPaulo()
        {
            var response = await _client.PostAsJsonAsync("/api/Order?city=São Paulo", new OrderDto()
            {
                OrderDescription = "Teste de integração",
                ExecutionDate = DateTime.UtcNow.AddDays(1),
                ResponsiblePerson = "Teste"
            });

            var result = await response.Content.ReadFromJsonAsync<ProcessOrderSuccessResponse>();
           
            response.StatusCode.Should().NotBe(HttpStatusCode.NotFound);
            
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task Test2_SwaggerShouldBeAccessible()
        {
            var response = await _client.GetAsync("/swagger");

            Console.WriteLine($"Swagger Response Status: {response.StatusCode}");

            response.StatusCode.Should().NotBe(HttpStatusCode.InternalServerError);
        }
        
        [Fact]
        public async Task Test2_ShoudFail()
        {
            var response = await _client.PostAsJsonAsync("/api/Order?city=Cidade não existe", new OrderDto()
            {
                OrderDescription = "Teste de integração",
                ExecutionDate = DateTime.UtcNow.AddDays(1),
                ResponsiblePerson = "Teste"
            });

            var result = await response.Content.ReadFromJsonAsync<ProcessOrderSuccessResponse>();
           
            response.StatusCode.Should().NotBe(HttpStatusCode.NotFound);
            
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Erro no processamento do pedido");
        }
    }

    public class ProcessOrderSuccessResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public object ProcessedOrder { get; set; } = null!;
        public object Notifications { get; set; } = null!;
    }
}
