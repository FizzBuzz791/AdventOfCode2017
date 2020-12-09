using System.Linq;

namespace AdventOfCode.Year2020.Day2
{
    public class PasswordPolicy
    {
        private readonly PasswordPolicyType _policyType;
        private readonly int _firstNumber;
        private readonly int _secondNumber;
        private readonly char _desiredCharacter;

        public PasswordPolicy(string policy, PasswordPolicyType policyType)
        {
            _policyType = policyType;
            
            string[] policyParts = policy.Split(" ");
            
            _firstNumber = int.Parse(policyParts[0].Split('-')[0]);
            _secondNumber = int.Parse(policyParts[0].Split('-')[1]);
            _desiredCharacter = char.Parse(policyParts[1]);
        }

        public bool IsValid(string password)
        {
            switch (_policyType)
            {
                case PasswordPolicyType.Count:
                {
                    int charCount = password.Count(c => c == _desiredCharacter);
                    return _firstNumber <= charCount && charCount <= _secondNumber;
                }
                case PasswordPolicyType.Index:
                {
                    if (_firstNumber <= password.Length && _secondNumber <= password.Length)
                    {
                        if (password[_firstNumber - 1] == _desiredCharacter &&
                            password[_secondNumber - 1] != _desiredCharacter)
                        {
                            return true;
                        }

                        if (password[_firstNumber - 1] != _desiredCharacter &&
                            password[_secondNumber - 1] == _desiredCharacter)
                        {
                            return true;
                        }
                    }

                    return false;
                }
                default:
                    return false;
            }
        }
    }
}