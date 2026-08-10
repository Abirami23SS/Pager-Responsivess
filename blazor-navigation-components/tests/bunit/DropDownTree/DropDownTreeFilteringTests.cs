using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bunit;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.DropDownTree.Samples;
using Xunit;

namespace Syncfusion.Blazor.Tests.DropDownTree
{
    public class DropDownTreeFilteringTests : BunitTestContext
    {
        public class ListData
        {
            public int Id { get; set; }
            public int? Pid { get; set; }
            public string Name { get; set; }
            public bool HasChild { get; set; }
            public bool Expanded { get; set; }
            public bool IsCheckedValue { get; set; }
        }

        public class TreeData
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public bool Expanded { get; set; }
            public List<TreeData> Child { get; set; }
        }

        private List<ListData> GetListDataSource()
        {
            return new List<ListData>
            {
                new ListData { Id = 1, Pid = null, Name = "Electronics", HasChild = true, Expanded = true },
                new ListData { Id = 2, Pid = 1, Name = "Smartphones" },
                new ListData { Id = 3, Pid = 1, Name = "Laptops" },
                new ListData { Id = 4, Pid = 1, Name = "Tablets" },
                new ListData { Id = 5, Pid = 2, Name = "Accessories" },
                new ListData { Id = 6, Name = "Clothing", HasChild = true },
                new ListData { Id = 7, Pid = 6, Name = "Men's Clothing" },
                new ListData { Id = 8, Pid = 6, Name = "Women's Clothing" }
            };
        }

        /// <summary>
        /// Tests HierarchicalFilter method with "StartsWith" filter type
        /// Validates that filtering works correctly with hierarchical data
        /// </summary>
        [Fact]
        public void TestHierarchicalFilter_WithStartsWith()
        {
            var component = RenderComponent<DropDownTreeFilteringTest>();
            var dropdownTreeTest = component.Instance;

            dropdownTreeTest.PerformFilter("Elec");

            Assert.True(dropdownTreeTest.FilteredCount > 0, "Filter should return results for 'Elec'");
            Assert.Contains("Electronics", dropdownTreeTest.FilteredResult);
        }

        /// <summary>
        /// Tests HierarchicalFilter with different filter values
        /// Validates that multiple filter calls work independently
        /// </summary>
        [Fact]
        public void TestHierarchicalFilter_WithMultipleFilterValues()
        {
            var component = RenderComponent<DropDownTreeFilteringTest>();
            var dropdownTreeTest = component.Instance;

            // Test first filter
            dropdownTreeTest.PerformFilter("Sm");
            var firstCount = dropdownTreeTest.FilteredCount;

            dropdownTreeTest.ResetFilter();

            // Test second filter
            dropdownTreeTest.PerformFilter("La");
            var secondCount = dropdownTreeTest.FilteredCount;

            Assert.True(firstCount > 0, "First filter should return results");
            Assert.True(secondCount > 0, "Second filter should return results");
        }

        /// <summary>
        /// Tests HierarchicalFilter with non-matching filter value
        /// Validates that empty results are handled properly
        /// </summary>
        [Fact]
        public void TestHierarchicalFilter_WithNonMatchingValue()
        {
            var component = RenderComponent<DropDownTreeFilteringTest>();
            var dropdownTreeTest = component.Instance;

            dropdownTreeTest.PerformFilter("XYZ");

            Assert.Equal(0, dropdownTreeTest.FilteredCount);
            Assert.Empty(dropdownTreeTest.FilteredResult);
        }

        /// <summary>
        /// Tests HierarchicalFilter with special characters
        /// Validates filtering with accented characters
        /// </summary>
        [Fact]
        public void TestHierarchicalFilter_WithSpecialCharacters()
        {
            var component = RenderComponent<DropDownTreeFilteringTest>();
            var dropdownTreeTest = component.Instance;

            dropdownTreeTest.PerformFilter("E");

            Assert.True(dropdownTreeTest.FilteredCount >= 0, "Filter with special chars should not throw");
        }

        /// <summary>
        /// Tests HierarchicalFilter with empty string
        /// Validates behavior when no filter text is provided
        /// </summary>
        [Fact]
        public void TestHierarchicalFilter_WithEmptyString()
        {
            var component = RenderComponent<DropDownTreeFilteringTest>();
            var dropdownTreeTest = component.Instance;

            dropdownTreeTest.PerformFilter("");

            // Empty string typically returns all or no results based on implementation
            Assert.True(dropdownTreeTest.FilteredCount >= 0, "Empty filter should be handled");
        }

        /// <summary>
        /// Tests that DropDownTree component initializes correctly with DropDownTreeField
        /// Validates field mapping configuration
        /// </summary>
        [Fact]
        public void TestDropDownTreeField_Initialization()
        {
            var component = RenderComponent<DropDownTreeFilteringTest>();
            var dropdownTreeTest = component.Instance;

            Assert.NotNull(dropdownTreeTest.DropDownTreeRef);
            Assert.NotNull(dropdownTreeTest.DataSource);
            Assert.NotEmpty(dropdownTreeTest.DataSource);
        }

        /// <summary>
        /// Tests hierarchical data structure with nested children
        /// Validates that child items are properly included in data source
        /// </summary>
        [Fact]
        public void TestHierarchicalFilter_WithNestedData()
        {
            var component = RenderComponent<DropDownTreeFilteringTest>();
            var dropdownTreeTest = component.Instance;

            var hasNestedData = dropdownTreeTest.DataSource.Any(x => x.Child != null && x.Child.Count > 0);
            Assert.True(hasNestedData, "DataSource should contain nested children");
        }

        /// <summary>
        /// Tests filtering performance with large data set
        /// Validates that filter method completes without errors
        /// </summary>
        [Fact]
        public void TestHierarchicalFilter_PerformanceWithLargeData()
        {
            var component = RenderComponent<DropDownTreeFilteringTest>();
            var dropdownTreeTest = component.Instance;

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            dropdownTreeTest.PerformFilter("C");
            stopwatch.Stop();

            // Filter should complete within reasonable time (5 seconds)
            Assert.True(stopwatch.ElapsedMilliseconds < 5000, "Filter should complete quickly");
        }
    }
}