using Infrastructure.Guard.Exceptions;

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

        public static void ValueMinIsNotValid(int value, int minValue, string message)
        {
            if (value < minValue)
            {
                throw new IntValueIsNegativeException(message);
            }
        }
    }
}