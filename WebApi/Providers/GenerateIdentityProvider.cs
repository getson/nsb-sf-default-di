using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Providers
{
    public interface IGenerateIdentityProvider
    {
        Guid Generate();
    }
    public class GenerateIdentityProvider : IGenerateIdentityProvider
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}
