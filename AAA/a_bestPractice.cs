using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAA
{
    internal class a_bestPractice
    {
        /*NAMING
        PascalCase: ClassName, MethodName(), PropertyName
        camelCase: localVar, paramName
        _camelCase: _privateField
        PascalCase: CONSTANT_VALUE
            */
        // Class: PascalCase - Starts with uppercase, no underscores
        public class UserProfile
        {
            // Constant: PascalCase - All uppercase words combined
            public const int MaxLoginAttempts = 5;

            // Private field: _camelCase - Underscore prefix, then lowercase start
            private string _userName;

            // Property: PascalCase - Public, uppercase start
            public string UserName
            {
                get { return _userName; }
                set { _userName = value; }
            }

            // Method: PascalCase - Action words, uppercase start
            public void UpdateProfile(string newName, int age)  // Parameters: camelCase - Lowercase start
            {
                // Local variable: camelCase - Temporary, lowercase start
                int updatedAge = age + 1;  // Example calculation

                // Use private field and constant
                if (updatedAge > MaxLoginAttempts)
                {
                    _userName = newName;
                }
            }
        }


    }
}
