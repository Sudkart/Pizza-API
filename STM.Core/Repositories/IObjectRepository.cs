using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STM.Core.EntityLayer;
using System.Threading.Tasks;

namespace STM.Core.Repositories
{
    public interface IObjectRepository
    {
        Task<IQueryable<TestObject>> GetAllObjects();
        Task<IQueryable<TestObject>> GetObjects(string objectName);
        Task<IQueryable<TestObject>> GetObjectsByName(string objectName, string projectName);
        Task<IQueryable<TestObject>> GetObjectsByNameMatch(string objectName);
        Task<IQueryable<TestObject>> GetObjectsByName(string projectName);
        Task<IQueryable<TestObject>> GetObjectsById(int objectId);
        Task<bool> UpdateObject(TestObject testObject, int objectId);
        Task<int> CreateObject(TestObject testObject);
        Task<int> CreateObjectWithReturnId(TestObject testObject);

    }

}
