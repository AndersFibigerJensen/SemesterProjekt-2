using Microsoft.AspNetCore.DataProtection;

namespace SemesterProjekt_2.Services
{
    public abstract class Connection
    {
        protected String connectionString;
        public IConfiguration Configuration { get; }

        public Connection(IConfiguration configuration)
        {
            connectionString = "";
            Configuration = configuration;
            //connectionString = Configuration["ConnectionStrings:SimplyConnection"];
            //connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

        public Connection(string connectionstring)
        {
            Configuration = null;
            this.connectionString = connectionstring;
        }

    }
}
