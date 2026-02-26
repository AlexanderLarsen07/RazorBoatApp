using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    public interface IMemberRepository
    {
        int Count { get; }
        void AddMember(Member member);
        void RemoveMember(Member member);
        void UpdateMember(Member member);
        List<Member> GetAllMembers();
        void PrintAll();
        Member? SearchMember(string phoneNumber);
        List<Member> FilterMembers(string filterCriteria);
    }
}
