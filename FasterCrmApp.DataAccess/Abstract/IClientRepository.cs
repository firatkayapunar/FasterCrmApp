﻿using FasterCrmApp.DataAccess.Abstract.Base;
using FasterCrmApp.Entities;

namespace FasterCrmApp.DataAccess.Abstract
{
    public interface IClientRepository :
                     IRepository<Client>
    { }
}