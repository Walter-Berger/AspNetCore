﻿global using BookService.Data;
global using BookService.Endpoints;
global using BookService.Features.BuyBook;
global using BookService.Features.CreateBook;
global using BookService.Features.DeleteBook;
global using BookService.Features.GetAllBooks;
global using BookService.Features.GetBook;
global using BookService.Features.LoanBook;
global using BookService.Features.UpdateBook;
global using BookService.Models;
global using Common.ErrorDetails;
global using Common.Exceptions;
global using Common.Extensions;
global using Common.Interfaces;
global using Common.Middlewares;
global using Common.Services;
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
