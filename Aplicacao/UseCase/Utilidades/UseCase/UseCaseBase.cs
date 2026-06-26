using FluentResults;

namespace Aplicacao.UseCase.UseCasePadrao;

public abstract class UseCaseBase<TI, TO>
    where TI : UseCaseBaseInput
    where TO : UseCaseBaseOutput
{
    public Result<TO> Execute(TI input)
    {
        return ExecuteUseCase(input);
    }

    protected abstract Result<TO> ExecuteUseCase(TI input);
}