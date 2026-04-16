using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailClubLibrary.Helpers.Attributes;

namespace SailClubLibrary.Models
{
    public class Member : IComparable<Member>
    {
        #region Instance Fields
        #endregion

        #region Properties
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Mail { get; set; }
        public MemberType TheMemberType { get; set; }
        public MemberRole TheMemberRole { get; set; }
        [IgnoreUpdate]
        public int Id { get; set; }

        public string MemberImage { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor used for creating new member objects
        /// </summary>
        /// 

        public Member()
        {

        }
        public Member(int id, string name, string surName, string phoneNumber, string address, string city, string mail, MemberType theMemberType, MemberRole theMemberRole)
        {
            FirstName = name;
            SurName = surName;
            PhoneNumber = phoneNumber;
            Address = address;
            City = city;
            Mail = mail;
            TheMemberType = theMemberType;
            TheMemberRole = theMemberRole;
            Id = id;
        }

        #endregion

        #region Methods
        /// <summary>
        /// ToString method used for printing out member information
        /// </summary>
        public override string ToString()
        {
            return $"Medlemsnummer: {Id}\nFornavn: {FirstName}\nEfternavn: {SurName}\nTelefonnummer: {PhoneNumber}\n" +
                $"Adresse: {Address}\nBy: {City}\nEmail: {Mail}\nType: {TheMemberType}\n" +
                $"Rolle: {TheMemberRole}";
        }
        public string FilterAll()
        {
            return $"{FirstName ?? ""} {SurName ?? ""} {PhoneNumber ?? ""} {Address ?? ""} {City ?? ""} {Mail ?? ""}";
        }

        public int CompareTo(Member? member)
        {
            if (member == null) return 1;
            return Id.CompareTo(member.Id);
        }
        #endregion
    }
}
