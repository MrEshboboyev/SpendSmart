using SpendSmart.Application.Queries.Handlers.Abstractions;

namespace SpendSmart.Application.Queries.Handlers.Transactions.GetTransactionById;

/// <summary>
/// Represents the data request interface for getting a transaction by identifier.
/// </summary>
public interface IGetTransactionByIdDataRequest : IDataRequest<GetTransactionByIdRequest, TransactionModel>
{
}
