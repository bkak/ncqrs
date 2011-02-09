using System;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Ncqrs.Commanding
{
    /// <summary>
    /// A command message. A command should contain all the information and
    /// intend that is needed to execute an corresponding action.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets the unique identifier for this command.
        /// </summary>
        [ExcludeInMapping]
        Guid CommandIdentifier
        { 
            get;
        }
        /// <summary>
        /// Gets the Composite Command Identifier for this command.
        /// </summary>
        [ExcludeInMapping]
        Guid CompositeCommandIdentifier
        {
            get;
        }
        /// <summary>
        /// Gets the unique sequence no for this command.
        /// </summary>
        [ExcludeInMapping]
        Int16 SequenceNo
        {
            get;
        }
        void SetCompositeCommandIdentifier(Guid compositeCommandIdentifier);
        void SetSequenceNo(short sequenceNo);
    }
}
