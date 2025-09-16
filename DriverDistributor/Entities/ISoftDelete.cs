using DocumentFormat.OpenXml.Drawing;

namespace DriverDistributor.Entities;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}