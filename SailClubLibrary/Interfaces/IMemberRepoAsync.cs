using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailClubLibrary.Models;

namespace SailClubLibrary.Interfaces
{
    /// <summary>
    /// An interface for the Member class, to interact with an SQL DB
    /// </summary>
    public interface IMemberRepoAsync
    {
        #region Methods
        Task<int> Count();
        Task AddMember(Member member);
        Task RemoveMember(Member member);
        Task UpdateMember(Member member);
        Task<List<Member>> GetAllMembers();
        Task<Member?> SearchMember(int id);
        #endregion
    }
}
