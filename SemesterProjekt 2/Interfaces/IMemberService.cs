using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Interfaces
{

    //Anders
    public interface IMemberService
    {

        public Task<List<Member>> GetAllMembersAsync();

        public Task<Member> GetMemberByIdAsync(int id);

        public Task AddMemberAsync(Member member);

        public Task DeleteMemberAsync(int id);

        public Task UpdateMemberAsync(int id);

        public Task<List<Member>> FilterMembersAsync(string filter);


    }
}
