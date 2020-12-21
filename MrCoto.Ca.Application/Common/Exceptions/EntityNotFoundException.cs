using System.Collections.Generic;
using MrCoto.Ca.Domain.Common.Exceptions;

namespace MrCoto.Ca.Application.Common.Exceptions
{
    public class EntityNotFoundException : BusinessException
    {
        public const string Code = "SYS:NO_ENTITY";

        public EntityNotFoundException()
            : base(Code, "Entidad no encontrada")
        {
        }

        public EntityNotFoundException(string message)
            : base(Code, message)
        {
        }
        
        public EntityNotFoundException(string name, string key)
            : base(Code, $"Entidad de tipo \"{name}\" ({key}) no encontrada.")
        {
        }

        public EntityNotFoundException(string name, long key)
            : base(Code, $"Entidad de tipo \"{name}\" ({key}) no encontrada.")
        {
        }
        
        public EntityNotFoundException(string name, List<long> keys)
            : base(Code, $"Entidades de tipo \"{name}\" ({string.Join(", ", keys)}) no encontrada.")
        {
        }

    }
}