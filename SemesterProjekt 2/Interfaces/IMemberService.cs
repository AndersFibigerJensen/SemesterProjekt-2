using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Interfaces
{

    //Anders
    public interface IMemberService
    {

        public Task<List<Member>> GetAllMembers();

        public Task<Member> GetMemberById(int id);

        public Task AddMember();

        public Task DeleteMember(int id);

        public Task UpdateMember(int id);

        public Task<List<Member>> FilterMembers(string filter);


    }
}
