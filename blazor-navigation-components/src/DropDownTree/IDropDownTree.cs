using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Interface for SfDropDownTree.
    /// </summary>
    public interface IDropDownTree
    {
        /// <summary>
        /// This method updates the child properties of Dropdown Tree.
        /// </summary>
        /// <param name="details">Specifies the property value parameter.</param>
        public Task UpdateChildProperties(object details);
    }
}
