using Infrastructure.Guard.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Guard
{
    public static class Validator
    {
        public static void StringIsNullOrEmpty(string str, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new StringIsNullEmptyOrWhiteSpaceException(errorMessage);
            }
        }

        public static void StringMustBeNullOrEmpty(string str, string errorMessage)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                throw new StringIsNotNullEmptyOrWhiteSpaceException(errorMessage);
            }
        }

        public static void ObjectIsNull(object obj, string message)
        {
            if (obj == null)
            {
                throw new ObjectIsNullException(message);
            }
        }

        public static void ObjectNotNull(object obj, string message)
        {
            if (obj != null)
            {
                throw new ObjectIsNullException(message);
            }
        }

        public static void StringEqualsString(string str1, string str2, string message)
        {
            if (str1 != str2)
            {
                throw new StringNotEqualToAnotherStringException(message);
            }
        }

        public static void ValueMinIsNotValid(int value, string message)
        {
            if (value < 1)
            {
                throw new IntValueIsNegativeException(message);
            }
        }

    }
}
