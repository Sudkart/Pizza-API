using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STM.Core.EntityLayer;
using System.Threading.Tasks;


namespace STM.Core.Repositories
{
    public interface ITestStepRepository
    {
        Task<IQueryable<TestStep>> GetTestStepsAsync(int projectId, int scenId, int subscenId);

        Task<bool> CreateTestStep(TestStep testStep);

        Task<bool> UpdateTestStep(TestStep testStep, int teststepId);


        Task<bool> DeleteTestStep(int TestStepId, int ProjectId);

        Task<int> DeleteAllTestStepsfromSubscenerio(int SubScenId, int ProjectId);



    }
}
