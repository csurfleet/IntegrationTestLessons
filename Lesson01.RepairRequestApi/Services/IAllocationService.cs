using Lesson01.RepairRequestApi.Models;

namespace Lesson01.RepairRequestApi.Services
{
    public interface IAllocationService
    {
        void AllocateRepair(RepairRequest request);
    }

    internal class AllocationService : IAllocationService
    {
        public void AllocateRepair(RepairRequest request)
        {
            // Do nothing
        }
    }
}
