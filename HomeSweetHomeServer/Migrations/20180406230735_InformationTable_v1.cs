using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HomeSweetHomeServer.Migrations
{
    public partial class InformationTable_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Information",
                new string[] { "InformationName", "InformationType", "InformationId" },
                new object[] { "FirstName", "string", 1 });

            migrationBuilder.InsertData("Information",
                new string[] { "InformationName", "InformationType", "InformationId" },
                new object[] { "LastName", "string", 2 });

            migrationBuilder.InsertData("Information",
                new string[] { "InformationName", "InformationType", "InformationId" },
                new object[] { "Email", "string", 3 });

            migrationBuilder.InsertData("Information",
                new string[] { "InformationName", "InformationType", "InformationId" },
                new object[] { "PhoneNumber", "string", 4 });

            migrationBuilder.InsertData("Information",
                new string[] { "InformationName", "InformationType", "InformationId" },
                new object[] { "RegistrationDate", "DateTime", 5 });

            migrationBuilder.InsertData("Information",
                new string[] { "InformationName", "InformationType", "InformationId" },
                new object[] { "EmailVerificationCode", "string", 6 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Information",
                new string[] { "InformationName", "InformationType", "InformationId" },
                new object[] { "FirstName", "string", 1 });

            migrationBuilder.DeleteData("Information",
                new string[] { "InformationName", "InformationType", "InformationId" },
                new object[] { "LastName", "string", 2 });

            migrationBuilder.DeleteData("Information",
                new string[] { "InformationName", "InformationType", "InformationId" },
                new object[] { "Email", "string", 3 });

            migrationBuilder.DeleteData("Information",
                new string[] { "InformationName", "InformationType", "InformationId" },
                new object[] { "PhoneNumber", "string", 4 });

            migrationBuilder.DeleteData("Information",
                new string[] { "InformationName", "InformationType", "InformationId" },
                new object[] { "RegistrationDate", "DateTime", 5 });

            migrationBuilder.DeleteData("Information",
                new string[] { "InformationName", "InformationType", "InformationId" },
                new object[] { "EmailVerificationCode", "string", 6 });
        }
    }
}
