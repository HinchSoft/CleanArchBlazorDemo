using CommonCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Model;

public class Author:Entity
{
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public int Order { get; set; }
}
