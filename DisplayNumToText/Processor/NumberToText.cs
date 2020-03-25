using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayNumToText.Logger;

namespace DisplayNumToText.Processor
{
    public class NumberToText<T> where T : IComparable
    {
        private T _low = default(T);
        private T _high = default(T);
        private T _a = default(T);
        private T _b = default(T);
        List<T> inputs;
        
        /// <summary>
        /// It initializes the object properties with inputs
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void SetValues(T low, T high, T a, T b)
        {
            inputs = new List<T>();

            _low = low;
            _high = high;
            _a = a;
            _b = b;

            AddToList(_low, _high, _a, _b);
        }

        /// <summary>
        /// It processes the input like sort, validate and convert to text and returns the result else returns error message
        /// </summary>
        /// <returns></returns>
        public string Process()
        {
            string message;
  
            Sort();
            var isValid = Validate(out message);
            if (isValid)
            {
                message = NumToText();
            }

            return message;
        }

        /// <summary>
        /// It adds inputs to list of type T
        /// </summary>
        /// <param name="values"></param>
        public void AddToList(params T[] values)
        {
            foreach(T value in values)
            {
                inputs.Add(value);
            }

            FileLogger.Instance.LogToFile($"User entered: {String.Join(",", values)}");
        }

        /// <summary>
        /// It sorts the list from low to high
        /// </summary>
        public void Sort()
        {
            inputs.Sort();
        }

        /// <summary>
        /// It validates the input and returns error message
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Validate(out string errorMessage)
        {
            T min = inputs.First();
            T max = inputs.Last();
            errorMessage = string.Empty;

            if (_low.CompareTo(min) > 0)
            {
                errorMessage = "Error: Low should be lesser value" + Environment.NewLine;
            }

            if (_high.CompareTo(max) < 0)
            {
                errorMessage += "Error: High should be higher value";
            }

            if (inputs.Any<T>(value => value.CompareTo(0) < 0))
            {
                errorMessage += "Error: Input should be > 0";
            }

            //if (min == max)
            //{
            //    errorMessage += "Error: Low should not be equal to High";
            //}

            return (errorMessage.Length > 0) ? false : true;
        }

        /// <summary>
        /// It iterates each number from low to high and converts into text if conditions met
        /// </summary>
        /// <returns></returns>
        public string NumToText()
        {
            StringBuilder builder = new StringBuilder("Output -");
            builder.Append(Environment.NewLine);

            inputs.ForEach(input =>
            {
                var output = ConvertToText(input);
                builder.Append($"{input.ToString()}: {output}");
                builder.Append(Environment.NewLine);
            });

            return builder.ToString();
        }

        /// <summary>
        /// It converts the number to text
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string ConvertToText(T num)
        {
            dynamic aValue = _a;
            dynamic bValue = _b;

            if (num % aValue == 0 && num % bValue == 0)
            {
                return "Fancy Pants";
            }
            else if (num % aValue == 0)
            {
                return "Fancy";
            }
            else if (num % bValue == 0)
            {
                return "Pants";
            }
            return num.ToString();
        }
    }
}
