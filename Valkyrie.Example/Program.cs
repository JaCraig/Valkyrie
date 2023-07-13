using System.ComponentModel.DataAnnotations;

namespace Valkyrie.Example
{
    /// <summary>
    /// Example class to show how to use the validation attributes and the TryValidate method in Valkyrie
    /// </summary>
    public class ExampleClass
    {
        /// <summary>
        /// Gets or sets the compare property. This needs to be greater than 5 for the validation to pass.
        /// </summary>
        /// <value>
        /// The compare property.
        /// </value>
        [Compare(5, ComparisonType.GreaterThan)]
        public int CompareProperty { get; set; }

        /// <summary>
        /// Gets or sets the compare to property. This needs to be greater than CompareProperty for the validation to pass.
        /// </summary>
        /// <value>
        /// The compare to property.
        /// </value>
        [CompareTo("CompareProperty", ComparisonType.GreaterThan)]
        public int CompareToProperty { get; set; }

        /// <summary>
        /// Gets or sets the not in range property. This needs to be between 5 and 10 for the validation to pass.
        /// </summary>
        /// <value>
        /// The not in range property.
        /// </value>
        [NotInRange(5, 10)]
        public int NotInRangeProperty { get; set; }
    }

    /// <summary>
    /// Example program to show how to use the validation attributes and the TryValidate method in Valkyrie
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            // Let's create an object to test
            var MyObject = new ExampleClass();
            MyObject.CompareProperty = 10;
            MyObject.CompareToProperty = 5;
            MyObject.NotInRangeProperty = 15;

            // And a list to store the results
            var Results = new List<ValidationResult>();

            // This will fail because ComparePropertyTo is less than CompareProperty
            Console.WriteLine("Is Valid: {0}", MyObject.TryValidate(Results));
            // Let's display the error message
            foreach (var Result in Results)
                Console.WriteLine(Result.ErrorMessage);
        }
    }
}