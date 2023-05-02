using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Interfaces
{

    //Anders
    public interface IMemberService
    {
        /// <summary>
        /// Henter alle medlemmer fra databasen ned på en liste
        /// </summary>
        /// <returns>returnere en liste med alle medlemmer</returns>
        public Task<List<Member>> GetAllMembersAsync();

        /// <summary>
        /// Henter et medlem fra databasen
        /// </summary>
        /// <param name="id">Tager medlemmets id som paramaeter</param>
        /// <returns>returnere et medlem</returns>
        public Task<Member> GetMemberByIdAsync(int id);

        /// <summary>
        /// Tilføjer et medlem til databasen
        /// </summary>
        /// <param name="member">tilføjer medlem inde i parameteren til  databasen</param>
        /// <returns></returns>
        public Task AddMemberAsync(Member member);

        /// <summary>
        /// Sletter et medlem fra databasen
        /// </summary>
        /// <param name="id"> bruger id til at finde det medlem som skal slettes </param>
        /// <returns>returnere det medlem som bliver slettet</returns>
        public Task<Member> DeleteMemberAsync(int id);

        /// <summary>
        /// Opdatere et medlem
        /// </summary>
        /// <param name="id"> bruger id til at finde det medlem som skal opdateres i databasen</param>
        /// <param name="member">erstatter alt information omkring det gamle medlem med de properities som er i member</param>
        /// <returns></returns>
        public Task<bool> UpdateMemberAsync(int id,Member member);

        /// <summary>
        /// Filtere alle medlemmer ud fra navn
        /// </summary>
        /// <param name="filter">bruges til at filtre medlemmer</param>
        /// <returns>returnere medlemmer som er blevet filtreret </returns>
        public Task<List<Member>> FilterMembersAsync(string filter);


    }
}
