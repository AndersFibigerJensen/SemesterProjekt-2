using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using Microsoft.Data.SqlClient;

namespace SemesterProjekt_2.Services
{
    // Anders
    public class MemberService :Connection, IMemberService
    {
        //sql code
        private string Memberstrings = " select * from Members";
        private string MemberfromID = " select * from Members where memberid=@ID";
        private string Membersearch = " select * from Members where Name Like '%'+@Name+'%'";
        private string MemberUpdate = "update Member where MemberID=@ID";
        private string MemberDelete = "delete from hotel where MemberID=@ID";
        private string MemberAdd = "insert into Members values(@ID,@Name,@Password,@Email,@Address,@isFamily,@Course,@isAdmin)";


        public MemberService(IConfiguration configuration):base(configuration)
        {

        }

        public async Task AddMemberAsync(Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(MemberAdd, connection))
                {

                }
            }
        }

        public Task DeleteMemberAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(MemberAdd, connection))
                {

                }
            }
            return null;
        }

        public Task<List<Member>> FilterMembersAsync(string filter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(MemberAdd, connection))
                {

                }
            }
            return null;
        }

        public Task<List<Member>> GetAllMembersAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(MemberAdd, connection))
                {

                }
            }
            return null;
        }

        public Task<Member> GetMemberByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(MemberAdd, connection))
                {

                }
            }
            return null;
        }

        public Task UpdateMemberAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(MemberAdd, connection))
                {

                }
            }
            return null;
        }
    }
}
