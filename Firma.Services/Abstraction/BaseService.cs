using Firma.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Services.Abstraction
{
    public abstract class BaseService
    {

        protected readonly FirmaContext _context;

        public BaseService(FirmaContext context)
        {
            _context = context;
        }
    }
}
