﻿using Microsoft.AspNetCore.DataProtection;

namespace SemesterProjekt_2.Services
{
    public abstract class Connection
    {
        protected String connectionString;
        public IConfiguration Configuration { get; }

        public Connection(IConfiguration configuration)
        {
            connectionString = Secret1.ConnectionString;
            Configuration = configuration;
            //connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

        public Connection(string connectionstring)
        {
            this.connectionString = connectionstring;
        }
    }
}
