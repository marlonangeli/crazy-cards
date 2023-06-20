using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Classes;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Classes.Queries;

public record GetClassByIdQuery(Guid Id) : IQuery<Result<ClassResponse>>;