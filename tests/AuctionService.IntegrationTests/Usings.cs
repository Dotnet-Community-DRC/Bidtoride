global using Xunit;
global using Microsoft.AspNetCore.Mvc.Testing;
global using AuctionService.Data;
global using AuctionService.Entities;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using AuctionService.IntegrationTests.Fixtures;
global using AuctionService.IntegrationTests.Helpers;
global using MassTransit;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.TestHost;
global using Testcontainers.PostgreSql;
global using System.Net;
global using System.Net.Http.Json;
global using AuctionService.DTOs;
global using MassTransit.Testing;