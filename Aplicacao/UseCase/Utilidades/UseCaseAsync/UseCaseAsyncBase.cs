using Aplicacao.UseCase.UseCasePadrao;
using FluentResults;

namespace Aplicacao.UseCase.UseCaseAsync;

public abstract class UseCaseAsyncBase<TI, TO>
    where TI : UseCaseAsyncBaseInput
    where TO : UseCaseAsyncBaseOutput
{
    public Task<Result<TO>> Execute(TI input)
    {
        return ExecuteUseCase(input);
    }

    protected abstract Task<Result<TO>> ExecuteUseCase(TI input);
}