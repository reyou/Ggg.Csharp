using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using intro1.Interfaces;

namespace intro1.Implementations
{
    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton, IOperationSingletonInstance
    {
        public Operation() : this(Guid.NewGuid())
        {

        }
        public Operation(Guid guid)
        {
            OperationId = guid;
        }

        public Guid OperationId { get; set; }
    }
}
