using Microsoft.EntityFrameworkCore;
using PremiumAccess.Application;
using PremiumAccess.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumAccess.Infrastructure;

public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
{
    public IContentRepository ContentRepository { get; private set; }

    public ApplicationUnitOfWork(IContentRepository courseRepository,
        IApplicationDbContext dbContext) : base((DbContext)dbContext)
    {
        ContentRepository = courseRepository;
    }

}

