using FluentResults;

namespace Aplicacao.UseCase.UseCaseBase;

public abstract class IUseCaseBase<TI, TO>
    where TI: UseCaseBaseInput
    where TO: UseCaseBaseOutput
{
    public IUseCaseBase(){}

    protected abstract Result<TO> executeUseCase(TI input);
}