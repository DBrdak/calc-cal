using MediatR;
using Responses.DB;

namespace CalcCal.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}