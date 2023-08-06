﻿global using BookService.Constants.ApiRoutes;
global using BookService.Constants.ErrorDetails;
global using BookService.Data;
global using BookService.Endpoints;
global using BookService.Exceptions;
global using BookService.Features.BuyBook;
global using BookService.Features.CreateBook;
global using BookService.Features.DeleteBook;
global using BookService.Features.GetAllBooks;
global using BookService.Features.GetBook;
global using BookService.Features.LoanBook;
global using BookService.Features.UpdateBook;
global using BookService.Interfaces;
global using BookService.Middlewares;
global using BookService.Models;
global using BookService.Services;
global using Common.Extensions;
global using Contracts.Requests;
global using Contracts.Responses;
global using FluentValidation;
global using MediatR;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.IdentityModel.Tokens;
global using System.Reflection;
global using System.Text;
