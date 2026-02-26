using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Exceptions
{
    /// <summary>
    /// Exception for when a member's phone number already exists
    /// </summary>
    public class MemberPhoneNumberExistsException : Exception
    {
        public MemberPhoneNumberExistsException(string message) : base(message)
        {

        }
    }
}
