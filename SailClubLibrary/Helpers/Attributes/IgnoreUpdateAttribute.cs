using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Helpers.Attributes
{
    /// <summary>
    /// Specifies that a property should be ignored during update operations.
    /// </summary>
    /// <remarks>
    /// This attribute can be used by reflection-based update logic to exclude
    /// specific properties from being modified when applying updates to an object.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreUpdateAttribute : Attribute
    {
    }
}
