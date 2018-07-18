using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STM.Core.EntityLayer;

namespace STM.Core.Repositories
{
    public interface IRunSuiteRepository
    {

        Task<IQueryable<Countries>> GetEntities();

        Task<IQueryable<RunSuite>> GetRunSuitesByEntity(string entityId, string packId,string userName);

        Task<IQueryable<RunSuite>> GetRunSuites(string userName);
        Task<IQueryable<Common>> GetBrowser();
        Task<IQueryable<Common>> GetEnvironemnts();

        Task<IQueryable<Common>> GetNodes();

        void PostUserTestRun(TblUserTestRun tblUserTestRun);

        Task<string> PostTestRunTemp(List<TblTestRunTemp> tblTestRunTemp);

        Task<IQueryable<String>> GetSubScenariosByPackId(string packId);

        Task<IQueryable<String>> GetScenariosByPackId(string packId);

        Task<String> GetProjectNameByProjectId(string projectId);

        Task<String> GetPackNameByPackId(string packId);

        Task<TblNode> GetNodesData(string nodeName);

        Task<IQueryable<String>> GetSubScenIdBySubScenarioIdAndProjectId(string subscenarioId, string projectId);

        Task<IQueryable<TblTestSteps>> GetTestStepsBySceneIdAndSubSceneId(string sceneId, string subsceneId, string nodeIp, string nodePortNo);

        Task<String> GetProjectCodeByProjectId(string projectId);

        void DeleteTableTestStepsByProjectCode(string projectCode, string projectId);

        void DeleteTableTestRunTempByTestGroupId(string testGroupId);

        void UpdateTblCurrProjsByPackId(string packId);

        void UpdateTblCurrProjsByGroupId(string testGroupId);

        Task<String> GetPackIdByPackName(string packId);

        Task<TestGroupandTestRunId> GetTestRunIdAndGroupIdByPackId(string packId);

        Task<TblPacks> GetPackByPackId(string packId);

        Task<IQueryable<String>> GeActiveRunSuites();

    }
}
