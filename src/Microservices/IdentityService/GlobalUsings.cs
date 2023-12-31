﻿global using Contracts.Events;
global using Contracts.Requests;
global using Contracts.Responses;
global using FluentValidation;
global using IdentityService.Data;
global using IdentityService.Endpoints.ApiRoutes;
global using IdentityService.Exceptions;
global using IdentityService.Exceptions.ErrorDetails;
global using IdentityService.Features.Login;
global using IdentityService.Features.Register;
global using IdentityService.Interfaces;
global using IdentityService.Middlewares;
global using IdentityService.Services;
global using Libraries.Extensions;
global using MassTransit;
global using MediatR;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
