using System;
using System.Collections.Generic;
using System.Text;

namespace PremiumAccess.Domain;

public interface IUnitOfWork : IDisposable
{
    Task SaveAsync();
}