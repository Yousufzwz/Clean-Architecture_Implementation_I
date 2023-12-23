using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumAccess.Domain.Entities;

public class Content : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public uint Duration { get; set; }
}
