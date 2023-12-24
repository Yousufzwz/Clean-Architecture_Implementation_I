using PremiumAccess.Domain;
using PremiumAccess.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumAccess.Application;

public interface IApplicationUnitOfWork : IUnitOfWork
{
    IContentRepository ContentRepository { get; }
}