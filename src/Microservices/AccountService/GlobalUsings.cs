﻿global using AccountService.Data;
global using AccountService.Endpoints.ApiRoutes;
global using AccountService.Exceptions.ErrorDetails;
global using AccountService.Features.CreateUser;
global using AccountService.Features.DeleteUser;
global using AccountService.Features.GetAllUsers;
global using AccountService.Features.GetUser;
global using AccountService.Features.UpdateUser;
global using AccountService.Middlewares;
global using AccountService.Models;
global using Contracts.Events;
global using Contracts.Requests;
global using Contracts.Responses;
global using FluentValidation;
global using Libraries.Exceptions;
global using Libraries.Exceptions.ErrorDetails;
global using Libraries.Extensions;
global using Libraries.Interfaces;
global using Libraries.Services;
global using MassTransit;
global using MediatR;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.IdentityModel.Tokens;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text;
