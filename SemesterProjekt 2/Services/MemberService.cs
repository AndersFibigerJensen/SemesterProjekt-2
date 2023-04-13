using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Services
{
    // Anders
    public class MemberService : IMemberService
    {
        //sql code
        private string Memberstrings = " select * from Members";
        private string MemberfromID = " select * from Members where memberid=@ID";
        private string Membersearch = " select * from Members where Name Like '%'+@Name+'%'";
        private string MemberUpdate = "update Member where MemberID=@ID";
        private string MemberDelete = "delete from hotel where MemberID=@ID";
        private string MemberAdd = "insert into Members values(@ID,@Name,@Password,@Email,@Address,@isFamily,@Course,@isAdmin)";

        public Task AddMemberAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteMemberAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Member>> FilterMembersAsync(string filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<Member>> GetAllMembersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Member> GetMemberByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMemberAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
