using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STM.Core.EntityLayer;
using System.Threading.Tasks;

namespace STM.Core.Repositories
{
   public  interface INodeRepository
    {
        Task<IQueryable<Node>> GetNodes();

        Task<bool> UpdateNode(Node Node, int nodeId);

        Task<bool> CreateNode(Node Node);

        Task<bool> DeleteNode(int nodeId);
    }
}
