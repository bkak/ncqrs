using Ncqrs.CompositeCommanding;

namespace Ncqrs.Commanding.ServiceModel
{
    public interface ICommandService
    {
        void Execute(ICommand command);
        void Execute(ICompositeCommand compositeCommand);
    }
}