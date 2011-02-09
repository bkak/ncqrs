using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;

namespace Ncqrs.CompositeCommanding
{
    public interface ICompositeCommand
    {
        /// <summary>
        /// Gets the unique identifier for this command.
        /// </summary>
        Guid CompositeCommandIdentifier
        {
            get;
        }
        IEnumerable<ICommand> Commands { get; }
    }
}
