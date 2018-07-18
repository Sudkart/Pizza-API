using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace STM.Core.Repositories
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
