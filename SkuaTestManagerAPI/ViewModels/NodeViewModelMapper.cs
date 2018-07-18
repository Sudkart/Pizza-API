using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STM.Core.EntityLayer;

namespace STMAPI.ViewModels
{
    public static class NodeViewModelMapper
    {

        public static NodeViewModel ToViewModel(this Node entity)
        {
            return new NodeViewModel
            {
                NodeId = entity.NodeId,
                NodeIp = entity.NodeIp,
                NodeName = entity.NodeName,
                NodePortNo = entity.NodePortNo,
                HostName = entity.HostName,
                HostIp= entity.HostIp,
                HostPortNo = entity.HostPortNo,
                Active = entity.Active,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate


            };
        }

        public static Node ToEntity(this NodeViewModel viewModel)
        {

            return new Node
            {

                NodeId =viewModel.NodeId,
                NodeIp = viewModel.NodeIp,
                NodeName = viewModel.NodeName,
                NodePortNo = viewModel.NodePortNo,
                HostName = viewModel.HostName,
                HostIp = viewModel.HostIp,
                HostPortNo = viewModel.HostPortNo,
                Active = viewModel.Active,
                CreatedBy = viewModel.CreatedBy,
                CreatedDate = viewModel.CreatedDate,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedDate = viewModel.UpdatedDate
            };


        }




    }
}
