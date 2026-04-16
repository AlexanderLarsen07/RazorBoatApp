using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using SailClubLibrary.Data;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace SailClubLibrary.Services
{
    /// <summary>
    /// Class for Constructing and calling Member Repository Objects using the interface
    /// </summary>
    public class MemberRepository : Connection, IMemberRepoAsync
    {
        #region Instance Fields
        private Dictionary<string, Member> _members;
        private string _queryString = "select * from SailMember";
        private string _insertSql = "insert into SailMember values(@FirstName, @SurName, @PhoneNo, @Address, @City, @Mail, @Type, @Role, @Image)";
        private string _queryCount = "select count(*) from SailMember";
        private string _deleteSql = "delete from sailmember where Member_ID = @ID";
        private string _updateSql = "update SailMember set Member_FirstName = @FirstName, Member_SurName = @SurName, Member_Phonenumber = @PhoneNo, Member_Address = @Address, Member_City = @City, Member_Mail = @Mail, Member_Membertype= @Type, Member_MemberRole = @Role, Member_Image = @Image where Member_ID = @ID";
        private string _searchSql = "select * from SailMember where Member_ID = @ID";
        #endregion

        #region Constructor
        /// <summary>
        /// MemberRepository constructor used for making a new member repository called _members with string as key and IMember as value
        /// </summary>
        public MemberRepository()
        {
            //_members = new Dictionary<string, Member>();
            _members = new MockData().MemberData;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Count used for counting members in _members repository
        /// </summary>
        public async Task<int> Count()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(_queryCount, conn))
            {
                await conn.OpenAsync();
                object? result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        // Formål:
        // Tilføje Medlem
        // if-statement:
        // Hvis Dictionary _members ikke indeholder Telefonnummer på det Medlem man vil tilføje. Tilføjes Medlemmet
        // Else if:
        //Medlem bliver ikke tilføjet

        /// <summary>
        /// Method for adding members to our repository, which runs a check to tell if the phone number is available
        /// </summary>
        public async Task AddMember(Member member)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(_insertSql, connection);
                await command.Connection.OpenAsync();
                //command.Parameters.AddWithValue("@ID", member.Id);
                command.Parameters.AddWithValue("@FirstName", member.FirstName);
                command.Parameters.AddWithValue("@SurName", member.SurName);
                command.Parameters.AddWithValue("@PhoneNo", member.PhoneNumber);
                command.Parameters.AddWithValue("@Address", member.Address);
                command.Parameters.AddWithValue("@City", member.City);
                command.Parameters.AddWithValue("@Mail", member.Mail);
                command.Parameters.AddWithValue("@Type", member.TheMemberType);
                command.Parameters.AddWithValue("@Role", member.TheMemberRole);
                command.Parameters.AddWithValue("@Image", member.MemberImage);
                await command.ExecuteNonQueryAsync();
            }
        }
        // Formål:
        // At få fat på en list med alle medlemmer/objekter
        // Metoden returnere via en indbygget metode som hedder ToList(); som henter liste med _members Values

        /// <summary>
        /// Method for returning a list of members
        /// </summary>
        public async Task<List<Member>> GetAllMembers()
        {
            List<Member> members = new List<Member>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(_queryString, connection);
                await command.Connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    int memberId = reader.GetInt32("Member_Id");
                    string firstName = reader.GetString("Member_FirstName");
                    string surName = reader.GetString("Member_SurName");
                    string phoneNumber = reader.GetString("Member_PhoneNumber");
                    string memberAddress = reader.GetString("Member_Address");
                    string city = reader.GetString("Member_City");
                    string mail = reader.GetString("Member_Mail");
                    MemberType memberType = Enum.GetValues<MemberType>()[reader.GetInt32("Member_MemberType")];
                    MemberRole memberRole = Enum.GetValues<MemberRole>()[reader.GetInt32("Member_MemberRole")];
                    Member member = new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, memberType, memberRole);
                    member.MemberImage = reader.GetString("Member_Image");
                    members.Add(member);
                }
                reader.Close();
            }
            return members;
        }

        // Formål:
        // Fjerne Medlem
        // Metoden sletter via metoden Remove, og sletter telefonnummeret fra _members

        /// <summary>
        /// Method for removing a member from the dictionary, using their phone number
        /// </summary>
        public async Task RemoveMember(Member member)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(_deleteSql, conn);
                await sqlCommand.Connection.OpenAsync();
                sqlCommand.Parameters.AddWithValue("@ID", member.Id);
                await sqlCommand.ExecuteNonQueryAsync();
            }
        }
        // Formål:
        // Opdatere Medlem
        // if-statement:
        // Hvis _members indholder Telefonnummeret argumentet, så overskrider de nye værdier de nuværende med samme telefonnummer.

        /// <summary>
        /// Method to update a member's info, using their phone number to distinguish them
        /// </summary>
        public async Task UpdateMember(Member updatedMember)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(_updateSql, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ID", updatedMember.Id);
                command.Parameters.AddWithValue("@FirstName", updatedMember.FirstName);
                command.Parameters.AddWithValue("@SurName", updatedMember.SurName);
                command.Parameters.AddWithValue("@PhoneNo", updatedMember.PhoneNumber);
                command.Parameters.AddWithValue("@Address", updatedMember.Address);
                command.Parameters.AddWithValue("@City", updatedMember.City);
                command.Parameters.AddWithValue("@Mail", updatedMember.Mail);
                command.Parameters.AddWithValue("@Type", updatedMember.TheMemberType);
                command.Parameters.AddWithValue("@Role", updatedMember.TheMemberRole);
                command.Parameters.AddWithValue("@Image", updatedMember.MemberImage);
                await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Searches through the member dictionary and returns the member with the given phonenumber. 
        /// </summary>


        public async Task<Member?> SearchMember(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(_searchSql, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    int memberId = reader.GetInt32("Member_ID");
                    string firstName = reader.GetString("Member_FirstName");
                    string surName = reader.GetString("Member_SurName");
                    string phoneNumber = reader.GetString("Member_PhoneNumber");
                    string memberAddress = reader.GetString("Member_Address");
                    string city = reader.GetString("Member_City");
                    string mail = reader.GetString("Member_Mail");
                    MemberType memberType = Enum.GetValues<MemberType>()[reader.GetInt32("Member_MemberType")];
                    MemberRole memberRole = Enum.GetValues<MemberRole>()[reader.GetInt32("Member_MemberRole")];
                    Member member = new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, memberType, memberRole);
                    member.MemberImage = reader.GetString("Member_Image");
                    reader.Close();
                    return member;
                }
                return null;
            }
        }
    }
    #endregion
}