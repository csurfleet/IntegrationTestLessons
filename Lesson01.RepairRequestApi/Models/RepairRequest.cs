namespace Lesson01.RepairRequestApi.Models
{
    public readonly struct RepairRequest
    {
        public RepairRequest(RepairType repairType, int requestingUserId, string repairAddress)
        {
            RepairType = repairType;
            RequestingUserId = requestingUserId;
            RepairAddress = repairAddress;
        }

        public RepairType RepairType { get; }

        public int RequestingUserId { get; }

        public string RepairAddress { get; }
    }
}
